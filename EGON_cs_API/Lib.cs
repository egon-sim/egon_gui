using System;
using System.Collections.Generic;

namespace EGON_cs_API {
	public class Lib {
		public Lib() {
		}
		
		public static float StringToFloat(string val) {
			return float.Parse(val, System.Globalization.CultureInfo.InvariantCulture);
		}
		
		public static string BoolToString(bool val) {
			return val.ToString().ToLower();
		}

		public static bool StringToBool(string val) {
			return bool.Parse(val.Substring(0, 1).ToUpper() + val.Substring(1));
		}

		public static string[] StringToArray(string val) {
			return Lib.StringToArray(val, new char[]{'['}, new char[]{']'});
		}

		public static string[] StringToArray(string val, char[] startBraces, char[] endBraces) {
			return Lib.StringToList(val, startBraces, endBraces).ToArray();
		}

		public static List<string> StringToList(string val) {
			return Lib.StringToList(val, new char[]{'['}, new char[]{']'});
		}

		public static List<string> StringToList(string val, char[] startBraces, char[] endBraces) {
			if (val == "") {
				throw new Exception("Input string empty.");
			}
			foreach (char brace in startBraces) {
				if (val.IndexOf(brace, 1) > -1) {
					return Lib.Flatten(new List<string>(), "", val.Trim(), startBraces, endBraces, -1);
				}
			}
			foreach (char brace in endBraces) {
				if (val.IndexOf(brace) < val.Length - 1) {
					return Lib.Flatten(new List<string>(), "", val.Trim(), startBraces, endBraces, -1);
				}
			}

			foreach (char brace in startBraces) {
				if (val[0] == brace) {
					val = val.TrimStart(brace);
					break;
				}
			}
			foreach (char brace in endBraces) {
				if (val[val.Length - 1] == brace) {
					val = val.TrimEnd(brace);
					break;
				}
			}

			if (val == "") {
				return new List<string>();
			}

			string[] parts = val.Split(',');
			for(int i = 0; i < parts.Length; i++) {
				parts[i] = parts[i].Trim();
			}

			return new List<string>(parts);
		}

		private static List<string> Flatten(List<string> done, string current, string val, char[] startBraces, char[] endBraces, int depth) {
			if (depth == -1) { // entering the method for the first time, first character should be in startBraces
				if ((done.Count == 0) && (current == "") && (Array.IndexOf(startBraces, val[0]) > -1)) {
					return Flatten(done, "", val.Substring(1), startBraces, endBraces, depth + 1);
				} else {
					throw new Exception("Array not correctly formatted: first character not a brace.");
				}
			}
			if ((depth == 0) && (Array.IndexOf(endBraces, val[0]) > -1) && (val.Length == 1)) { //ending the recursion
				if (current.Trim() != "") {
					done.Add(current.Trim());
				}						
				return done;
			}
			if (val.Length == 1) { //should have already ended
				throw new Exception("Array not correctly formatted.");
			}
			if ((depth == 0) && (val.Substring(0, 1) == "]")) { //should have already ended
				throw new Exception("Array not correctly formatted.");
			}

			char head = val[0];
			string tail = val.Substring(1);
			if (head == ',') {
				if (depth == 0) { // add new element to the list
					if (current.Trim() != "") {
						done.Add(current.Trim());
					}						
					return Flatten(done, "", tail, startBraces, endBraces, depth);
				} else { // we're inside an array with depth 2, so just continue
					return Flatten(done, current + head.ToString(), tail, startBraces, endBraces, depth);
				}
			}

			if (Array.IndexOf(startBraces, head) > -1) { // entering an array with depth 2, from now on just copy chars from val to current until depth == 0
				if (depth == 0) {
					if (current.Trim() != "") {
						throw new Exception("Array not correctly formatted.");
					}
					return Flatten(done, head.ToString(), tail, startBraces, endBraces, depth + 1);
				} else {
					return Flatten(done, current + head.ToString(), tail, startBraces, endBraces, depth + 1);
				}
			}

			if (Array.IndexOf(endBraces, head) > -1) {
				if (depth < 1) {
					throw new Exception("Array not correctly formatted.");
				} else if (depth == 1) { // we're back to array with depth 1, so current to done and start collecting next element of array
					current += head.ToString();
					done.Add(current.Trim());
					return Flatten(done, "", tail, startBraces, endBraces, depth - 1);
				} else {
					return Flatten(done, current + head.ToString(), tail, startBraces, endBraces, depth - 1);
				}
			}

			return Flatten(done, current + head.ToString(), tail, startBraces, endBraces, depth);
		}
	}
}

