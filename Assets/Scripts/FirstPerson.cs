using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]  
[RequireComponent(typeof(CapsuleCollider))]

public class FirstPerson : MonoBehaviour {

	//把运动相关的参数，独立出来  
	[System.Serializable]
	public class MoveSetting {
		public float ForwardSpeed = 3f;
		public float BackSpeed = 2f;
		public float HorizonSpeed = 2f;

		public float RunValue = 2f;
	}

	//把视角相关的独立出来  
	[System.Serializable]
	public class MouseLook {
		public float XSensitive = 2f;
		public float YSensitive = 2f;
	}

	public MoveSetting moveSet;  
	public MouseLook CameraSet;  

	//当前速度  
	private float currentSpeed;

	//第一人称，胶囊碰撞  
	private CapsuleCollider m_capsule;  
	//第一人称，刚体  
	private Rigidbody m_rigidbody;  

	[SerializeField]private Camera m_camera;  
	//相机的Transform（减少Update中transform的调用）  
	private Transform m_camTrans;  
	//主角的Transform  
	public Transform m_chaTrans;

	//摄像机旋转四元数  
	private Quaternion m_camQutation;  
	//主角的旋转四元数  
	private Quaternion m_chaQutation;  

	//爬坡的速度曲线  
	public AnimationCurve SlopCurve;  

	//是否在地面上  
	private bool m_isOnGround;  
	//地面法线向量  
	private Vector3 curGroundNormal;

	public DirectionType viewTurnDirection = DirectionType.None;
	public DirectionType personMoveDirection = DirectionType.None;

	public bool isUseViewRocker = false;
	public static bool isCouldViewTurn = true;

	void Start () {  
		m_capsule = GetComponent<CapsuleCollider>();
		m_rigidbody = GetComponent<Rigidbody>();

		m_camTrans = m_camera.transform;
		m_chaTrans = transform;

		//初始化参数  
		m_camQutation = m_camTrans.rotation;
		m_chaQutation = m_chaTrans.rotation;
	}

	void Update () {
		//视角转动
		if(isCouldViewTurn){
			RotateView ();
		}
		DoMove ();
	}  

	void RotateView() {
		if (isUseViewRocker) {
			switch (viewTurnDirection) {
			case DirectionType.Left:
				m_chaQutation *= Quaternion.Euler (0f, -CameraSet.XSensitive * 0.5f, 0f);
				break;
			case DirectionType.Right:
				m_chaQutation *= Quaternion.Euler (0f, CameraSet.XSensitive * 0.5f, 0f);
				break;
			case DirectionType.Up:
				m_chaQutation *= Quaternion.Euler (-1, 0f, 0f);
				break;
			case DirectionType.Down:
				m_chaQutation *= Quaternion.Euler (1, 0f, 0f);
				break;
			}
			m_chaTrans.localRotation = m_chaQutation;
		} else {
			#if !UNITY_EDITOR
			Touch turnTouch;
			if (personMoveDirection != DirectionType.None && Input.touchCount > 1) {
				turnTouch = Input.touches [1];
			} else {
				turnTouch = Input.touches [0];
			}
			//手指在屏幕上移动，移动摄像机
			if (turnTouch.phase == TouchPhase.Moved) {
				m_chaQutation *= Quaternion.Euler (0f, turnTouch.deltaPosition.x * Time.deltaTime * CameraSet.XSensitive, 0f);
				m_chaQutation = ClampRotation (m_chaQutation);
				m_chaTrans.rotation = m_chaQutation;
			}
			#else
			float yRot = Input.GetAxis("Mouse X") * CameraSet.XSensitive;
			//四元数使用
			{
				m_chaQutation *= Quaternion.Euler(0f, yRot, 0f);
				m_chaTrans.localRotation = m_chaQutation;
			}
			#endif
		}
	}

