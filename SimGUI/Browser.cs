using Gtk;
using System;

namespace SimGUI
{

	public partial class Browser : Gtk.Window
	{
		ErlInterface simInterface;
		
		protected virtual void OnButton1Clicked (object sender, System.EventArgs e)
		{
			string username = this.entry3.Text;
			string server_address = this.entry2.Text;
			int server_port = this.spinbutton1.ValueAsInt;
			
			try {
				this.simInterface = new ErlInterface(username, server_address, server_port);
			} catch (System.Net.Sockets.SocketException) {
				this.simInterface = null;
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close,
				"Server not running on " + server_address + ":" + server_port + ".");
				md.Run();
				md.Destroy();
			} catch {			
				this.simInterface = null;
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close,
				"Connection error.");
				md.Run();
				md.Destroy();
			}
			if (this.simInterface != null) {
				this.textview1.Buffer.Text = this.simInterface.listSims();
			}

		}
		
		

		public Browser () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.simInterface = null;
						
			//this.nodeview1.Model = new SimulatorNode("");
			this.nodeview1.AppendColumn("Id", new Gtk.CellRendererText (), "text", 0);
			this.nodeview1.AppendColumn("Name", new Gtk.CellRendererText (), "text", 1);
			this.nodeview1.ShowAll();
			
			this.DeleteEvent += this.Disconnect;
		}
		protected virtual void OnButton3Clicked (object sender, System.EventArgs e)
		{
			this.RefreshSimList();
		}
		
		protected virtual void OnButton4Clicked (object sender, System.EventArgs e)
		{
			if (this.simInterface != null) {
				this.textview1.Buffer.Text = "";
				this.simInterface.Disconnect();
				this.simInterface = null;
			} else {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close,
				"Not connected to a server.");
				md.Run();
				md.Destroy();
			}
		}
		
		protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
		{
			NewSimStarter nss = new NewSimStarter(this.simInterface, this);
			nss.ShowAll();
		}
		
		public void RefreshSimList() {
			if (this.simInterface != null) {
				this.textview1.Buffer.Text = this.simInterface.listSims();
			} else {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close,
				"Not connected to a server.");
				md.Run();
				md.Destroy();
			}
		}
		
		public void Disconnect(object obj, DeleteEventArgs args) {
			this.simInterface.Disconnect();
		}
		
		~Browser() {
			this.simInterface.Disconnect();
		}
		
	}
}
