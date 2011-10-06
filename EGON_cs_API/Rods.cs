using System;
namespace EGON_cs_API {
	public class Rods {
		private ErlInterface erlInterface;

		public Rods(ErlInterface erlInterface) {
			this.erlInterface = erlInterface;
		}
		
		public string Mode {
			get { return this.erlInterface.Call("{get, es_rod_controller_server, mode}\n"); }
			set { this.erlInterface.Call("{set, es_rod_controller_server, mode, " + value + "}\n"); }
		}
		
		public float Speed {
			get { return float.Parse(this.erlInterface.Call("{get, es_rod_controller_server, speed}\n")); }
		}
		
		public int getCtrlRodPosition(int rodgroup) {
			return int.Parse(this.erlInterface.Call("{get, es_rod_position_server, control_position, " + rodgroup.ToString() + "}\n"));
		}
		
		public int getSdRodPosition(int rodgroup) {
			return int.Parse(this.erlInterface.Call("{get, es_rod_position_server, shutdown_position, " + rodgroup.ToString() + "}\n"));
		}
		
		public string StepIn() {
			return this.erlInterface.Call("{action, es_rod_position_server, step_in}\n");
		}

		public string StepOut() {
			return this.erlInterface.Call("{action, es_rod_position_server, step_out}\n");
		}
	}
}

