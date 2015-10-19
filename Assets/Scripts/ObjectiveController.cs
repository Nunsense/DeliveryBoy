using UnityEngine;
using System.Collections;

public class ObjectiveController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Relocate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		Application.LoadLevel (0);
	}

	void Relocate(){
		GameObject[] corners = GameObject.FindGameObjectsWithTag("Corner");
		var index = Random.Range (0, (corners.Length - 1));
		var position = corners [index].transform.position;
		transform.position = new Vector3 (position.x, transform.position.y, position.z);
	}
}
