using UnityEngine;
using System.Collections;

public class TruckController : MonoBehaviour {
	public float Acceleration = 10;
	[HideInInspector] public Vector3 Speed;
	public float TurnAcceleration = 10;
	public float TurnSpeed = 0;
	
	private StreetData currentStreet;
	private bool N, W, E, S;
	[SerializeField] private Vector3 direction;
	[SerializeField] private Quaternion rotation;
	private bool turning;
	private CrossOptions currentOptions;

	void Start () {
		direction = Vector3.forward;
		turning = false;
		SetCardinalDirection ();
	}
	
	void Update () {
		rotation = transform.rotation;
		Speed *= 0.8f;
		TurnSpeed *= 0.8f;
		
		if (Input.GetKey (KeyCode.UpArrow)) {
			Speed += Acceleration * direction;
		}
		
		//right
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			TurnSpeed -= TurnAcceleration;
		}
		//left
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			TurnSpeed += TurnAcceleration;
		}
		
		rotation.y -= TurnSpeed * Time.deltaTime;
		transform.rotation = rotation;
		transform.position += Speed * Time.deltaTime;
		
		direction = transform.forward;
	}

	void SetCardinalDirection () {
		E = direction.z == 1;
		W = direction.z == -1;
		S = direction.x == 1;
		N = direction.x == -1;
	}

	bool CanTurn(bool right) {
		if (currentOptions != null) {
			return right && 
					(E && currentOptions.TurningOptions.S) ||
					(N && currentOptions.TurningOptions.E) ||
					(W && currentOptions.TurningOptions.N) ||
					(S && currentOptions.TurningOptions.W) ||
				  !right &&
					(E && currentOptions.TurningOptions.N) ||
					(N && currentOptions.TurningOptions.W) ||
					(W && currentOptions.TurningOptions.S) ||
					(S && currentOptions.TurningOptions.E);
		}
		return false;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals("Corner")) {
			currentOptions = other.GetComponent<CrossOptions> ();			
		}
	}
	
	void OnTriggerEexit(Collider other){
		if (other.gameObject.tag.Equals("Corner")) {
			currentOptions = null;			
		}
	}
	
	Vector3 TurnFinalPosition(Vector3 targetDirection) {
		if (currentOptions != null) {
			Vector3 targetPosition = currentOptions.gameObject.transform.position;
			targetPosition.x += targetDirection.x * 10f;
			targetPosition.z += targetDirection.z * 10f;
			targetPosition.y = 0;
			return targetPosition;
		}

		return transform.position;
	}

//	IEnumerator Turn(float angle) {
//		Quaternion rotation = Quaternion.Euler (0, angle, 0);
//		
//		Quaternion targetOrientation = transform.rotation * rotation;
//		Vector3 targetDirection = (rotation * direction).normalized;
//		Vector3 targetPosition = TurnFinalPosition(targetDirection);
//		
//		float startTime = Time.time;
//		float scale = TurnSpeedScale;
//		float turnTime = TurnTime;
//		float delta = 0;
//		
//		while (Time.time - startTime < turnTime) {
//			delta = (Time.time - startTime) / turnTime;
//
//			transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetOrientation, delta);
//			direction = Vector3.Slerp(direction, targetDirection, delta);
//			transform.position = Vector3.Slerp(transform.position, targetPosition, delta);
//			
//			yield return new WaitForSeconds(0.1f);
//		}
//		transform.rotation = targetOrientation;
//		direction = targetDirection.normalized;
//		transform.position = targetPosition;
//		turning = false;
//		currentOptions = null;
//		SetCardinalDirection ();
//	}

}