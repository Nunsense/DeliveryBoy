using UnityEngine;
using System.Collections;

public class CarRespawn : MonoBehaviour {

	// Use this for initialization
	private int counter;
	private Vector3 lastPosition, lastPlayerPosition;
	private float distance = 0.1f;
	public int playerDistance = 25;
	public int maxStuck = 100;
	public int minRespawnDistance = 200;
	public Transform player;

	void Start () {
		counter = 0;
		lastPosition = transform.position;
		lastPlayerPosition = player.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.position, lastPlayerPosition) > 0) {
			if ((Vector3.Distance (transform.position, lastPosition) <= distance) &&
				(Vector3.Distance (transform.position, player.position) >= distance)) {
				counter++;
			} else {
				counter = 0;
			}
			if (counter > maxStuck) {
				Vector3 position = new Vector3 ();
				GameObject[] streets = GameObject.FindGameObjectsWithTag ("Street");
				bool selected = false;
				while (!selected) {
					position = streets [Random.Range (0, (streets.Length - 1))].transform.position;
				//	if (Vector3.Distance (position, player.transform.position) > minRespawnDistance) {
						selected = true;
				//	}
				}
				transform.position = new Vector3(position.x, transform.position.y,position.z);

//				var heading = player.position - transform.position ;
//				heading.Normalize ();
//				transform.rotation = Quaternion.LookRotation(heading);

				counter = 0;
			}
		}
		lastPosition = transform.position;
		lastPlayerPosition = player.position;
	}

}
