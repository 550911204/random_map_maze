using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jerryGameUIManager : MonoBehaviour {

	public float timer;

	public string nextLevelName;

	void Start () {
		
	}

	void Update () {
		timer += Time.deltaTime; 
		if (timer >= 6f) {
			Application.LoadLevel (nextLevelName);
		}
	}
}
