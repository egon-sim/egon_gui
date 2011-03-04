
using System;
using Gtk;

namespace SimGUI
{


	public partial class Startup : Gtk.Window
	{
		public Startup () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Application.Quit ();
			args.RetVal = true;
		}
		
		protected virtual void OnReactorActionActivated (object sender, System.EventArgs e)
		{
			MainWindow win = new MainWindow ();
			win.Show ();
		}
		
		protected virtual void OnTurbineActionActivated (object sender, System.EventArgs e)
		{
			SimGUI.Turbine win = new SimGUI.Turbine ();
			win.Show ();
		}
		protected virtual void OnExitActionActivated (object sender, System.EventArgs e)
		{
			Application.Quit ();

		}
		
		
	}
}
