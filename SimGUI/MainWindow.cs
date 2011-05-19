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
		this.label3.Text = this.simInterface.Call("core:burnup()\n");
		this.label5.Text = this.simInterface.Call("core:boron()\n");
		this.label10.Text = float.Parse(this.simInterface.Call("core:tavg()\n")).ToString();
		this.label11.Text = this.simInterface.Call("reactor:tref()\n");
		this.label12.Text = this.simInterface.Call("core:flux()\n");
		this.label13.Text = this.simInterface.Call("turbine:power()\n");
		this.vscale1.Value = int.Parse(this.simInterface.Call("rod_position:position(0)\n"));
 		this.vscale2.Value = int.Parse(this.simInterface.Call("rod_position:position(1)\n"));
 		this.vscale3.Value = int.Parse(this.simInterface.Call("rod_position:position(2)\n"));
 		this.vscale4.Value = int.Parse(this.simInterface.Call("rod_position:position(3)\n"));
 		this.vscale5.Value = int.Parse(this.simInterface.Call("rod_position:position(4)\n"));
		this.vscale6.Value = int.Parse(this.simInterface.Call("rod_position:position(5)\n"));
		
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
		this.simInterface.Call("rod_position:step_out()\n");
		this.Refresh();

	}
	
	protected virtual void OnButton5Clicked (object sender, System.EventArgs e)
	{
		this.simInterface.Call("rod_position:step_in()\n");
		this.Refresh();

	}
	
	protected virtual void OnTogglebutton2Clicked (object sender, System.EventArgs e)
	{
		string action = "";
		int val;
		
		if (this.radiobutton1.Active) {
			action = "borate";
			val = (int)this.spinbutton1.Value;
		} else if (this.radiobutton2.Active) {
			action = "dilute";
			val = (int)this.spinbutton2.Value;
		} else {
			return;
		}
		this.simInterface.Call("reactor:" + action + "(" + val + ")\n");
		
	}
	
	
	
	
	
	
}
