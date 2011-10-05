using System;
using Gtk;
using EGON_cs_API;

namespace SimGUI
{

	public partial class Startup : Gtk.Window
	{
		ErlInterface simInterface;
		
		public Startup (ErlInterface erlInterface) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.simInterface = erlInterface;
			
			this.spinbutton1.Value = double.Parse(this.simInterface.Call("{get, es_core_server, burnup}"));
			this.spinbutton2.Value = double.Parse(this.simInterface.Call("{get, es_core_server, boron}"));
			this.spinbutton8.Value = double.Parse(this.simInterface.Call("{get, es_core_server, flux}"));
			this.spinbutton6.Value = double.Parse(this.simInterface.Call("{get, es_turbine_server, power}"));                       
		}
		
		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			this.simInterface.Call("{set, es_core_server, burnup, " + this.spinbutton1.ValueAsInt + "}\n");
			this.simInterface.Call("{set, es_core_server, boron, " + this.spinbutton2.ValueAsInt + "}\n");
			this.simInterface.Call("{set, es_core_server, flux, " + this.spinbutton8.ValueAsInt + "}\n");
			this.simInterface.Call("{set, es_turbine_server, power, " + this.spinbutton6.ValueAsInt + "}\n");
			string rods = this.entry2.Text + this.spinbutton7.ValueAsInt.ToString();
			this.simInterface.Call("{set, es_rod_position_server, control_position, \"" + rods + "\"}\n");
		}
	}
}
