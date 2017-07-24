using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTool : MonoBehaviour {

	public static AssetBundle DialogAB;
	public static Dictionary<string, AssetBundle> neededABDic = new Dictionary<string, AssetBundle>();
	private readonly static string DialogABPath = "prefabs/dialogs/dialogtwobtn" + AssetBundleConfig.suffix;

	public static void ShowTwoBtnDialog(Transform trans, string title, string content, DialogHitType type,
		DialogController.ConfirmBtnClicked confirmCallback = null, DialogTwoBtnController.CancelBtnClicked cancelCallback = null){
		if (DialogAB == null) {
			DialogAB = AssetBundleConfig.LoadAssetBundle (DialogABPath, neededABDic);
			if (DialogAB == null) {
				Debug.Log ("ShowDialog fail, DialogAB is null");
				return;
			}
		}
		Object dialog = DialogAB.LoadAllAssets() [0];
		if (dialog != null) {
			GameObject obj = Instantiate (dialog) as GameObject;
			obj.transform.SetParent (trans, false);
			DialogTwoBtnController dialogTwo = obj.GetComponent<DialogTwoBtnController> ();
			dialogTwo.InitContent(title, content, type);
			dialogTwo.confirmBtnClicked = confirmCallback;
			dialogTwo.cancelBtnClicked = cancelCallback;
		}
	}
}
