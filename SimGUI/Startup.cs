
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
			Application.Quit ();
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
			Application.Quit ();

		}
		
		protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
		{
			this.simInterface = new ErlInterface(this.entry1.Text, this.entry3.Text, "reactor", "FOO", "/home/nick/code/simulator/erl");

			//this.simInterface.StartNode();
			//this.simInterface.StartModule();
			
			/*Console.WriteLine(e.StartNode());
			Console.WriteLine("Started!");*/
			/*Console.WriteLine(e.StartModule());
			Console.WriteLine (e.Call("boron"));
			Console.WriteLine (e.Call("tavg"));
			Console.WriteLine (e.Call("step_in"));
			Console.WriteLine (e.Call("tavg"));
			Console.WriteLine (e.Call("dilute [5]"));
			Console.WriteLine (e.Call("tavg"));*/
			/*Console.WriteLine(e.StopModule());
			Console.WriteLine(e.StopNode());*/

		}
		
		
		
	}
}
