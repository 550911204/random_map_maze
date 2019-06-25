using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstView : MonoBehaviour {

	public float sensitivityx = 10F;

	public float sensitivityy = 10F;


	public float minimumY = -60F;

	public float maximumY = -60F;

	float rotationY = 0F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityx;

		rotationY += Input.GetAxis ("Mouse Y") * sensitivityy;

		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
	}
}
