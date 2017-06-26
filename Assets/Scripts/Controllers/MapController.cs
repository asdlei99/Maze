using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	[SerializeField]private GameObject paintProjector;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void Paint(Transform transform, PaintType type){
		GameObject obj = Instantiate (paintProjector) as GameObject;
		obj.GetComponent<PaintProjectorController> ().Init (transform, type);
	}
}