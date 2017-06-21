using UnityEngine;
using System.Collections;

public class PaintProjectorController : MonoBehaviour {

	private float nearDistance, farDistance, distanceTolerance = 0.5f;

	void Start () {
		nearDistance = GetComponent<Projector> ().nearClipPlane;
		farDistance = GetComponent<Projector> ().farClipPlane;

		Ray mRay = new Ray (transform.position + transform.forward.normalized * nearDistance, transform.forward);
		RaycastHit mHi;
		//判断是否击中了什么
		if(Physics.Raycast(mRay,out mHi)){
			float dist = mHi.distance + nearDistance;
			GetComponent<Projector> ().nearClipPlane =  Mathf.Max(dist - distanceTolerance, 0);
			GetComponent<Projector> ().farClipPlane = dist + distanceTolerance;
		}
	}
}