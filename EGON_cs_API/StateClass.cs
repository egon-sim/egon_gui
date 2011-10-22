using System;
using System.Collections;

namespace EGON_cs_API {
	public class Parameter {
		private float value;
		private string call;
		private int ref_counter;

		public Parameter(string call) {
			this.call = call;
			this.ref_counter = 0;
		}

		public void AddRef() {
			this.ref_counter++;
		}

		public void RemRef() {
			if (this.Orphan) {
				throw new Exception("Cannot remove reference from Parameter: Parameter is orphan.");
			} else {
 				this.ref_counter--;
			}
		}

		public bool Orphan {
			get { return this.ref_counter <= 0; }
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

		public bool Equals(Parameter param) {
			return this.call == param.call;
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
			Parameter parameter = this.simInterface.Register(call);
			this.parameters2.Add(parameter);
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