	public void DoMove(){
		if (personMoveDirection != DirectionType.None) {
			//检测地面  
			CheckGround();  
			//更新当前速度，根据移动方向  
			CaculateSpeed();
			//判断是否有移动的速度，没有就不给刚体施加力  
			if (m_isOnGround) {
				//计算方向力
				Vector3 desireMove;

				if (personMoveDirection == DirectionType.Back) {
					desireMove = m_camTrans.forward * -1 + m_camTrans.right * 0;
				} else if (personMoveDirection == DirectionType.Left) {
					desireMove = m_camTrans.forward * 0 + m_camTrans.right * -1;
				} else if (personMoveDirection == DirectionType.Right) {
					desireMove = m_camTrans.forward * 0 + m_camTrans.right * 1;
				} else {
					desireMove = m_camTrans.forward * 1 + m_camTrans.right * 0;
				}

				//力在地面投影的向量的（单位向量）
				desireMove = Vector3.ProjectOnPlane(desireMove, curGroundNormal).normalized;
				desireMove.x = desireMove.x * currentSpeed;
				desireMove.y = 0;
				desireMove.z = desireMove.z * currentSpeed;

				//当前速度不能大于规定速度（Magnitude方法，需要开平方根，使用sqr节省运算）  
				if (m_rigidbody.velocity.sqrMagnitude < currentSpeed * currentSpeed) {
					//给刚体施加（坡度计算后）的力  
					m_rigidbody.AddForce (desireMove, ForceMode.Impulse);
				}
			}
		} else {
			m_rigidbody.Sleep ();
		}
	}

	void CaculateSpeed() {  
		currentSpeed = moveSet.ForwardSpeed;  

		//后退
		if (personMoveDirection == DirectionType.Back) {  
			currentSpeed = moveSet.BackSpeed;  
		}
		//左右
		if(personMoveDirection == DirectionType.Left || personMoveDirection == DirectionType.Right){
			currentSpeed = moveSet.HorizonSpeed;
		}
	}

	//检测地面  
	void CheckGround() {  
		RaycastHit hit;
		//球形碰撞检测（第9个方法）  
		if (Physics.SphereCast (m_capsule.transform.position, m_capsule.radius, Vector3.down, out hit, ((m_capsule.height / 2 - m_capsule.radius) + 0.01f))) {  
			//获取碰撞位置的发现向量  
			curGroundNormal = hit.normal;  
			m_isOnGround = true;  
		} else {  
			curGroundNormal = Vector3.up;  
			m_isOnGround = false;  
		}  
	}  

	void CheckBuffer() {  
		RaycastHit hit;  
		float speed = m_rigidbody.velocity.y;  
		if (speed < 0) {  
			if (Physics.SphereCast (m_capsule.transform.position, m_capsule.radius, Vector3.down, out hit, ((m_capsule.height / 2 - m_capsule.radius) + 1f))) {  
				speed *= 0.5f;  
				m_rigidbody.velocity = new Vector3 (m_rigidbody.velocity.x, speed, m_rigidbody.velocity.z);  
			}  
		}  
	} 

	//四元数俯角，仰角限制  限制旋转角度在【-90，90】内
	Quaternion ClampRotation(Quaternion q) {  
		//四元数的xyzw，分别除以同一个数，只改变模，不改变旋转  
		q.x /= q.w;  
		q.y /= q.w;  
		q.z /= q.w;  
		q.w = 1;  

		/*给定一个欧拉旋转(X, Y, Z)（即分别绕x轴、y轴和z轴旋转X、Y、Z度），则对应的四元数为 
x = sin(Y/2)sin(Z/2)cos(X/2)+cos(Y/2)cos(Z/2)sin(X/2) 
y = sin(Y/2)cos(Z/2)cos(X/2)+cos(Y/2)sin(Z/2)sin(X/2) 
z = cos(Y/2)sin(Z/2)cos(X/2)-sin(Y/2)cos(Z/2)sin(X/2) 
w = cos(Y/2)cos(Z/2)cos(X/2)-sin(Y/2)sin(Z/2)sin(X/2) 
         */  

		//得到推导公式[欧拉角x=2*Aan(q.x)]  
		float angle = 2 * Mathf.Rad2Deg * Mathf.Atan (q.x);  
		//限制速度  
		angle = Mathf.Clamp (angle, -90f, 90f);  
		//反推出q的新x的值  
		q.x = Mathf.Tan (Mathf.Deg2Rad * (angle / 2));  

		return q;  
	}
}