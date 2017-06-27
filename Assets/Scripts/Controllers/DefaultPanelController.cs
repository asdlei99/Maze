using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefaultPanelController : PanelController {

	[SerializeField]private FirstPerson firstPerson;
	[SerializeField]private MapController mapController;
	[SerializeField]private PaintPanelController paintController;
	[SerializeField]private SettingPanelController settingController;
	[SerializeField]private EndPanelController endController;

	[SerializeField]private RockerController viewRocker;

	private const string PersonRockerPathName = "Canvas/DefaultPanel/PersonRocker/";
	private const string GoForwardBtnName = "GoForwardBtn";
	private const string GoBackBtnName = "GoBackBtn";
	private const string GoLeftBtnName = "GoLeftBtn";
	private const string GoRightBtnName = "GoRightBtn";

	private const string PaintBtnName = "PaintBtn";

	private const string BannerName = "Canvas/DefaultPanel/Banner/";
	private const string SettingBtnName = "SettingBtn";

	private bool isViewRocker = false;
	[HideInInspector]public static bool isCouldViewTurn;

	public delegate void GameEndDelegate();  

	public static GameEndDelegate gameEnd; 

	void Awake(){
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoForwardBtnName)).onDown = BtnOnDownListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoForwardBtnName)).onUp = BtnOnUpListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoBackBtnName)).onDown = BtnOnDownListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoBackBtnName)).onUp = BtnOnUpListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoLeftBtnName)).onDown = BtnOnDownListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoLeftBtnName)).onUp = BtnOnUpListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoRightBtnName)).onDown = BtnOnDownListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoRightBtnName)).onUp = BtnOnUpListener;

		if (!SettingInfo.Instance.isOpenViewRocker) {
			viewRocker.gameObject.SetActive (false);
		}

		GBEventListener.Get(GameObject.Find ("Canvas/DefaultPanel/" + PaintBtnName)).onClick = BtnOnClickListener;
		GBEventListener.Get(GameObject.Find (BannerName + SettingBtnName)).onClick = BtnOnClickListener;

		paintController.seletedPaint = CreatePaint;
		settingController.settingValueChanged = SettingValueChanged;

		viewRocker.rockerStart = ViewRockerStart;
		viewRocker.rockerEnd = ViewRockerEnd;

		gameEnd = GameEnd;

		isCouldViewTurn = true;
	}

	void Start () {
		firstPerson.transform.position = CurrentLevelMessage.Instance.bornPosition;
		GameObject.Find ("Canvas/DefaultPanel/Banner/LevelText").GetComponent<Text> ().text = CurrentLevelMessage.Instance.name;
		name = "Level" + CurrentLevelMessage.Instance.levelIndex;
	}
	
	void Update () {
		if(Input.GetKeyDown (KeyCode.W)){
			firstPerson.personMoveDirection = DirectionType.Forward;
		}
		if(Input.GetKeyUp (KeyCode.W)){
			firstPerson.personMoveDirection = DirectionType.None;
		}
		if(Input.GetKeyDown (KeyCode.A)){
			firstPerson.personMoveDirection = DirectionType.Left;
		}
		if(Input.GetKeyUp (KeyCode.A)){
			firstPerson.personMoveDirection = DirectionType.None;
		}
		if(Input.GetKeyDown (KeyCode.S)){
			firstPerson.personMoveDirection = DirectionType.Back;  
			return;
		}
		if(Input.GetKeyUp (KeyCode.S)){
			firstPerson.personMoveDirection = DirectionType.None;  
			return;
		}
		if(Input.GetKeyDown (KeyCode.D)){
			firstPerson.personMoveDirection = DirectionType.Right;
		}
		if(Input.GetKeyUp (KeyCode.D)){
			firstPerson.personMoveDirection = DirectionType.None;
		}

		if (isCouldViewTurn) {
			CheckViewRotate ();
		}
	}

	void CheckViewRotate(){
		if (viewRocker.gameObject.activeSelf) {
			if (isViewRocker) {
				firstPerson.RotateView (viewRocker.MovePosiNorm.x / 2.5f, viewRocker.MovePosiNorm.z / 2.5f);
			}
		} else {
			int touchIndex = 0;
			bool isNeedTurn = false;
			if (firstPerson.personMoveDirection != DirectionType.None && Input.touchCount > 1) {
				touchIndex = 1;
				isNeedTurn = true;
			} else if (Input.touchCount == 1) {
				isNeedTurn = true;
			}
			//手指在屏幕上移动，移动摄像机
			if (isNeedTurn && Input.touches [touchIndex].phase == TouchPhase.Moved) {
				firstPerson.RotateView (Input.touches [touchIndex].deltaPosition.x * Time.deltaTime, Input.touches [touchIndex].deltaPosition.y * Time.deltaTime);
			}
		}
	}

	void BtnOnDownListener(GameObject obj){
		if (obj.name == GoForwardBtnName) {
			firstPerson.personMoveDirection = DirectionType.Forward;
		}else if(obj.name == GoBackBtnName){
			firstPerson.personMoveDirection = DirectionType.Back;
		}else if(obj.name == GoLeftBtnName){
			firstPerson.personMoveDirection = DirectionType.Left;
		}else if(obj.name == GoRightBtnName){
			firstPerson.personMoveDirection = DirectionType.Right;
		}
	}

	void BtnOnUpListener(GameObject obj){
		if (obj.name == GoForwardBtnName || obj.name == GoBackBtnName || obj.name == GoLeftBtnName || obj.name == GoRightBtnName) {
			firstPerson.personMoveDirection = DirectionType.None;
		}
	}

	void BtnOnClickListener(GameObject obj){
		if (obj.name == PaintBtnName) {
			paintController.SetActive (true);
		}else if(obj.name == SettingBtnName){
			settingController.SetActive (true);
		}
	}

	void CreatePaint(PaintType type){
		mapController.Paint (firstPerson.m_chaTrans, type);
	}

	void SettingValueChanged(SettingType type, bool value){
		switch (type) {
		case SettingType.ViewRocker:
			if (value) {
				viewRocker.gameObject.SetActive (true);
			} else {
				viewRocker.gameObject.SetActive (false);
			}
			SettingInfo.Instance.isOpenViewRocker = value;
			break;
		case SettingType.Audio:
			SettingInfo.Instance.isOpenAudio = value;
			break;
		}
	}

	public void GameEnd(){
		endController.SetActive (true);
	}

	void ViewRockerStart(){
		isViewRocker = true;
	}

	void ViewRockerEnd(){
		isViewRocker = false;
	}

	void OnApplicationPause(){
		SettingInfo.Instance.Save ();
	}

	void OnApplicationQuit() {
	}
}
