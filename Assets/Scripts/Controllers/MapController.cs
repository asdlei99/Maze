using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {
	[SerializeField]private GameObject paintProjector;

	[SerializeField]private GameObject level1;
	[SerializeField]private GameObject level2;

	void Awake () {
		if (CurrentLevelMessage.Instance.levelIndex == 0) {
			//第一次进入
			CurrentLevelMessage.Instance.Init ();
		} else {
			//过关进入
			CurrentLevelMessage.Instance.Reset ();
		}
		GameObject MapObj;
		switch (CurrentLevelMessage.Instance.levelIndex) {
//		case 1:
//			MapObj = Instantiate (level1) as GameObject;
//			break;
		case 2:
			MapObj = Instantiate (level2) as GameObject;
			CurrentLevelMessage.Instance.name = LevelsMessage.Level2Name;
			if (CurrentLevelMessage.Instance.bornPosition.x == MazeTool.errorFloat) {
				CurrentLevelMessage.Instance.bornPosition = LevelsMessage.Level2BornPosition;
			}
			if (CurrentLevelMessage.Instance.bodyRotation.x == MazeTool.errorFloat) {
				CurrentLevelMessage.Instance.bodyRotation = LevelsMessage.Level2BodyRotation;
			}
			break;
		default:
			MapObj = Instantiate (level1) as GameObject;
			CurrentLevelMessage.Instance.name = LevelsMessage.Level1Name;
			if (CurrentLevelMessage.Instance.bornPosition.x == MazeTool.errorFloat) {
				CurrentLevelMessage.Instance.bornPosition = LevelsMessage.Level1BornPosition;
			}
			if (CurrentLevelMessage.Instance.bodyRotation.x == MazeTool.errorFloat) {
				CurrentLevelMessage.Instance.bodyRotation = LevelsMessage.Level1BodyRotation;
			}
			CurrentLevelMessage.Instance.levelIndex = 1;
			break;
		}
		MapObj.transform.SetParent (this.transform, false);
	}

	void Start(){
		for (int i = 0; i < CurrentLevelMessage.Instance.projectorMessageList.Count; i++) {
			CurrentLevelMessage.ProjectorMessage pm = CurrentLevelMessage.Instance.projectorMessageList [i];
			Paint (pm.position, pm.rotation, pm.type);
		}
	}
	
	void Update () {
	
	}

	public void Paint(Vector3 position, Quaternion rotation, PaintType type){
		GameObject obj = Instantiate (paintProjector) as GameObject;
		obj.transform.SetParent (this.transform, false);
		obj.GetComponent<PaintProjectorController> ().Init (position, rotation, type);
	}
}