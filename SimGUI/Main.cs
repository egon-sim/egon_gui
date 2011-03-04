using System;
using Gtk;

namespace SimGUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			Startup win = new Startup ();
			win.Show ();
			Application.Run ();
		}
	}
}
