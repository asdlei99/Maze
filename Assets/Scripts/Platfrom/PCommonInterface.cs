using UnityEngine;
using System.Collections;

public class PCommonInterface {

	public static void SendLoginMessage(PCallback<string> cb){
		RegisterMessageCallback (cb);
		SendPlatformMessage (PEventMessage.Login);
	}

	public static void SendLogoutMessage(){
		SendPlatformMessage (PEventMessage.LoginOut);
	}

	public static void RegisterMessageCallback(PCallback<string> cb) {
		if (cb != null) {
			string messageKey = cb.Method.Name;
			PT_ThirdNoticer.RegisterMessageCallback(messageKey, cb);
		}
	}

	public static void SendPlatformMessage(PEventMessage nWhat) {
		#if UNITY_EDITOR
//		Nothing to do.
		#elif UNITY_ANDROID
//		PAndroidInterface.CallStaticMethod(PClazzNames.ANDROID_SDK_MANAGER, "SendUnityMessage", (int)nWhat);
		#elif UNITY_IPHONE
		PIOSInterface.IOSVoidCallStaticMethod("SendPlatformUnityMessage", (int)nWhat + "");
		#endif
	}
}
