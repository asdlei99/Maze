using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefaultPanelController : PanelController {

	[SerializeField]private FirstPerson firstPerson;
	[SerializeField]private MapController mapController;
	[SerializeField]private PaintPanelController paintController;
	[SerializeField]private SettingPanelController settingController;
	[SerializeField]private EndPanelController endController;

	[SerializeField]private GameObject viewRocker;
	[SerializeField]private FinishTriggerController finishTriggerController;

	private const string PersonRockerPathName = "Canvas/DefaultPanel/PersonRocker/";
	private const string GoForwardBtnName = "GoForwardBtn";
	private const string GoBackBtnName = "GoBackBtn";
	private const string GoLeftBtnName = "GoLeftBtn";
	private const string GoRightBtnName = "GoRightBtn";

	private const string ViewRockerPathName = "Canvas/DefaultPanel/ViewRocker/";
	private const string LeftBtnName = "LeftBtn";
	private const string RightBtnName = "RightBtn";
	private const string UpBtnName = "UpBtn";
	private const string DownBtnName = "DownBtn";

	private const string PaintBtnName = "PaintBtn";

	private const string BannerName = "Canvas/DefaultPanel/Banner/";
	private const string SettingBtnName = "SettingBtn";

	void Awake(){
		ButtonEventListener.Get(GameObject.Find (PersonRockerPathName + GoForwardBtnName)).onDown = BtnOnDownListener;
		ButtonEventListener.Get(GameObject.Find (PersonRockerPathName + GoForwardBtnName)).onUp = BtnOnUpListener;
		ButtonEventListener.Get(GameObject.Find (PersonRockerPathName + GoBackBtnName)).onDown = BtnOnDownListener;
		ButtonEventListener.Get(GameObject.Find (PersonRockerPathName + GoBackBtnName)).onUp = BtnOnUpListener;
		ButtonEventListener.Get(GameObject.Find (PersonRockerPathName + GoLeftBtnName)).onDown = BtnOnDownListener;
		ButtonEventListener.Get(GameObject.Find (PersonRockerPathName + GoLeftBtnName)).onUp = BtnOnUpListener;
		ButtonEventListener.Get(GameObject.Find (PersonRockerPathName + GoRightBtnName)).onDown = BtnOnDownListener;
		ButtonEventListener.Get(GameObject.Find (PersonRockerPathName + GoRightBtnName)).onUp = BtnOnUpListener;

		ButtonEventListener.Get(GameObject.Find (ViewRockerPathName + LeftBtnName)).onDown = BtnOnDownListener;
		ButtonEventListener.Get(GameObject.Find (ViewRockerPathName + LeftBtnName)).onUp = BtnOnUpListener;
		ButtonEventListener.Get(GameObject.Find (ViewRockerPathName + RightBtnName)).onDown = BtnOnDownListener;
		ButtonEventListener.Get(GameObject.Find (ViewRockerPathName + RightBtnName)).onUp = BtnOnUpListener;
		ButtonEventListener.Get(GameObject.Find (ViewRockerPathName + UpBtnName)).onDown = BtnOnDownListener;
		ButtonEventListener.Get(GameObject.Find (ViewRockerPathName + UpBtnName)).onUp = BtnOnUpListener;
		ButtonEventListener.Get(GameObject.Find (ViewRockerPathName + DownBtnName)).onDown = BtnOnDownListener;
		ButtonEventListener.Get(GameObject.Find (ViewRockerPathName + DownBtnName)).onUp = BtnOnUpListener;
		viewRocker.SetActive (false);

		ButtonEventListener.Get(GameObject.Find ("Canvas/DefaultPanel/" + PaintBtnName)).onClick = BtnOnClickListener;
		ButtonEventListener.Get(GameObject.Find (BannerName + SettingBtnName)).onClick = BtnOnClickListener;

		paintController.seletedPaint = CreatePaint;
		settingController.settingValueChanged = SettingValueChanged;
		finishTriggerController.playerEntered = GameEnd;
	}

	void Start () {
	
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
		}else if(obj.name == LeftBtnName){
			firstPerson.viewTurnDirection = DirectionType.Left;
		}else if(obj.name == RightBtnName){
			firstPerson.viewTurnDirection = DirectionType.Right;
		}else if(obj.name == UpBtnName){
			firstPerson.viewTurnDirection = DirectionType.Up;
		}else if(obj.name == DownBtnName){
			firstPerson.viewTurnDirection = DirectionType.Down;
		}
	}

	void BtnOnUpListener(GameObject obj){
		if (obj.name == GoForwardBtnName || obj.name == GoBackBtnName || obj.name == GoLeftBtnName || obj.name == GoRightBtnName) {
			firstPerson.personMoveDirection = DirectionType.None;
		}else if (obj.name == LeftBtnName || obj.name == RightBtnName || obj.name == UpBtnName || obj.name == DownBtnName) {
			firstPerson.viewTurnDirection = DirectionType.None;
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
		mapController.Paint (firstPerson.m_chaTrans);
	}

	void SettingValueChanged(SettingType type, bool value){
		switch (type) {
		case SettingType.ViewRocker:
			if (value) {
				viewRocker.SetActive (true);
				firstPerson.isUseViewRocker = true;
			} else {
				viewRocker.SetActive (false);
				firstPerson.isUseViewRocker = false;
			}
			break;
		}
	}

	void GameEnd(){
		endController.SetActive (true);
	}
}
