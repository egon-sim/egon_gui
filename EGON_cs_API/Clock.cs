using System;

namespace EGON_cs_API {
	public class Clock : StateClass {
		private bool logTicks;
		private string status;

		public Clock(SimulatorInterface simInterface) : base (simInterface) {
			this.Register(new Connector.Setter(setLogTicks), "{get, es_clock_server, log_ticks}");
			this.Register(new Connector.Setter(setStatus), "{get, es_clock_server, status}");
		}

		public void setLogTicks(string val) {
			this.logTicks = Lib.StringToBool(val);
		}
		
		public void setStatus(string val) {
			this.status = val;
		}
		
		public bool LogTicks {
			get { return this.logTicks; }
			set { this.simInterface.Call("{set, es_clock_server, log_ticks, " + Lib.BoolToString(value) + "}"); }
		}
		
		public string Start() {
			return this.simInterface.Call("{action, es_clock_server, ticking, start}");
		}

		public string Stop() {
			return this.simInterface.Call("{action, es_clock_server, ticking, stop}");
		}

		public string Status {
			get { return this.status; }
		}

	}
}

