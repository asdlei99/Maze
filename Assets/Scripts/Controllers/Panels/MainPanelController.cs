using UnityEngine;
using System.Collections;

public class MainPanelController : PanelController {

	[SerializeField]private GameObject ShareBtn;

	void Awake(){
		GBEventListener.Get(ShareBtn).onClick = ShowSharePanel;
	}

	void Start () {
	
	}
	
	void Update () {
	
	}

	void ShowSharePanel(GameObject obj){
		
	}
}
