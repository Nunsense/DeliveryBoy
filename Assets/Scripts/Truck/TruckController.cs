﻿using UnityEngine;
using System.Collections;

public class TruckController : MonoBehaviour {

	public GameObject bonus;

	//Handling Car
	public float standingThreshold = 85;
	public float Acceleration = 10f;
	[HideInInspector] public Vector3 speed;
	private float TurnAcceleration = 120f;
	[SerializeField] private Vector3 direction;
	private bool turning;

	//Street Data
	private StreetData currentStreet;

	void Start () {
		direction = Vector3.forward;
		turning = false;
	}
	
	void Update () {
		speed *= 0.8f;
		
		if (Input.GetKey (KeyCode.UpArrow)) {
			speed += Acceleration * direction;
		}
		
		//right
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(Vector3.up, TurnAcceleration * Time.deltaTime);
		}

		//left
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.up, -TurnAcceleration * Time.deltaTime);
		}

		transform.position += speed * Time.deltaTime;
		
		direction = transform.forward;
		direction.y = 0;

		print (IsStanding());
	}

	public bool IsStanding () {
		Vector3 carAngle = transform.eulerAngles;
		float tiltX = Mathf.Abs (carAngle.x);
		float tiltZ = Mathf.Abs (carAngle.z);
		
		if (tiltX > standingThreshold || tiltZ > standingThreshold) {
			return false;
		}
		return true;
	}


	void OnTriggerEnter(Collider other){
	}
	
	void OnTriggerExit(Collider other){
	}
}