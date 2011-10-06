using System;
using Gtk;
using EGON_cs_API;

namespace SimGUI
{

	public partial class Startup : Gtk.Window
	{
		Simulator simulator;
		
		public Startup (Simulator sim) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.simulator = sim;
			
			this.spinbutton1.Value = this.simulator.reactor.Burnup;
			this.spinbutton2.Value = this.simulator.reactor.Boron;
			this.spinbutton8.Value = this.simulator.reactor.Flux;
			this.spinbutton6.Value = this.simulator.turbine.Power;
		}
		
		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			this.simulator.reactor.Burnup = this.spinbutton1.ValueAsInt;
			this.simulator.reactor.Boron = this.spinbutton2.ValueAsInt;
			this.simulator.reactor.Flux = this.spinbutton8.ValueAsInt;
			this.simulator.turbine.Power = this.spinbutton6.ValueAsInt;
			string rods = this.entry2.Text + this.spinbutton7.ValueAsInt.ToString();
			this.simulator.reactor.rods.setCtrlRodPosition(rods);
		}
	}
}
