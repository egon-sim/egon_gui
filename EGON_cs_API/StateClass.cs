using System;
using System.Collections;

namespace EGON_cs_API {
	public class StateClass {
		protected ErlInterface erlInterface;
		protected ArrayList parameters;
		
		public StateClass(ErlInterface erlInterface) {
			this.erlInterface = erlInterface;
			this.parameters = new ArrayList();
		}

		public void Register(Connector.Setter setter, string call) {
			this.parameters.Add(setter);
			this.erlInterface.Register(setter, call);
		}

		public void UnregisterAll() {
			foreach (Connector.Setter s in this.parameters) {
				this.erlInterface.Unregister(s);
			}
			this.parameters.Clear();
		}

		~StateClass() {
			Console.WriteLine("Destructor.");
			this.UnregisterAll();
		}
	}
}

