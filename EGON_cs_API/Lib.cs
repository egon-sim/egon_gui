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

/*		public static string[] StringToArray(string val) {
			string[] parts = val.Trim('[').Trim(']').Split(',');
			return parts;
		}*/

		public static string[] StringToArray(string val) {
			return Lib.Flatten(new List<string>(), "", val.Trim(), -1).ToArray();
		}

		public static List<string> Flatten(List<string> done, string current, string val, int depth) {
			if (depth == -1) { // entering the method for the first time, first character should be '['
				if ((done.Count == 0) && (current == "") && (val.Substring(0, 1) == "[")) {
					return Flatten(done, "", val.Substring(1), depth + 1);
				} else {
					throw new Exception("Array not correctly formatted.");
				}
			}
			if ((depth == 0) && (val.Substring(0, 1) == "]") && (val.Length == 1)) { //ending the recursion
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

			switch (val.Substring(0, 1)) {
				case ",":
					if (depth == 0) { // add new element to the list
						if (current.Trim() != "") {
							done.Add(current.Trim());
						}						
						return Flatten(done, "", val.Substring(1), depth);
					} else { // we're inside an array with depth 2, so just continue
						return Flatten(done, current + val.Substring(0, 1), val.Substring(1), depth);
					}
				case "[": // entering an array with depth 2, from now on just copy chars from val to current until depth == 0
					if (depth == 0) {
						if (current.Trim() != "") {
							throw new Exception("Array not correctly formatted.");
						}
						return Flatten(done, "[", val.Substring(1), depth + 1);
					} else {
						return Flatten(done, current + val.Substring(0, 1), val.Substring(1), depth + 1);
					}
				case "]":
					if (depth < 1) {
						throw new Exception("Array not correctly formatted.");
					} else if (depth == 1) { // we're back to array with depth 1, so current to done and start collecting next element of array
						current += val.Substring(0, 1);
						done.Add(current.Trim());
						return Flatten(done, "", val.Substring(1), depth - 1);
					} else {
						return Flatten(done, current + val.Substring(0, 1), val.Substring(1), depth - 1);
					}
				default: // any character in top level: add it to current and go on
					return Flatten(done, current + val.Substring(0, 1), val.Substring(1), depth);
			}
		}
	}
}

