using System;
namespace SimGUI
{
	public partial class NewSimStarter : Gtk.Window
	{
		ErlInterface erlInterface;
		Browser browser;
		
		public NewSimStarter (ErlInterface erlInterface, Browser browser) : base(Gtk.WindowType.Toplevel)
		{
			this.erlInterface = erlInterface;
			this.browser = browser;
			
			this.Build ();
		}
		
	}
}

