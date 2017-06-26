using UnityEngine;
using System.Collections;

public class RockerController : MonoBehaviour {
	
	private Vector3 Origin;
	private Vector3 _deltaPos;
	private bool _drag = false;
	private Vector3 deltaPosition;
	float dis; 

	[SerializeField]private GameObject rockerBtn;
	private Transform rockerBtnTrans;
	[SerializeField] private float MoveMaxDistance = 80; //最大拖动距离
	[HideInInspector] public Vector3 FiexdMovePosiNorm; //固定8个角度移动的距离
	[HideInInspector] public Vector3 MovePosiNorm; //标准化移动的距离
	[SerializeField] private float ActiveMoveDistance = 1; //激活移动的最低距离

	public delegate void RockerStart(); 
	public RockerStart rockerStart;
	public delegate void RockerEnd(); 
	public RockerEnd rockerEnd;

	void Awake() {
		GBEventListener.Get(rockerBtn).onDrag = OnDrag;
		GBEventListener.Get(rockerBtn).onDragOut = OnDragOut;
		GBEventListener.Get(rockerBtn).onDown = OnMoveStart;
	}

	void Start () {
		rockerBtnTrans = rockerBtn.transform;
		Origin = rockerBtnTrans.localPosition;//设置原点
	}

	void Update() {
		dis = Vector3.Distance (rockerBtnTrans.localPosition, Origin);//拖动距离，这不是最大的拖动距离，是根据触摸位置算出来的
		//如果大于可拖动的最大距离
		if (dis >= MoveMaxDistance) {
			Vector3 vec = Origin + (rockerBtnTrans.localPosition - Origin) * MoveMaxDistance / dis;  //求圆上的一点：(目标点-原点) * 半径/原点到目标点的距离
			rockerBtnTrans.localPosition = vec;
		}

		//距离大于激活移动的距离
		if (Vector3.Distance (rockerBtnTrans.localPosition, Origin) > ActiveMoveDistance) {
			MovePosiNorm = (rockerBtnTrans.localPosition - Origin).normalized;
			MovePosiNorm = new Vector3 (MovePosiNorm.x, 0, MovePosiNorm.y);  
		
		} else {
			MovePosiNorm = Vector3.zero;  
		}
	}

	void MiouseDown() {
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved)) {  

		} else {
			rockerBtnTrans.localPosition = Origin;  
		}
	}

	Vector3 result;  

	private Vector3 _checkPosition(Vector3 movePos, Vector3 _offsetPos) {
		result = movePos + _offsetPos;
		return result;
	}

	void OnDrag(GameObject go, Vector2 delta) {
		if (!_drag) {
			_drag = true;
		}
		_deltaPos = delta;
		rockerBtnTrans.localPosition += new Vector3 (_deltaPos.x, _deltaPos.y, 0);
	}

	void OnDragOut(GameObject go) {
		_drag = false;
		rockerBtnTrans.localPosition = Origin;
		if (rockerEnd != null) {
			rockerEnd();
		}
	}

	void OnMoveStart(GameObject go) {
		if (rockerStart != null) {
			rockerStart();
		}
	}
}  