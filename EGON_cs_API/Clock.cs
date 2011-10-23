using System;

namespace EGON_cs_API {
	public class Clock : StateClass {
		private Parameter<bool> logTicks;
		private Parameter<string> status;

		public Clock(SimulatorInterface simInterface) : base (simInterface) {
			this.logTicks = this.Register<bool>("{get, es_clock_server, log_ticks}");
			this.status = this.Register<string>("{get, es_clock_server, status}");
		}
		
		public bool LogTicks {
			get { return this.logTicks.Value; }
			set { this.simInterface.Call("{set, es_clock_server, log_ticks, " + Lib.BoolToString(value) + "}"); }
		}
		
		public string Start() {
			return this.simInterface.Call("{action, es_clock_server, ticking, start}");
		}

		public string Stop() {
			return this.simInterface.Call("{action, es_clock_server, ticking, stop}");
		}

		public string Status {
			get { return this.status.Value; }
		}

	}
}

