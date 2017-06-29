using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectLevelPanelController : PanelController {

	private const string Level1TitleName = "LevelTitle1";
	private const string Level2TitleName = "LevelTitle2";
	private const string Level3TitleName = "LevelTitle3";

	void Awake () {
		GBEventListener.Get (transform.FindChild (Level1TitleName).gameObject).onClick = SelectedLevel;
		GBEventListener.Get (transform.FindChild (Level2TitleName).gameObject).onClick = SelectedLevel;
		GBEventListener.Get (transform.FindChild (Level3TitleName).gameObject).onClick = SelectedLevel;
	}

	void Start () {

	}
	
	void Update () {
	
	}

	void SelectedLevel(GameObject obj){
		if (obj.name.Equals (Level1TitleName)) {
			CurrentLevelMessage.Instance.levelIndex = 1;
		} else if (obj.name.Equals (Level2TitleName)) {
			CurrentLevelMessage.Instance.levelIndex = 2;
		} else if (obj.name.Equals (Level3TitleName)) {
			CurrentLevelMessage.Instance.levelIndex = 3;
		}
		CurrentLevelMessage.Instance.Reset ();
		SceneManager.LoadSceneAsync ("Loading");
	}
}
