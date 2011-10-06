using Gtk;
using System;
using EGON_cs_API;

namespace SimGUI {
	public class Indicator {
		public delegate float Method();

		Gtk.Label label;
		Gtk.Scale scale;
		Method method;
		double offscalehigh;
		double offscalelow;
			
		public Indicator(Method method, double offscalelow, double offscalehigh, Gtk.Label label, Gtk.VScale scale) {
			this.label = label;
			this.scale = scale;
			this.method = method;
			this.offscalehigh = offscalehigh;
			this.offscalelow = offscalelow;
		}
		
		public void Refresh() {
			double val = this.method();
			double procent = 100.0 * (val - this.offscalelow) / (this.offscalehigh - this.offscalelow);

			this.label.Text = val.ToString("F2");
			this.scale.Value = Math.Round(procent);
			
			return;
		}
	}
}

