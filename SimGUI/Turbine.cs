
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
			
			this.label10.Text = this.simInterface.Call("turbine:power()\n");
			this.label11.Text = this.simInterface.Call("turbine:power()\n");
			this.label12.Text = this.simInterface.Call("turbine:target()\n");
			this.label13.Text = this.simInterface.Call("turbine:rate()\n");
			go = this.simInterface.Call("turbine:go()\n").Trim();
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
			this.simInterface.Call("turbine:set(target, " + val.ToString() + ")\n");
			
		}
		
		protected virtual void OnButton9Clicked (object sender, System.EventArgs e)
		{
			int val = int.Parse(this.entry4.Text);
			this.simInterface.Call("turbine:set(rate, " + val.ToString() + ")\n");

		}
		
		protected virtual void OnButton7Clicked (object sender, System.EventArgs e)
		{
			this.simInterface.Call("turbine:start_ramp()\n");
		}
		
	}
}
