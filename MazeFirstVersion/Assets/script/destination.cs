using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class destination : MonoBehaviour {

	public AudioClip win;

	public bool isPrefab;

	public GameObject show;

	public GameObject pause;

	public GameObject restart;

	public GameObject home;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other){
		show.SetActive (true);
		pause.SetActive (false);
		restart.SetActive (true);
		restart.transform.position = home.transform.position;
		home.transform.position = pause.transform.position;
		home.SetActive (true);
		show.GetComponent<Animator>().SetInteger ("GameOver", 1);	
		if (!isPrefab)
			GameObject.Find ("girl").GetComponent<ThirdPersonUserControl> ().enabled = false;
		else {
			GameObject.Find ("GameManager").SendMessage ("WinStop");
			GameObject.Find ("Maze(Clone)").transform.Find("player").GetComponent<ThirdPersonUserControl> ().enabled = false;
		}
		AudioSource.PlayClipAtPoint (win, GameObject.Find ("Main Camera").transform.position);
	}
}
