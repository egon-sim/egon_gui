using System;
namespace EGON_cs_API {
	public class Lib {
		public Lib() {
		}
		
		public static float StringToFloat(string val) {
			string clean;
			
			if (false) {
				clean = val;
			} else {
				clean = val.Replace('.', ',');
			}
			return float.Parse(clean);
		}
		
		public static string BoolToString(bool val) {
			return val.ToString().ToLower();
		}

		public static bool StringToBool(string val) {
			return bool.Parse(val.Substring(0, 1).ToUpper() + val.Substring(1));
		}

		public static string[] StringToArray(string val) {
			Console.WriteLine(val);
			return val.Trim('[').Trim(']').Split(',');
		}
	}
}

