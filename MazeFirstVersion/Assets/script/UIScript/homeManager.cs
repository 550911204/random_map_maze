using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homeManager : MonoBehaviour {

	private bool isCommonDown;

	private bool isInstructionDown;

	public GameObject instructionTexture;

	public GameObject commonModel;

	public GameObject timeModel;

	public GameObject instruction;

	public GameObject exit;

	public GameObject panel;

	public void CommonDown(){
		if (!isCommonDown) {
			panel.SetActive (true);
			timeModel.SetActive (false);
			instruction.SetActive (false);
			exit.SetActive (false);
			isCommonDown = true;
		} else {
			panel.SetActive (false);
			timeModel.SetActive (true);
			instruction.SetActive (true);
			exit.SetActive (true);
			isCommonDown = false;
		}
	}

	public void GoByeBye(){
		Application.LoadLevel ("byebye");	
	}

	public void InstructionDown(){
		if (!isInstructionDown) {
			commonModel.SetActive (false);
			timeModel.SetActive (false);
			exit.SetActive (false);
			instruction.transform.position = new Vector3 (12.4f, -22.8f, 0.0f);
			instructionTexture.SetActive (true);
			isInstructionDown = true;
		} else {
			commonModel.SetActive (true);
			timeModel.SetActive (true);
			exit.SetActive (true);
			instruction.transform.position = new Vector3 (12.4f, -23.5f, 0.0f);
			instructionTexture.SetActive (false);
			isInstructionDown = false;
		}
	}

	public void GoEasy(){
		Application.LoadLevel ("loading_easy");	
	}

	public void GoMiddle(){
		Application.LoadLevel ("loading_middle");	
	}

	public void GoHard(){
		Application.LoadLevel ("loading_hard");	
	}

	public void GoTime(){
		Application.LoadLevel ("loading_time");	
	}

}
