using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndPanelController : PanelController {

	void Start () {
		GameObject.Find ("EndBtn").GetComponent<Button> ().onClick.AddListener (delegate {
			SceneManager.LoadSceneAsync ("Main");
		});
	}
	
	void Update () {
	
	}
}
