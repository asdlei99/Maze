using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PaintPanelController : PanelController, IPointerClickHandler {

	[SerializeField]private Button paint0;
	[SerializeField]private Button paint1;
	[SerializeField]private Button paint2;

	public delegate void SelectedPaint(PaintType type); 
	public SelectedPaint seletedPaint;

	void Start () {
		ButtonEventListener.Get(paint0.gameObject).onClick = BtnOnClickListener;
		ButtonEventListener.Get(paint1.gameObject).onClick = BtnOnClickListener;
		ButtonEventListener.Get(paint2.gameObject).onClick = BtnOnClickListener;
	}
	
	void Update () {
	
	}

	void BtnOnClickListener(GameObject obj){
		if (seletedPaint != null) {
			if (obj.name == paint0.name) {
				seletedPaint (PaintType.Paint0);
			} else if (obj.name == paint1.name) {
				seletedPaint (PaintType.Paint1);
			} else if (obj.name == paint2.name) {
				seletedPaint (PaintType.Paint2);
			}
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
