using System;
using EGON_cs_API;

namespace SimGUI
{


	public partial class Turbine : Gtk.Window
	{
		EGON_cs_API.Turbine turbine;
		
		public Turbine () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
		
		public Turbine(EGON_cs_API.Simulator sim) : base(Gtk.WindowType.Toplevel) {
			this.turbine = sim.getTurbine();
			Build();
			this.Refresh();
			GLib.Timeout.Add(1000, new GLib.TimeoutHandler(Refresh));
		}
		
		public bool Refresh() {
			this.label10.Text = this.turbine.Power.ToString();
			this.label11.Text = this.turbine.Power.ToString();
			this.label12.Text = this.turbine.Target.ToString();
			this.label13.Text = this.turbine.Rate.ToString();
			bool go = this.turbine.Go;
			if (go == true) {
				this.label17.Text = "GO";
			} else if (go == false) {
				this.label17.Text = "STOP";
			} else {
				this.label17.Text = "ERROR: '" + go.ToString() + "'";
			}
			
			
			return true;
		}

		protected virtual void OnButton8Clicked (object sender, System.EventArgs e)
		{
			this.turbine.Target = int.Parse(this.entry2.Text);
		}
		
		protected virtual void OnButton9Clicked (object sender, System.EventArgs e)
		{
			this.turbine.Rate = int.Parse(this.entry4.Text);
		}
		
		protected virtual void OnButton7Clicked (object sender, System.EventArgs e)
		{
			this.turbine.Start();
		}
		
	}
}
