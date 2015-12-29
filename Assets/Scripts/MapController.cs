using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	public GameObject cameraTarget;
	public int height = 300;

	// Use this for initialization
	void Awake () {
		Vector3 target = cameraTarget.transform.position;
		target.y = height;
		transform.position = target;
	}
	
	// Update is called once per frame
	void Update () {

		//transform.position = new Vector3.Lerp
		//transform.position.x = cameraTarget.transform.position.x;
		//transform.position.z = cameraTarget.transform.position.z;
	}


}
