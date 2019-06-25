using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour {

	public GameObject player;

	public bool isStart = false;

	public bool isInit = true;

	public Maze3 maze3Prefab;

	private Maze3 maze3Instance;

	void Start () {
		BeginGame ();
	}

	void Update () {
		if (!maze3Instance.isInit&&isInit) {
			maze3Instance.AllUnView();
			isInit = false;
		}
		if (!isInit) {
			maze3Instance.ChangeLayer (PlayerPosition ());
		}
		if (!isInit && !isStart) {
			isStart = true;
			StartCoroutine (maze3Instance.UnViewList ());
		}
	}

	private void BeginGame(){
		maze3Instance = Instantiate (maze3Prefab) as Maze3;
		StartCoroutine (maze3Instance.Generate ());
	}

	private IntVector2 PlayerPosition(){
		float x = player.transform.position.x;
		float z = player.transform.position.z;
		return (new IntVector2 ((int)(x + 9.5), (int)(z + 9.5)));
	}
}
