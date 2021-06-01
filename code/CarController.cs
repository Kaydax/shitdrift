using Sandbox;
using System;

namespace ShitDrift
{
	[Library]
	public partial class CarController : BasePlayerController
	{
		// TODO: prediction
		[Net]
		Vector3 Acceleration { get; set; }
		[Net]
		Vector3 Speed { get; set; }

		public override void Simulate()
		{
			// Face whichever way the player is aiming
			Rotation = Input.Rotation;

			// Create a direction vector from the input from the client
			/*Acceleration = (new Vector3( Input.Forward, 0, 0 ) * Rotation).Normal * 10000.0f;

			Speed += Acceleration * Time.Delta;
			Acceleration *= MathF.Sin( MathF.Atan2(Speed.x * Acceleration.y - Speed.y * Acceleration.x, Speed.x * Acceleration.x + Speed.y * Acceleration.y ) ) * 100.0f;
			Speed *= 0.95f;

			// Apply the move
			Position += Speed * Time.Delta;

			DebugOverlay.Line( Position, (Position + Acceleration * Time.Delta).Normal, 1, false);*/
		}
	}
}
