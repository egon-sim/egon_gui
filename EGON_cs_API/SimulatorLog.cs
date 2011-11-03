using System;
using System.IO;
using System.Collections.Generic;
using EGON_cs_API;

namespace EGON_cs_API {
	public class LogRange {
		List<LogEntry> entries;

		public LogRange(List<LogEntry> entries) {
			this.entries = entries;
		}

		public List<LogEntry> Items {
			get { return this.entries; }
		}

	}

	public class SyncData {
		private SimulatorInterface simInterface;
		private DateTime baseLine;

		public SyncData(SimulatorInterface simInterface) {
			this.simInterface = simInterface;
			this.Sync();
		}

		private void Sync() {
			string erlStamp = simInterface.Call("{get, es_log_server, timestamp}");

			TimeSpan erlTimeSpan = SyncData.ErlToTimeSpan(erlStamp);

			DateTime csharpStamp = DateTime.Now;
			this.baseLine = csharpStamp - erlTimeSpan;
		}

		private static TimeSpan ErlToTimeSpan(string erlStamp) {
			List<string> parts = Lib.StringToList(erlStamp, new char[]{'{'}, new char[]{'}'});
			long microseconds = (long.Parse(parts[0]) * 1000000 + long.Parse(parts[1])) * 1000000 + long.Parse(parts[2]);
			
			return new TimeSpan(microseconds * 10);
		}

		public string DateTimeToErl(DateTime dt) {
			long buffer = (long)((dt - this.baseLine).TotalMilliseconds * 1000);
			int microseconds = (int)(buffer % 1000000);
			buffer = buffer / 1000000;
			int seconds = (int)(buffer % 1000000);
			int megaseconds = (int)(buffer / 1000000);

			return "{" + megaseconds.ToString() + "," + seconds.ToString() + "," + microseconds.ToString() + "}";
		}

		public DateTime ErlToDateTime(string erlStamp) {
			return this.baseLine + SyncData.ErlToTimeSpan(erlStamp);
		}

		public override string ToString() {
			return this.baseLine.ToString();
		}
	}

	public class LogEntry {
		public DateTime timestamp;
		public List<string> values;
		private SyncData syncData;

		public LogEntry(string entry, SyncData syncData) {
			if (entry == "") {
				throw new Exception("Entry string empty.");
			}

			this.syncData = syncData;

			List<string> parts = Lib.StringToList(entry, new char[] {'[', '{'}, new char[] {']', '}'});
			this.timestamp = this.syncData.ErlToDateTime(parts[0]);

			parts.RemoveAt(0);
			this.values = parts;
		}
		
		public string ToCsv() {
			string retval = this.timestamp.TimeOfDay.ToString() + ", ";

			foreach (string s in this.values) {
				retval += s + ", ";
			}

			return retval.Trim().Trim(new char[] {','});
		}

		public string ToXls() {
			string retval = this.timestamp.TimeOfDay.ToString().Split('.')[0] + "\t";

			foreach (string s in this.values) {
				retval += s.Replace(".", ",") + "\t";
			}

			return retval.Trim().Trim(new char[] {','});
		}

		public override string ToString() {
			string retval = this.timestamp.ToString() + " | ";

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

		public List<string> ParameterNames {
			get {
				string dump = this.simInterface.Call("{get, es_log_server, parameters}");

				List<string> ps = Lib.StringToList(dump, new char[] {'[', '{'}, new char[] {']', '}'});
				List<string> retval = new List<string>();
				foreach (string param in ps) {
					List<string> parts = Lib.StringToList(param, new char[] {'[', '{'}, new char[] {']', '}'});
					retval.Add(parts[1]);
				}
				return retval;
			}
		}

		public void ClearParameters() {
			this.simInterface.Call("{cast, es_log_server, parameters, []}\n");
		}

		public void AddParameter(string parameterName, string serverName, string parameterCall) {
			this.simInterface.Call("{action, es_log_server, add_parameter, {\"" + parameterName + "\", " + serverName + ", " + parameterCall + "}}");
		}

		public LogRange Range(DateTime start, DateTime finish, int frequency) {
			string query = "{get, es_log_server, range, {" + this.syncData.DateTimeToErl(start) + ", " + this.syncData.DateTimeToErl(finish) + ", " + frequency.ToString() + "}}";

			string dump = this.simInterface.Call(query);
			List<string> lines = Lib.StringToList(dump);

			List<LogEntry> entries = new List<LogEntry>();

			foreach (string line in lines) {
				entries.Add(new LogEntry(line, this.syncData));
			}

			return new LogRange(entries);
		}

		public void WriteRange(string FileName, DateTime start, DateTime finish, int frequency) {
			LogRange range = this.Range(start, finish, frequency);

			FileStream fs = new FileStream(FileName, FileMode.Create);
			StreamWriter sw = new StreamWriter(fs);

			string header = "Time";

			foreach (string s in this.ParameterNames) {
				header += "\t" + s;
			}
			sw.WriteLine(header);

			foreach (LogEntry l in range.Items) {
				sw.WriteLine(l.ToXls());
			}
			sw.Close();
			fs.Close();
		}

		public List<LogEntry> Dump() {
			string dump = this.simInterface.Call("{get, es_log_server, csv_dump}\n");

			List<string> lines = Lib.StringToList(dump);
			lines.RemoveAt(0);
			
			List<LogEntry> entries = new List<LogEntry>();

			foreach (string line in lines) {
				entries.Add(new LogEntry(line, this.syncData));
			}

			return entries;
		}
	}

}

