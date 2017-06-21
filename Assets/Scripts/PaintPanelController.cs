using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PaintPanelController : MonoBehaviour, IPointerClickHandler {

	[SerializeField]private Button paint0;
	[SerializeField]private Button paint1;
	[SerializeField]private Button paint2;

	public delegate void SelectedPaintCallBack(PaintType type); 
	public SelectedPaintCallBack seletPaintCallBack;

	void Start () {
		ButtonEventListener.Get(paint0.gameObject).onClick = BtnOnClickListener;
		ButtonEventListener.Get(paint1.gameObject).onClick = BtnOnClickListener;
		ButtonEventListener.Get(paint2.gameObject).onClick = BtnOnClickListener;
	}
	
	void Update () {
	
	}

	public void SetActive(bool isActive){
		this.gameObject.SetActive (isActive);
	}

	void BtnOnClickListener(GameObject obj){
		if (obj.name == paint0.name) {
			seletPaintCallBack (PaintType.Paint0);
		} else if (obj.name == paint1.name) {
			seletPaintCallBack (PaintType.Paint1);
		} else if (obj.name == paint2.name) {
			seletPaintCallBack (PaintType.Paint2);
		}
		SetActive (false);
	}

	public void OnPointerClick(PointerEventData eventData) {
		//双击，将选中的牌缩回
		if(eventData.clickCount == 1) {
			SetActive (false);
		}
	}
}
