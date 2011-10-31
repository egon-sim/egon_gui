using System;
using EGON_cs_API;

namespace EGON_cs_API {
	public class SyncData {
		SimulatorInterface simInterface;
		string erlStamp;
		DateTime csharpStamp;

		public SyncData(SimulatorInterface simInterface) {
			this.simInterface = simInterface;
			this.Sync();
		}

		public void Sync() {
			this.erlStamp = this.simInterface.Call("{get, es_log_server, timestamp}");
			this.csharpStamp = DateTime.Now;
		}

		public string ToString() {
			return this.erlStamp + " | " + this.csharpStamp.ToString();
		}
	}


	public class SimulatorLog {
		public SimulatorInterface simInterface;
		public SyncData syncData;

		public SimulatorLog(SimulatorInterface simInterface) {
			this.simInterface = simInterface;
			this.syncData = new SyncData(this.simInterface);
		}

		public void Start() {
			this.simInterface.Call("{action, es_log_server, start}");
		}

		public string CycleLen {
			get { return this.simInterface.Call("{get, es_log_server, cycle_len}"); }
			set { this.simInterface.Call("{cast, es_log_server, cycle_len, " + value + "}"); }
		}

		public string AvailableParameters() {
			return this.simInterface.Call("{get, es_log_server, parameters}");
		}

		public void ClearParameters() {
			this.simInterface.Call("{cast, es_log_server, parameters, []}\n");
		}

		public void AddParameter(string parameterName, string serverName, string parameterCall) {
			this.simInterface.Call("{action, es_log_server, add_parameter, {\"" + parameterName + "\", " + serverName + ", " + parameterCall + "}}");
		}

		public string GetCurrentValues() {
			return this.simInterface.Call("{get, es_log_server, csv_dump}\n");
		}
	}


	public class Simulator {
		public SimulatorInterface simInterface;
		public string name;
		public string description;
		public string simId;
		public string owner;
		public SimulatorLog log;
		
		public Simulator(ServerInterface serverInterface, string name, string description) {
			this.name = name;
			this.description = description;

			this.simId = serverInterface.StartSim(name, description);
			this.simInterface = serverInterface.ConnectToSim(this.simId);
			this.log = new SimulatorLog(this.simInterface);
		}

		public Simulator(ServerInterface serverInterface, string simId, string name, string description, string owner) {
			this.name = name;
			this.description = description;
			this.owner = owner;

			this.simId = simId;
			this.simInterface = serverInterface.ConnectToSim(this.simId);
			this.log = new SimulatorLog(this.simInterface);
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

