using System;
using EGON_cs_API;

namespace EGON_cs_API {
	public class Simulator {
		public ErlInterface erlInterface;
		public string name;
		public string description;
		public string simId;
		public string owner;
		public Clock clock;
		public Turbine turbine;
		public Reactor reactor;
		
		public Simulator(ErlInterface serverInterface, string name, string description) {
			this.name = name;
			this.description = description;

			this.simId = serverInterface.StartSim(name, description);
			this.erlInterface = serverInterface.ConnectToSim(this.simId);
			
			this.clock = new Clock(this.erlInterface);
			this.turbine = new Turbine(this.erlInterface);
			this.reactor = new Reactor(this.erlInterface);
		}
		
		public Simulator(ErlInterface serverInterface, string simId, string name, string description, string owner) {
			this.name = name;
			this.description = description;
			this.owner = owner;

			this.simId = simId;
			this.erlInterface = serverInterface.ConnectToSim(this.simId);
			
			this.clock = new Clock(this.erlInterface);
			this.turbine = new Turbine(this.erlInterface);
			this.reactor = new Reactor(this.erlInterface);
		}
		
		public void Stop() {
			int simId = 1;
			this.erlInterface.Call("{ask, stop_simulator, " + simId + "}");
		}
		
		public override string ToString() {
			return this.simId + " | " + this.name + " | " + this.description + " | " + this.owner + " | " + this.clock.Status;
		}
	}
}

