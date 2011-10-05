using Gtk;
using System;
using EGON_cs_API;

namespace SimGUI {
	public class Indicator {
		Gtk.Label label;
		Gtk.Scale scale;
		string call;
		ErlInterface erlInterface;
		double offscalehigh;
		double offscalelow;
			
		public Indicator(string call, ErlInterface erlInterface, double offscalelow, double offscalehigh, Gtk.Label label, Gtk.VScale scale) {
			this.label = label;
			this.scale = scale;
			this.call = call;
			this.erlInterface = erlInterface;
			this.offscalehigh = offscalehigh;
			this.offscalelow = offscalelow;
		}
		
		public void Refresh() {
			double val = double.Parse(this.erlInterface.Call(this.call));
			double procent = 100.0 * (val - this.offscalelow) / (this.offscalehigh - this.offscalelow);

			this.label.Text = val.ToString("F2");
			this.scale.Value = Math.Round(procent);
			
			return;
		}
	}
}

