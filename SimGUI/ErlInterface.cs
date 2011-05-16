using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace SimGUI {

	public class ErlInterface {
		public NetworkStream stream;
		public TcpClient client;
		
		public ErlInterface(String server, int port) {
			this.client = new TcpClient(server, port);
			this.stream = client.GetStream();
		}
		
		public String Call(String parameter) {
			return this.getParameter(parameter);
		}
		
		public String getParameter(String parameter) {
			Byte[] data = Encoding.ASCII.GetBytes(parameter);
			this.stream.Write(data, 0, data.Length);
			this.stream.Flush();
			
			data = new Byte[256];
			String responseData = String.Empty;
			Int32 bytes = stream.Read(data, 0, data.Length);
			responseData = Encoding.ASCII.GetString(data, 0, bytes);
			return responseData;
		}
	}
}