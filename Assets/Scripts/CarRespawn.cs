using UnityEngine;
using System.Collections;

public class CarRespawn : MonoBehaviour {

	// Use this for initialization
	private int counter;
	private GameObject[] streets;
	private float collisionDistance = 0.1f;

	public int maxStuck = 100;
	public Transform player;

	void Awake () {
		counter = 0;
		streets = GameObject.FindGameObjectsWithTag ("Street");
	}
		
	void OnCollisionStay (Collision other) {
		if (other.transform != player) {
			++counter;
			if (counter >= maxStuck) ResetPosition ();
		} else  { counter = 0; }
	}//collision stay
		
	void ResetPosition () {
		Transform newPos = streets [Random.Range (0, (streets.Length - 1))].transform;
		transform.position = new Vector3(newPos.position.x, transform.position.y, newPos.position.z);
		transform.rotation = Quaternion.LookRotation (newPos.right);
		counter = 0;
	}//reset Position
}
