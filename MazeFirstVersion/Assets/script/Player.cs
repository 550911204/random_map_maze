using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public IntVector2 coordinates;

	private Rigidbody rBody;

	void Start () {
		rBody = GetComponent<Rigidbody> ();
	}

	void Update () {

	}

	void FixedUpdate(){
		float vertical = Input.GetAxis ("Vertical");

		rBody.velocity = transform.forward * vertical * 2;


		float horizontal = Input.GetAxis ("Horizontal");

		rBody.angularVelocity = transform.up * horizontal * 5;
	}

}
