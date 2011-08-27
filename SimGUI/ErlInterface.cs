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
			String pattern = @"{(.+)}";
			
			MatchCollection matches = Regex.Matches(response, pattern);
			
			String retval = "";
			foreach (Match match in matches) {
				retval += match.Groups[1].Value + "\n";
			}
			
			if (retval == "") {
				return "No simulators.";
			}
			return String.Join(" ", retval.Split(','));
			
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
		
		public void Disconnect() {
			this.stream.Close();
		}
		
		~ErlInterface() {
			this.Disconnect();
		}
	}
}