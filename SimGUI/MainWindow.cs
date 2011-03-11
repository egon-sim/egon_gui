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
		this.label3.Text = this.simInterface.Call("burnup");
		this.label5.Text = this.simInterface.Call("boron");
		this.label10.Text = this.simInterface.Call("tavg");
		this.label11.Text = this.simInterface.Call("tref");
		this.label12.Text = this.simInterface.Call("flux");
		this.label13.Text = this.simInterface.Call("power");
 		this.vscale1.Value = int.Parse(this.simInterface.Call("rod_pos [0]"));
 		this.vscale2.Value = int.Parse(this.simInterface.Call("rod_pos [1]"));
 		this.vscale3.Value = int.Parse(this.simInterface.Call("rod_pos [2]"));
 		this.vscale4.Value = int.Parse(this.simInterface.Call("rod_pos [3]"));
 		this.vscale5.Value = int.Parse(this.simInterface.Call("rod_pos [4]"));
		this.vscale6.Value = int.Parse(this.simInterface.Call("rod_pos [5]"));
		
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
		this.simInterface.Call("rodcontrol", "step_out");
		this.Refresh();

	}
	
	protected virtual void OnButton5Clicked (object sender, System.EventArgs e)
	{
		this.simInterface.Call("rodcontrol", "step_in");
		this.Refresh();

	}
	
	protected virtual void OnTogglebutton2Clicked (object sender, System.EventArgs e)
	{
		string action = "";
		int val;
		
		if (this.radiobutton1.Active) {
			action = "borate_d";
			val = (int)this.spinbutton1.Value;
		} else if (this.radiobutton2.Active) {
			action = "dilute_d";
			val = (int)this.spinbutton2.Value;
		} else {
			return;
		}
		this.simInterface.Call("reactor", action + " [" + val + "]");
		
	}
	
	
	
	
	
	
}
