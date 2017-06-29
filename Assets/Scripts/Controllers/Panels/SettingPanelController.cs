using UnityEngine;
using System.Collections;

public class SettingPanelController : PanelController {

	[SerializeField]private Switch viewRockerSwitch;
	[SerializeField]private Switch audioSwitch;

	public delegate void SettingValueChanged(SettingType type, bool value); 
	public SettingValueChanged settingValueChanged;

	void Awake(){
		viewRockerSwitch.isOpen = SettingInfo.Instance.isOpenViewRocker;
		audioSwitch.isOpen = SettingInfo.Instance.isOpenAudio;
	}

	void Start () {
		viewRockerSwitch.valueChanged = IsOpenViewRocker;
		audioSwitch.valueChanged = IsOpenAudio;
	}
	
	void Update () {

	}

	void IsOpenViewRocker(bool value){
		if (settingValueChanged != null) {
			settingValueChanged (SettingType.ViewRocker, value);
		}
	}

	void IsOpenAudio(bool value){
		if (settingValueChanged != null) {
			settingValueChanged (SettingType.Audio, value);
		}
	}
}
