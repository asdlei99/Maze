using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogController : PanelController{

	protected string title = "提示";
	protected string content = "";
//	protected string confirmBtnText = "确定";
	protected DialogHitType hitType;

	[SerializeField]protected Text titleText;
	[SerializeField]protected Text contentText;
	[SerializeField]protected Button confirmBtn;

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
		titleText.text = title;
		contentText.text = content;

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
