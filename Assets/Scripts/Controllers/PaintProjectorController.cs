using UnityEngine;
using System.Collections;

public class PaintProjectorController : MonoBehaviour {

	private float nearDistance, maxDistance = 9f;

	void Start () {
		nearDistance = GetComponent<Projector> ().nearClipPlane;

		Ray mRay = new Ray (transform.position + transform.forward.normalized * nearDistance, transform.forward);
		RaycastHit mHi;
		//判断是否击中了什么
		if(Physics.Raycast(mRay,out mHi)){
			float dist = mHi.distance + nearDistance;
			if (dist <= maxDistance) {
				GetComponent<Projector> ().farClipPlane = dist + 1;
			} else {
				this.transform.rotation *= Quaternion.Euler (20f, 0f, 0f);
				GetComponent<Projector> ().farClipPlane = maxDistance;
			}
		}
	}
}