using System;
using System.Collections.Generic;

namespace EGON_cs_API {
	public class Parameter {
		protected string val;
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

		public void Set(string val) {
			this.val = val;
		}

		public string Call {
			get { return this.call; }
		}
		
		public bool Equals(Parameter param) {
			return this.call == param.call;
		}
	}

	public class Parameter<T> : Parameter {
		public Parameter(string call) : base(call) {
		}
		
		public T Value {
			get {
				if (this is Parameter<float>) {
					return (T)Convert.ChangeType(Lib.StringToFloat(this.val), typeof(T));
				}
				if (this is Parameter<bool>) {
					return (T)Convert.ChangeType(Lib.StringToBool(this.val), typeof(T));
				}
				if (this is Parameter<string>) {
					return (T)Convert.ChangeType(this.val, typeof(T));
				}
				return default(T);
			}
		}
	}
	
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

