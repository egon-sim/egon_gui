using System;
using System.Collections.Generic;

namespace EGON_cs_API {	
	public class StateClass : IDisposable {
		protected SimulatorInterface simInterface;
		protected List<Parameter> parameters;
		
		public StateClass(SimulatorInterface simInterface) {
			this.simInterface = simInterface;
			this.parameters = new List<Parameter>();
		}

		public Parameter<T> Register<T>(string call) {
			Parameter<T> parameter = this.simInterface.Register<T>(call);
			this.parameters.Add(parameter);
			return parameter;
		}

		public void Dispose() {
			foreach (Parameter p in this.parameters) {
				this.simInterface.Unregister(p);
			}
			this.parameters.Clear();
		}

		~StateClass() {
			this.Dispose();
		}
	}
}

