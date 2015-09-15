using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float smoothing = 15f;
	private GameObject truck;
	private Transform truckTransform;
	Vector3 offset;

	void Start () {
		truck = GameObject.FindGameObjectWithTag ("Truck");
		truckTransform = truck.transform;
		offset = transform.position - truckTransform.position;
	}

	void LateUpdate() {
		MoveCameraPos();
	}

	void MoveCameraPos () {
		Vector3 truckPos = truckTransform.position + offset;
		Vector3 targetCamPos = new Vector3 (truckPos.x, transform.position.y, truckPos.z);
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

}