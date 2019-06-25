using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour {

	public int index;

	public GameObject[] lists;

	public float timeMem;

	public float timer;

	// Use this for initialization
	void Start () {
		index = Random.Range (0, 8);
		timer = 0;
		timeMem = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		if (timer > 0)
			timer -= Time.deltaTime;
		if (timer < 0)
			timer = 0;
		if (timer == 0) {
			Instantiate(lists[index], new Vector3(9.5f,0f,9.5f), Quaternion.identity);
			timer = timeMem;
		}
	}
}
