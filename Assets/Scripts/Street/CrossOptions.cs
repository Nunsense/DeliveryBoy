using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrossOptions : MonoBehaviour {
	[System.Serializable]
	public class Options {
		public bool N, E, S, W;
	}
	public Options TurningOptions;

}
