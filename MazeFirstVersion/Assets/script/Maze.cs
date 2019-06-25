﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Maze : MonoBehaviour {

	public int clockCount;

	public bool isLife = false;

	public bool isStart = false;

	public bool isClock = false;

	public Clock clockPrefab;

	public ThirdPersonCharacter thirdPrefab;

	public IntVector2 size; 

	public float generationStepDelay; 

	public MazeCell cellPrefab;

	public MazeWall wallPrefab;

	public MazePassage passagePrefab;

	private MazeCell[,] cells;

	private int[,] mazeWallNum;

	public void Update(){
		if (isLife&&(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.Space))){
			isStart = true;
			isClock = true;
			isLife = false;
		}
	}

	public IEnumerator Generate(){
		WaitForSeconds delay = new WaitForSeconds (generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		mazeWallNum = new int [size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell> ();
		DoFirstGenerationStep (activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep (activeCells);
		}
		for (int i = 0; i < size.x; i++)
			for (int j = 0; j < size.z; j++) {
				if ((i != 0 && j != size.z - 1) || (i != size.x - 1 && j != 0))
					CreateClock (new IntVector2 (i, j));
			}
		CreatePlayer ();
		clockCount = transform.childCount;
	}

	private void DoFirstGenerationStep(List<MazeCell> activeCells){
		activeCells.Add (CreateCell (RandomCoordinates));
	}

	private void DoNextGenerationStep(List<MazeCell> activeCells){
		int currentIndex = activeCells.Count - 1;//middle
		//int currentIndex = Random.Range(0,activeCells.Count);//随机easy
		//int currentIndex = 0;//super easy
		//int currentIndex = (int)activeCells.Count/2;//super easy
		MazeCell currentCell = activeCells [currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt (currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2 ();
		if (ContainsCoordinates (coordinates)) {
			MazeCell neighbor = GetCell (coordinates);
			if (neighbor == null) {
				neighbor = CreateCell (coordinates);
				CreatePassage (currentCell, neighbor, direction);
				activeCells.Add (neighbor);
			} else {
				CreateWall (currentCell, neighbor, direction);
			}
		} else {
			CreateWall (currentCell, null, direction);
		}
	}

	public MazeCell CreateCell(IntVector2 coordinates){
		MazeCell newCell = Instantiate (cellPrefab) as MazeCell;
		cells [coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	public IntVector2 RandomCoordinates{
		get{ 
			return new IntVector2 (Random.Range (0, size.x), Random.Range (0, size.z));
		}
	}

	public bool ContainsCoordinates(IntVector2 coordinate){
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public MazeCell GetCell(IntVector2 coordinates){
		return cells [coordinates.x, coordinates.z];
	}

	private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction){
		MazePassage passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (cell, otherCell, direction, -1);
		passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (otherCell, cell, direction.GetOpposite (), -1);
	}

	private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction){
		MazeWall wall = Instantiate (wallPrefab) as MazeWall;
		wall.Initialize (cell, otherCell, direction, mazeWallNum[cell.coordinates.x, cell.coordinates.z]);
		mazeWallNum [cell.coordinates.x, cell.coordinates.z]++;
		if (otherCell != null) {
			wall = Instantiate (wallPrefab) as MazeWall;
			wall.Initialize (otherCell, cell, direction.GetOpposite (), mazeWallNum [otherCell.coordinates.x, otherCell.coordinates.z]);
			mazeWallNum [otherCell.coordinates.x, otherCell.coordinates.z]++;
		}
	}

	public void CreatePlayer(){
		ThirdPersonCharacter third = Instantiate (thirdPrefab) as ThirdPersonCharacter;
		IntVector2 newCoordinates = new IntVector2 (0, 0);
		third.name = "player";
		third.transform.parent = transform;
		third.transform.localPosition = new Vector3 (newCoordinates.x - size.x * 0.5f + 0.5f, 0f, newCoordinates.z - size.z * 0.5f + 0.5f);
		isLife = true;
	}

	public void CreateClock(IntVector2 coordinates){
		Clock newClock = Instantiate (clockPrefab) as Clock;
		newClock.name = "Clock " + coordinates.x + ", " + coordinates.z;
		newClock.transform.parent = transform;
		newClock.transform.localPosition = new Vector3 (coordinates.x - size.x * 0.5f + 0.5f, 0.3f, coordinates.z - size.z * 0.5f + 0.5f);
	}
}
