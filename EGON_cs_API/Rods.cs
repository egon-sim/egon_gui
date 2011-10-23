using System;

namespace EGON_cs_API {
	public class Rods : StateClass {
		private Parameter<string> mode;
		private Parameter<float> speed;
		private int[] ctrlRodPosition;

		public void setCtrlRodPosition(string val) {
			string[] parts = Lib.StringToArray(val.Trim('"'));
			this.ctrlRodPosition = new int[parts.Length];
			for (Int32 i = 0; i < parts.Length; i++) { // TODO: rewrite this as a map
				this.ctrlRodPosition[i] = int.Parse(parts[i]);
			}
		}

		public Rods(SimulatorInterface simInterface) : base(simInterface) {
			this.mode = this.Register<string>("{get, es_rod_controller_server, mode}");
			this.speed = this.Register<float>("{get, es_rod_controller_server, speed}");
//			this.Register(new Connector.Setter(setCtrlRodPosition), "{get, es_rod_position_server, control_position_array_str}"); // TODO: this should be uncomented when Lib.StringToArray learns to parse subarrays
		}
		
		public string Mode {
			get { return this.mode.Value; }
			set { this.simInterface.Call("{set, es_rod_controller_server, mode, " + value + "}\n"); }
		}
		
		public float Speed {
			get { return this.speed.Value; }
		}
		
		public Int32[] CtrlRodPosition {
			get { 
				string val = this.simInterface.Call("{get, es_rod_position_server, control_position_array_str}\n"); // TODO: this should be comented when Lib.StringToArray learns to parse subarrays
				this.setCtrlRodPosition(val); // TODO: this should be comented when Lib.StringToArray learns to parse subarrays
				return this.ctrlRodPosition;
			}
		}

		public int getCtrlRodPosition(int rodgroup) {
			return this.CtrlRodPosition[rodgroup - 1];
		}
		
		public string AlphaNumCtrlRodPosition {
			set { this.simInterface.Call("{set, es_rod_position_server, control_position_str, \"" + value + "\"}\n"); }
		}
		
		public int getSdRodPosition(int rodgroup) {
			return int.Parse(this.simInterface.Call("{get, es_rod_position_server, shutdown_position, " + rodgroup.ToString() + "}\n"));
		}
		
		public string StepIn() {
			return this.simInterface.Call("{action, es_rod_position_server, step_in}\n");
		}

		public string StepOut() {
			return this.simInterface.Call("{action, es_rod_position_server, step_out}\n");
		}

	}
}

