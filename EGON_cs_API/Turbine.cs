using System;
namespace EGON_cs_API {
	public class Turbine : StateClass {
		private Parameter power;
		private Parameter tref;
		private Parameter target;
		private Parameter rate;
		private bool go;


		public Turbine(SimulatorInterface simInterface) : base(simInterface) {
			this.power = this.Register("{get, es_turbine_server, power}");
			this.tref = this.Register("{get, es_turbine_server, tref}");
			this.target = this.Register("{get, es_turbine_server, target}");
			this.rate = this.Register("{get, es_turbine_server, rate}");
			this.Register(new Connector.Setter(setGo), "{get, es_turbine_server, go}");

		}
		
		public void setGo(string val) {
			this.go = Lib.StringToBool(val);
		}
		

		public float Power {
			get { return this.power.Value; }
			set { this.simInterface.Call("{set, es_turbine_server, power, " + value.ToString() + "}\n"); }
		}

		public float Tref {
			get { return this.tref.Value; }
		}

		public float Target {
			get { return this.target.Value; }
			set { this.simInterface.Call("{set, es_turbine_server, target, " + value.ToString() + "}\n"); }
		}

		public float Rate {
			get { return this.rate.Value; }
			set { this.simInterface.Call("{set, es_turbine_server, rate, " + value.ToString() + "}\n"); }
		}

		public bool Go {
			get { return this.go; }
			set {
				this.simInterface.Call("{set, es_turbine_server, go, " + Lib.BoolToString(value) + "}");
			}
		}
		
		public string Start() {
			return this.simInterface.Call("{action, es_turbine_server, ramp, start}\n");
		}

	}
}

