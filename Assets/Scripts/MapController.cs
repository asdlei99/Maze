using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	[SerializeField]private GameObject paintProjector;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void CreateSign(Transform transform){
		GameObject obj = Instantiate (paintProjector) as GameObject;
		obj.transform.position = transform.position + Vector3.up * 2.5f + transform.rotation * Vector3.forward * 2;
		obj.transform.rotation = transform.rotation * Quaternion.Euler(15f, 0f, 0f);
	}
}