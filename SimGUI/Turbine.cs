using System;
using EGON_cs_API;

namespace SimGUI
{


	public partial class Turbine : Gtk.Window
	{
		ErlInterface simInterface;
		
		public Turbine () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
		
		public Turbine(Simulator sim) : base(Gtk.WindowType.Toplevel) {
			this.simInterface = sim.erlInterface;
			Build();
			this.Refresh();
			GLib.Timeout.Add(1000, new GLib.TimeoutHandler(Refresh));
		}
		
		public bool Refresh() {
			string go;
			
			this.label10.Text = this.simInterface.Call("{get, es_turbine_server, power}\n");
			this.label11.Text = this.simInterface.Call("{get, es_turbine_server, power}\n");
			this.label12.Text = this.simInterface.Call("{get, es_turbine_server, target}\n");
			this.label13.Text = this.simInterface.Call("{get, es_turbine_server, rate}\n");
			go = this.simInterface.Call("{get, es_turbine_server, go}\n").Trim();
			if (go == "true") {
				this.label17.Text = "GO";
			} else if (go == "false") {
				this.label17.Text = "STOP";
			} else {
				this.label17.Text = "ERROR: '" + go.ToString() + "'";
			}
			
			
			return true;
		}

		protected virtual void OnButton8Clicked (object sender, System.EventArgs e)
		{
			int val = int.Parse(this.entry2.Text);
			this.simInterface.Call("{set, es_turbine_server, target, " + val.ToString() + "}\n");
			
		}
		
		protected virtual void OnButton9Clicked (object sender, System.EventArgs e)
		{
			int val = int.Parse(this.entry4.Text);
			this.simInterface.Call("{set, es_turbine_server, rate, " + val.ToString() + "}\n");

		}
		
		protected virtual void OnButton7Clicked (object sender, System.EventArgs e)
		{
			this.simInterface.Call("{action, es_turbine_server, ramp, start}\n");
		}
		
	}
}
