using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogController : PanelController{

	protected string title = "提示";
	protected string content = "";
	protected DialogHitType hitType;

	protected const string dialogPath = "Panel";
	protected const string titleTextPath = dialogPath + "/TitleText";
	protected const string contentTextPath = dialogPath + "/ContentText";
	protected const string confirmBtnPath = dialogPath + "/BtnPanel/ConfirmBtn";

	public delegate void ConfirmBtnClicked(DialogHitType hitType); 
	public ConfirmBtnClicked confirmBtnClicked;

	void Awake () {
		
	}

	void Start(){
		Init ();
	}

	public void InitContent(string title, string content, DialogHitType hitType){
		this.title = title;
		this.content = content;
		this.hitType = hitType;
	}

	protected void Init(){
		this.transform.FindChild (titleTextPath).gameObject.GetComponent<Text>().text = title;
		this.transform.FindChild (contentTextPath).gameObject.GetComponent<Text>().text = content;
		this.transform.FindChild (confirmBtnPath).gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
			SetActive(false);
			if(confirmBtnClicked != null){
				confirmBtnClicked(hitType);
			}
		});
	}
}
