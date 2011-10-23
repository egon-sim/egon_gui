using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EGON_cs_API {
	public class EgonServer {
		public ServerInterface servInterface;
		private List<Simulator> simulators;
		
		public EgonServer() {
			this.servInterface = null;
		}
		
		public void Connect(String username, String server, int port) {
			this.servInterface = new ServerInterface(username, server, port);
			this.simulators = new List<Simulator>();
			this.refreshSimsList();
		}

		public List<Simulator> listSims() {
			return this.simulators;
		}
		
		public List<Simulator> refreshSimsList() {
			String response = this.servInterface.Call("{ask, list_sims}");
			String pattern = @"{(.+)}";
			
			MatchCollection matches = Regex.Matches(response, pattern);
			List<Simulator> newSimList = new List<Simulator>();
			Simulator foundSim = null;
			foreach (Match match in matches) {
				string[] parts = match.Groups[1].Value.Split(',');
				string simId = parts[1];
				foundSim = null;
				foreach (Simulator sim in this.simulators) {
					if (sim.simId == simId) {
						foundSim = sim;
					}
				}
				if (foundSim != null) {
					newSimList.Add(foundSim);
					this.simulators.Remove(foundSim);
				} else {
					newSimList.Add(new Simulator(this.servInterface, parts[1], parts[3], parts[4], parts[5]));
				}
			}

			this.simulators = newSimList;
			return listSims();
		}
		
		public bool Connected {
			get {
				return (this.servInterface != null);
			}
		}
		
		public void Disconnect() {
			if (this.servInterface != null) {
				this.servInterface.Disconnect();
				this.servInterface = null;
			}
		}
		
		public Simulator NewSimulator(string name, string description) {
			Simulator sim = new Simulator(this.servInterface, name, description);
			this.simulators.Add(sim);
			return sim;
		}
		
		public void StopSim(string simId) {
			this.servInterface.StopSim(simId);
		}
		
		public Simulator ConnectToSim(string simId) {
			foreach (Simulator sim in this.simulators) {
				if (sim.simId == simId) {
					return sim;
				}
			}
			return null;
		}
	}
}

