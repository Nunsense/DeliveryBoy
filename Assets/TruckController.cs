using UnityEngine;
using System.Collections;

public class TruckController : MonoBehaviour {
	public float Acceleration = 1000;
	bool right, left, forward;
	Vector3 speed;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		right = false;
		left = false;
		forward = true;
		direction = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion orientation = transform.rotation;//

		if (Input.GetKey (KeyCode.UpArrow)) {
			//Vector3 direction = orientation * Vector3.forward;
			speed += Acceleration * Time.deltaTime * direction;
		}
		if (Input.GetKey(KeyCode.RightArrow) && right) {
			print(speed.ToString());
			speed = new Vector3 (speed.z,speed.y,speed.x);
			print(speed.ToString());
			orientation *= Quaternion.Euler (0,90,0);
		}
		if (Input.GetKey(KeyCode.LeftArrow) && left) {
			orientation *= Quaternion.Euler (0,270,0);
			speed = new Vector3 (-speed.z,speed.y,speed.x);
		}

		transform.rotation = orientation;
		transform.position += speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals("Street")) {
			right = false;
			left = false;
			forward = true;
		}
		if (other.gameObject.tag.Equals("Corner")) {
			right = true;
			left = true;
			forward = true;
		}
	}
}