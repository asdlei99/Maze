using UnityEngine;
using System.Collections;

public class AssetBundleConfig {
	public static readonly string suffix = ".assetbundle";
	public static readonly string streamingAssetsFileName = "StreamingAssets";

	public static T LoadAssetBundle<T>(string assetbundlePath, string fileName) where T: Object{
		AssetBundle ab = AssetBundle.LoadFromFile (streamingAssetsPath + streamingAssetsFileName);
		AssetBundleManifest mainfest = (AssetBundleManifest)ab.LoadAsset("AssetBundleManifest");
		string[] dps = mainfest.GetAllDependencies(assetbundlePath);
		for (int i = 0; i < dps.Length; i++) {
//			Debug.Log ("dps---"+dps[i]);
			AssetBundle.LoadFromFile (streamingAssetsPath + dps[i]);
		}
		AssetBundle needAB = AssetBundle.LoadFromFile (streamingAssetsPath + assetbundlePath);
		return needAB.LoadAsset<T> (fileName);
	}

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
