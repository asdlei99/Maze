using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PaintProjectorController : MonoBehaviour {

	private float nearDistance, maxDistance = 9f;
	private PaintType type;
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

		CurrentLevelMessage.ProjectorMessage pm = new CurrentLevelMessage.ProjectorMessage ();
		pm.position = transform.position;
		pm.rotation = transform.rotation;
		pm.type = type;
		CurrentLevelMessage.Instance.projectorMessageList.Add (pm);
	}

	public void Init(Vector3 position, Quaternion rotation, PaintType type){
		this.transform.position = position + Vector3.up * 2f;
		this.transform.rotation = rotation;
		this.type = type;
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