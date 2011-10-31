using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EGON_cs_API {
	public class EgonServer {
		public ServerInterface servInterface;
		private List<Simulator> simulators;
		private string releasePath;
		
		public EgonServer(string releasePath) {
			this.servInterface = null;
			this.releasePath = releasePath;
		}
		
		public EgonServer() :this("egon_server-0.1") {}
		
		public string CurrentPath() {
			return System.IO.Directory.GetCurrentDirectory();
		}

		public void GenerateIni() {
			this.GenerateIni("egon_server-0.1", "erts-5.6.4");
		}

		public void GenerateIni(string releasePath, string ertsPath) {
			string pathToIni = this.CurrentPath() + "/" + releasePath + "/" + ertsPath + "/bin/erl.ini";
			StreamWriter sw = new StreamWriter(pathToIni);

			sw.WriteLine("[erlang]");
			sw.WriteLine("Bindir=E:/User_files/nskoric/bin/Git/code/egon_gui/EGON_cs_test/bin/Debug/" + releasePath + "/" + ertsPath + "/bin");
			sw.WriteLine("Progname=erl");
			sw.WriteLine("Rootdir=E:/User_files/nskoric/bin/Git/code/egon_gui/EGON_cs_test/bin/Debug/" + releasePath);

			sw.Close();
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

		public void Shutdown() {
			this.servInterface.Call("{shutdown_server}");
		}

		public void StartServer() {
			System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c start.bat\n");

			procStartInfo.WorkingDirectory = "E:\\User_files\\nskoric\\bin\\Git\\code\\egon_gui\\EGON_cs_test\\bin\\Debug\\" + this.releasePath;
			procStartInfo.RedirectStandardOutput = true;
			procStartInfo.UseShellExecute = false;
			procStartInfo.CreateNoWindow = true;
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo = procStartInfo;
			proc.Start();

			System.Threading.Thread.Sleep(1000);

//			string result = proc.StandardOutput.ReadToEnd();
//			Console.WriteLine(result);
		}
	}
}

