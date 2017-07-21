using UnityEngine;
using System.Collections;

public class MazeTool {
	public static float errorFloat = -999f;
	public static string shareImgPath = Application.persistentDataPath + "/share.png";

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

	//截图 rect截图窗口大小
	public static Texture2D CaptureScreenshot(Rect rect) {
		// 先创建一个的空纹理，大小可根据实现需要来设置  
		Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false); 

		// 读取屏幕像素信息并存储为纹理数据，  
		screenShot.ReadPixels(rect, 0, 0);  
		screenShot.Apply();  

		// 然后将这些纹理数据，成一个png图片文件  
		byte[] bytes = screenShot.EncodeToPNG();
		System.IO.File.WriteAllBytes(shareImgPath, bytes);  
		Debug.Log(string.Format("wjr----截屏了一张图片: {0}", shareImgPath));

		// 最后，我返回这个Texture2d对象，这样我们直接，所这个截图图示在游戏中，当然这个根据自己的需求的。  
		return screenShot;  
	}
}
