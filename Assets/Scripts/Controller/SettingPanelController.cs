using UnityEngine;
using System.Collections;

public class SettingPanelController : PanelController {

	[SerializeField]private Switch viewRockerSwitch;

	public delegate void SettingValueChanged(SettingType type, bool value); 
	public SettingValueChanged settingValueChanged;

	void Start () {
		viewRockerSwitch.valueChanged = IsOpenViewRocker;
	}
	
	void Update () {

	}

	void IsOpenViewRocker(bool value){
		if (settingValueChanged != null) {
			settingValueChanged (SettingType.ViewRocker, value);
		}
	}
}
