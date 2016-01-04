using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class CarRespawn : MonoBehaviour {

	// Use this for initialization
	private float stuckTimerCount;
	private GameObject[] streets;
	private float collisionDistance = 0.1f;

	private CityManager city;
	private CarController controller;
	private Transform player;

	public float maxStuckTime = 3f;

	void Awake () {
		stuckTimerCount = 0;
		city = GameObject.FindObjectOfType<CityManager>();
		controller = GetComponent<CarController> ();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		GetComponentInChildren<UnityStandardAssets.Vehicles.Car.CarAIControl>().SetTarget(player);
	}

	void Start() {
		ResetPosition ();
	}

	void Update() {
		if (transform.position.y < -1.5 || transform.position.y > 2.5) {
			ResetPosition ();
		}
	}

	void OnCollisionStay (Collision other) {
		if (other.transform != player && controller.CurrentSpeed <= 2f) {
			if (stuckTimerCount == 0) {
				stuckTimerCount = Time.time;
			} else if (Time.time - stuckTimerCount >= maxStuckTime) {
				ResetPosition ();
				stuckTimerCount = 0;
			}
		} else {
			stuckTimerCount = 0;
		}
	}//collision stay

	public void ResetPosition () {
		Vector3 newPos = city.PositionNearPlayer();
		//		Debug.Log(newPos);
		transform.position = newPos;
		transform.rotation = Quaternion.LookRotation (Vector3.forward);
		stuckTimerCount = 0;
	}//reset Position
}