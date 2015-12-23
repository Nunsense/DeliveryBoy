using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

public class PlayerController : MonoBehaviour {

	private Vector3 lastPosition;
	public float distance = 0.5f;
	private bool status = true;
	public LevelManager manager;
	private int screenWidth;

	private CarController m_Car; // the car controller we want to use


	private void Awake()
	{
		// get the car controller
		m_Car = GetComponent<CarController>();
		screenWidth = Screen.width;
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
		print (h + ", " + v);
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

	void OnCollisionStay(Collision collision) {
		if (collision.collider.CompareTag("Cop") && 
			(Vector3.Distance(transform.position, lastPosition) <= distance )) {
			status = false;
			manager.Loose();
		}
//		print ("collisionstay");
		lastPosition = transform.position;
	}
}
