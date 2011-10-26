using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Threading;
//using NUnit.Framework;

namespace EGON_cs_API {
	public class ServerInterface : ErlInterface {
		public ServerInterface(String username, String server, int port) {
			TcpClient client;
			
			this.server = server;
			this.username = username;
			
			client = new TcpClient(server, port);
			this.stream = client.GetStream();
		}

		public string StartSim(string name, string description) {
			String response = this.Call("{ask, start_new_simulator, [\"" + name + "\", \"" + description + "\", \"" + this.username + "\"]}");
			String pattern = @"{connected,(.+),.+}";
			
			//Console.WriteLine(response);
			
			Match match = Regex.Match(response, pattern);
			
			String simId = match.Groups[1].Value;
			
			return simId;
		}

		public bool StopSim(string simId) {
			this.Call("{ask, stop_simulator, " + simId + "}");
			
			return true;
		}

		public SimulatorInterface ConnectToSim(string simId) {
			string response = this.Call("{ask, connect_to_simulator, [" + simId + ", \"" + username + "\"]}");
			string pattern = @"{connected,.+,(.+)}";
			Match match;
			
			try {
				match = Regex.Matches(response, pattern)[0];
			} catch (ArgumentOutOfRangeException) {
				Console.WriteLine("Response doesn't match: {0}", response);
				return null;
			}

			int port = int.Parse(match.Groups[1].Value);	
			return new SimulatorInterface(this.username, this.server, port);
		}

	}

	public class SimulatorInterface : ErlInterface {
		protected List<Parameter> parameters;
		protected Timer refresher;

		public SimulatorInterface(String username, String server, int port) {
			TcpClient client;
			
			this.server = server;
			this.username = username;
			
			client = new TcpClient(server, port);
			this.stream = client.GetStream();
			this.parameters = new List<Parameter>();
			
			this.refresher = new Timer(new TimerCallback(Refresh), null, 0, 1000);
		}

		public Parameter<T> Register<T>(string call) {
			Parameter<T> existing_parameter = null;

			foreach (Parameter p in this.parameters) {
				if (p.Call == call) {
					existing_parameter = (Parameter<T>)p;
				}
			}

			if (existing_parameter != null) {
				existing_parameter.AddRef();
				return existing_parameter;
			} else {
				Parameter<T> parameter = new Parameter<T>(call);
				parameter.AddRef();
				lock (this.parameters) {
					this.parameters.Add(parameter);
				}
				return parameter;
			}
		}

		public void Unregister(Parameter parameter) {
			lock (this.parameters) {
				int i = this.parameters.IndexOf(parameter);

				if (i == -1) {
					throw new Exception("Cannot unregister Parameter: Parameter is not registered.");
				} else {
					Parameter p = (Parameter)this.parameters[i];
					p.RemRef();
					if (p.Orphan) {
						this.parameters.Remove(p);
					}
				}
			}
		}

		public void Refresh() {
			this.Refresh(null);
		}

		public void Refresh(Object o) {
			if (this.parameters.Count == 0) {
				Console.WriteLine("Nothing to refresh");
				return;
			}
			
			lock (this.parameters) {
				string call = "[";
				foreach (Parameter param in this.parameters) {
					call += param.Call + ",";
				}
				call = call.Trim(',') + "]";
				string retval = this.Call(call);
				string[] parts = Lib.StringToArray(retval);
				
				for (int i = 0; i < this.parameters.Count; i++) {
					((Parameter)this.parameters[i]).Set(parts[i]);
				}
			}
			
			return;
		}
	}

	public class ErlInterface {
		public NetworkStream stream;
		public String username;
		public String server;

		public String Call(String parameter) {
			//Console.WriteLine(parameter);
			
			Byte[] data = Encoding.ASCII.GetBytes("[" + parameter + "]\n");
			this.stream.Write(data, 0, data.Length);
			this.stream.Flush();
			String responseData = String.Empty;
			
//			StreamReader reader = new StreamReader(this.stream);
//			responseData = reader.ReadToEnd();
			
			data = new Byte[10000];
			Int32 bytes = this.stream.Read(data, 0, data.Length);
			responseData = Encoding.ASCII.GetString(data, 0, bytes);
			
			return responseData;
		}

		public void Disconnect() {
			this.stream.Close();
		}

		~ErlInterface() {
			this.Disconnect();
		}
	}

//	[TestFixture]
	public class ErlInterfaceTest {
//		[Test]
		public void BurekTest() {
//			Assert.AreEqual(1, 1);
			//Assert.AreEqual(1, 2);
		}

//		[Test]
		public void ConnectFail() {
			//Assert.Fail(new ErlInterface("Test user", "localhost", 1055));
		}
	}
}
