using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Maze2 : MonoBehaviour {

	public bool isInit = true;

	public IntVector2 size; 

	public float generationStepDelay; 

	public MazeCell cellPrefab;

	public MazeWall wallPrefab;

	public MazePassage passagePrefab;

	private MazeCell[,] cells;

	private MazeWall[,,] walls;

	private int[,] mazeWallNum;

	public IEnumerator Generate(){
		WaitForSeconds delay = new WaitForSeconds (generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		walls = new MazeWall[size.x, size.z, 4];
		mazeWallNum = new int [size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell> ();
		DoFirstGenerationStep (activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep (activeCells);
		}
		isInit = false;
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
		walls [cell.coordinates.x, cell.coordinates.z, mazeWallNum [cell.coordinates.x, cell.coordinates.z]] = wall;
		wall.Initialize (cell, otherCell, direction, mazeWallNum[cell.coordinates.x, cell.coordinates.z]);
		mazeWallNum [cell.coordinates.x, cell.coordinates.z]++;
		if (otherCell != null) {
			wall = Instantiate (wallPrefab) as MazeWall;
			walls [otherCell.coordinates.x, otherCell.coordinates.z, mazeWallNum [otherCell.coordinates.x, otherCell.coordinates.z]] = wall;
			wall.Initialize (otherCell, cell, direction.GetOpposite (), mazeWallNum[otherCell.coordinates.x, otherCell.coordinates.z]);
			mazeWallNum [otherCell.coordinates.x, otherCell.coordinates.z]++;
		}
	}

	public void ChangeLayer(IntVector2 coordinates){
		IntVector2 coor1 = new IntVector2 (coordinates.x - 1, coordinates.z - 1);
		IntVector2 coor2 = new IntVector2 (coordinates.x + 1, coordinates.z + 1);
		if (coor1.x < 0)coor1.x = 0;
		if (coor1.z < 0)coor1.z = 0;
		if (coor2.x >= size.x)coor2.x = size.x - 1;
		if (coor2.z >= size.z)coor2.z = size.z - 1;

		for(int x = coor1.x; x <= coor2.x; x++){
			for(int z = coor1.z; z <= coor2.z; z++){
				if ((x - coordinates.x) * (x - coordinates.x) + (z - coordinates.z) * (z - coordinates.z) <= 1) {
					GameObject obj = transform.Find ("Maze Cell " + x + ", " + z).gameObject;
					for (int num = 0; num < mazeWallNum [x, z]; num++) {
						GameObject objChild = obj.transform.Find ("Maze Wall " + x + ", " + z + ", " + num).gameObject;
						objChild.layer = LayerMask.NameToLayer ("viewWall");
						GameObject objectGrandChild = objChild.transform.GetChild (0).gameObject;
						objectGrandChild.layer = LayerMask.NameToLayer ("viewWall");
					}
				}
			}
		}
	}

	public void AllUnView(){
		for(int x = 0; x < size.x; x++){
			for(int z = 0; z < size.z; z++){
				GameObject obj = transform.Find ("Maze Cell " + x + ", " + z).gameObject;
				for (int num = 0; num < mazeWallNum [x, z]; num++) {
					GameObject objChild = obj.transform.Find ("Maze Wall " + x + ", " + z + ", " + num).gameObject;
					objChild.layer = LayerMask.NameToLayer ("unviewWall");
					GameObject objectGrandChild = objChild.transform.GetChild (0).gameObject;
					objectGrandChild.layer = LayerMask.NameToLayer ("unviewWall");
				}
			}
		}
	}
}
