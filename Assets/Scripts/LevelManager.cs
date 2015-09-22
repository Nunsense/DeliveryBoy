using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;
	
	void Start () {
		if (autoLoadNextLevelAfter <= 0) {
			Debug.Log("auto load next level disabled, use a posisitve number in seconds");
		} else {
			Invoke("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}
	
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}
	
	public void LoadNextLevel(){
		Application.LoadLevel (Application.loadedLevel + 1);
	}
	
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
}
	