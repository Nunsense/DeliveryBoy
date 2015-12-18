using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	// Use this for initialization
	public Transform car;
	public Transform objective;
	Quaternion zero;

	void Start () {
		zero = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var heading = transform.position - objective.position;
		heading.Normalize ();
		heading.y = 7.8f;
		//transform.rotation = Quaternion.LookRotation(heading); 
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(heading),Time.deltaTime*3);
	}
	
}
