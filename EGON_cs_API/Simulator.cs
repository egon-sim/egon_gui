using System;
using EGON_cs_API;

namespace EGON_cs_API {
	public class Simulator {
		public SimulatorInterface simInterface;
		public string name;
		public string description;
		public string simId;
		public string owner;
		
		public Simulator(ServerInterface serverInterface, string name, string description) {
			this.name = name;
			this.description = description;

			this.simId = serverInterface.StartSim(name, description);
			this.simInterface = serverInterface.ConnectToSim(this.simId);			
		}

		public Simulator(ServerInterface serverInterface, string simId, string name, string description, string owner) {
			this.name = name;
			this.description = description;
			this.owner = owner;

			this.simId = simId;
			this.simInterface = serverInterface.ConnectToSim(this.simId);
		}
		
		public void Refresh() {
			this.simInterface.Refresh();
		}

		public void Stop() {
			int simId = 1;
			this.simInterface.Call("{ask, stop_simulator, " + simId + "}");
		}
		
		public Clock getClock() {
		        return new Clock(this.simInterface);
		}

		public Turbine getTurbine() {
		        return new Turbine(this.simInterface);
		}
		
		public Reactor getReactor() {
		        return new Reactor(this.simInterface);
		}
		
		public Rods getRods() {
		        return new Rods(this.simInterface);
		}
		
		public override string ToString() {
			return this.simId + " | " + this.name + " | " + this.description + " | " + this.owner;
		}
	}
}

