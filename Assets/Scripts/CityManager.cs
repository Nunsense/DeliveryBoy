using UnityEngine;
using System.Collections;

public class CityManager : MonoBehaviour
{
	[System.Serializable]
	public class BlocksSet
	{
		public GameObject[] blockSet;
	}

	public BlocksSet[] blocksSets;
	public GameObject[] cops;

	public int blockSize = 34;
	public Transform player;

	public int mapW;
	public int mapH;

	private float halfBlockSize;
	private GameObject[,] map;
	private int mapOffsetX;
	private int mapOffsetY;
	private float lastPlayerX;
	private float lastPlayerZ;

	void Awake ()
	{
		mapOffsetX = 0;
		mapOffsetY = 0;
	}

	void Start ()
	{
		halfBlockSize = blockSize / 2f;
		CreateMap ();
		InitPlayer ();
		InitCops ();
	}

	void FixedUpdate ()
	{
		float difX = player.position.x - lastPlayerX;
		float difZ = player.position.z - lastPlayerZ;

		if (difX < -blockSize) {
			mapOffsetX--;
			int mapXPos = ((mapOffsetX % mapW) + mapW) % mapW;
			for (int j = 0; j < mapH; j++) {
				GameObject block = map [mapXPos, j];
				Vector3 pos = block.transform.position;
				pos.x = mapOffsetX * blockSize;
				block.transform.position = pos;
			}
			lastPlayerX = player.position.x;
		} else if (difX > blockSize) {
			mapOffsetX++;
			int mapXPos = ((mapOffsetX % mapW) + mapW - 1) % mapW;
			for (int j = 0; j < mapH; j++) {
				GameObject block = map [mapXPos, j];
				Vector3 pos = block.transform.position;
				pos.x = (mapW - 1 + mapOffsetX) * blockSize;
				block.transform.position = pos;
			}
			lastPlayerX = player.position.x;
		} else if (difZ < -blockSize) {
			mapOffsetY--;
			int mapYPos = ((mapOffsetY % mapH) + mapH) % mapH;
			for (int i = 0; i < mapW; i++) {
				GameObject block = map [i, mapYPos];
				Vector3 pos = block.transform.position;
				pos.z = mapOffsetY * blockSize;
				block.transform.position = pos;
			}
			lastPlayerZ = player.position.z;
		} else if (difZ > blockSize) {
			mapOffsetY++;
			int mapYPos = ((mapOffsetY % mapH) + mapH - 1) % mapH;
			for (int i = 0; i < mapW; i++) {
				GameObject block = map [i, mapYPos];
				Vector3 pos = block.transform.position;
				pos.z = (mapH - 1 + mapOffsetY) * blockSize;
				block.transform.position = pos;
			}
			lastPlayerZ = player.position.z;
		}
	}

	void CreateMap ()
	{
		map = new GameObject[mapW, mapH];
		Transform trans;
		GameObject[] blocksSet = blocksSets [Random.Range (0, blocksSets.Length - 1)].blockSet;

		for (int i = 0; i < mapW; i++) {
			for (int j = 0; j < mapH; j++) {
				GameObject block = GameObject.Instantiate (blocksSet [Random.Range (0, blocksSet.Length - 1)]);
				trans = block.transform;
				trans.parent = transform;
				trans.position = new Vector3 (i * blockSize, 0, j * blockSize);
				map [i, j] = block;
			}
		}
	}

	void InitPlayer ()
	{
		player.position = new Vector3 ((mapW / 2) * blockSize, 0, (mapH / 2) * blockSize);	

		lastPlayerX = player.position.x;
		lastPlayerZ = player.position.z;
	}

	void InitCops ()
	{
		for (int i = 0; i < 3; i++) {
			GameObject cop = GameObject.Instantiate (cops [0]);
			cop.transform.parent = transform;
		} 
	}

	public Vector3 PositionNearPlayer ()
	{
		Vector3 pos = player.transform.position;
		pos.x += RandomSign (1) * blockSize;
		pos.z += RandomSign (1) * blockSize;

		pos.x = SnapToMap (pos.x);
		pos.y = 0.1f;
		pos.z = SnapToMap (pos.z);

		return pos;
	}

	float RandomSign (float val)
	{
		return Random.value > .5 ? -val : val;
	}

	int SnapToMap (float val)
	{
		return MapPosition (val) * blockSize;
	}

	int MapPosition (float val)
	{
		return Mathf.CeilToInt (val / blockSize);
	}
}
