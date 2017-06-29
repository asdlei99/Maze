using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogTwoBtnController : DialogController {

	public delegate void CancelBtnClicked(); 
	public CancelBtnClicked cancelBtnClicked;

	private const string cancelBtnPath = dialogPath + "/BtnPanel/CancelBtn";

	void Start () {
		Init ();
		this.transform.FindChild (cancelBtnPath).gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
			SetActive(false);
			if(cancelBtnClicked != null){
				cancelBtnClicked();
			}
		});
	}
}
