using Sandbox;

namespace ShitDrift
{
	[Library]
	public class CarController : BasePlayerController
	{
		public override void Simulate()
		{
			// Face whichever way the player is aiming
			Rotation = Input.Rotation;

			// Create a direction vector from the input from the client
			var direction = new Vector3( Input.Forward, Input.Left, 0 );

			// Rotate the vector so forward is the way we're facing
			direction *= Rotation;

			// Normalize it and multiply by speed
			direction = direction.Normal * 1000;

			// Apply the move
			Position += direction * Time.Delta;
		}
	}
}
