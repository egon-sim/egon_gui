using System;

namespace EGON_cs_API {
	public class Reactor : StateClass {
		private float burnup;
		private float boron;
		private float flux;
		private float tavg;

		public Reactor(SimulatorInterface simInterface) : base(simInterface) {
			this.Register(new Connector.Setter(setBurnup), "{get, es_core_server, burnup}");
			this.Register(new Connector.Setter(setBoron), "{get, es_core_server, boron}");
			this.Register(new Connector.Setter(setFlux), "{get, es_core_server, flux}");
			this.Register(new Connector.Setter(setTavg), "{get, es_core_server, tavg}");
		}

		public void setBurnup(string val) {
			this.burnup = Lib.StringToFloat(val);
		}
		
		public void setBoron(string val) {
			this.boron = Lib.StringToFloat(val);
		}
		
		public void setFlux(string val) {
			this.flux = Lib.StringToFloat(val);
		}
		
		public void setTavg(string val) {
			this.tavg = Lib.StringToFloat(val);
		}
		
		public float Burnup {
			get { return this.burnup; }
			set { this.simInterface.Call("{set, es_core_server, burnup, " + value.ToString() + "}\n"); }
		}
		public float Boron {
			get { return this.boron; }
			set { this.simInterface.Call("{set, es_core_server, boron, " + value.ToString() + "}\n"); }
		}
		public float Tavg {
			get { return this.tavg; }
		}
		public float Flux {
			get { return this.flux; }
			set { this.simInterface.Call("{set, es_core_server, flux, " + value.ToString() + "}\n"); }
		}
		
		public string Borate(int litres) {
			string boron = this.boron.ToString();
			return this.simInterface.Call("{action, es_makeup_buffer_server, borate, [" + boron + ", " + litres + "]}\n");
		}

		public string Dilute(int litres) {
			string boron = this.boron.ToString();
			return this.simInterface.Call("{action, es_makeup_buffer_server, dilute, [" + boron + ", " + litres + "]}\n");
		}

	}
}
