using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace SimGUI {

	public class ErlInterface {
		public NetworkStream stream;
		public String username;
		
		public ErlInterface(String username, String server, int port) {
			TcpClient client;
			
			client = new TcpClient(server, port);
			this.stream = client.GetStream();
			this.username = username;
			
		}
		
		public String listSims() {
			String response = this.Call("{ask, list_sims}");
//			Regex r = new Regex("\\[(.+)\\]");
			Regex r = new Regex("(.+)");
			Match m = r.Match(response);
			
			if (m.Groups[1].Success) {
				String retval = "";
				foreach (Capture c in m.Groups[1].Captures) {
					retval += "Sim: >" + c.Value + "<\n";
				}
				return retval;
			}
			return "Empty";
		}
		
		public String Call(String parameter) {
			
			Byte[] data = Encoding.ASCII.GetBytes(parameter);
			this.stream.Write(data, 0, data.Length);
			this.stream.Flush();
			
			data = new Byte[256];
			String responseData = String.Empty;
			Int32 bytes = this.stream.Read(data, 0, data.Length);
			responseData = Encoding.ASCII.GetString(data, 0, bytes);
			return responseData;
		}
	}
}