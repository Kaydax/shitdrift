using Sandbox;
using System;

namespace ShitDrift
{
	class SDCamera : Camera
	{
		[UserVar]
		public static bool sd_cam_collision { get; set; } = false;
		public float Elevation;
		
		float DesiredElevation;

		Vector3 lastPos;
		float minFOV;
		float maxFOV;
		float maxSpeed;
		float minElevation;
		float maxElevation;

		readonly float stepElevation = 25.0f;

		SDPlayer pawn;

		public SDCamera() : this( 250.0f, 150.0f, 500.0f, 70.0f, 110.0f, 450.0f )
		{
		}

		public SDCamera( float initialElevation, float minElevation, float maxElevation, float minFOV, float maxFOV, float maxSpeed )
		{
			Elevation = DesiredElevation = initialElevation;
			this.minFOV = minFOV;
			this.maxFOV = maxFOV;
			this.maxSpeed = maxSpeed;

			this.minElevation = minElevation;
			this.maxElevation = maxElevation;

			FieldOfView = minFOV;
			Rot = Rotation.FromPitch(90.0f);
			Viewer = null;

			pawn = Local.Pawn as SDPlayer;
			if ( pawn != null )
				Pos = lastPos = pawn.EyePos;
		}

		public override void Update()
		{
			var mw = Local.Client.Input.MouseWheel;
			if ( mw != 0 )
			{
				if ( mw < 0 )
					DesiredElevation -= stepElevation;
				if ( mw > 0 )
					DesiredElevation += stepElevation;

				DesiredElevation = MathX.Clamp( DesiredElevation, minElevation, maxElevation );
			}
			Elevation = MathX.LerpTo( Elevation, DesiredElevation, 5.0f * Time.Delta );

			var angleRadians = MathX.DegreeToRadian( pawn.angleLocal );
			Pos = pawn.EyePos;
			var speed = MathF.Sqrt( MathF.Pow( Pos.x - lastPos.x, 2 ) + MathF.Pow( Pos.y - lastPos.y, 2 ) ) / Time.Delta;
			FieldOfView = Lerp( FieldOfView, MapFOV( speed ), 5f * Time.Delta, 15f * Time.Delta );
			lastPos = Pos;

			var dist = MathX.Clamp( pawn.distanceLocal, .0f, Math.Min(Screen.Height, Screen.Width) / 5 ); // TODO: magic number
			Pos += new Vector3( MathF.Cos( angleRadians ) * dist, MathF.Sin( angleRadians ) * dist, .0f );

			var targetPos = Pos + Vector3.Up * Elevation;

			if ( sd_cam_collision )
			{
				var tr = Trace.Ray( Pos, targetPos )
					.WorldOnly()
					.Radius( 8 )
					.Run();

				targetPos.z = MathX.Clamp(tr.EndPos.z, minElevation, Elevation);
				Pos = tr.EndPos;
			}
			else
			{
				Pos = targetPos;
			}
		}

		public override void BuildInput( InputBuilder input )
		{
			input.AnalogMove = input.AnalogMove.Normal;
			input.InputDirection = input.AnalogMove;
			input.ViewAngles = new Angles( 0, pawn.angleLocal, 0 );

			input.StopProcessing = true;
		}
		private float MapFOV( float value )
		{
			return Math.Min(value, maxSpeed) / maxSpeed * (maxFOV - minFOV) + minFOV;
		}

		private static float Lerp( float from, float to, float by, float by_gt)
		{
			return from + ((to - from) * ((to > from) ? by_gt : by));
		}
	}
}
