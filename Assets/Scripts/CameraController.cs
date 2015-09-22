using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float smoothing = 15f;
	private GameObject cameraTarget;
	private Transform truckTransform;
	Vector3 offset;

	void Start () {
		GetTarget ("Truck");
	}

	void LateUpdate() {
		MoveCameraPos();
	}

	void GetTarget (string target) {
		cameraTarget = GameObject.FindGameObjectWithTag (target);

		if (cameraTarget) {
			truckTransform = cameraTarget.transform;
			offset = transform.position - truckTransform.position;
		} else { Debug.LogWarning ("No Target for Camera to follow"); }
	}// Get Camera Target

	void MoveCameraPos () {
		if (cameraTarget) {
			Vector3 truckPos = truckTransform.position + offset;
			Vector3 targetCamPos = new Vector3 (truckPos.x, transform.position.y, truckPos.z);
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
	}//move Camera Pos
}