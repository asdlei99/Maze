using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogTwoBtnController : DialogController {

	public delegate void CancelBtnClicked(DialogHitType type); 
	public CancelBtnClicked cancelBtnClicked;

//	private const string cancelBtnPath = dialogPath + "/BtnPanel/CancelBtn";
	[SerializeField]private Button cancelBtn;

	void Start () {
		Init ();
		cancelBtn.onClick.AddListener (delegate {
			if(cancelBtnClicked != null){
				cancelBtnClicked(hitType);
			}
			Destroy();
		});
	}

//	public void InitContent(string title, string content, DialogHitType hitType, string confirmBtnText, string cancelBtnText){
//		InitContent (title, content, hitType);
//
//	}
}
