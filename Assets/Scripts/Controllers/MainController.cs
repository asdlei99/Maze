using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	void Start () {
		GameObject.Find ("StartBtn").GetComponent<Button> ().onClick.AddListener (delegate {
			Player.Instance.maxLevel = 1;
			SceneManager.LoadSceneAsync ("SelectLevel");
		});

		SettingInfo.Instance.Init();
		CurrentLevelMessage.Instance.Init ();
	}
	
	void Update () {
	
	}

	void OnApplicationPause(){
	}

	void OnApplicationQuit() {
	}
}
