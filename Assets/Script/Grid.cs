using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid: MonoBehaviour
{
	public GameObject cube;
	public int horCells = 10;
	public int verCells = 10;
	public Vector3 startPos = new Vector3(0f, 0f, 0f);
	public float spacingX = 1f;
	public float spacingY = 1f;
	public Nodo[,] cellsArray;
	public static Grid sharedInstance;



	void Awake()
	{
		sharedInstance = this;
	}

	void Start()
	{
		MakeGrid(horCells, verCells);
	}

	void MakeGrid(int hor, int vert)
	{
		cellsArray = new Nodo[hor,vert];
		GameObject clone;
		Vector3 clonePos;

		GameObject grid = new GameObject("Grid");

		for(int x = 0; x < hor; x++)
		{
			for(int y = 0; y < vert; y++)
			{
				clonePos = new Vector3(startPos.x + (x * -spacingX), startPos.y + (y * -spacingY), startPos.z);
				clone = Instantiate(cube, clonePos, Quaternion.identity) as GameObject;
				clone.name = (y /*+ 1*/) + "x" + (x /*+ 1*/);
				clone.tag = "Node";
				clone.AddComponent <Nodo>();
				clone.GetComponent<Nodo> ().Col = y;///0 + 1;
				clone.GetComponent<Nodo> ().Row = x;// + 1;
				clone.GetComponent<Nodo> ().pos = clone.transform.position;
				clone.transform.SetParent (grid.transform);
				cellsArray[x,y] = clone.GetComponent<Nodo>();
			}
		}

		//Agrego adyacentes
		for (int x = 0; x < hor; x++) 
		{
			for (int y = 0; y < vert; y++) 
			{
				cellsArray[x,y].GetComponent<Nodo> ().Adj = GetAdj (cellsArray[x,y].GetComponent<Nodo> ());
			}
		}


		/*int rHor = Random.Range (0, hor);
		int rVer = Random.Range (0, vert);
		int rHor2 = Random.Range (0, hor);
		int rVer2 = Random.Range (0, vert);

		cellsArray[rHor,rVer].GetComponent<Nodo>().root = true;
		cellsArray[rHor2,rVer2].GetComponent<Nodo>().goal = true;*/

//		cellsArray [1, 4].GetComponent<Nodo>().root = true;
//		cellsArray [5, 7].GetComponent<Nodo>().goal = true;

		grid.transform.position = new Vector3(13f, 8f, 0f);

	}

	public void Update()
	{
		//si hay dos nodos goal o root en la grilla se borra la anterior

		//recorro la grilla
//
//		for (int x = 0; x < horCells; x++) 
//		{
//			for (int y = 0; y < verCells; y++) 
//			{
//				if(cellsArray[x,y].GetComponent<Nodo>().goal == true && !cellsArray[x,y].GetComponent<Nodo>().checkG)
//				{
//					Goals.Add (cellsArray [x, y]);
//					cellsArray [x, y].GetComponent<Nodo> ().checkG = true;
//				}
//
//				if(cellsArray[x,y].GetComponent<Nodo>().root == true && !cellsArray[x,y].GetComponent<Nodo>().checkR)
//				{
//					Roots.Add (cellsArray [x, y]);
//					cellsArray [x, y].GetComponent<Nodo> ().checkR = true;
//				}
//
//			}
//		
//		}
//
//		if (Roots.Count > 1) 
//		{
//			Roots [0].root = false; 
//			Roots [0].checkR = false; 
//			Roots.RemoveAt (0);
//		} 
//			
//		else if(Goals.Count == 0)
//		{
//
//		}
//
//		if (Goals.Count > 1) 
//		{
//			Goals [0].goal = false; 
//			Goals [0].checkG = false; 
//			Goals.RemoveAt (0);
//		}
//
//		else if(Goals.Count == 0)
//		{
//				
//		}

	}

	public List<Nodo> GetAdj(Nodo current)
	{
		List<Nodo> neighbours = new List<Nodo> ();

		for (int ix = current.Row - 1; ix <= current.Row + 1; ix++) {
			for (int iy = current.Col - 1; iy <= current.Col + 1; iy++) {
				if (ix >= 0 && ix < cellsArray.GetLength (0) && iy >= 0 && iy < cellsArray.GetLength (1))// && ix != current.Row && iy != current.Col)
					neighbours.Add (cellsArray [ix, iy]);
			}
		}

		for (int ix = 0; ix < neighbours.Count; ix++) {
			if(neighbours[ix] == current){
				neighbours.Remove (current);
				break;
			}
		}
		//debug only
		string s = string.Empty;

		for (int i = 0; i < neighbours.Count; ++i) {
			s += neighbours[i].name + "\n";
		}
		//Debug.LogFormat ("Adyacentes de {0}: {1}", current.name, s);
		//

		return neighbours;
	}
}