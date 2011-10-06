using System;
using EGON_cs_API;

namespace EGON_cs_API {
	public class Simulator {
		public ErlInterface erlInterface;
		private string name;
		private string description;
		public Clock clock;
		public Turbine turbine;
		public Reactor reactor;
		
		public Simulator(ErlInterface erlIterface, string name, string description) {
			this.erlInterface = erlIterface;
			this.name = name;
			this.description = description;
			this.clock = new Clock(this.erlInterface);
			this.turbine = new Turbine(this.erlInterface);
			this.reactor = new Reactor(this.erlInterface);
		}
		
		public void Start() {
			string retval = this.erlInterface.Call("{ask, start_new_simulator, [\"" + this.name + "\", \"" + this.description + "\", \"" + this.erlInterface.username + "\"]}");
			Console.WriteLine(retval);
		}
		
		public void Stop() {
			int simId = 1;
			this.erlInterface.Call("{ask, stop_simulator, " + simId + "}");
		}
	}
}

