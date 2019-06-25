using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSetPos : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.R)) {
			Vector3 pos = player.transform.position;
			player.transform.position = new Vector3 (pos.x, pos.y + 0.1f, pos.z);
		}
	}
}
