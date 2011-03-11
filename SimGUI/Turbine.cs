
using System;

namespace SimGUI
{


	public partial class Turbine : Gtk.Window
	{
		ErlInterface simInterface;
		
		public Turbine () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
		
		public Turbine(ErlInterface simInterface) : base(Gtk.WindowType.Toplevel) {
			this.simInterface = simInterface;
			Build();
			this.Refresh();
			GLib.Timeout.Add(1000, new GLib.TimeoutHandler(Refresh));
		}
		
		public bool Refresh() {
			string go;
			
			this.label10.Text = this.simInterface.Call("power");
			this.label11.Text = this.simInterface.Call("power");
			this.label12.Text = this.simInterface.Call("target");
			this.label13.Text = this.simInterface.Call("rate");
			go = this.simInterface.Call("turbine", "go");
			if (go == "true") {
				this.label17.Text = "GO";
			} else if (go == "false") {
				this.label17.Text = "STOP";
			} else {
				this.label17.Text = "ERROR";
			}
			
			
			return true;
		}

		protected virtual void OnButton8Clicked (object sender, System.EventArgs e)
		{
			int val = int.Parse(this.entry2.Text);
			this.simInterface.Call("turbine", "set_target [" + val.ToString() + "]");
			
		}
		
		protected virtual void OnButton9Clicked (object sender, System.EventArgs e)
		{
			int val = int.Parse(this.entry4.Text);
			this.simInterface.Call("turbine", "set_rate [" + val.ToString() + "]");

		}
		
		protected virtual void OnButton7Clicked (object sender, System.EventArgs e)
		{
			this.simInterface.Call("turbine", "start_ramp");
		}
		
	}
}
