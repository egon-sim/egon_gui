
using System;
using Gtk;

namespace SimGUI
{

	public partial class Startup : Gtk.Window
	{
		ErlInterface simInterface;
		
		public Startup () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.simInterface = null;
		}
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			this.Quit (o, args);
			args.RetVal = true;
		}
		
		protected virtual void OnReactorActionActivated (object sender, System.EventArgs e)
		{
			if (this.simInterface == null) {
				throw new Exception("Simulation server not started.");
			}
			MainWindow win = new MainWindow (this.simInterface);
			win.Show ();
		}
		
		protected virtual void OnTurbineActionActivated (object sender, System.EventArgs e)
		{
			if (this.simInterface == null) {
				throw new Exception("Simulation server not started.");
			}
			SimGUI.Turbine win = new SimGUI.Turbine (this.simInterface);
			win.Show ();
		}
		protected virtual void OnExitActionActivated (object sender, System.EventArgs e)
		{
			this.Quit (sender, e);

		}
		
		protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
		{
			this.simInterface = new ErlInterface(this.entry1.Text, this.spinbutton8.ValueAsInt, this.spinbutton9.ValueAsInt, this.spinbutton10.ValueAsInt);

		}
		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			this.simInterface.setCall("{es_core_server, burnup, " + this.spinbutton1.ValueAsInt + "}\n");
			this.simInterface.setCall("{es_core_server, boron, " + this.spinbutton2.ValueAsInt + "}\n");
			this.simInterface.setCall("{es_core_server, flux, " + this.spinbutton6.ValueAsInt + "}\n");
			this.simInterface.setCall("{es_turbine_server, power, " + this.spinbutton6.ValueAsInt + "}\n");
			
			/*this.simInterface.Call("core:set(burnup, " + this.spinbutton1.ValueAsInt + ")\n");
			this.simInterface.Call("core:set(boron, " + this.spinbutton2.ValueAsInt + ")\n");
			this.simInterface.Call("core:set(flux, " + this.spinbutton6.ValueAsInt + ")\n");
			this.simInterface.Call("turbine:set(power, " + this.spinbutton6.ValueAsInt + ")\n");*/
		}
		protected void Quit(object sender, System.EventArgs e)
		{
//			if (this.simInterface != null)
//				this.simInterface.Call("interface:reset()\n");
			Application.Quit ();
		}
		
		
	}
}
