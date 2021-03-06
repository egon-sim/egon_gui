using System;
using EGON_cs_API;
using System.Threading;

namespace SimGUI {
	public partial class SimPanel : Gtk.Window {
		Simulator simulator;
		Clock clock;
		Timer refresher;
		int period;

		
		public SimPanel(Simulator sim) : base(Gtk.WindowType.Toplevel) {
			this.simulator = sim;
			this.clock = sim.getClock();

			this.Build();
			this.period = 1000;
			this.refresher = new Timer(new TimerCallback(Refresh), null, 0, this.period);
		}
		
		public void Refresh() {
			this.Refresh(null);
		}
		
		public void Refresh(object o) {
			string status = this.clock.Status;
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
				this.Refresh();
			}

			if (this.label1.Text == "RUNNING") {
				this.clock.Stop();
			} else if (this.label1.Text == "STOPPED") {
				this.clock.Start();
			} else {
				throw new Exception("Simulator state invalid (not STOPPED nor RUNNING).");
			}
			this.Refresh();
		}
		
		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			Startup su = new Startup(this.simulator);
			su.ShowAll();
		}
	}
}