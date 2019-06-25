using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour {

	public bool isInit = true;

	public Maze1 maze1Prefab;

	private Maze1 maze1Instance;

	void Start () {
		BeginGame ();
	}

	void Update () {
		if (!maze1Instance.isInit)
			isInit = false;
	}

	private void BeginGame(){
		maze1Instance = Instantiate (maze1Prefab) as Maze1;
		StartCoroutine (maze1Instance.Generate ());
	}
}
