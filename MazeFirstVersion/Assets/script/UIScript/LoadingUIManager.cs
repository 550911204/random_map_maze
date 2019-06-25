using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUIManager : MonoBehaviour {

	public UISlider progressBar;

	public string nextSceenName;

	AsyncOperation op;

	private float progress;

	private float target;

	private float timer;

	void Start () {
		op = Application.LoadLevelAsync (nextSceenName);
		op.allowSceneActivation = false;
		progressBar.value = 0;
		StartCoroutine (progressLoading());
	}

	void Update () {
		progressBar.value = Mathf.Lerp (progressBar.value, target, timer * 0.02f);
		timer += Time.deltaTime;
		if (progressBar.value >= 0.99f && timer > 3) {
			progressBar.value = 1;
			op.allowSceneActivation = true;
		}
	}

	IEnumerator progressLoading(){
		while (true) {
			target = op.progress;
			if (target >= 0.9f) {
				target = 1;
				yield break;
			}
			yield return 0;
		}
	}

}
