using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectLevelPanelController : PanelController {

	[SerializeField]private GameObject pass1;
	[SerializeField]private GameObject pass2;

	[SerializeField]private Button lastBtn;
	[SerializeField]private Button nextBtn;
	[SerializeField]private Button returnBtn;

	private const string LevelTitlePath = "SelectLevelPanel/";
	private const string Level1TitleName = "LevelTitle1";
	private const string Level2TitleName = "LevelTitle2";
	private const string Level3TitleName = "LevelTitle3";
	private const string Level4TitleName = "LevelTitle4";
	private const string Level5TitleName = "LevelTitle5";

	private Transform m_Transform;
	private int passIndex;

	private GameObject passObj;

	void Awake () {
		#if UNITY_EDITOR
		Player.Instance.maxLevel = 1;
		CurrentLevelMessage.Instance.Init ();
		#endif
		m_Transform = transform;
		passIndex = (Player.Instance.maxLevel - 1) / 5 + 1;

		CreatePass ();

		returnBtn.onClick.AddListener (delegate {
			SceneManager.LoadSceneAsync ("Main");
		});

		lastBtn.onClick.AddListener (delegate {
			ToLastPass();
		});

		nextBtn.onClick.AddListener (delegate {
			ToNextPass();
		});
	}

	void Start () {

	}
	
	void Update () {
	
	}

	void ToNextPass(){
		passIndex++;
		Destroy (passObj);
		CreatePass ();
	}

	void ToLastPass(){
		passIndex--;
		Destroy (passObj);
		CreatePass ();
	}

	void CreatePass(){
		if (passIndex == 1) {
			lastBtn.gameObject.SetActive (false);
		} else {
			lastBtn.gameObject.SetActive (true);
		}
		if (passIndex == LevelsMessage.allPassCount) {
			nextBtn.gameObject.SetActive (false);
		} else {
			nextBtn.gameObject.SetActive (true);
		}

		switch (passIndex) {
		case 1:
			passObj = Instantiate (pass1) as GameObject;
			break;
		case 2:
			passObj = Instantiate (pass2) as GameObject;
			break;
		default:
			passObj = Instantiate (pass1) as GameObject;
			break;
		}
		passObj.name = "Pass";
		passObj.transform.SetParent (m_Transform, false);
		passObj.transform.SetSiblingIndex (0);

		passObj.transform.FindChild (LevelTitlePath + Level1TitleName).gameObject.GetComponent<LevelTitleController> ().Init (m_Transform, (passIndex - 1) * 5 + 1);
		passObj.transform.FindChild (LevelTitlePath + Level2TitleName).gameObject.GetComponent<LevelTitleController> ().Init (m_Transform, (passIndex - 1) * 5 + 2);
		passObj.transform.FindChild (LevelTitlePath + Level3TitleName).gameObject.GetComponent<LevelTitleController> ().Init (m_Transform, (passIndex - 1) * 5 + 3);
		passObj.transform.FindChild (LevelTitlePath + Level4TitleName).gameObject.GetComponent<LevelTitleController> ().Init (m_Transform, (passIndex - 1) * 5 + 4);
		passObj.transform.FindChild (LevelTitlePath + Level5TitleName).gameObject.GetComponent<LevelTitleController> ().Init (m_Transform, (passIndex - 1) * 5 + 5);
	}

	public override void DialogConfirmBtnClicked(DialogHitType type){
		switch(type){
		case DialogHitType.FindArchive:
			SceneManager.LoadSceneAsync ("Loading");
			break;
		}
	}

	public override void DialogCancelBtnClicked(DialogHitType type){
		switch(type){
		case DialogHitType.FindArchive:
			CurrentLevelMessage.Instance.Reset ();
			SceneManager.LoadSceneAsync ("Loading");
			break;
		}
	}
}
