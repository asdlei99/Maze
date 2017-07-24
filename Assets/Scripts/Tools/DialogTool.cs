using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTool : MonoBehaviour {

	public static AssetBundle DialogTwoAB;
	public static AssetBundle DialogOneAB;
	public static Dictionary<string, AssetBundle> neededABDic = new Dictionary<string, AssetBundle>();
	private readonly static string DialogTwoABPath = "prefabs/dialogs/dialogtwobtn" + AssetBundleConfig.suffix;
	private readonly static string DialogOneABPath = "prefabs/dialogs/dialogonebtn" + AssetBundleConfig.suffix;

	public static void ShowTwoBtnDialog(Transform trans, string title, string content, DialogHitType type,
		DialogController.ConfirmBtnClicked confirmCallback = null, DialogTwoBtnController.CancelBtnClicked cancelCallback = null){
		if (DialogTwoAB == null) {
			DialogTwoAB = AssetBundleConfig.LoadAssetBundle (DialogTwoABPath, neededABDic);
			if (DialogTwoAB == null) {
				Debug.Log ("ShowTwoBtnDialog fail, DialogAB is null");
				return;
			}
		}
		Object dialog = DialogTwoAB.LoadAllAssets() [0];
		if (dialog != null) {
			GameObject obj = Instantiate (dialog) as GameObject;
			obj.transform.SetParent (trans, false);
			DialogTwoBtnController dialogTwo = obj.GetComponent<DialogTwoBtnController> ();
			dialogTwo.InitContent(title, content, type);
			dialogTwo.confirmBtnClicked = confirmCallback;
			dialogTwo.cancelBtnClicked = cancelCallback;
		}
	}

	public static void ShowOneBtnDialog(Transform trans, string title, string content, DialogHitType type,
		DialogController.ConfirmBtnClicked confirmCallback = null){
		if (DialogOneAB == null) {
			DialogOneAB = AssetBundleConfig.LoadAssetBundle (DialogOneABPath, neededABDic);
			if (DialogOneAB == null) {
				Debug.Log ("ShowOneBtnDialog fail, DialogAB is null");
				return;
			}
		}
		Object dialog = DialogOneAB.LoadAllAssets() [0];
		if (dialog != null) {
			GameObject obj = Instantiate (dialog) as GameObject;
			obj.transform.SetParent (trans, false);
			DialogController dialogTwo = obj.GetComponent<DialogController> ();
			dialogTwo.InitContent(title, content, type);
			dialogTwo.confirmBtnClicked = confirmCallback;
		}
	}
}
