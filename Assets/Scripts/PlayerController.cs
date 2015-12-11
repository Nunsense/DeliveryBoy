using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

public class PlayerController : MonoBehaviour {

	public LevelManager manager;
	private Vector3 lastPosition;
	public float distance = 0.5f;
	private CarController m_Car; // the car controller we want to use
	private bool status = true;
	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
	}

	private void Awake()
	{
		// get the car controller
		m_Car = GetComponent<CarController>();
	}
	
	private void FixedUpdate()
	{
		if (status) {
			// pass the input to the car!
			float h = CrossPlatformInputManager.GetAxis ("Horizontal");
			m_Car.Move (h, 1, 1, 0);
		} else {
			m_Car.Move (0, 0, 0, 0);
		}
	}
	
	void OnCollisionStay(Collision collision) {
		if (collision.collider.CompareTag("Cop") && 
		    	(Vector3.Distance(transform.position, lastPosition) <= distance )) {
			status = false;
			manager.Loose();
		}
		lastPosition = transform.position;
	}
}
