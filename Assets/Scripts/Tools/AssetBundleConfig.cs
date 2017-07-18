using UnityEngine;
using System.Collections;

public class AssetBundleConfig {
	public static readonly string suffix = ".assetbundle";

	//不同平台下StreamingAssets的路径是不同的，这里需要注意一下。
	public static readonly string streamingAssetsPath =
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
//		"file://" + Application.dataPath + "/StreamingAssets/";
		Application.dataPath + "/StreamingAssets/";
		#elif UNITY_IPHONE
		Application.dataPath + "/Raw/";
		#elif UNITY_ANDROID
		"jar:file://" + Application.dataPath + "!/assets/";
		#else
		string.Empty;
		#endif

}
