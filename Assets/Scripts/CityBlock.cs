using UnityEngine;
using System.Collections;

public class CityBlock : MonoBehaviour {
	static int[] rotations = new int[] { 0, 90, 180, 270 };

	void Start () {
		transform.eulerAngles = new Vector3(0, rotations[Random.Range(0, rotations.Length)], 0);
	}
}
