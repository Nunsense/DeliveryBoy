using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveController : MonoBehaviour {

	private int points;
	public Text pointsField;
	// Use this for initialization
	void Start () {
		points = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collision) {
		//Application.LoadLevel (0);
		points ++;
		pointsField.text = "" + points;
		PlayerPrefs.SetInt("score", points);
		print ("Points: " + points);
		Relocate ();
	}

	public void Relocate(){
		GameObject[] corners = GameObject.FindGameObjectsWithTag("Corner");
		var index = Random.Range (0, (corners.Length - 1));
		var position = corners [index].transform.position;
		transform.position = new Vector3 (position.x, transform.position.y, position.z);
	}
}
