using System;
using System.Collections;

namespace EGON_cs_API {
	public class EgonServer {
		public ErlInterface erlInterface;
		private ArrayList simulators;
		
		public EgonServer() {
			this.erlInterface = null;
		}
		
		public void Connect(String username, String server, int port) {
			this.erlInterface = new ErlInterface(username, server, port);
		}
		
		public bool Connected {
			get {
				return (this.erlInterface != null);
			}
		}
		
		public void Disconnect() {
			if (this.erlInterface != null) {
				this.erlInterface.Disconnect();
				this.erlInterface = null;
			}
		}
		
		public void NewSimulator(string name, string description) {
			this.simulators.Add(new Simulator(this.erlInterface, name, description));
		}
		
		public ArrayList listSims() {
			return this.erlInterface.listSims();
		}
		
		public void StopSim(string simId) {
			this.erlInterface.StopSim(simId);
		}
		
		public Simulator ConnectToSim(string simId) {
			string [] info = this.erlInterface.simInfo(simId);
			return new Simulator(this.erlInterface.ConnectToSim(simId), info[2], info[3]);
		}
	}
}

