using Sandbox;

namespace ShitDrift
{
	partial class SDPlayer : Prop, IPhysicsUpdate
	{
		public float angleLocal = 0.0f;
		public float distanceLocal = 0.0f;

		public SDPlayer()
		{
		}

		public override void Spawn()
		{
			Camera = new SDCamera();

			base.Spawn();

			SetModel( "models/citizen/citizen.vmdl" );

			MoveType = MoveType.Physics;
			CollisionGroup = CollisionGroup.Interactive;
			PhysicsEnabled = true;
			UsePhysicsCollision = true;

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			//
			// If we're running serverside and Attack1 was just pressed, spawn a ragdoll
			//
			if ( IsServer && Input.Down( InputButton.Attack1 ) )
			{
				/*var ragdoll = new ModelEntity();
				ragdoll.SetModel( "models/citizen/citizen.vmdl" );
				ragdoll.Position = EyePos + EyeRot.Forward * 40;
				ragdoll.Rotation = Rotation.LookAt( Vector3.Random.Normal );
				ragdoll.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				ragdoll.PhysicsGroup.Velocity = EyeRot.Forward * 1000;*/
				Log.Info( "Pew" );
				PhysicsGroup.Velocity = Vector3.Forward * Input.Rotation * 1000;
			}
		}

		public override void FrameSimulate( Client cl )
		{
			Input.Rotation = Rotation.FromYaw(angleLocal);

			base.FrameSimulate( cl );
		}

		public void OnPostPhysicsStep( float dt )
		{
			//throw new System.NotImplementedException();
		}
	}
}
