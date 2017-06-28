using UnityEngine;
using System.Collections;

public class MazeTool {
	public static float errorFloat = -999f;

	public static Quaternion StringToQuaternion(string s){
		Quaternion q = new Quaternion (0, 0, 0, 0);
		if (s.Length > 8) {
			s = s.Substring (1, s.Length - 2);
			string[] arr = s.Split (',');
			if (arr.Length == 4) {
				float x = 0f, y = 0f, z = 0f, w = 0f;
				if (float.TryParse (arr [0], out x)) {
					q.x = x;
				}
				if (float.TryParse (arr [1], out y)) {
					q.y = y;
				}
				if (float.TryParse (arr [2], out z)) {
					q.z = z;
				}
				if (float.TryParse (arr [3], out w)) {
					q.w = w;
				}
			}
		}
		return q;
	}

	public static Vector3 StringToVector3(string s){
		Vector3 v = new Vector3 (0, 0, 0);
		if (s.Length > 6) {
			s = s.Substring (1, s.Length - 2);
			string[] arr = s.Split (',');
			if (arr.Length == 3) {
				float x = 0f, y = 0f, z = 0f;
				if (float.TryParse (arr [0], out x)) {
					v.x = x;
				}
				if (float.TryParse (arr [1], out y)) {
					v.y = y;
				}
				if (float.TryParse (arr [2], out z)) {
					v.z = z;
				}
			}
		}
		return v;
	}
}
