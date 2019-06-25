using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager4 : MonoBehaviour {

	public Maze4 mazePrefab;

	private Maze4 mazeInstance;

	void Start () {
		BeginGame ();
	}

	private void BeginGame(){
		mazeInstance = Instantiate (mazePrefab) as Maze4;
		StartCoroutine (mazeInstance.Generate ());
	}
}
