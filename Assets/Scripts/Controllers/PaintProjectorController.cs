using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PaintProjectorController : MonoBehaviour {

	private float nearDistance, maxDistance = 9f;
	[SerializeField]private Material kang;
	[SerializeField]private Material sbf;

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

	public void Init(Transform transform, PaintType type){
		this.transform.position = transform.position + Vector3.up * 2f;
		this.transform.rotation = transform.rotation;
		switch (type) {
		case PaintType.Paint0:
			GetComponent<Projector> ().material = sbf;
			break;
		case PaintType.Paint1:
			GetComponent<Projector> ().material = kang;
			break;
		}
	}
}