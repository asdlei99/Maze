using UnityEngine;
using System.Collections;

public class MainPanelController : PanelController {

	[SerializeField]private GameObject ShareBtn;
	[SerializeField]private GameObject MarketBtn;
	[SerializeField]private GameObject SignBtn;
	[SerializeField]private SignPanelController SignPanel;

	void Awake(){
		GBEventListener.Get(ShareBtn).onClick = ShowSharePanel;
		GBEventListener.Get(MarketBtn).onClick = ShowMarketPanel;
		GBEventListener.Get(SignBtn).onClick = ShowSignPanel;
	}

	void Start () {
	
	}
	
	void Update () {
	
	}

	void ShowSharePanel(GameObject obj){
		
	}

	void ShowMarketPanel(GameObject obj){
		DialogTool.ShowOneBtnDialog (this.transform, "a", "b", DialogHitType.Exit, test);
//		DialogTool.ShowTwoBtnDialog (this.transform, "a", "b", DialogHitType.Exit, null, test);
	}

	void ShowSignPanel(GameObject obj){
		SignPanel.SetActive (true);
	}

	void test(DialogHitType type){
		Debug.Log ("test--"+type);
	}
}
