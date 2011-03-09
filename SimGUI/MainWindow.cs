using System;
using Gtk;
using SimGUI;

public partial class MainWindow : Gtk.Window
{
	ErlInterface simInterface;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	public MainWindow(ErlInterface simInterface) : base(Gtk.WindowType.Toplevel) {
		this.simInterface = simInterface;
		Build();
		this.Refresh();
		GLib.Timeout.Add(1000, new GLib.TimeoutHandler(Refresh));
	}
	
	public bool Refresh() {
//		this.label9.Text = this.simInterface.Call("burnup");
		this.label5.Text = this.simInterface.Call("boron");
		this.label10.Text = this.simInterface.Call("tavg");
		this.label11.Text = this.simInterface.Call("tref");
		this.label12.Text = this.simInterface.Call("flux");
		this.label13.Text = this.simInterface.Call("power");
// 		this.vscale1.Text = this.simInterface.Call("group a");
// 		this.vscale2.Text = this.simInterface.Call("group b");
// 		this.vscale3.Text = this.simInterface.Call("group c");
// 		this.vscale4.Text = this.simInterface.Call("group d");
// 		this.vscale5.Text = this.simInterface.Call("group sa");
//		this.vscale6.Text = this.simInterface.Call("group sb")
		
		return true;
	}
	
	protected virtual void OnTurbineActionActivated (object sender, System.EventArgs e)
	{
		Turbine win = new Turbine();
		win.Show();
	}
	
	protected virtual void OnExitActionActivated (object sender, System.EventArgs e)
	{
	}
	
	protected virtual void OnButton4Clicked (object sender, System.EventArgs e)
	{
		this.simInterface.Call("step_out");
		this.Refresh();

	}
	
	protected virtual void OnButton5Clicked (object sender, System.EventArgs e)
	{
		this.simInterface.Call("step_in");
		this.Refresh();

	}
	
	
	
	
	
}
