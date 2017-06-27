using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {
	[SerializeField]private GameObject paintProjector;

	[SerializeField]private GameObject level1;
	[SerializeField]private GameObject level2;

	void Awake () {
		GameObject MapObj;
		switch (CurrentLevelMessage.Instance.levelIndex) {
//		case 1:
//			MapObj = Instantiate (level1) as GameObject;
//			break;
		case 2:
			MapObj = Instantiate (level2) as GameObject;
			CurrentLevelMessage.Instance.name = LevelsMessage.Level2Name;
			CurrentLevelMessage.Instance.bornPosition = LevelsMessage.Level2BornPosition;
			break;
		default:
			MapObj = Instantiate (level1) as GameObject;
			CurrentLevelMessage.Instance.name = LevelsMessage.Level1Name;
			CurrentLevelMessage.Instance.bornPosition = LevelsMessage.Level1BornPosition;
			CurrentLevelMessage.Instance.levelIndex = 1;
			break;
		}
		MapObj.transform.SetParent (this.transform, false);
	}
	
	void Update () {
	
	}

	public void Paint(Transform transform, PaintType type){
		GameObject obj = Instantiate (paintProjector) as GameObject;
		obj.transform.SetParent (this.transform, false);
		obj.GetComponent<PaintProjectorController> ().Init (transform, type);
	}
}