using System;
using Gtk;
using EGON_cs_API;
using SimGUI;

public partial class MainWindow : Gtk.Window
{
	Indicator [] indicators;
	EGON_cs_API.Reactor reactor;
	EGON_cs_API.Rods rods;
	EGON_cs_API.Turbine turbine;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	public MainWindow(Simulator sim) : base(Gtk.WindowType.Toplevel) {
		this.reactor = sim.reactor;
		this.rods = this.reactor.rods;
		this.turbine = sim.turbine;
		Build();
		
		this.indicators = new Indicator[4] {
			new Indicator(delegate { return this.reactor.Flux; }, 0, 120, this.label28, this.vscale7),
			new Indicator(delegate { return this.turbine.Power; }, 0, 120, this.label30, this.vscale8),
			new Indicator(delegate { return this.reactor.Tavg; }, 280, 320, this.label32, this.vscale9),
			new Indicator(delegate { return this.turbine.Tref; }, 280, 320, this.label34, this.vscale10),
		};
		
		this.Refresh();
		GLib.Timeout.Add(1000, new GLib.TimeoutHandler(Refresh));
	}

	public bool Refresh() {
		this.label3.Text = this.reactor.Burnup.ToString("F2");
		this.label5.Text = this.reactor.Boron.ToString("F2");
		this.label10.Text = this.reactor.Tavg.ToString("F2");
		this.label11.Text = this.turbine.Tref.ToString("F2");
		this.label12.Text = this.reactor.Flux.ToString("F2");
		this.label13.Text = this.turbine.Power.ToString();
		this.vscale1.Value = this.rods.getCtrlRodPosition(1);
 		this.vscale2.Value = this.rods.getCtrlRodPosition(2);
 		this.vscale3.Value = this.rods.getCtrlRodPosition(3);
 		this.vscale4.Value = this.rods.getCtrlRodPosition(4);
 		this.vscale5.Value = this.rods.getSdRodPosition(1);
		this.vscale6.Value = this.rods.getSdRodPosition(2);
		this.label26.Text = this.rods.Speed.ToString();
		
		string rod_controller_mode = this.rods.Mode;
		if (rod_controller_mode == "auto") {
			this.radiobutton3.Activate();
		}
		if (rod_controller_mode == "manual") {
			this.radiobutton4.Activate();
		}
		
		foreach (Indicator i in this.indicators) {
			i.Refresh();
		}
		
		return true;
	}
	
	protected virtual void OnTurbineActionActivated (object sender, System.EventArgs e)
	{
		SimGUI.Turbine win = new SimGUI.Turbine();
		win.Show();
	}
	
	protected virtual void OnExitActionActivated (object sender, System.EventArgs e)
	{
		//Application.Quit();
	}
	
	protected virtual void OnButton4Clicked (object sender, System.EventArgs e)
	{
		this.rods.StepOut();
		this.Refresh();

	}
	
	protected virtual void OnButton5Clicked (object sender, System.EventArgs e)
	{
		this.rods.StepIn();
		this.Refresh();

	}
	
	protected virtual void OnTogglebutton2Clicked (object sender, System.EventArgs e)
	{
		string action = "";
		int val;
		
		if (this.radiobutton1.Active) {
			val = (int)this.spinbutton1.Value;
			this.reactor.Borate(val);
		} else if (this.radiobutton2.Active) {
			val = (int)this.spinbutton2.Value;
			this.reactor.Dilute(val);
		} else {
			return;
		}		
	}
	
	protected virtual void OnRadiobutton3Clicked (object sender, System.EventArgs e)
	{
		this.rods.Mode = "auto";
	}
	
	protected virtual void OnRadiobutton4Clicked (object sender, System.EventArgs e)
	{
		this.rods.Mode = "manual";
	}
	
	
	
}
