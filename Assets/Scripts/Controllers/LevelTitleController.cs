using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelTitleController : MonoBehaviour, IPointerClickHandler {

	public int level;
	public Transform parentTransform;

	void Start(){
		Image image = transform.Find ("Image").gameObject.GetComponent<Image> () as Image;
		if (level > Player.Instance.maxLevel) {
			image.color = new Color (0.8f, 0.8f, 0.8f);
		}
	}

	public void Init(Transform transform, int level){
		this.level = level;
		parentTransform = transform;
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.clickCount == 1) {
			if (level > Player.Instance.maxLevel) {
				Toast.Show (parentTransform, "该关卡尚未开启，请先通关前面关卡");
			} else {
				if (level == CurrentLevelMessage.Instance.levelIndex) {
					parentTransform.gameObject.GetComponent<SelectLevelPanelController> ().ShowDialog ("提示", "发现存档，是否从存档处继续游戏？",DialogHitType.FindArchive);
				} else {
					CurrentLevelMessage.Instance.levelIndex = level;
					CurrentLevelMessage.Instance.Reset ();
					SceneManager.LoadSceneAsync ("Loading");
				}
			}
		}
	}
}
