using UnityEngine;
using System.Collections;

public class TruckController : MonoBehaviour {

	//Handling Car
	public float tiltThreshold = 85;
	public float Acceleration = 10f;
	[SerializeField] float speed;
	Vector3 frontOfTheCar;
	private float TurnAcceleration = 150f;
	Rigidbody rb;
	[SerializeField] private Vector3 direction;
	private bool turning;
	private bool isStanding;
	private bool notFlying;
	private bool onGround;
	private float flyingTime = 2f;

	public Transform motoTransform;

	//Street Data
	private StreetData currentStreet;

	void Awake() {
		rb = GetComponent<Rigidbody> ();
	}
	
	void Start () {
		frontOfTheCar = new Vector3(0, 0, 1f);
		direction = Vector3.forward;
		turning = false;
		isStanding = true;
		notFlying = false;
		onGround = false;
	}
	
	void FixedUpdate () {
		IsStanding();

//		speed *= 0.8f;
		
		if (isStanding && onGround) {
			if (speed < 3000) {
				speed += Acceleration ;//* direction;
			}

			//right
			if (Input.GetKey(KeyCode.RightArrow)) {
				transform.Rotate(Vector3.up, TurnAcceleration * Time.deltaTime);
			}
			
			//left
			if (Input.GetKey(KeyCode.LeftArrow)) {
				transform.Rotate(Vector3.up, -TurnAcceleration * Time.deltaTime);
			}
//			transform.position += speed * Time.deltaTime;
			
			rb.AddForceAtPosition(direction * speed * Time.fixedDeltaTime, motoTransform.position, ForceMode.Acceleration);// = speed * Time.deltaTime;
		}//if standing and not flipped
//		rb.mo
		direction = transform.forward;
		direction.y = 0;
	}

	public void IsStanding () {
		Vector3 carAngle = transform.eulerAngles;
		float tiltX = Mathf.Abs (carAngle.x);
		float tiltZ = Mathf.Abs (carAngle.z);
		float oppositeAngle = 360 - tiltThreshold;

		isStanding = tiltX > tiltThreshold && tiltX < oppositeAngle || tiltZ > tiltThreshold && tiltZ < oppositeAngle ? false : true;
	}

	void OnCollisionStay (Collision coll) {
		if (coll.gameObject.tag == "ground") {
			notFlying = true;
			onGround = true;
			print ("I'm Riding !");
		}
	}

	void OnCollisionExit (Collision coll) {
		if (coll.gameObject.tag == "ground" && onGround) {
			Invoke ("KeepsFlying", flyingTime);
		}
	}

	public void KeepsFlying () {
		onGround = false;
		notFlying = false;
		print ("Psss.. Master? I keep Flying");
	}
}




