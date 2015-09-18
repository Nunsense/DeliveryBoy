using UnityEngine;
using System.Collections;

public class StreetData : MonoBehaviour {
	public Vector3 GetStreetVector(int lane, Vector3 direction) { // Allow multiple lanes streets
		float value = lane * 27;
		
		Vector3 pos = transform.position;
		if (direction.z != 0) {
			pos.z = 0;
			pos.x = pos.x + value;
		} 
		if (direction.x != 0) {
			pos.z = pos.z + value;
			pos.x = 0;
		}
		
		return pos;
	}
}
