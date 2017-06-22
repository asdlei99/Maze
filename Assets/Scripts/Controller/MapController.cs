using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	[SerializeField]private GameObject paintProjector;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void Paint(Transform transform){
		GameObject obj = Instantiate (paintProjector) as GameObject;
		obj.transform.position = transform.position + Vector3.up * 2f;
		obj.transform.rotation = transform.rotation;
	}
}