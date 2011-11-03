using System;
using System.Collections.Generic;
using EGON_cs_API;

namespace EGON_cs_API {
	public class SyncData {
		SimulatorInterface simInterface;
		string erlStamp;
		DateTime csharpStamp;

		public SyncData(SimulatorInterface simInterface) {
			this.simInterface = simInterface;
			this.Sync();
		}

		public void Sync() {
			this.erlStamp = this.simInterface.Call("{get, es_log_server, timestamp}");
			this.csharpStamp = DateTime.Now;
		}

		public override string ToString() {
			return this.erlStamp + " | " + this.csharpStamp.ToString();
		}
	}

	public class LogEntry {
		public string timestamp;
		public List<string> values;

		public LogEntry(string entry) {
			List<string> parts = Lib.StringToList(entry, new char[] {'[', '{'}, new char[] {']', '}'});

			this.timestamp = parts[0];

			parts.RemoveAt(0);
			this.values = parts;
		}
		
		public override string ToString() {
			string retval = this.timestamp + " | ";

			foreach (string s in this.values) {
				retval += s + ", ";
			}

			return retval.Trim().Trim(new char[] {','});
		}
	}

	public class SimulatorLog {
		public SimulatorInterface simInterface;
		public SyncData syncData;

		public SimulatorLog(SimulatorInterface simInterface) {
			this.simInterface = simInterface;
			this.syncData = new SyncData(this.simInterface);
		}

		public void Start() {
			this.simInterface.Call("{action, es_log_server, start}");
		}

		public string CycleLen {
			get { return this.simInterface.Call("{get, es_log_server, cycle_len}"); }
			set { this.simInterface.Call("{cast, es_log_server, cycle_len, " + value + "}"); }
		}

		public List<string> AvailableParameters() {
			string dump = this.simInterface.Call("{get, es_log_server, parameters}");

			List<string> ps = Lib.StringToList(dump, new char[] {'[', '{'}, new char[] {']', '}'});
			List<string> retval = new List<string>();
			foreach (string param in ps) {
				List<string> parts = Lib.StringToList(param, new char[] {'[', '{'}, new char[] {']', '}'});
				retval.Add(parts[1]);
			}
			return retval;
		}

		public void ClearParameters() {
			this.simInterface.Call("{cast, es_log_server, parameters, []}\n");
		}

		public void AddParameter(string parameterName, string serverName, string parameterCall) {
			this.simInterface.Call("{action, es_log_server, add_parameter, {\"" + parameterName + "\", " + serverName + ", " + parameterCall + "}}");
		}

		public List<LogEntry> CsvDump() {
			string dump = this.simInterface.Call("{get, es_log_server, csv_dump}\n");

			List<string> lines = Lib.StringToList(dump);
			lines.RemoveAt(0);
			
			List<LogEntry> entries = new List<LogEntry>();

			foreach (string line in lines) {
				entries.Add(new LogEntry(line));
			}
			
			return entries;
		}
	}

}

