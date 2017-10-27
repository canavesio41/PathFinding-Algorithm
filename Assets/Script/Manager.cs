using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	public Nodo r = null;
	public Nodo g = null;
	public PathFinding p;
	public List<Nodo> Path  = new List<Nodo> ();
	private Nodo lastR = null;
	private Nodo lastG = null;
	private RaycastHit raycastHit = new RaycastHit ();
	public Text ObjectText;

	public enum Type
	{
		BreadthFirts,
		DeepFirts,
		Dijktra,
		Astar,
		nothing
	}

	public Type find;

	public void changeToBreadthFirts()
	{
		find = Type.BreadthFirts;
	}
	public void changeToDeepFirts()
	{
		find = Type.DeepFirts;
	}
	public void changeToDijktra()
	{
		find = Type.Dijktra;
	}
	public void changeToAstar()
	{
		find = Type.Astar;
	}

	public void Update()
	{
		
		SetRootAndGoal ();

		if (lastR != null && lastR != r) 
		{
			lastR.GetComponent<Renderer> ().material.color = Color.white;
		} 

		if (lastG != null && lastG != g) 
		{
			lastG.GetComponent<Renderer> ().material.color = Color.white;
		} 

	}

	public void SetRootAndGoal()
	{

		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out raycastHit))
		{
			ObjectText.text = raycastHit.collider.gameObject.GetComponent<Nodo> ().Weight.ToString();
			Vector3 newPos = raycastHit.transform.position;
			newPos.z = -1;
			//newPos.x += 1.5f;
			ObjectText.transform.position = newPos;

			if (Input.GetMouseButtonDown (0)) 
			{
				if(r != null)
				{
					r = null;	
					raycastHit.collider.gameObject.GetComponent<Renderer> ().material.color = Color.white; 
				}
				else
				{
					raycastHit.collider.gameObject.GetComponent<Renderer> ().material.color = Color.blue; 
					r = raycastHit.collider.gameObject.GetComponent<Nodo> ();
					lastR = r;
				}

			}
			if (Input.GetMouseButtonDown (1)) 
			{
				if(g != null)
				{
					g = null;	
					raycastHit.collider.gameObject.GetComponent<Renderer> ().material.color = Color.white; 
				}
				else
				{
					raycastHit.collider.gameObject.GetComponent<Renderer> ().material.color = Color.red; 
					g = raycastHit.collider.gameObject.GetComponent<Nodo> ();
					lastG = g;
				}
			}

			if (Input.GetMouseButtonDown (2)) 
			{
				raycastHit.collider.gameObject.GetComponent<Renderer> ().material.color = Color.black; 
				raycastHit.collider.gameObject.GetComponent<Nodo> ().walkable = false;
			}
		}
	}


	public void Clear()
	{
		find = Type.nothing;
		r = null;
		g = null;

		if (p != null)
		{
			p = null;
		}

		if (Path.Count >= 0) 
		{
			Path.Clear ();
		}

		for(int x = 0; x < Grid.sharedInstance.horCells; x++)
		{
			for(int y = 0; y < Grid.sharedInstance.verCells; y++)
			{
				Grid.sharedInstance.cellsArray [x, y].Padre = null;
				Grid.sharedInstance.cellsArray [x, y].walkable = true;
				Grid.sharedInstance.cellsArray [x, y].GetComponent<Renderer> ().material.color = Color.white;
			}
		}


	}
		
	public void Play()
	{
		switch (find)
		{
			case Type.BreadthFirts:
				p = new BreadthFirts ();
				break;
			case Type.DeepFirts:
				p =  new DeepFirts ();
				break;
			case Type.Dijktra:
				p =  new Dijktra ();
				break;
			case Type.Astar:
				p =  new AStart ();
				break;
			default:
				p = null;
				break;
		}
			
		Path = p.GetPath (r, g); 

		for (int i = 0; i < Path.Count; i++) 
		{
			Nodo n = Path [i];
			if(n != r && n != g && n == Path [i].walkable )
				Path [i].GetComponent<Renderer> ().material.color = Color.green;
		}

	}
}

