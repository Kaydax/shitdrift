using Sandbox;
using Sandbox.UI;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace ShitDrift
{
	/// <summary>
	/// This is the HUD entity. It creates a RootPanel clientside, which can be accessed
	/// via RootPanel on this entity, or Local.Hud.
	/// </summary>
	public class SDHudEntity : Sandbox.HudEntity<RootPanel>
	{
		public MouseCapture mc;

		public SDHudEntity()
		{
			if ( IsClient )
			{
				//RootPanel.SetTemplate( "/ui/tschud.html" );
				RootPanel.StyleSheet.Load( "/ui/SDHud.scss" );

				mc = RootPanel.AddChild<MouseCapture>();

				RootPanel.AddChild<Health>();
				RootPanel.AddChild<ChatBox>();
				RootPanel.AddChild<KillFeed>();
				RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
			}
		}
	}

}
