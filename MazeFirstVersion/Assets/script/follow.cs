using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {

	public GameObject gameManager;

	public Transform character; 
	public float smoothTime = 0.01f; 
	private Vector3 cameraVelocity = Vector3.zero;
	private Camera mainCamera; 

	void Awake () 
	{ 
		mainCamera = Camera.main;
	}

	void Update()
	{
		if (!(gameManager.GetComponent<GameManager2> ().isInit)) {
			transform.position = Vector3.SmoothDamp (transform.position, character.position + new Vector3 (-2f, 3f, -1.2f), ref cameraVelocity, smoothTime);
			transform.localEulerAngles = new Vector3 (45.76f, 53.59f, -1.95f);
		}
	}
}
