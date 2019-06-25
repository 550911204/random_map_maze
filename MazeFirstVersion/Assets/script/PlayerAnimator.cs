using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

	public IntVector2 coordinates;

	private Rigidbody rBody;

	private Animator animator = null;

	private int leftRight = 0;

	private int forwardBack = 0;

	void Start () {
		animator = GetComponent<Animator> ();

		rBody = GetComponent<Rigidbody> ();
	}

	void Update () {
		CartoonMove ();
	
	}

	private void CartoonMove(){
		if (Input.GetKey (KeyCode.A) && leftRight != 1) {
			animator.SetInteger ("ActionID", 3);
			leftRight = -1;
		}
		if (Input.GetKey (KeyCode.D) && leftRight != -1) {
			animator.SetInteger ("ActionID", -3);
			leftRight = 1;
		}
		if (Input.GetKey (KeyCode.W) && forwardBack != -1) {
			animator.SetInteger ("ActionID", 1);
			forwardBack = 1;
		}
		if (Input.GetKey (KeyCode.S) && forwardBack != 1) {
			animator.SetInteger ("ActionID", -1);
			forwardBack = -1;
		}
		if(Input.GetKey(KeyCode.Space))
			animator.SetInteger ("ActionID", 4);
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.Space)) {
			leftRight = forwardBack = 0;
			if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.Space))
				animator.SetInteger ("ActionID", 0);
			else {
				animator.SetInteger ("ActionID", 0);
				if (Input.GetKey (KeyCode.A) && leftRight != 1) {
					animator.SetInteger ("ActionID", 3);
					leftRight = -1;
				}
				if (Input.GetKey (KeyCode.D) && leftRight != -1) {
					animator.SetInteger ("ActionID", -3);
					leftRight = 1;
				}

				if (Input.GetKey (KeyCode.W) && forwardBack != -1) {
					animator.SetInteger ("ActionID", 1);
					forwardBack = 1;
				}
				if (Input.GetKey (KeyCode.S) && forwardBack != 1) {
					animator.SetInteger ("ActionID", -1);
					forwardBack = -1;
				}
			}
		
		}
	}
}
