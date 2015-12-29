using UnityEngine;
using System.Collections;

public class CarRespawn : MonoBehaviour {

	// Use this for initialization
	private int counter;
	private GameObject[] streets;
	private float collisionDistance = 0.1f;

	CityManager city;

	public int maxStuck = 100;
	public Transform player;

	void Awake () {
		counter = 0;
		city = GameObject.FindObjectOfType<CityManager>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		GetComponentInChildren<UnityStandardAssets.Vehicles.Car.CarAIControl>().SetTarget(player);
	}

	void Start() {
		ResetPosition ();
	}

	void Update() {
		if (transform.position.y < -1.5) {
			ResetPosition ();
		}
	}

	void OnCollisionStay (Collision other) {
		if (other.transform != player) {
			++counter;
			if (counter >= maxStuck) ResetPosition ();
		} else  { counter = 0; }
	}//collision stay

	public void ResetPosition () {
		Vector3 newPos = city.PositionNearPlayer();
		//		Debug.Log(newPos);
		transform.position = newPos;
		transform.rotation = Quaternion.LookRotation (Vector3.forward);
		counter = 0;
	}//reset Position
}