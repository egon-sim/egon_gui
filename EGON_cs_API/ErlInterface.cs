using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using NUnit.Framework;

namespace EGON_cs_API {
	public class ErlInterface : ICloneable {
		public NetworkStream stream;
		public String username;
		public String server;

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
			
			ArrayList simStrings = new ArrayList();
			foreach (Match match in matches) {
				simStrings.Add(match.Groups[1].Value);
			}
			
			return simStrings;
		}
		
		public string[] simInfo(string simId) {
			String response = this.Call("{ask, sim_info, " + simId + "}");
			String pattern = @"{simulator_manifest,(.+)}";
			
			Match match = Regex.Match(response, pattern);
			
			String info = match.Groups[1].Value;
			
			return info.Split(',');
		}

		public string StartSim(string name, string description) {
			String response = this.Call("{ask, start_new_simulator, [\"" + name + "\", \"" + description + "\", \"" + this.username + "\"]}");
			String pattern = @"{ok,(.+)}";
			
			Match match = Regex.Match(response, pattern);
			
			String simId = match.Groups[1].Value;
			
			return simId;
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
			//Console.WriteLine(parameter);
			
			Byte[] data = Encoding.ASCII.GetBytes("[" + parameter + "]\n");
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
	
	[TestFixture]
	public class ErlInterfaceTest {
		[Test]
		public void BurekTest() {
			Assert.AreEqual(1, 1);
			//Assert.AreEqual(1, 2);
		}

		[Test]
		public void ConnectFail() {
			//Assert.Fail(new ErlInterface("Test user", "localhost", 1055));
		}		
	}
}
