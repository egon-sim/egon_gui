using System;
using System.Collections.Generic;

namespace EGON_cs_API {
	public abstract class Parameter {
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

		public abstract void Set(string val);

		public string Call {
			get { return this.call; }
		}
		
		public bool Equals(Parameter param) {
			return this.call == param.call;
		}
	}

	public class Parameter<T> : Parameter {
		protected string s_val;
		private T val;

		public Parameter(string call) : base(call) {
		}
		
		public override void Set(string val) {
			this.Set1(val);
		}

		public T Value {
			get {
				return this.Value1;
			}
		}

		public void Set1(string val) {
			this.s_val = val;
		}

		public T Value1 {
			get {
				if (this is Parameter<float>) {
					return (T)Convert.ChangeType(Lib.StringToFloat(this.s_val), typeof(T));
				}
				if (this is Parameter<bool>) {
					return (T)Convert.ChangeType(Lib.StringToBool(this.s_val), typeof(T));
				}
				if (this is Parameter<string>) {
					return (T)Convert.ChangeType(this.s_val, typeof(T));
				}
				return default(T);
			}
		}

		public void Set2(string val) {
				if (this is Parameter<float>) {
					this.val = (T)Convert.ChangeType(Lib.StringToFloat(val), typeof(T));
				}
				if (this is Parameter<bool>) {
					this.val = (T)Convert.ChangeType(Lib.StringToBool(val), typeof(T));
				}
				if (this is Parameter<string>) {
					this.val = (T)Convert.ChangeType(val, typeof(T));
				}
				this.val = default(T);
		}

		public T Value2 {
			get {
				return this.val;
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

