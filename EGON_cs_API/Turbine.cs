using System;
namespace EGON_cs_API {
	public class Turbine {
		private ErlInterface erlInterface;
		private float power;
		private float tref;
		private float target;
		private float rate;
		private bool go;


		public Turbine(ErlInterface erlInterface) {
			this.erlInterface = erlInterface;

			erlInterface.Register(new Connector.Setter(setPower), "{get, es_turbine_server, power}");
			erlInterface.Register(new Connector.Setter(setTref), "{get, es_turbine_server, tref}");
			erlInterface.Register(new Connector.Setter(setTarget), "{get, es_turbine_server, target}");
			erlInterface.Register(new Connector.Setter(setRate), "{get, es_turbine_server, rate}");
			erlInterface.Register(new Connector.Setter(setGo), "{get, es_turbine_server, go}");

		}
		
		public void setPower(string val) {
			this.power = float.Parse(val.Replace('.', ','));
		}
		
		public void setTref(string val) {
			this.tref = float.Parse(val.Replace('.', ','));
		}
		
		public void setTarget(string val) {
			this.target = float.Parse(val.Replace('.', ','));
		}
		
		public void setRate(string val) {
			this.rate = float.Parse(val.Replace('.', ','));
		}
		
		public void setGo(string val) {
			this.go = ErlInterface.StringToBool(val);
		}
		

		public float Power {
			get { return this.power; }
			set { this.erlInterface.Call("{set, es_turbine_server, power, " + value.ToString() + "}\n"); }
		}

		public float Tref {
			get { return this.tref; }
		}

		public float Target {
			get { return this.target; }
			set { this.erlInterface.Call("{set, es_turbine_server, target, " + value.ToString() + "}\n"); }
		}

		public float Rate {
			get { return this.rate; }
			set { this.erlInterface.Call("{set, es_turbine_server, rate, " + value.ToString() + "}\n"); }
		}

		public bool Go {
			get { return this.go; }
			set {
				this.erlInterface.Call("{set, es_turbine_server, go, " + ErlInterface.BoolToString(value) + "}");
			}
		}
		
		public string Start() {
			return this.erlInterface.Call("{action, es_turbine_server, ramp, start}\n");
		}

	}
}

