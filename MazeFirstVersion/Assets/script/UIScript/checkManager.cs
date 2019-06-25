using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkManager : MonoBehaviour {

	public string home;

	public string byebye;

	public void GoHome(){
		Application.LoadLevel (home);
	}

	public void GoByeBye(){
		Application.LoadLevel (byebye);
	}
}
