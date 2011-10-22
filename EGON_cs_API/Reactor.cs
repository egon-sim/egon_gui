using System;

namespace EGON_cs_API {
	public class Reactor : StateClass {
		private Parameter burnup;
		private Parameter boron;
		private Parameter flux;
		private Parameter tavg;

		public Reactor(SimulatorInterface simInterface) : base(simInterface) {
			this.burnup = this.Register("{get, es_core_server, burnup}");
			this.boron = this.Register("{get, es_core_server, boron}");
			this.flux = this.Register("{get, es_core_server, flux}");
			this.tavg = this.Register("{get, es_core_server, tavg}");
		}

		public float Burnup {
			get { return this.burnup.Value; }
			set { this.simInterface.Call("{set, es_core_server, burnup, " + value.ToString() + "}\n"); }
		}
		public float Boron {
			get { return this.boron.Value; }
			set { this.simInterface.Call("{set, es_core_server, boron, " + value.ToString() + "}\n"); }
		}
		public float Tavg {
			get { return this.tavg.Value; }
		}
		public float Flux {
			get { return this.flux.Value; }
			set { this.simInterface.Call("{set, es_core_server, flux, " + value.ToString() + "}\n"); }
		}
		
		public string Borate(int litres) {
			string boron = this.boron.Value.ToString();
			return this.simInterface.Call("{action, es_makeup_buffer_server, borate, [" + boron + ", " + litres + "]}\n");
		}

		public string Dilute(int litres) {
			string boron = this.boron.Value.ToString();
			return this.simInterface.Call("{action, es_makeup_buffer_server, dilute, [" + boron + ", " + litres + "]}\n");
		}

	}
}
