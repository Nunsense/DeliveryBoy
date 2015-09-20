using UnityEngine;
using System.Collections;

public class TruckController : MonoBehaviour {
	public float Acceleration = 1000;
	[HideInInspector] public Vector3 Speed;
	public float TurnSpeedScale = 10;
	public float TurnTime = 2.3f;
	
	private StreetData currentStreet;
	private bool N, W, E, S;
	private Vector3 direction;
	private bool turning;

	void Start () {
		direction = Vector3.forward;
		turning = false;
		SetCardinalDirection ();
	}
	
	void FixedUpdate () {
		Quaternion orientation = transform.rotation;//
		Speed *= 0.8f;

		if (Input.GetKey (KeyCode.UpArrow)) {
			Speed += Acceleration * direction;
		}
		transform.position += Speed * Time.deltaTime;
		
		if (!turning) {
			//right
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				turning = true;
				StartCoroutine(Turn(90));
			}
			//left
			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				turning = true;
				StartCoroutine(Turn(270));
			}
	
			transform.rotation = orientation;
		}
	}

	void SetCardinalDirection () {
		E = (direction.z == 1) ? true : false;
		W = (direction.z == -1) ? true : false;
		S = (direction.x == 1) ? true : false;
		N = (direction.x == -1) ? true : false;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals("Corner")) {
			CrossOptions.Options turningOptions = other.GetComponent<CrossOptions> ().TurningOptions;

			print ("E = " + E);
			print ("W = " + W);
			print ("S = " + S);
			print ("N = " + N);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag.Equals("Corner")) {
			CrossOptions.Options turningOptions = other.GetComponent<CrossOptions> ().TurningOptions;

			print ("E = " + E);
			print ("W = " + W);
			print ("S = " + S);
			print ("N = " + N);
		}
	}

	IEnumerator Turn(float angle) {
		Quaternion rotation = Quaternion.Euler (0, angle, 0);
		
		Quaternion targetOrientation = transform.rotation * rotation;
		Vector3 targetDirection = rotation * direction;
		
		float startTime = Time.fixedTime;
		float scale = TurnSpeedScale;
		float turnTime = TurnTime;
		float delta = 0;
		
		while (Time.fixedTime - startTime < turnTime) {
			delta = (Time.fixedTime - startTime) / turnTime;

			transform.rotation = Quaternion.Slerp (transform.rotation, targetOrientation, delta);
			direction = Vector3.Slerp(direction, targetDirection, delta);
			
			yield return new WaitForSeconds(0.1f);
		}
		transform.rotation = targetOrientation;
		direction = targetDirection.normalized;
		turning = false;
		SetCardinalDirection ();
	}

}