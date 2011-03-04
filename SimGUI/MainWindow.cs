using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected virtual void OnTurbineActionActivated (object sender, System.EventArgs e)
	{
		SimGUI.Turbine win = new SimGUI.Turbine();
		win.Show();
	}
	
	protected virtual void OnExitActionActivated (object sender, System.EventArgs e)
	{
	}
	
	
	
}
