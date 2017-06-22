using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public class CloseBtn : MonoBehaviour {

	void Start () {
		GetComponent<Button> ().onClick.AddListener (delegate {
			ClosePanel();
		});
	}
	
	void Update () {
	
	}

	void ClosePanel(){
		this.transform.parent.gameObject.GetComponent<PanelController> ().SetActive (false);
	}
}
