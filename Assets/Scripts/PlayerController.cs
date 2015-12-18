using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

public class PlayerController : MonoBehaviour {

	private Vector3 lastPosition;
	public float distance = 0.5f;
	private bool status = true;
	public LevelManager manager;

	private CarController m_Car; // the car controller we want to use


	private void Awake()
	{
		// get the car controller
		m_Car = GetComponent<CarController>();
	}


	private void FixedUpdate()
	{
		// pass the input to the car!
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		float handbrake = Input.GetAxis("Jump");
		m_Car.Move(h, v, v, handbrake);
	}

	void OnCollisionStay(Collision collision) {
		if (collision.collider.CompareTag("Cop") && 
			(Vector3.Distance(transform.position, lastPosition) <= distance )) {
			status = false;
			manager.Loose();
		}
		print ("collisionstay");
		lastPosition = transform.position;
	}
}
