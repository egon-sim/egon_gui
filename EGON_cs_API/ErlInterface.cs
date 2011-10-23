using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Threading;
//using NUnit.Framework;

namespace EGON_cs_API {
	public class Connector {
		public delegate void Setter(string val);

		public Setter setter;
		public string call;
		public SimulatorInterface simInterface;

		public Connector(SimulatorInterface simInterface, Setter setter, string call) {
			this.setter = setter;
			this.call = call;
			this.simInterface = simInterface;
			
			this.Initialize();
		}

		public void Initialize() {
			this.setter(this.simInterface.Call(this.call));
		}

		public void Set(string val) {
			this.setter(val);
		}
	}

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
			
			Match match = Regex.Matches(response, pattern)[0];
			int port = int.Parse(match.Groups[1].Value);
			
			return new SimulatorInterface(this.username, this.server, port);
		}

	}

	public class SimulatorInterface : ErlInterface {
		protected ArrayList setters;
		protected ArrayList parameters;
		protected Timer refresher;

		public SimulatorInterface(String username, String server, int port) {
			TcpClient client;
			
			this.server = server;
			this.username = username;
			
			client = new TcpClient(server, port);
			this.stream = client.GetStream();
			this.setters = new ArrayList();
			this.parameters = new ArrayList();
			
			this.refresher = new Timer(new TimerCallback(Refresh), null, 0, 1000);
		}

		public void Register(Connector.Setter setter, string call) {
			lock (this.setters) {
				this.setters.Add(new Connector(this, setter, call));
			}
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

		public void Unregister(Connector.Setter setter) {
			Connector c = null;
			
			lock (this.setters) {
				foreach (Connector conn in this.setters) {
					if (conn.setter == setter) {
						c = conn;
						break;
					}
				}
				this.setters.Remove(c);
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
			if ((this.setters.Count == 0) && (this.parameters.Count == 0)) {
				Console.WriteLine("Nothing to refresh");
				return;
			}
			
			if (this.setters.Count > 0) {
			lock (this.setters) {
				string call = "[";
				foreach (Connector conn in this.setters) {
					call += conn.call + ",";
					//conn.Set();
				}
				call = call.Trim(',') + "]";
				string retval = this.Call(call);
				string[] parts = Lib.StringToArray(retval);
				
				for (int i = 0; i < this.setters.Count; i++) {
					((Connector)this.setters[i]).Set(parts[i]);
				}
			}
			}
			
			if (this.parameters.Count > 0) {
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
