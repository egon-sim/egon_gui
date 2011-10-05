using System;
using EGON_cs_API;

namespace SimGUI {
	public partial class SimStarter : Gtk.Window {
		ErlInterface erlInterface;
		Browser browser;
		
		protected virtual void OnButton6Clicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
		public SimStarter(EgonServer server, Browser browser) : base(Gtk.WindowType.Toplevel) {
			this.erlInterface = server.erlInterface;
			this.browser = browser;
			
			this.Build();
		}
		protected virtual void OnButton5Clicked (object sender, System.EventArgs e)
		{
			
			this.erlInterface.StartSim(this.entry1.Text, this.entry2.Text);
			this.browser.RefreshSimList();
			this.Destroy();
		}
	}
}

