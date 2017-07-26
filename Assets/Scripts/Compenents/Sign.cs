using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour {

	[SerializeField]private GameObject dayText;
	[SerializeField]private GameObject content;
	[SerializeField]private GameObject countText;

	public delegate void SignCallback(bool isSuc);
	public SignCallback signCallback;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void Init(string day, string count, bool isSigned, SignCallback callback){
		dayText.GetComponent<Text> ().text = day;
		countText.GetComponent<Text> ().text = count;
		if (isSigned) {
			content.GetComponent<Image> ().color = Color.green;
		}
		signCallback = callback;
	}

	public void Signed(){
		content.GetComponent<Image> ().color = Color.green;
		if (signCallback != null) {
			signCallback (true);
		}
	}
}
