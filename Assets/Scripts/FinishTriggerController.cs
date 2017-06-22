using UnityEngine;
using System.Collections;

public class FinishTriggerController : MonoBehaviour {

	public delegate void PlayerEntered(); 
	public PlayerEntered playerEntered;

	void OnTriggerEnter(Collider collider){
		//进入触发器执行的代码
		if (collider.gameObject.name == "FirstPerson") {
			if(playerEntered != null){
				playerEntered ();
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		//进入碰撞器执行的代码
	}
}
