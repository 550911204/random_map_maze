using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easyManager : MonoBehaviour {

	public string modeName;

	public GameObject pause;

	public GameObject home;

	public GameObject restart;

	public GameObject play;

	public void PauseDown(){
		Time.timeScale = 0;
		home.SetActive (true);
		restart.SetActive (true);
		play.SetActive (true);
		pause.SetActive (false);
	}

	public void PlayDown(){
		Time.timeScale = 1.0f;
		home.SetActive (false);
		restart.SetActive (false);
		play.SetActive (false);
		pause.SetActive (true);
	}

	public void RestartDown(){
		Time.timeScale = 1.0f;
		Application.LoadLevel (modeName);
	}

	public void HomeDown(){
		Time.timeScale = 1.0f;
		Application.LoadLevel ("home");
	}
}
