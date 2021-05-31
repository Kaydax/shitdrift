using Sandbox;
using Sandbox.UI;
using System;

namespace ShitDrift
{
	public class MouseCapture : Panel
	{
		public float angle = .0f;

		public override void Tick()
		{
			var player = Local.Pawn as SDPlayer; // yandere dev moment
			if ( player == null ) return;

			angle = -1 * MathX.RadianToDegree( (float)(MathF.Atan2( MousePos.y - Screen.Height / 2, MousePos.x - Screen.Width / 2 )) ) - 90.0f;
			player.angleLocal = angle;
		}
	}
}
