using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;
	public GameObject canvasUI, canvasRestart,canvasCounter;
	
	void Start () {
		PauseLevel ();
		int scoreValue = PlayerPrefs.GetInt ("score",-1);
		if (scoreValue >= 0) {
			StartLevel ();
		} else {
			canvasCounter.SetActive(false);
			canvasRestart.SetActive(false);
			canvasUI.SetActive(true);
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

	public void ReloadLevel(){
		Application.LoadLevel(0);
	}

	public void StartLevel(){
		canvasCounter.SetActive(true);
		canvasUI.SetActive (false);
		canvasRestart.SetActive (false);
		Time.timeScale = 1;
		PlayerPrefs.SetInt("score", -1);
	}

	public void PauseLevel(){
		Time.timeScale = 0;
	}

	public void Loose(){
		canvasUI.SetActive (false);
		canvasRestart.SetActive (true);
		//Time.timeScale = 0;
	}
}
	