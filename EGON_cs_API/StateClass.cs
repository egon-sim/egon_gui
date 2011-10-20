using System;
using System.Collections;

namespace EGON_cs_API {
	public class StateClass {
		protected SimulatorInterface simInterface;
		protected ArrayList parameters;
		
		public StateClass(SimulatorInterface simInterface) {
			this.simInterface = simInterface;
			this.parameters = new ArrayList();
		}

		public void Register(Connector.Setter setter, string call) {
			this.parameters.Add(setter);
			this.simInterface.Register(setter, call);
		}

		public void UnregisterAll() {
			foreach (Connector.Setter s in this.parameters) {
				this.simInterface.Unregister(s);
			}
			this.parameters.Clear();
		}

		~StateClass() {
			Console.WriteLine("Destructor.");
			this.UnregisterAll();
		}
	}
}

