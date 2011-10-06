using System;
namespace EGON_cs_API {
	public class Turbine {
		private ErlInterface erlInterface;

		public Turbine(ErlInterface erlInterface) {
			this.erlInterface = erlInterface;
		}
		
		public float Power {
			get { return float.Parse(this.erlInterface.Call("{get, es_turbine_server, power}")); }
			set { this.erlInterface.Call("{set, es_turbine_server, power, " + value.ToString() + "}\n"); }
		}

		public float Tref {
			get { return float.Parse(this.erlInterface.Call("{get, es_w7300_server, tref}")); }
		}

		public int Target {
			get { return int.Parse(this.erlInterface.Call("{get, es_turbine_server, target}")); }
			set { this.erlInterface.Call("{set, es_turbine_server, target, " + value.ToString() + "}\n"); }
		}

		public int Rate {
			get { return int.Parse(this.erlInterface.Call("{get, es_turbine_server, rate}")); }
			set { this.erlInterface.Call("{set, es_turbine_server, rate, " + value.ToString() + "}\n"); }
		}

		public string Go {
			get { return this.erlInterface.Call("{get, es_turbine_server, go}").Trim(); }
		}
		
		public string Start() {
			return this.erlInterface.Call("{action, es_turbine_server, ramp, start}\n");
		}

	}
}

