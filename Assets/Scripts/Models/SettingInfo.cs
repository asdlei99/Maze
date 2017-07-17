using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

public class SettingInfo {

	private static SettingInfo instance = null;

	public bool isOpenViewRocker;
	public bool isOpenAudio;

	public static SettingInfo Instance{
		get{
			if (instance == null) {
				instance = new SettingInfo ();
			}
			return instance;
		}
	}

	public void Save(){
		PlayerPrefs.SetString ("isOpenViewRocker", isOpenViewRocker.ToString());
		PlayerPrefs.SetString ("isOpenAudio", isOpenAudio.ToString());
	}

	public void Init(){
		isOpenViewRocker = false;
		isOpenAudio = true;
		#if UNITY_EDITOR
		isOpenViewRocker = true;
		#endif

		if (PlayerPrefs.HasKey ("isOpenViewRocker")) {
			bool.TryParse (PlayerPrefs.GetString ("isOpenViewRocker"), out isOpenViewRocker);
		}

		if (PlayerPrefs.HasKey ("isOpenAudio")) {
			bool.TryParse (PlayerPrefs.GetString ("isOpenAudio"), out isOpenAudio);
		}
	}
}
