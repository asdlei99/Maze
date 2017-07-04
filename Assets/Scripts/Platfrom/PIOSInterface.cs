using System.Runtime.InteropServices;

/// <summary>
/// IOS基础交互类
/// </summary>
public class PIOSInterface {
	//注册IOS方法
	[DllImport("__Internal")]
	private static extern void _IOSGenarelClass(string methodName,string[] str, int length);
	
	[DllImport("__Internal")]
	private static extern string _IOSGenarelClassString(string methodName,string[] str, int length);
	
	public static void IOSVoidCallStaticMethod(string methodName, params string[] methodParams) {
#if UNITY_IPHONE && !UNITY_EDITOR
		int length;
		if (methodParams == null) {
			length = 0;
		}else {
			length = methodParams.Length;
		}
		_IOSGenarelClass(methodName, methodParams, length);
#endif
	}
	
	public static string IOSStringCallStaticMethod(string methodName, params string[] methodParams) {
		string res = "";

#if UNITY_IPHONE && !UNITY_EDITOR
		int arrlength;
		if (methodParams == null) {
			arrlength = 0;
		}else {
			arrlength = methodParams.Length;
		}
		res = _IOSGenarelClassString(methodName, methodParams, arrlength);
#endif
		return res;
	}
}
