using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class CustomEditor {

	// 资源目录
	private static string RES_SRC_PATH = "Assets/Res/";
	// 打包输出目录
	private static string RES_OUTPUT_PATH = "Assets/StreamingAssets";

//	// xml文件生成器
//	private static XMLDocment doc;
//	// bundleName <-> List<AssetBuildBundleInfo>
//	private static Dictionary<string, List<AssetBuildBundleInfo>> bundleMap = new Dictionary<string, List<AssetBuildBundleInfo>>();
//	// 文件名 <-> AssetBuildBundleInfo
//	private static Dictionary<string, AssetBuildBundleInfo> fileMap = new Dictionary<string, AssetBuildBundleInfo>();

	[MenuItem("CustomEditor/Build AssetBundle")]
	public static void Pack() {
		// 清理输出目录
		CreateOrClearOutPath();

		// 清理之前设置过的bundleName
		ClearAssetBundleName();

		// 设置bunderName
//		bundleMap.Clear();
		List<string> resList = GetAllResDirs (RES_SRC_PATH);
		foreach (string dir in resList) {
			Debug.Log (dir);
			setAssetBundleName (dir);
		}
		return;
		// 打包
		BuildPipeline.BuildAssetBundles(RES_OUTPUT_PATH, BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneOSXIntel);
		AssetDatabase.Refresh ();
	}

	/// <summary>
	/// 设置AssetBundleName
	/// </summary>
	/// <param name="fullpath">Fullpath.</param>
	public static void setAssetBundleName(string fullPath) 
	{
		string[] files = System.IO.Directory.GetFiles (fullPath);
		if (files == null || files.Length == 0) {
			return;
		}

		string dirBundleName = fullPath.Substring (RES_SRC_PATH.Length);
		dirBundleName = dirBundleName + AssetBundleConfig.suffix;
		foreach (string file in files) {
			Debug.Log (file);
			if (file.EndsWith (".meta")) {
				continue;
			}
			AssetImporter importer = AssetImporter.GetAtPath (file);
			if (importer != null) {
				string ext = System.IO.Path.GetExtension (file);
				string bundleName = dirBundleName;
				if (null != ext && (ext.Equals (".prefab")||ext.Equals(".unity"))) {
					// prefab单个文件打包
					bundleName = file.Substring (RES_SRC_PATH.Length);
					if (null != ext) {
						bundleName = bundleName.Replace (ext, AssetBundleConfig.suffix);
					} else {
						bundleName += AssetBundleConfig.suffix;
					}

				}
				bundleName = bundleName.ToLower ();
				Debug.LogFormat ("Set AssetName Succ, File:{0}, AssetName:{1}", file, bundleName);
				importer.assetBundleName = bundleName;
				EditorUtility.UnloadUnusedAssetsImmediate();

				// 存储bundleInfo
				//				AssetBuildBundleInfo info = new AssetBuildBundleInfo();
				//				info.assetName = file;
				//				info.fileName = file;
				//				info.bundleName = bundleName;
				//				if (null != ext) {
				//					info.fileName = file.Substring (0, file.IndexOf (ext));
				//				}
				//				fileMap.Add (file, info);
				//
				//				List<AssetBuildBundleInfo> infoList = null;
				//				bundleMap.TryGetValue(info.bundleName, out infoList);
				//				if (null == infoList) {
				//					infoList = new List<AssetBuildBundleInfo> ();
				//					bundleMap.Add (info.bundleName, infoList);
				//				}
				//				infoList.Add (info);
			} else {
				Debug.LogFormat ("Set AssetName Fail, File:{0}, Msg:Importer is null", file);
			}
		}
	}

	/// <summary>
	/// 获取所有资源目录
	/// </summary>
	/// <returns>The res all dir path.</returns>
	public static List<string> GetAllResDirs(string fullPath) {
		List<string> dirList = new List<string> ();

		// 获取所有子文件
		GetAllSubResDirs (fullPath, dirList);

		return dirList;
	}

	/// <summary>
	/// 递归获取所有子目录文件夹
	/// </summary>
	/// <param name="fullPath">当前路径</param>
	/// <param name="dirList">文件夹列表</param>
	public static void GetAllSubResDirs(string fullPath, List<string> dirList) {
		if ((dirList == null) || (string.IsNullOrEmpty (fullPath))) {
			return;
		}

		string[] dirs = System.IO.Directory.GetDirectories (fullPath);
		if (dirs != null && dirs.Length > 0) {
			for (int i = 0; i < dirs.Length; ++i) {
				GetAllSubResDirs (dirs [i], dirList);
			}
		} else {
			dirList.Add (fullPath);
		}
	}

	/// <summary>
	/// 创建和清理输出目录
	/// </summary>
	public static void CreateOrClearOutPath() {
		if (!System.IO.Directory.Exists (RES_OUTPUT_PATH)) {
			// 不存在创建
			System.IO.Directory.CreateDirectory (RES_OUTPUT_PATH);
		} else {
			// 存在就清理
			System.IO.Directory.Delete(RES_OUTPUT_PATH, true);
			System.IO.Directory.CreateDirectory (RES_OUTPUT_PATH);
		}
	}

	/// <summary>
	/// 清理之前设置的bundleName
	/// </summary>
	public static void ClearAssetBundleName() {
		string[] bundleNames = AssetDatabase.GetAllAssetBundleNames ();
		for (int i = 0; i < bundleNames.Length; i++) {
			AssetDatabase.RemoveAssetBundleName (bundleNames [i], true);
		}
	}
}
