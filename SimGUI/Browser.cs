using Gtk;
using System;
using EGON_cs_API;

namespace SimGUI {

	public partial class Browser : Gtk.Window {
		EgonServer server;
		SimEntry selected;

		protected virtual void OnButton1Clicked(object sender, System.EventArgs e) {
			string username = this.entry3.Text;
			string server_address = this.entry2.Text;
			int server_port = this.spinbutton1.ValueAsInt;
			
			if (username == "") {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Username not set.");
				md.Run();
				md.Destroy();
				return;
			}
			
			if (this.server.Connected) {
				this.server.Disconnect();
			}
			
			try {
				this.server.Connect(username, server_address, server_port);
				this.RefreshSimList();
			} catch (System.Net.Sockets.SocketException) {
				this.server.Disconnect();
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Server not running on " + server_address + ":" + server_port + ".");
				md.Run();
				md.Destroy();
			} catch {
				this.server.Disconnect();
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Connection error.");
				md.Run();
				md.Destroy();
			}
		}

		public Browser() : base(Gtk.WindowType.Toplevel) {
			this.Build();
			this.server = new EgonServer();
			
			this.selected = null;
			
			this.nodeview1.AppendColumn("Id", new Gtk.CellRendererText(), "text", 0);
			this.nodeview1.AppendColumn("Name", new Gtk.CellRendererText(), "text", 1);
			this.nodeview1.AppendColumn("Description", new Gtk.CellRendererText(), "text", 2);
			this.nodeview1.AppendColumn("Owner", new Gtk.CellRendererText(), "text", 3);
			this.nodeview1.NodeSelection.Changed += new System.EventHandler(OnSelectionChanged);
			this.nodeview1.ShowAll();
			
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
			if (this.server.Connected) {
				this.server.Disconnect();
			} else {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Not connected to a server.");
				md.Run();
				md.Destroy();
			}
		}

		protected virtual void OnButton2Clicked(object sender, System.EventArgs e) {
			SimStarter ss = new SimStarter(this.server, this);
			ss.ShowAll();
		}

		public void RefreshSimList() {
			if (this.server.Connected) {
				this.nodeview1.NodeStore = this.generateStore();
			} else {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Not connected to a server.");
				md.Run();
				md.Destroy();
			}
		}

		public void Disconnect(object obj, DeleteEventArgs args) {
			if (this.server.Connected)
				this.server.Disconnect();
		}

		~Browser() {
			this.server.Disconnect();
		}

		protected virtual void OnButton35Clicked(object sender, System.EventArgs e) {
			if (this.selected != null) {
				Simulator sim = this.server.ConnectToSim(this.selected.SimId);
				
				SimPanel sp = new SimPanel(sim);
				sp.ShowAll();
			} else {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Please select a simulator.");
				md.Run();
				md.Destroy();
			}
		}

		protected virtual void OnButton36Clicked(object sender, System.EventArgs e) {
			if (this.selected != null) {
				this.server.StopSim(this.selected.SimId);
				this.RefreshSimList();
			} else {
				MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Please select a simulator.");
				md.Run();
				md.Destroy();
			}
		}
		
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Application.Quit();
		}
		
		public Gtk.NodeStore generateStore() {
			Gtk.NodeStore store = new Gtk.NodeStore(typeof(SimEntry));
			
			foreach (Simulator s in this.server.listSims()) {
				store.AddNode(new SimEntry(s));
			}
			return store;
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

		public SimEntry(Simulator sim) {			
			this.simId = sim.simId;
			this.name = sim.name;
			this.description = sim.description;
			this.owner = sim.owner;
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
