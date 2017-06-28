using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	void Start () {
		GameObject.Find ("StartBtn").GetComponent<Button> ().onClick.AddListener (delegate {
			SceneManager.LoadSceneAsync ("Loading");
		});

		SettingInfo.Instance.Init();
	}
	
	void Update () {
	
	}

	void OnApplicationPause(){
	}

	void OnApplicationQuit() {
	}
}
