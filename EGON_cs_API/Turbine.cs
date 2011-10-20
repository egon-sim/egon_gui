using System;
namespace EGON_cs_API {
	public class Turbine : StateClass {
		private float power;
		private float tref;
		private float target;
		private float rate;
		private bool go;


		public Turbine(SimulatorInterface simInterface) : base(simInterface) {
			this.Register(new Connector.Setter(setPower), "{get, es_turbine_server, power}");
			this.Register(new Connector.Setter(setTref), "{get, es_turbine_server, tref}");
			this.Register(new Connector.Setter(setTarget), "{get, es_turbine_server, target}");
			this.Register(new Connector.Setter(setRate), "{get, es_turbine_server, rate}");
			this.Register(new Connector.Setter(setGo), "{get, es_turbine_server, go}");

		}
		
		public void setPower(string val) {
			this.power = Lib.StringToFloat(val);
		}
		
		public void setTref(string val) {
			this.tref = Lib.StringToFloat(val);
		}
		
		public void setTarget(string val) {
			this.target = Lib.StringToFloat(val);
		}
		
		public void setRate(string val) {
			this.rate = Lib.StringToFloat(val);
		}
		
		public void setGo(string val) {
			this.go = Lib.StringToBool(val);
		}
		

		public float Power {
			get { return this.power; }
			set { this.simInterface.Call("{set, es_turbine_server, power, " + value.ToString() + "}\n"); }
		}

		public float Tref {
			get { return this.tref; }
		}

		public float Target {
			get { return this.target; }
			set { this.simInterface.Call("{set, es_turbine_server, target, " + value.ToString() + "}\n"); }
		}

		public float Rate {
			get { return this.rate; }
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

