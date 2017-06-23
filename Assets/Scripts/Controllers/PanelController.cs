using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour {

	public void SetActive(bool isActive){
		this.gameObject.SetActive (isActive);
		if (isActive) {
			FirstPerson.isCouldViewTurn = false;
		} else {
			FirstPerson.isCouldViewTurn = true;
		}
	}
}
