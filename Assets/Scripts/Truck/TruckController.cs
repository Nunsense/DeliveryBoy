using UnityEngine;
using System.Collections;

public class TruckController : MonoBehaviour {
	public float Acceleration = 1000;
	[HideInInspector] public Vector3 Speed;
	public float TurnSpeedScale = 10;
	public float TurnTime = 2.3f;
	
	StreetData currentStreet;
	bool right, left, forward;
	Vector3 direction;
	bool turning;

	void Start () {
		right = false;
		left = false;
		forward = true;
		direction = Vector3.forward;
		turning = false;
	}
	
	void Update () {
		Quaternion orientation = transform.rotation;//
		Speed *= 0.8f;

		if (Input.GetKey (KeyCode.UpArrow)) {
			Speed += Acceleration * direction;
		}
		transform.position += Speed * Time.deltaTime;
		
		if (!turning) {
			if (Input.GetKey(KeyCode.RightArrow) && right) {
				turning = true;
				StartCoroutine(Turn(90));
			}
			if (Input.GetKey(KeyCode.LeftArrow) && left) {
				turning = true;
				StartCoroutine(Turn(270));
			}
	
			transform.rotation = orientation;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals("Street")) {
			right = false;
			left = false;
			forward = true;

			ChangeCurrentStreetColor(Color.white);
			currentStreet = other.GetComponent<StreetData> ();
			ChangeCurrentStreetColor(Color.red);
		}
		if (other.gameObject.tag.Equals("Corner")) {
			right = true;
			left = true;
			forward = true;

			ChangeCurrentStreetColor(Color.white);
			currentStreet = other.GetComponent<StreetData> ();
			ChangeCurrentStreetColor(Color.blue);
		}
	}

	void ChangeCurrentStreetColor(Color color) {
		if (!currentStreet) return;
		Material mat = currentStreet.gameObject.GetComponent<MeshRenderer>().materials[0];
		mat.color = color;
	}

	IEnumerator Turn(float angle) {
		Quaternion rotation = Quaternion.Euler (0, angle, 0);
		
		Quaternion targetOrientation = transform.rotation * rotation;
		Vector3 targetDirection = rotation * direction;
		
		float startTime = Time.time;
		float scale = TurnSpeedScale;
		float turnTime = TurnTime;
		float delta = 0;
		
		while (Time.time - startTime < turnTime) {
			delta = (Time.time - startTime) / turnTime;

			transform.rotation = Quaternion.Slerp (transform.rotation, targetOrientation, delta);
			direction = Vector3.Slerp(direction, targetDirection, delta);
			
			yield return new WaitForSeconds(0.1f);
		}
		transform.rotation = targetOrientation;
		direction = targetDirection;
		turning = false;
	}

}