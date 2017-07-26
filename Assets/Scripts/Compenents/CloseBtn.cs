using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public class CloseBtn : MonoBehaviour {

	[SerializeField]private GameObject parent;

	void Start () {
		GetComponent<Button> ().onClick.AddListener (delegate {
			ClosePanel();
		});
	}
	
	void Update () {
	
	}

	void ClosePanel(){
		parent.SetActive (false);
	}
}
