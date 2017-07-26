using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignPanelController : PanelController {

	[SerializeField]private Sign[] signObjArr;
	[SerializeField]private Button signBtn;
	private string[] dayTextArr = new string[]{ "第一天", "第二天", "第三天", "第四天", "第五天", "第六天", "第七天"};
	private int[] countArr = new int[]{ 100, 200, 400, 700, 1000, 1500, 2000};
	private int currentDay;//当前签到的天数

	void Awake () {
		currentDay = 2;
		for (int i = 0; i < signObjArr.Length; i++) {
			bool isSige = false;
			if (i <= currentDay) {
				isSige = true;
			}
			signObjArr [i].Init (dayTextArr[i], countArr[i].ToString(), isSige, SignCallback);	
		}
		signBtn.onClick.AddListener (delegate() {
			SignBtnClicked();
		});
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	private void SignBtnClicked () {
		signObjArr [++currentDay].Signed ();
	}

	private void SignCallback (bool isSuc) {
		if (isSuc) {
			DialogTool.ShowOneBtnDialog (this.transform, "提示", "恭喜您，奖励领取成功", DialogHitType.None, null);
			signBtn.interactable = false;
		}
	}
}
