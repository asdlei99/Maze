﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogController : PanelController{

	protected string title = "提示";
	protected string content = "";
//	protected string confirmBtnText = "确定";
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
		this.transform.Find (titleTextPath).gameObject.GetComponent<Text>().text = title;
		this.transform.Find (contentTextPath).gameObject.GetComponent<Text>().text = content;

		Button confirmBtn = this.transform.Find (confirmBtnPath).gameObject.GetComponent<Button> ();
		confirmBtn.onClick.AddListener (delegate {
			if(confirmBtnClicked != null){
				confirmBtnClicked(hitType);
			}
			Destroy();
		});
	}

	protected void Destroy(){
		Destroy (this.gameObject);
	}
}
