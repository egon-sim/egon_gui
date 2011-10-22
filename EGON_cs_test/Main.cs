using System;
using System.Collections;
using EGON_cs_API;

namespace EGON_cs_test {
	class MainClass {
		public static void Main(string[] args) {
			EgonServer server = new EgonServer();
			server.Connect("Nikola", "127.0.0.1", 1055);
			
			ArrayList sims = server.listSims();
			Console.WriteLine("Number of sims at start: {0}", sims.Count);
			
			if (sims.Count < 1) {
				server.NewSimulator("Test1", "Test simulator no. 1");
				server.NewSimulator("Test2", "Test simulator no. 2");
//				server.NewSimulator("Test3", "Test simulator no. 3");
//				server.NewSimulator("Test4", "Test simulator no. 4");
			}

			sims = server.listSims();

			server.refreshSimsList();
			server.refreshSimsList();

			sims = server.listSims();
			Console.WriteLine("SIMULATORS:");
			foreach (Simulator s in sims) {
				Console.WriteLine(s.ToString());
			}
			return;
			
			Simulator sim1 = (Simulator)sims[0];
			
			Clock clock = sim1.getClock();
			Reactor reactor = sim1.getReactor();
			Rods rods = sim1.getRods();
			Turbine turbine = sim1.getTurbine();

			clock.Start();
			clock.Start();
			clock.Stop();
			clock.Stop();
			clock.Start();
			
			reactor.Flux = 80;
			reactor.Burnup = 5000;
			
			rods.Mode = "auto";
			rods.AlphaNumCtrlRodPosition = "D200";
			
			turbine.Power = 75;
			turbine.Target = 85;
			turbine.Rate = 1;
			turbine.Go = true;

			sim1.simInterface.Refresh();
			
			Console.WriteLine(reactor.Flux == 80);
			Console.WriteLine(reactor.Burnup == 5000);
			Console.WriteLine(rods.Mode == "auto");

			for (int i = 0; i < 3; i++) {
				Console.WriteLine(reactor.Flux + " | " + reactor.Tavg + " | " + turbine.Power + " | " + clock.Status + " | " + rods.getCtrlRodPosition(3) + " | " + rods.getCtrlRodPosition(4));
				System.Threading.Thread.Sleep(1000);
			}

			clock.Dispose();
			rods.Dispose();

			while (true) {
				Console.WriteLine(reactor.Flux + " | " + reactor.Tavg + " | " + turbine.Power);
				System.Threading.Thread.Sleep(1000);
			}
			
		}

	}
}

