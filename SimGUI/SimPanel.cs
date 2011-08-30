using System;
namespace SimGUI {
	public partial class SimPanel : Gtk.Window {
		ErlInterface erlInterface;
		
		public SimPanel(ErlInterface erlInterface) : base(Gtk.WindowType.Toplevel) {
			this.erlInterface = erlInterface;
			this.Build();
			this.refreshStatus();
		}
		
		public void refreshStatus() {
			string status = this.erlInterface.Call("{get, es_clock_server, status}");
			if (status == "stopped") {
				this.button1.Label = "Start";
				this.label1.Text = "STOPPED";
			} else if (status == "running") {
				this.button1.Label = "Stop";
				this.label1.Text = "RUNNING";
			} else {
				throw new Exception("Simulator state invalid (not STOPPED nor RUNNING).");
			}
			return;
		}

		protected virtual void OnButton40Clicked (object sender, System.EventArgs e)
		{
			MainWindow win = new MainWindow (this.erlInterface);
			win.Show ();
		}
		
		protected virtual void OnButton41Clicked (object sender, System.EventArgs e)
		{
			SimGUI.Turbine win = new SimGUI.Turbine (this.erlInterface);
			win.Show ();
		}
		
		protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
		{
			if (this.label1.Text != "RUNNING" && this.label1.Text != "STOPPED") {
				this.refreshStatus();
			}

			if (this.label1.Text == "RUNNING") {
				this.erlInterface.StopClock();
			} else if (this.label1.Text == "STOPPED") {
				this.erlInterface.StartClock();
			} else {
				throw new Exception("Simulator state invalid (not STOPPED nor RUNNING).");
			}
			this.refreshStatus();
		}
		
		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			Startup su = new Startup(this.erlInterface);
			su.ShowAll();
		}
	}
}

