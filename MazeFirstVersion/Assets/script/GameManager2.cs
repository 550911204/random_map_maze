using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour {

	public GameObject player;

	public bool isInit = true;

	public Maze2 maze2Prefab;

	private Maze2 maze2Instance;

	void Start () {
		BeginGame ();
	}

	void Update () {
		if (!maze2Instance.isInit&&isInit) {
			maze2Instance.AllUnView();
			isInit = false;
		}
		if (!isInit) {
			maze2Instance.ChangeLayer (PlayerPosition ());
		}
	}

	private void BeginGame(){
		maze2Instance = Instantiate (maze2Prefab) as Maze2;
		StartCoroutine (maze2Instance.Generate ());
	}

	private IntVector2 PlayerPosition(){
		float x = player.transform.position.x;
		float z = player.transform.position.z;
		return (new IntVector2 ((int)(x + 9.5), (int)(z + 9.5)));
	}
}
