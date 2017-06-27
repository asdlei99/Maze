using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		FileTool fileTool = new FileTool();
		fileTool.WriteFile (Application.persistentDataPath, fileName, "isOpenViewRocker_" + isOpenViewRocker);
		fileTool.WriteFileToAppend (Application.persistentDataPath, fileName, "isOpenAudio_" + isOpenAudio);
	}

	public void Init(){
		FileTool fileTool = new FileTool();
//		Debug.Log (Application.persistentDataPath);
		ArrayList list = fileTool.LoadFile (Application.persistentDataPath, fileName);
		if (list != null) {
			string openViewRocker = list [0].ToString ().Split ('_') [1].ToString ();
			if ("True".Equals (openViewRocker)) {
				isOpenViewRocker = true;
			} else {
				isOpenViewRocker = false;
			}

			string openAudio = list [1].ToString ().Split ('_') [1].ToString ();
			if ("False".Equals (openAudio)) {
				isOpenAudio = false;
			} else {
				isOpenAudio = true;
			}
		} else {
			isOpenViewRocker = false;
			isOpenAudio = true;
		}
	}
}
