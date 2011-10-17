using System;
using EGON_cs_API;

namespace EGON_cs_API {
	public class Simulator {
		public ErlInterface erlInterface;
		public string name;
		public string description;
		public string simId;
		public string owner;
		
		public Simulator(ErlInterface serverInterface, string name, string description) {
			this.name = name;
			this.description = description;

			this.simId = serverInterface.StartSim(name, description);
			this.erlInterface = serverInterface.ConnectToSim(this.simId);			
		}

		public Simulator(ErlInterface serverInterface, string simId, string name, string description, string owner) {
			this.name = name;
			this.description = description;
			this.owner = owner;

			this.simId = simId;
			this.erlInterface = serverInterface.ConnectToSim(this.simId);
		}

		public void Stop() {
			int simId = 1;
			this.erlInterface.Call("{ask, stop_simulator, " + simId + "}");
		}
		
		public Clock getClock() {
		        return new Clock(this.erlInterface);
		}

		public Turbine getTurbine() {
		        return new Turbine(this.erlInterface);
		}
		
		public Reactor getReactor() {
		        return new Reactor(this.erlInterface);
		}
		
		public Rods getRods() {
		        return new Rods(this.erlInterface);
		}
		
		public override string ToString() {
			return this.simId + " | " + this.name + " | " + this.description + " | " + this.owner;
		}
	}
}

