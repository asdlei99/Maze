using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleConfig {
	public static readonly string suffix = ".assetbundle";
	public static readonly string streamingAssetsFileName = "StreamingAssets";
	public static AssetBundleManifest mainfest;

	public static AssetBundleManifest Mainfest{
		get{ 
			if (mainfest == null) {
				AssetBundle ab = AssetBundle.LoadFromFile (streamingAssetsPath + streamingAssetsFileName);
				if (ab != null) {
					mainfest = (AssetBundleManifest)ab.LoadAsset ("AssetBundleManifest");
				} else {
					Debug.Log ("Get Mainfest Error");
					return null;
				}
			}
			return mainfest;
		}
	}

	public static T LoadObjByAssetBundle<T>(string assetbundlePath, string fileName = null) where T: Object{
		if (Mainfest != null) {
			string[] dps = Mainfest.GetAllDependencies (assetbundlePath);
			AssetBundle[] abarr = new AssetBundle[dps.Length];
			for (int i = 0; i < dps.Length; i++) {
				abarr [i] = AssetBundle.LoadFromFile (streamingAssetsPath + dps [i]);
			}
			AssetBundle needAB = AssetBundle.LoadFromFile (streamingAssetsPath + assetbundlePath);
			if (needAB != null) {
				T obj;
				if (fileName == null) {
					obj = needAB.LoadAllAssets<T> () [0];
				} else {
					obj = needAB.LoadAsset<T> (fileName);
				}
				needAB.Unload (false);
				foreach (AssetBundle ab in abarr) {
					ab.Unload (false);
				}
				return obj;
			}
		}
		return null;
	}

	public static AssetBundle LoadAssetBundle(string assetbundlePath, Dictionary<string, AssetBundle> dic) {
		if (Mainfest != null) {
			string[] dps = Mainfest.GetAllDependencies (assetbundlePath);
			for (int i = 0; i < dps.Length; i++) {
				if (!dic.ContainsKey (dps [i])) {
					dic.Add (dps [i], AssetBundle.LoadFromFile (streamingAssetsPath + dps [i]));
				}
			}
			AssetBundle needAB = AssetBundle.LoadFromFile (streamingAssetsPath + assetbundlePath);
			return needAB;
		}
		return null;
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
