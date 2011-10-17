using System;
namespace EGON_cs_API {
	public class Reactor {
		private ErlInterface erlInterface;
		private float burnup;
		private float boron;
		private float flux;
		private float tavg;

		public Reactor(ErlInterface erlInterface) {
			this.erlInterface = erlInterface;

			erlInterface.Register(new Connector.Setter(setBurnup), "{get, es_core_server, burnup}");
			erlInterface.Register(new Connector.Setter(setBoron), "{get, es_core_server, boron}");
			erlInterface.Register(new Connector.Setter(setFlux), "{get, es_core_server, flux}");
			erlInterface.Register(new Connector.Setter(setTavg), "{get, es_core_server, tavg}");
		}

		public void setBurnup(string val) {
			this.burnup = float.Parse(val.Replace('.', ','));
		}
		
		public void setBoron(string val) {
			this.boron = float.Parse(val.Replace('.', ','));
		}
		
		public void setFlux(string val) {
			this.flux = float.Parse(val.Replace('.', ','));
		}
		
		public void setTavg(string val) {
			this.tavg = float.Parse(val.Replace('.', ','));
		}
		
		public float Burnup {
			get { return this.burnup; }
			set { this.erlInterface.Call("{set, es_core_server, burnup, " + value.ToString() + "}\n"); }
		}
		public float Boron {
			get { return this.boron; }
			set { this.erlInterface.Call("{set, es_core_server, boron, " + value.ToString() + "}\n"); }
		}
		public float Tavg {
			get { return this.tavg; }
		}
		public float Flux {
			get { return this.flux; }
			set { this.erlInterface.Call("{set, es_core_server, flux, " + value.ToString() + "}\n"); }
		}
		
		public string Borate(int litres) {
			string boron = this.boron.ToString();
			return this.erlInterface.Call("{action, es_makeup_buffer_server, borate, [" + boron + ", " + litres + "]}\n");
		}

		public string Dilute(int litres) {
			string boron = this.boron.ToString();
			return this.erlInterface.Call("{action, es_makeup_buffer_server, dilute, [" + boron + ", " + litres + "]}\n");
		}
	}
}
