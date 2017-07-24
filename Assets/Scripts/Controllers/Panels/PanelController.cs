using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour {

	protected DialogTwoBtnController dialogTwoBtn;

	public void SetActive(bool isActive){
		this.gameObject.SetActive (isActive);
		if (isActive) {
			DefaultPanelController.isCouldViewTurn = false;
		} else {
			DefaultPanelController.isCouldViewTurn = true;

			if ("SettingPanel".Equals (name)) {
				SettingInfo.Instance.Save ();
			}
		}
	}

	public virtual void DialogConfirmBtnClicked(DialogHitType type){
	}

	public virtual void DialogCancelBtnClicked(DialogHitType type){
	}
}
