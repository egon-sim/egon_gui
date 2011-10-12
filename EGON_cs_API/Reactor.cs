using System;
namespace EGON_cs_API {
	public class Reactor {
		private ErlInterface erlInterface;
		public Rods rods;
		public float flux;
		public float tavg;

		public Reactor(ErlInterface erlInterface) {
			this.erlInterface = erlInterface;
			this.rods = new Rods(this.erlInterface);

			erlInterface.Register(new Connector.Setter(setFlux), "{get, es_core_server, flux}");
			erlInterface.Register(new Connector.Setter(setTavg), "{get, es_core_server, tavg}");
		}

		public void setFlux(string val) {
			this.flux = float.Parse(val);
		}
		
		public void setTavg(string val) {
			this.tavg = float.Parse(val);
		}
		
		public float Burnup {
			get { return float.Parse(this.erlInterface.Call("{get, es_core_server, burnup}")); }
			set { this.erlInterface.Call("{set, es_core_server, burnup, " + value.ToString() + "}\n"); }
		}
		public float Boron {
			get { return float.Parse(this.erlInterface.Call("{get, es_core_server, boron}")); }
			set { this.erlInterface.Call("{set, es_core_server, boron, " + value.ToString() + "}\n"); }
		}
		public float Tavg {
			get { return float.Parse(this.erlInterface.Call("{get, es_core_server, tavg}")); }
		}
		public float Flux {
			get { return float.Parse(this.erlInterface.Call("{get, es_core_server, flux}")); }
			set { this.erlInterface.Call("{set, es_core_server, flux, " + value.ToString() + "}\n"); }
		}
		
		public string Borate(int litres) {
			string boron = this.erlInterface.Call("{get, es_core_server, boron}\n").Trim();
			return this.erlInterface.Call("{action, es_makeup_buffer_server, borate, [" + boron + ", " + litres + "]}\n");
		}

		public string Dilute(int litres) {
			string boron = this.erlInterface.Call("{get, es_core_server, boron}\n").Trim();
			return this.erlInterface.Call("{action, es_makeup_buffer_server, dilute, [" + boron + ", " + litres + "]}\n");
		}
	}
}

