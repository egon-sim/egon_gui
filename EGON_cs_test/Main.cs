using System;
using System.Collections;
using EGON_cs_API;

namespace EGON_cs_test {
	class MainClass {
		public static void Main(string[] args) {
			EgonServer server = new EgonServer();
			server.Connect("Nikola", "127.0.0.1", 1056);
			
			ArrayList sims = server.listSims();
			
			if (sims.Count < 4) {
				server.NewSimulator("Test1", "Test simulator no. 1");
				server.NewSimulator("Test2", "Test simulator no. 2");
				server.NewSimulator("Test3", "Test simulator no. 3");
				server.NewSimulator("Test4", "Test simulator no. 4");

				sims = server.listSims();
			}
			
			Console.WriteLine("SIMULATORS:");
			foreach (Simulator s in sims) {
				Console.WriteLine(s.ToString());
			}
			
			Simulator sim1 = (Simulator)sims[1];
			
			sim1.clock.Start();
			sim1.clock.Start();
			sim1.clock.Stop();
			sim1.clock.Stop();
			//sim1.clock.Start();
			
			sim1.reactor.Flux = 80;
			sim1.reactor.Burnup = 5000;
			
			sim1.reactor.rods.Mode = "auto";
			sim1.reactor.rods.setCtrlRodPosition("D200");
			
			sim1.turbine.Power = 75;
			sim1.turbine.Target = 85;
			sim1.turbine.Rate = 1;
			sim1.turbine.Go = true;

			
			Console.WriteLine(sim1.reactor.Flux == 80);
			Console.WriteLine(sim1.reactor.Burnup == 5000);
			Console.WriteLine(sim1.reactor.rods.Mode == "auto");
		
		}
	}
}

