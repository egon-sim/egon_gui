using System;
using EGON_cs_API;

namespace SimGUI {
	public partial class SimStarter : Gtk.Window {
		EgonServer server;
		Browser browser;
		
		protected virtual void OnButton6Clicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
		public SimStarter(EgonServer server, Browser browser) : base(Gtk.WindowType.Toplevel) {
			this.server = server;
			this.browser = browser;
			
			this.Build();
		}
		protected virtual void OnButton5Clicked (object sender, System.EventArgs e)
		{
			this.server.NewSimulator(this.entry1.Text, this.entry2.Text);
			this.browser.RefreshSimList();
			this.Destroy();
		}
	}
}

