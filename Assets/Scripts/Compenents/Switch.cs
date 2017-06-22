using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Switch : MonoBehaviour {

	[SerializeField] private Button bg;
	[SerializeField] private Button btn;
	public bool isOpen = false;
	private Vector3 closePosition, openPosition;

	public delegate void SwitchValueChanged(bool value); 
	public SwitchValueChanged valueChanged;

	void Start(){
		closePosition = btn.transform.localPosition;
		openPosition = closePosition + Vector3.right * GetComponent<RectTransform> ().sizeDelta.x * 0.5f;

		bg.onClick.AddListener (delegate {
			SwicthClicked();
		});
		btn.onClick.AddListener (delegate {
			SwicthClicked();
		});
	}

	public void Open(){
		bg.GetComponent<Image> ().color = Color.green;
		btn.transform.localPosition = openPosition;
		isOpen = true;
	}

	public void Close(){
		bg.GetComponent<Image> ().color = Color.white;
		btn.transform.localPosition = closePosition;
		isOpen = false;
	}

	private void SwicthClicked(){
		if (isOpen) {
			Close ();
		} else {
			Open ();
		}
		if (valueChanged != null) {
			valueChanged (isOpen);
		}
	}
}
