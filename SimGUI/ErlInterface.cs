using System;
using System.Diagnostics;

namespace SimGUI {

	public class ErlInterface {
		string ReactorNode;
		string ReactorModule;
		string NodeName;
		string Cookie;
		string ModulePath;
		string shell;
		
		public ErlInterface(string NodeName, string ServerName, string ReactorModule, string Cookie, string ModulePath) {
			this.ReactorNode = NodeName + "@" + ServerName;
			this.NodeName = NodeName;
			this.ReactorModule = ReactorModule;
			this.Cookie = Cookie;
			this.ModulePath = ModulePath;
			this.shell = "bash";

			if (this.ModulePath.EndsWith("/") != true) {
				this.ModulePath += "/";
			}
		}
		
		public string StartNode() {
			//return this.Exec("erl -sname " + this.NodeName + " -pa '" + this.ModulePath + "' -setcookie '" + this.Cookie + "' -detached");
			Console.WriteLine(this.ModulePath + "start.sh");
			return this.Exec(this.ModulePath + "start.sh");
		}
		
		public string StopNode() {
			string command = "erl_call -a 'init stop' -n " + this.ReactorNode + " -c '" + this.Cookie + "'";
			return this.Exec(command);
		}
		
		public string StartModule() {
			return this.Call("start");
		}
	
		public string StopModule() {
			return this.Call("stop");
		}
	
		public string Call(string param) {
			string command = "erl_call -a '" + this.ReactorModule + " " + param + "' -n " + this.ReactorNode + " -c '" + this.Cookie + "'";
			return this.Exec(command);
		}
		
		public string Call(string module, string param) {
			string command = "erl_call -a '" + module + " " + param + "' -n " + this.ReactorNode + " -c '" + this.Cookie + "'";
			return this.Exec(command);
		}
		
		public string Exec(string cmd) {
			string retval;
	
			ProcessStartInfo PSI = new ProcessStartInfo(this.shell);
	        PSI.RedirectStandardInput = true;
	        PSI.RedirectStandardOutput = true;
	        PSI.RedirectStandardError = true;
	        PSI.UseShellExecute = false;
	        Process p = Process.Start(PSI);
	        System.IO.StreamWriter SW = p.StandardInput;
	        System.IO.StreamReader SR = p.StandardOutput;
	        SW.WriteLine(cmd);
			//Console.WriteLine("Log: " + cmd);
	        SW.Close();
			retval = SR.ReadToEnd();
			SR.Close();
			p.Close();
			return retval;
		}
		
	}
}