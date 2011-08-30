using System;
namespace SimGUI {
	public partial class SimStarter : Gtk.Window {
		ErlInterface erlInterface;
		Browser browser;
		
		protected virtual void OnButton6Clicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
		public SimStarter(ErlInterface erlInterface, Browser browser) : base(Gtk.WindowType.Toplevel) {
			this.erlInterface = erlInterface;
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

