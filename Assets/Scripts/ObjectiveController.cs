using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveController : MonoBehaviour {

	private int points;
	public Text pointsField;
	public Text timeField;
	public float timeFactor = 0.2f;
	private bool status = true;
	private GameObject cube, circle;
	private Vector3 lastPosition;
	private float time;
	// Use this for initialization
	void Start () {
		points = 0;
		circle = transform.Find("Circle").gameObject;
		circle.SetActive (false);
		cube = transform.Find("Cube").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!status) {
			time -= Time.deltaTime;
			timeField.text = (time<0) ?
				"-" + (int)(time / 60) + ":" + Mathf.Abs((int)(time % 60)) :
				"" + (int)(time / 60) + ":" + Mathf.Abs((int)(time % 60));
		}
	}

	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.tag.Equals("Player")){
			Relocate ();
			if (status) {
				timeField.gameObject.SetActive (true);
				cube.SetActive (false);
				circle.SetActive (true);
				time = Vector3.Distance (lastPosition, transform.position) * timeFactor;
			} else {
				timeField.gameObject.SetActive (false);
				points = points + (int)time;
				pointsField.text = "" + points;
				PlayerPrefs.SetInt ("score", points);
				circle.SetActive (false);
				cube.SetActive (true);
			}
			status = !status;
		}
	}

	public void Relocate(){
		lastPosition = transform.position;
		GameObject[] corners = GameObject.FindGameObjectsWithTag("Corner");
		var index = Random.Range (0, (corners.Length - 1));
		var position = corners [index].transform.position;
		transform.position = new Vector3 (position.x, transform.position.y, position.z);
	}
}
