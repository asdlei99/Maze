using UnityEngine;
using System.Collections;
/// <summary>
/// 类路径集合
/// </summary>
public class PClazzNames
{
	/// <summary>
	/// 只用于Andorid平台
	/// </summary>
	public const string ANDROID_CURRENT_ACTIVITY = "com.unity3d.player.UnityPlayer";		//当前活动的Activity类
	public const string ANDROID_SDK_MANAGER = "com.ddianle.common.inface.SDKManager";		//SDK管理类
	public const string ANDROID_OS_ENVIRONMENT = "android.os.Environment";					//系统类
	public const string ANDROID_AUTO_UPDATE = "com.ddianle.autoupdate.AutoUpdate";			//版本更新类
	public const string ANDROID_SHARING = "com.ddianle.share.DdianleShareWeiBoInterface";	//分享类
	public const string ANDROID_GOTYEA = "ddianlegotyea.Ddianlegotyea";						//语音类

	/// <summary>
	/// 只用于IOS
	/// </summary>
	public const string IOS_SDK_MANAGER = "com.ddianle.mmau2.SDKManager";
}
