
using System;

namespace SimGUI
{


	public partial class Browser : Gtk.Window
	{
		ErlInterface simInterface;
		
		protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
		{
			this.simInterface = new ErlInterface(this.entry3.Text, this.entry2.Text, this.spinbutton1.ValueAsInt);
			this.textview2.Buffer.Text = this.simInterface.listSims();

		}
		
		

		public Browser () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}
