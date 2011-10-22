using System;
using System.Collections;

namespace EGON_cs_API {
	public class Parameter {
		private float value;
		private string call;

		public Parameter(string call) {
			this.call = call;
		}

		public void Set(string value) {
			this.value = Lib.StringToFloat(value);
		}

		public float Value {
			get { return this.value; }
		}

		public string Call {
			get { return this.call; }
		}
	}

	public class StateClass : IDisposable {
		protected SimulatorInterface simInterface;
		protected ArrayList parameters;
		protected ArrayList parameters2;
		
		public StateClass(SimulatorInterface simInterface) {
			this.simInterface = simInterface;
			this.parameters = new ArrayList();
			this.parameters2 = new ArrayList();
		}

		public void Register(Connector.Setter setter, string call) {
			this.parameters.Add(setter);
			this.simInterface.Register(setter, call);
		}

		public Parameter Register(string call) {
			Parameter parameter = new Parameter(call);
			this.parameters2.Add(parameter);
			this.simInterface.Register(parameter);
			return parameter;
		}

		public void Dispose() {
			foreach (Connector.Setter s in this.parameters) {
				this.simInterface.Unregister(s);
			}
			foreach (Parameter p in this.parameters2) {
				this.simInterface.Unregister(p);
			}
			this.parameters.Clear();
		}

		~StateClass() {
			this.Dispose();
		}
	}
}

