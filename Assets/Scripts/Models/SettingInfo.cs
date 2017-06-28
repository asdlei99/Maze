using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

public class SettingInfo {

	private static SettingInfo instance = null;

	public bool isOpenViewRocker;
	public bool isOpenAudio;
	private const string fileName = "settingInfo.txt";

	public static SettingInfo Instance{
		get{
			if (instance == null) {
				instance = new SettingInfo ();
			}
			return instance;
		}
	}

	public void Save(){
		JsonObject settingJson = new JsonObject();
		settingJson ["isOpenViewRocker"] = isOpenViewRocker;
		settingJson ["isOpenAudio"] = isOpenAudio;

		FileTool fileTool = new FileTool();
		fileTool.WriteFile (Application.persistentDataPath, fileName, settingJson.ToString());
	}

	public void Init(){
		isOpenViewRocker = false;
		isOpenAudio = true;
		#if UNITY_EDITOR
		isOpenViewRocker = true;
		#endif

		FileTool fileTool = new FileTool ();
//		Debug.Log (Application.persistentDataPath);
		ArrayList list = fileTool.LoadFile (Application.persistentDataPath, fileName);
		if (list != null) {
			object settingObj = new object ();
			if (SimpleJson.SimpleJson.TryDeserializeObject (list [0].ToString(), out settingObj)) {
				JsonObject settingJson = (JsonObject)settingObj;
				object openViewRocker;
				if (settingJson.TryGetValue ("isOpenViewRocker", out openViewRocker)) {
					bool.TryParse (openViewRocker.ToString (), out isOpenViewRocker);
				}

				object openAudio;
				if (settingJson.TryGetValue ("isOpenAudio", out openAudio)) {
					bool.TryParse (openAudio.ToString (), out isOpenAudio);
				}
			}
		} 
	}
}
