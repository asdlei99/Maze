using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogTwoBtnController : DialogController {

	public delegate void CancelBtnClicked(DialogHitType type); 
	public CancelBtnClicked cancelBtnClicked;

	private const string cancelBtnPath = dialogPath + "/BtnPanel/CancelBtn";

	void Start () {
		Init ();
		this.transform.Find (cancelBtnPath).gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
			SetActive(false);
			if(cancelBtnClicked != null){
				cancelBtnClicked(hitType);
			}
		});
	}

//	public void InitContent(string title, string content, DialogHitType hitType, string confirmBtnText, string cancelBtnText){
//		InitContent (title, content, hitType);
//
//	}
}
