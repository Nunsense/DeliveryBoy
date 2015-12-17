using UnityEngine;
using System.Collections;

public class CityManager : MonoBehaviour {

	public int blockSize = 34;
	public GameObject player;
	public GameObject[] blocks;

	void Start () {
		blocks = GameObject.FindGameObjectsWithTag("Block");
	}

	void FixedUpdate () {
		int carPositionX = Mathf.CeilToInt( player.transform.position.x / blockSize);
		int carPositionZ = Mathf.CeilToInt( player.transform.position.z / blockSize);
		foreach (GameObject block in blocks) {
			int difX = Mathf.CeilToInt(Mathf.Abs ((block.transform.position.x / blockSize) - carPositionX));
			int difZ = Mathf.CeilToInt(Mathf.Abs ((block.transform.position.z / blockSize) - carPositionZ));
			if (difX > 2 || difZ > 2) {
				//relocate block
				RaycastHit hit;
				Ray ray;
				bool found = false;
				for (int i = carPositionX - 2; !found && i <= carPositionX + 2; i++) {
					for (int k = carPositionZ - 2; !found && k <= carPositionZ + 2; k++) {
						//Debug.DrawRay(new Vector3(i*blockSize-17,-0.5f,k*blockSize), transform.up*10,Color.white,0.01f);
						//print ("pos:" + i + ", " + k);
						if (!(Physics.Raycast (new Vector3 (i * blockSize - 17, 5, k * blockSize), transform.up * -1, out hit, 10))) {
							//relocate
							block.transform.localPosition = new Vector3 ((i * blockSize) - 2, block.transform.position.y, (k * blockSize) + 2);
							//print ("pos:" + ((i * blockSize) - 2) + ", " + ((k * blockSize) + 2));
							found = true;
						}// else {
						//	print ("hit");
						//}
					}
				}
			}
		}
	}
}
