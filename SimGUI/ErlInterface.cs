using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace SimGUI {

	public class ErlInterface {
		public NetworkStream getStream;
		public NetworkStream actionStream;
		public NetworkStream setStream;
		
		public ErlInterface(String server, int getPort, int actionPort, int setPort) {
			TcpClient client;
			
			client = new TcpClient(server, getPort);
			this.getStream = client.GetStream();
			
			client = new TcpClient(server, actionPort);
			this.actionStream = client.GetStream();
			
			client = new TcpClient(server, setPort);
			this.setStream = client.GetStream();
			
		}
		
		public String getCall(String parameter) {
			return this.Call(this.getStream, parameter);
		}
		
		public String actionCall(String parameter) {
			return this.Call(this.actionStream, parameter);
		}
		
		public String setCall(String parameter) {
			return this.Call(this.setStream, parameter);
		}
		
		public String Call(NetworkStream stream, String parameter) {
			Byte[] data = Encoding.ASCII.GetBytes(parameter);
			stream.Write(data, 0, data.Length);
			stream.Flush();
			
			data = new Byte[256];
			String responseData = String.Empty;
			Int32 bytes = stream.Read(data, 0, data.Length);
			responseData = Encoding.ASCII.GetString(data, 0, bytes);
			return responseData;
		}
	}
}