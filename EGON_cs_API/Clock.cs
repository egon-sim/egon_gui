using System;
namespace EGON_cs_API {
	public class Clock {
		private ErlInterface erlInterface;
		private bool logTicks;
		private string status;

		public Clock(ErlInterface erlInterface) {
			this.erlInterface = erlInterface;

			erlInterface.Register(new Connector.Setter(setLogTicks), "{get, es_clock_server, log_ticks}");
			erlInterface.Register(new Connector.Setter(setStatus), "{get, es_clock_server, status}");
		}

		public void setLogTicks(string val) {
			this.logTicks = ErlInterface.StringToBool(val);
		}
		
		public void setStatus(string val) {
			this.status = val;
		}
		
		public bool LogTicks {
			get { return this.logTicks; }
			set { this.erlInterface.Call("{set, es_clock_server, log_ticks, " + ErlInterface.BoolToString(value) + "}"); }
		}
		
		public string Start() {
			return this.erlInterface.Call("{action, es_clock_server, ticking, start}");
		}

		public string Stop() {
			return this.erlInterface.Call("{action, es_clock_server, ticking, stop}");
		}

		public string Status {
			get { return this.status; }
		}
	}
}

