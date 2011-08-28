using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace SimGUI {

	public class ErlInterface : ICloneable {
		public NetworkStream stream;
		public String username;
		public String server;


		public Gtk.NodeStore generateStore() {
			Gtk.NodeStore store = new Gtk.NodeStore(typeof(SimEntry));
			
			foreach (SimEntry s in this.listSims()) {
				store.AddNode(s);
			}
			return store;
		}

		public ErlInterface(String username, String server, int port) {
			TcpClient client;
			
			this.server = server;
			this.username = username;
			
			client = new TcpClient(server, port);
			this.stream = client.GetStream();
			
		}

		public ArrayList listSims() {
			String response = this.Call("{ask, list_sims}");
			String pattern = @"{(.+)}";
			
			MatchCollection matches = Regex.Matches(response, pattern);
			
			ArrayList sims = new ArrayList();
			foreach (Match match in matches) {
				sims.Add(new SimEntry(match.Groups[1].Value));
			}
			
			return sims;
			
		}

		public bool StartSim(string name, string description) {
			this.Call("{ask, start_new_simulator, [\"" + name + "\", \"" + description + "\", \"" + this.username + "\"]}");
			return true;
		}

		public bool StopSim(string simId) {
			this.Call("{ask, stop_simulator, " + simId + "}");
			
			return true;
		}

		public ErlInterface ConnectToSim(string simId) {
			string response = this.Call("{ask, connect_to_simulator, [" + simId + ", \"" + username + "\"]}");
			string pattern = "{connected,(.+)}";
			
			Match match = Regex.Matches(response, pattern)[0];
			int port = int.Parse(match.Groups[1].Value);
			
			return new ErlInterface(this.username, this.server, port);
		}

		public String Call(String parameter) {
			
			Byte[] data = Encoding.ASCII.GetBytes(parameter);
			this.stream.Write(data, 0, data.Length);
			this.stream.Flush();
			String responseData = String.Empty;
			
			/*StreamReader reader = new StreamReader(this.stream);
			responseData = reader.ReadToEnd();*/

			data = new Byte[10000];
			Int32 bytes = this.stream.Read(data, 0, data.Length);
			responseData = Encoding.ASCII.GetString(data, 0, bytes);
			
			return responseData;
		}

		public void Disconnect() {
			this.stream.Close();
		}
		
		public object Clone() {
			return this.MemberwiseClone();
		}

		~ErlInterface() {
			this.Disconnect();
		}
	}


	[Gtk.TreeNode(ListOnly = true)]
	public class SimEntry : Gtk.TreeNode {
		string simId;
		string name;
		string description;
		string owner;

		public SimEntry() {
			this.simId = "";
			this.name = "";
			this.description = "";
			this.owner = "";
		}

		public SimEntry(string line) {
			string[] parts = line.Split(',');
			
			this.simId = parts[1];
			this.name = parts[3];
			this.description = parts[4];
			this.owner = parts[5];
		}

		[Gtk.TreeNodeValue(Column = 0)]
		public string SimId {
			get { return this.simId; }
		}

		[Gtk.TreeNodeValue(Column = 1)]
		public string Name {
			get { return this.name; }
		}

		[Gtk.TreeNodeValue(Column = 2)]
		public string Description {
			get { return this.description; }
		}

		[Gtk.TreeNodeValue(Column = 3)]
		public string Owner {
			get { return this.owner; }
		}
		
	}
}
