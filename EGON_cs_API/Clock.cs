using System;
namespace EGON_cs_API {
	public class Clock {
		private ErlInterface erlInterface;

		public Clock(ErlInterface erlInterface) {
			this.erlInterface = erlInterface;
		}
		
		public string Start() {
			return this.erlInterface.Call("{action, es_clock_server, start}");
		}

		public string Stop() {
			return this.erlInterface.Call("{action, es_clock_server, stop}");
		}

		public string Status {
			get { return this.erlInterface.Call("{get, es_clock_server, status}"); }
		}
	}
}

