using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace EGON_cs_API {
	public class EgonServer {
		public ErlInterface erlInterface;
		private ArrayList simulators;
		
		public EgonServer() {
			this.erlInterface = null;
		}
		
		public void Connect(String username, String server, int port) {
			this.erlInterface = new ErlInterface(username, server, port);
			this.simulators = this.listSims();
		}
		
		public ArrayList listSims() {
			String response = this.erlInterface.Call("{ask, list_sims}");
			String pattern = @"{(.+)}";
			
			MatchCollection matches = Regex.Matches(response, pattern);
			
			ArrayList sims = new ArrayList();
			foreach (Match match in matches) {				
				string[] parts = match.Groups[1].Value.Split(',');
				
				sims.Add(new Simulator(this.erlInterface, parts[1], parts[3], parts[4], parts[5]));
			}
			
			return sims;
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
		
		public Simulator NewSimulator(string name, string description) {
			Simulator sim = new Simulator(this.erlInterface, name, description);
			this.simulators.Add(sim);
			return sim;
		}
		
		public void StopSim(string simId) {
			this.erlInterface.StopSim(simId);
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

