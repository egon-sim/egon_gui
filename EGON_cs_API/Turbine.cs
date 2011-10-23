using System;
namespace EGON_cs_API {
	public class Turbine : StateClass {
		private Parameter<float> power;
		private Parameter<float> tref;
		private Parameter<float> target;
		private Parameter<float> rate;
		private Parameter<bool> go;


		public Turbine(SimulatorInterface simInterface) : base(simInterface) {
			this.power = this.Register<float>("{get, es_turbine_server, power}");
			this.tref = this.Register<float>("{get, es_turbine_server, tref}");
			this.target = this.Register<float>("{get, es_turbine_server, target}");
			this.rate = this.Register<float>("{get, es_turbine_server, rate}");
			this.go = this.Register<bool>("{get, es_turbine_server, go}");

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
			get { return this.go.Value; }
			set { this.simInterface.Call("{set, es_turbine_server, go, " + Lib.BoolToString(value) + "}"); }
		}
		
		public string Start() {
			return this.simInterface.Call("{action, es_turbine_server, ramp, start}\n");
		}

	}
}

