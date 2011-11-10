using System;
using EGON_cs_API;
using System.Collections.Generic;

namespace EGON_cs_API {
	public class Simulator {
		public SimulatorInterface simInterface;
		public string name;
		public string description;
		public string simId;
		public string owner;
		public SimulatorLog log;
		
		public Simulator(ServerInterface serverInterface, string name, string description) {
			this.simId = serverInterface.StartSim(name, description);
			this.simInterface = serverInterface.ConnectToSim(this.simId);

			this.Init();
			this.log = new SimulatorLog(this.simInterface);
		}

		public Simulator(ServerInterface serverInterface, string simId) {
			this.simId = simId;
			this.simInterface = serverInterface.ConnectToSim(this.simId);

			this.Init();
			this.log = new SimulatorLog(this.simInterface);
		}

		public void Init() {
			List<string> parts = this.simInterface.GetSimInfo(this.SimId);

			this.name = parts[2];
			this.description = parts[3];
			this.owner = parts[4];
		}
		
		public string SimId {
			get { return this.simId; }
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

		public SimulatorLog Log {
			get { return this.log; }
		}
		
		public override string ToString() {
			return this.simId + " | " + this.name + " | " + this.description + " | " + this.owner;
		}
	}
}

