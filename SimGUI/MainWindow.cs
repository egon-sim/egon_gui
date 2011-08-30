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
		this.label3.Text = this.simInterface.Call("{get, es_core_server, burnup}\n");
		this.label5.Text = this.simInterface.Call("{get, es_core_server, boron}\n");
		this.label10.Text = float.Parse(this.simInterface.Call("{get, es_core_server, tavg}\n")).ToString();
		this.label11.Text = this.simInterface.Call("{get, es_w7300_server, tref}\n");
		this.label12.Text = this.simInterface.Call("{get, es_core_server, flux}\n");
		this.label13.Text = this.simInterface.Call("{get, es_turbine_server, power}\n");
		this.vscale1.Value = int.Parse(this.simInterface.Call("{get, es_rod_position_server, control_position, 1}\n"));
 		this.vscale2.Value = int.Parse(this.simInterface.Call("{get, es_rod_position_server, control_position, 2}\n"));
 		this.vscale3.Value = int.Parse(this.simInterface.Call("{get, es_rod_position_server, control_position, 3}\n"));
 		this.vscale4.Value = int.Parse(this.simInterface.Call("{get, es_rod_position_server, control_position, 4}\n"));
 		this.vscale5.Value = int.Parse(this.simInterface.Call("{get, es_rod_position_server, shutdown_position, 1}\n"));
		this.vscale6.Value = int.Parse(this.simInterface.Call("{get, es_rod_position_server, shutdown_position, 2}\n"));
		this.label26.Text = this.simInterface.Call("{get, es_rod_controller_server, speed}\n").ToString();
		
		string rod_controller_mode = this.simInterface.Call("{get, es_rod_controller_server, mode}\n").Trim();
		if (rod_controller_mode == "auto") {
			this.radiobutton3.Activate();
		}
		if (rod_controller_mode == "manual") {
			this.radiobutton4.Activate();
		}
		
		return true;
	}
	
	protected virtual void OnTurbineActionActivated (object sender, System.EventArgs e)
	{
		Turbine win = new Turbine();
		win.Show();
	}
	
	protected virtual void OnExitActionActivated (object sender, System.EventArgs e)
	{
		//Application.Quit();
	}
	
	protected virtual void OnButton4Clicked (object sender, System.EventArgs e)
	{
		this.simInterface.Call("{action, es_rod_position_server, step_out}\n");
		this.Refresh();

	}
	
	protected virtual void OnButton5Clicked (object sender, System.EventArgs e)
	{
		this.simInterface.Call("{action, es_rod_position_server, step_in}\n");
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
		String boron = this.simInterface.Call("{get, es_core_server, boron}\n").Trim();
		this.simInterface.Call("{action, es_makeup_buffer_server, " + action + ", [" + boron + ", " + val + "]}\n");
		
	}
	
	protected virtual void OnRadiobutton3Activated (object sender, System.EventArgs e)
	{
		//this.simInterface.Call("{set, es_rod_controller_server, mode, auto}\n");
	}
	
	protected virtual void OnRadiobutton4Activated (object sender, System.EventArgs e)
	{
		this.simInterface.Call("{set, es_rod_controller_server, mode, manual}\n");
	}

	protected virtual void OnRadiobutton3Clicked (object sender, System.EventArgs e)
	{
		this.simInterface.Call("{set, es_rod_controller_server, mode, auto}\n");
	}
	
	protected virtual void OnRadiobutton4Clicked (object sender, System.EventArgs e)
	{
		this.simInterface.Call("{set, es_rod_controller_server, mode, manual}\n");
	}
	
	
	
}
