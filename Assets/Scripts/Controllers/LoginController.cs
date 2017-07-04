using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour {

	[SerializeField]private Button loginBtn;
	[SerializeField]private Button wechatLoginBtn;

	void Awake(){
		PT_ThirdNoticer.InitThirdNoticer ();

		loginBtn.onClick.AddListener (delegate {
			SceneManager.LoadSceneAsync ("Main");
		});

		wechatLoginBtn.onClick.AddListener (delegate {
			PCommonInterface.SendLoginMessage(WechatLoginCallBack);
		});
	}

	void WechatLoginCallBack(string s){
		Debug.Log ("wjr------WechatLoginCallBack:"+s);
		SceneManager.LoadSceneAsync ("Main");
	}

	void Start () {
	
	}
	
	void Update () {
	
	}
}
