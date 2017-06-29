using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefaultPanelController : PanelController {

	[SerializeField]private FirstPerson firstPerson;
	[SerializeField]private MapController mapController;
	[SerializeField]private PaintPanelController paintController;
	[SerializeField]private SettingPanelController settingController;
	[SerializeField]private EndPanelController endController;
	[SerializeField]private RockerController viewRocker;

	[HideInInspector]public static bool isCouldViewTurn;
	[HideInInspector]public delegate void GameEndDelegate();
	[HideInInspector]public static GameEndDelegate gameEnd;

	private const string PersonRockerPathName = "Canvas/DefaultPanel/PersonRocker/";
	private const string GoForwardBtnName = "GoForwardBtn";
	private const string GoBackBtnName = "GoBackBtn";
	private const string GoLeftBtnName = "GoLeftBtn";
	private const string GoRightBtnName = "GoRightBtn";

	private const string PaintBtnName = "PaintBtn";

	private const string BannerName = "Canvas/DefaultPanel/Banner/";
	private const string SettingBtnName = "SettingBtn";
	private const string ExitBtnName = "ExitBtn";

	private bool isViewRocker = false;

	private DirectionType personMoveDirection;// 人物行走的方向
	private bool IsCanMove;

	void Awake(){
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoForwardBtnName)).onDown = BtnOnDownListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoForwardBtnName)).onUp = BtnOnUpListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoBackBtnName)).onDown = BtnOnDownListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoBackBtnName)).onUp = BtnOnUpListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoLeftBtnName)).onDown = BtnOnDownListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoLeftBtnName)).onUp = BtnOnUpListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoRightBtnName)).onDown = BtnOnDownListener;
		GBEventListener.Get(GameObject.Find (PersonRockerPathName + GoRightBtnName)).onUp = BtnOnUpListener;

		#if UNITY_EDITOR
		SettingInfo.Instance.Init ();
		#endif

		GBEventListener.Get(GameObject.Find ("Canvas/DefaultPanel/" + PaintBtnName)).onClick = BtnOnClickListener;
		GBEventListener.Get(GameObject.Find (BannerName + SettingBtnName)).onClick = BtnOnClickListener;
		GBEventListener.Get(GameObject.Find (BannerName + ExitBtnName)).onClick = BtnOnClickListener;

		paintController.seletedPaint = CreatePaint;
		settingController.settingValueChanged = SettingValueChanged;

		viewRocker.rockerStart = ViewRockerStart;
		viewRocker.rockerEnd = ViewRockerEnd;

		gameEnd = GameEnd;

		isCouldViewTurn = true;
		IsCanMove = true;
		personMoveDirection = DirectionType.None;
	}

	void Start () {
		GameObject.Find ("Canvas/DefaultPanel/Banner/LevelText").GetComponent<Text> ().text = CurrentLevelMessage.Instance.name;

		if (!SettingInfo.Instance.isOpenViewRocker) {
			viewRocker.gameObject.SetActive (false);
		}
	}
	
	void Update () {
		if(Input.GetKeyDown (KeyCode.W)){
			personMoveDirection = DirectionType.Forward;
		}
		if(Input.GetKeyUp (KeyCode.W)){
			personMoveDirection = DirectionType.None;
		}
		if(Input.GetKeyDown (KeyCode.A)){
			personMoveDirection = DirectionType.Left;
		}
		if(Input.GetKeyUp (KeyCode.A)){
			personMoveDirection = DirectionType.None;
		}
		if(Input.GetKeyDown (KeyCode.S)){
			personMoveDirection = DirectionType.Back;  
		}
		if(Input.GetKeyUp (KeyCode.S)){
			personMoveDirection = DirectionType.None;  
		}
		if(Input.GetKeyDown (KeyCode.D)){
			personMoveDirection = DirectionType.Right;
		}
		if(Input.GetKeyUp (KeyCode.D)){
			personMoveDirection = DirectionType.None;
		}

		if (isCouldViewTurn) {
			CheckViewRotate ();
		}
	}

	private void FixedUpdate() {
		if (IsCanMove && personMoveDirection != DirectionType.None) {
			firstPerson.DoMove (personMoveDirection);
		} else {
			firstPerson.MoveStop ();
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
			if (personMoveDirection != DirectionType.None && Input.touchCount > 1) {
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
			personMoveDirection = DirectionType.Forward;
		}else if(obj.name == GoBackBtnName){
			personMoveDirection = DirectionType.Back;
		}else if(obj.name == GoLeftBtnName){
			personMoveDirection = DirectionType.Left;
		}else if(obj.name == GoRightBtnName){
			personMoveDirection = DirectionType.Right;
		}
	}

	void BtnOnUpListener(GameObject obj){
		if (obj.name == GoForwardBtnName || obj.name == GoBackBtnName || obj.name == GoLeftBtnName || obj.name == GoRightBtnName) {
			personMoveDirection = DirectionType.None;
		}
	}

	void BtnOnClickListener(GameObject obj){
		if (obj.name == PaintBtnName) {
			paintController.SetActive (true);
		}else if(obj.name == SettingBtnName){
			settingController.SetActive (true);
		}else if(obj.name == ExitBtnName){
			ShowDialog ("提asd示", "sadsadas", DialogHitType.Exit);
		}
	}

	void CreatePaint(PaintType type){
		mapController.Paint (firstPerson.m_chaTrans.position, firstPerson.m_chaTrans.rotation, type);
		CurrentLevelMessage.ProjectorMessage pm = new CurrentLevelMessage.ProjectorMessage ();
		pm.position = firstPerson.m_chaTrans.position;
		pm.rotation = firstPerson.m_chaTrans.rotation;
		pm.type = type;
		CurrentLevelMessage.Instance.projectorMessageList.Add (pm);
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
		IsCanMove = false;
		endController.SetActive (true);
	}

	void ViewRockerStart(){
		isViewRocker = true;
	}

	void ViewRockerEnd(){
		isViewRocker = false;
	}

	void SaveMapMessage(){
		CurrentLevelMessage.Instance.bodyRotation = firstPerson.m_chaTrans.rotation;
		CurrentLevelMessage.Instance.headRotation = firstPerson.m_camTrans.localRotation;
		CurrentLevelMessage.Instance.bornPosition = firstPerson.m_chaTrans.position;
		CurrentLevelMessage.Instance.Save ();
	}

	public override void DialogConfirmBtnClicked(DialogHitType type){
		switch(type){
		case DialogHitType.Exit:
			SaveMapMessage ();
			SceneManager.LoadScene ("Main");
			break;
		}
	}

	void OnApplicationPause(){
		#if !UNITY_EDITOR
		SaveMapMessage ();
		#endif
	}

	void OnApplicationQuit() {
		SaveMapMessage ();
	}
}
