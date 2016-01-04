using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

public class PlayerController : MonoBehaviour {

	public LevelManager manager;
	public float maxStuckTime = 3f;

	private float stuckTimerCount;
	private Vector3 lastPosition;
	private bool status = true;
	private int screenWidth;

	private int copsTouchingCount;
	private CarController m_Car; // the car controller we want to use


	private void Awake()
	{
		// get the car controller
		m_Car = GetComponent<CarController>();
		screenWidth = Screen.width;
	}

	void Start () {
		stuckTimerCount = 0;
		copsTouchingCount = 0;
	}

	private void FixedUpdate()
	{
//		// pass the input to the car!
//		float h = Input.GetAxis("Horizontal");
//		float v = Input.GetAxis("Vertical");
//
//		float handbrake = Input.GetAxis("Jump");
//		m_Car.Move(h, v, v, handbrake);


#if UNITY_EDITOR || UNITY_WEBPLAYER
        // pass the input to the car!
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float handbrake = Input.GetAxis("Jump");
//		print (h + ", " + v);
        m_Car.Move(h, v, v, handbrake);
#else
		if(status) {
			if(Input.touchCount > 1){
				m_Car.Move(0f, -1f, -1f, 0f);
			}else{
				if(Input.touchCount > 0) {
					Touch touch = Input.GetTouch(0);	
					m_Car.Move(((touch.position.x * 2 / screenWidth) - 1), 1f, 1f, 0f);
				}else {
					m_Car.Move(0f, 1f, 1f, 0f);
				}
			}
		}
#endif
	}

	void Update() {
		if (copsTouchingCount > 0 && m_Car.CurrentSpeed <= 10f) {
			stuckTimerCount += Time.deltaTime;
			if (stuckTimerCount >= maxStuckTime) {
				status = false;
				manager.Loose();
				stuckTimerCount = 0;
			}
		} else {
			stuckTimerCount = 0;
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Cop")) {
			copsTouchingCount ++;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.collider.CompareTag("Cop")) {
			if (--copsTouchingCount == 0) {
				stuckTimerCount = 0;
			}
		}
	}
}
