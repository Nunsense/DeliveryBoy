using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GpsManager : MonoBehaviour {

	public Transform player;
	public Transform target;
	public Sprite keepStraight;
	public Sprite turnLeft;
	public Sprite turnRight;
	public Sprite uTurn;
	public float deviateAllowance;

	public float recalculatingTime;
	private float waitingTime;

	private Image image;
	private float lastDx;
	private float lastDz;



	// Use this for initialization
	void Awake () {
		image = GetComponentInChildren<Image>() ;
	}

	void Start () {
		image.sprite = keepStraight;
		lastDx = 0;
		lastDz = 0;
		waitingTime = 0;
	}

	void Update () {
		waitingTime += Time.deltaTime;
		if (waitingTime < recalculatingTime) return;
		waitingTime = 0;

		float dx = target.position.x - player.position.x;
		float dz = target.position.z - player.position.z;

//		if (dz > deviateAllowance || dz < -deviateAllowance || dx > deviateAllowance || dx < -deviateAllowance) {
//			if (Mathf.Abs(dx - lastDx) >= 0 || Mathf.Abs(dz - lastDz) >= 0) {
//				image.sprite = uTurn;
//			} else {
				float playerPos, targetPos;
				if (dx > dz) {
					playerPos = player.position.x;
					targetPos = target.position.x;
				} else {
					playerPos = player.position.z;
					targetPos = target.position.z;
				}

				float dPos = targetPos - playerPos;
				if (dPos < -deviateAllowance) {
					image.sprite = turnRight;
				} else if (dPos > deviateAllowance) {
					image.sprite = turnLeft;
				} else {
					image.sprite = keepStraight;
				}
//			}
//		} else {
//			image.sprite = keepStraight;
//		}

		lastDx = dx;
		lastDz = dz;
	}
}
