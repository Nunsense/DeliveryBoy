using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	// Use this for initialization
	public GameObject car;
	public GameObject objective;
	Quaternion zero;

	void Start () {
		zero = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float deltaZ = objective.transform.position.z - car.transform.position.z;
		float deltaX = objective.transform.position.x - car.transform.position.x;

		float angle = (Mathf.Atan (deltaZ / deltaX)) * 180 / Mathf.PI;
		if (angle < 0) {
			angle = 360 - Mathf.Abs(angle);
		}
		float lastAngle = transform.localRotation.z * 180 / Mathf.PI;
		if (lastAngle < 0) {
			lastAngle = 360 - Mathf.Abs (lastAngle);
		}
		print (lastAngle + "  " + angle + "  " + (angle - lastAngle));
		transform.RotateAround (transform.position, transform.forward, angle - lastAngle);
	}
	
}
