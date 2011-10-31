using System;
using System.Collections.Generic;
using EGON_cs_API;

namespace EGON_cs_test {
	class MainClass {
		public static void Main(string[] args) {
/*			foreach (string s in Lib.StringToArray2("[1,   2, [3,  [4, 5]], 6, [7, 8]]")) {
//			foreach (string s in Lib.StringToArray2("[1, 2, [3, [4, 5], 6], 7]")) {
//			foreach (string s in Lib.StringToArray2("[1, 2, 3, 4, 5, 6, 7]")) {
				Console.WriteLine("|{0}", s);
			}
			return;*/

			EgonServer server = new EgonServer("egon_server-0.1");

			server.GenerateIni();
//			return;

			server.StartServer();

			server.Connect("Nikola", "127.0.0.1", 1055);
//			server.Shutdown();
//			return;
			
			List<Simulator> sims = server.listSims();
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
			
			Simulator sim1 = sims[0];
			SimulatorLog log = sim1.Log;
			
			Console.WriteLine(log.syncData.ToString());
			Console.WriteLine(log.AvailableParameters());
			log.ClearParameters();
			Console.WriteLine(log.AvailableParameters());
			log.AddParameter("Tavg", "es_core_server", "{get, tavg}");
			log.AddParameter("Neutron flux", "es_core_server", "{get, flux}");
			Console.WriteLine(log.AvailableParameters());
			Console.WriteLine(log.CycleLen);

			Clock clock = sim1.getClock();
			Reactor reactor = sim1.getReactor();
			Reactor reactor2 = sim1.getReactor();
			Rods rods = sim1.getRods();
			Turbine turbine = sim1.getTurbine();

			clock.Start();
			clock.Start();
			clock.Stop();
			clock.Stop();
			clock.Start();

			log.CycleLen = "1000";
			log.Start();
			
			reactor2.Flux = 90;
			reactor2.Burnup = 4000;
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
			Console.WriteLine(reactor2.Flux == 80);
			Console.WriteLine(reactor2.Burnup == 5000);
			Console.WriteLine(rods.Mode == "auto");

			for (int i = 0; i < 2; i++) {
				Console.WriteLine(reactor.Flux + " | " + reactor2.Tavg + " | " + turbine.Power + " | " + clock.Status + " | " + rods.getCtrlRodPosition(3) + " | " + rods.getCtrlRodPosition(4));
				System.Threading.Thread.Sleep(1000);
			}

			clock.Dispose();
			rods.Dispose();

			for (int i = 0; i < 2; i++) {
				Console.WriteLine(reactor.Flux + " | " + reactor2.Tavg + " | " + turbine.Power);
				System.Threading.Thread.Sleep(1000);
			}

			turbine.Dispose();
			reactor2.Dispose();

			for (int i = 0; i < 7; i++) {
				Console.WriteLine(reactor.Flux + " | " + reactor.Tavg);
				System.Threading.Thread.Sleep(1000);
			}

			Console.WriteLine(log.GetCurrentValues());
			
			server.Shutdown();
		}

	}
}

