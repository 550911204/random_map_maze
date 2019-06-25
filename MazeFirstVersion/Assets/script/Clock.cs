using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * 90 * RealTime.deltaTime);
	}

	private void OnTriggerEnter(Collider other){
		Destroy (gameObject);
	}
}
