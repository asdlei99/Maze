using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndPanelController : PanelController {

	void Start () {
		GameObject.Find ("EndBtn").GetComponent<Button> ().onClick.AddListener (delegate {
			if(CurrentLevelMessage.Instance.levelIndex == Player.Instance.maxLevel){
				Player.Instance.maxLevel++;
			}
			SceneManager.LoadSceneAsync ("SelectLevel");
		});

		GameObject.Find ("NextBtn").GetComponent<Button> ().onClick.AddListener (delegate {
			if(CurrentLevelMessage.Instance.levelIndex == Player.Instance.maxLevel){
				Player.Instance.maxLevel++;
			}
			CurrentLevelMessage.Instance.levelIndex ++;
			CurrentLevelMessage.Instance.Reset ();
			SceneManager.LoadSceneAsync ("Loading");
		});
	}
	
	void Update () {
	
	}
}
