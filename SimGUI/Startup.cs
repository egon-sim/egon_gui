using System;
using Gtk;
using EGON_cs_API;

namespace SimGUI
{

	public partial class Startup : Gtk.Window
	{
		EGON_cs_API.Reactor reactor;
		EGON_cs_API.Turbine turbine;
		EGON_cs_API.Rods rods;
		
		public Startup (Simulator sim) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();

			this.reactor = sim.getReactor();
			this.turbine = sim.getTurbine();
			this.rods = sim.getRods();
			
			this.spinbutton1.Value = this.reactor.Burnup;
			this.spinbutton2.Value = this.reactor.Boron;
			this.spinbutton8.Value = this.reactor.Flux;
			this.spinbutton6.Value = this.turbine.Power;
		}
		
		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			this.reactor.Burnup = this.spinbutton1.ValueAsInt;
			this.reactor.Boron = this.spinbutton2.ValueAsInt;
			this.reactor.Flux = this.spinbutton8.ValueAsInt;
			this.turbine.Power = this.spinbutton6.ValueAsInt;
			string rods = this.entry2.Text + this.spinbutton7.ValueAsInt.ToString();
			this.rods.setCtrlRodPosition(rods);
		}
	}
}
