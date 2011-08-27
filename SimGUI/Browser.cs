using Gtk;
using System;

namespace SimGUI
{

	public partial class Browser : Gtk.Window {
		ErlInterface simInterface;
		SimEntry selected;

		protected virtual void OnButton1Clicked(object sender, System.EventArgs e) {
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
			this.RefreshSimList();
			
		}



		public Browser() : base(Gtk.WindowType.Toplevel) {
			this.Build();
			this.simInterface = null;
			
			this.selected = null;
			
			this.nodeview1.AppendColumn("Id", new Gtk.CellRendererText(), "text", 0);
			this.nodeview1.AppendColumn("Name", new Gtk.CellRendererText(), "text", 1);
			this.nodeview1.AppendColumn("Description", new Gtk.CellRendererText(), "text", 2);
			this.nodeview1.AppendColumn("Owner", new Gtk.CellRendererText(), "text", 3);
			this.nodeview1.NodeSelection.Changed += new System.EventHandler(OnSelectionChanged);
			this.nodeview1.ShowAll();
			// Create our TreeView and add it as our child widget
			
			
			this.DeleteEvent += this.Disconnect;
		}

		void OnSelectionChanged(object o, System.EventArgs args) {
			Gtk.NodeSelection selection = (Gtk.NodeSelection)o;
			SimEntry node = (SimEntry)selection.SelectedNode;
			if (node != null) {
				this.selected = node;
			}
		}

		protected virtual void OnButton3Clicked(object sender, System.EventArgs e) {
			this.RefreshSimList();
		}

		protected virtual void OnButton4Clicked(object sender, System.EventArgs e) {
			if (this.simInterface != null) {
				this.simInterface.Disconnect();
				this.simInterface = null;
			} else {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Not connected to a server.");
				md.Run();
				md.Destroy();
			}
		}

		protected virtual void OnButton2Clicked(object sender, System.EventArgs e) {
			NewSimStarter nss = new NewSimStarter(this.simInterface, this);
			nss.ShowAll();
		}

		public void RefreshSimList() {
			if (this.simInterface != null) {
				this.nodeview1.NodeStore = this.simInterface.generateStore();
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

		protected virtual void OnButton35Clicked(object sender, System.EventArgs e) {
			if (this.selected != null) {
				this.simInterface.ConnectToSim(this.selected.SimId);
				
				Startup su = new Startup(this.simInterface);
				su.ShowAll();
			}
			
		}
		
		
	}
}
