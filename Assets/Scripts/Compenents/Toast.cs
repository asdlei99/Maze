using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]

public class Toast : MonoBehaviour {

	void Start () {
		GetComponent<Image> ().color = new Color (0, 0, 0, 0.3f);
		Text text = transform.GetChild (0).gameObject.GetComponent<Text> () as Text;
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (text.preferredWidth + 20f, 40f);
		StartCoroutine (WaitToDestroy());
	}
	
	void Awake () {
		transform.localPosition = new Vector3 (0,  - Screen.height / 4);
	}

	public static void Show(Transform transform, string content){
		GameObject toastobj = new GameObject ();
		Toast toast = toastobj.AddComponent<Toast> () as Toast;
		toast.transform.SetParent (transform, false);
		toast.name = "Toast";

		GameObject textobj = new GameObject ();
		Text text = textobj.AddComponent<Text> () as Text;
		text.text = content;
		text.alignment = TextAnchor.MiddleCenter;
		text.fontSize = 26;
		text.font = Font.CreateDynamicFontFromOSFont ("Arial", 26);
		text.name = "ToastText";
		text.rectTransform.sizeDelta = new Vector2 (text.preferredWidth, 30);

		text.transform.SetParent (toast.transform, false);

	}

	private IEnumerator WaitToDestroy(){
		yield return new WaitForSeconds(1f);
		Destroy (this.gameObject);
	}
}
