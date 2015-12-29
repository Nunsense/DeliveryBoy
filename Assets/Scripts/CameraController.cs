using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float smoothing = 15f;
	//private GameObject cameraTarget;
	public Transform truckTransform;
	public int height = 300, x = 0, y = 0;
	Vector3 offset;

	void Start () {
		offset = new Vector3 (x, height, y);
	}

	void LateUpdate() {
		MoveCameraPos();
	}

	void MoveCameraPos () {
		//if (cameraTarget) {
			Vector3 targetPos = truckTransform.position + offset;
			Vector3 targetCamPos = new Vector3 (targetPos.x, transform.position.y, targetPos.z);
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		//}
	}//move Camera Pos
}