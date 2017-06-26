using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour {

	public void SetActive(bool isActive){
		this.gameObject.SetActive (isActive);
		if (isActive) {
			DefaultPanelController.isCouldViewTurn = false;
		} else {
			DefaultPanelController.isCouldViewTurn = true;
		}
	}
}
