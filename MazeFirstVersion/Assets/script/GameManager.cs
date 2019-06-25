using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameManager : MonoBehaviour {

	public double wholeTime;

	public UILabel labelScore;

	public double timeLeft;

	public Maze mazePrefab;

	private Maze mazeInstance;

	public GameObject pause;

	public GameObject restart;

	public GameObject home;

	public GameObject show;

	public AudioClip lose;

	void Start () {
		BeginGame ();
	}

	void Update () {
		if (mazeInstance.isStart) {
			StartCoroutine (Count ());
			mazeInstance.isStart = false;
		}
		if ((mazeInstance.clockCount != mazeInstance.transform.childCount)&&mazeInstance.isClock) {
			timeLeft += (mazeInstance.clockCount - mazeInstance.transform.childCount) * 0.5;
			mazeInstance.clockCount = mazeInstance.transform.childCount;
			labelScore.text = ((float)((int)(timeLeft*10)/10.0f)).ToString();
		}
		if(Input.GetKeyDown(KeyCode.P)){
			timeLeft += 10.0f;
		}
	}

	private void BeginGame(){
		timeLeft = wholeTime;
		labelScore.text = ((float)((int)(timeLeft*10)/10.0f)).ToString();
		mazeInstance = Instantiate (mazePrefab) as Maze;
		StartCoroutine (mazeInstance.Generate ());
	}

	IEnumerator Count(){
		while (timeLeft >= 0) {
			labelScore.text = ((float)((int)(timeLeft*10)/10.0f)).ToString();
			yield return new WaitForSeconds (0.1f);
			timeLeft -= 0.1f;
		}
		GameOver ();
	}

	void GameOver(){
		show.SetActive (true);
		pause.SetActive (false);
		restart.SetActive (true);
		restart.transform.position = home.transform.position;
		home.transform.position = pause.transform.position;
		home.SetActive (true);
		show.GetComponent<Animator>().SetInteger ("GameOver", -1);
		GameObject.Find ("Maze(Clone)").transform.Find("player").GetComponent<ThirdPersonUserControl> ().enabled = false;
		AudioSource.PlayClipAtPoint (lose, GameObject.Find ("Main Camera").transform.position);
	}

	public void WinStop(){
		StopAllCoroutines ();
	}
}
