﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlock1 : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!(gameManager.GetComponent<GameManager1> ().isInit))
			Destroy (gameObject);
	}
}