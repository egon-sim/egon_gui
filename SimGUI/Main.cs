using System;
using Gtk;

namespace SimGUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			Browser win = new Browser ();
			win.Show ();
			Application.Run ();
		}
	}
}
