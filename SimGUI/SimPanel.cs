using System;
using EGON_cs_API;

namespace SimGUI {
	public partial class SimPanel : Gtk.Window {
		Simulator simulator;
		
		public SimPanel(Simulator sim) : base(Gtk.WindowType.Toplevel) {
			this.simulator = sim;
			this.Build();
			this.refreshStatus();
		}
		
		public void refreshStatus() {
			string status = this.simulator.clock.Status;
			if (status == "stopped") {
				this.button1.Label = "Start";
				this.label1.Text = "STOPPED";
			} else if (status == "running") {
				this.button1.Label = "Stop";
				this.label1.Text = "RUNNING";
			} else {
				throw new Exception("Simulator state invalid: '" + status + "' (not STOPPED nor RUNNING).");
			}
			return;
		}

		protected virtual void OnButton40Clicked (object sender, System.EventArgs e)
		{
			MainWindow win = new MainWindow(this.simulator);
			win.Show ();
		}
		
		protected virtual void OnButton41Clicked (object sender, System.EventArgs e)
		{
			SimGUI.Turbine win = new SimGUI.Turbine(this.simulator);
			win.Show ();
		}
		
		protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
		{
			if (this.label1.Text != "RUNNING" && this.label1.Text != "STOPPED") {
				this.refreshStatus();
			}

			if (this.label1.Text == "RUNNING") {
				this.simulator.clock.Stop();
			} else if (this.label1.Text == "STOPPED") {
				this.simulator.clock.Start();
			} else {
				throw new Exception("Simulator state invalid (not STOPPED nor RUNNING).");
			}
			this.refreshStatus();
		}
		
		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			Startup su = new Startup(this.simulator);
			su.ShowAll();
		}
	}
}

