using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BreadthFirts : PathFinding
{
	Queue<Nodo> closed = new Queue<Nodo> ();
	Queue<Nodo> open = new Queue<Nodo>();
	List<Nodo> path = new List<Nodo>();

	public void OpenNode (Nodo n)
	{
		open.Enqueue(n);
	}

	public void CloseNode(Nodo n)
	{
		closed.Enqueue(n);
	}

	public Nodo getNode()
	{
		return open.Dequeue();
	}

	public override List<Nodo> GetPath (Nodo root, Nodo goal)
	{

		OpenNode(root);

				while (open.Count > 0) 
				{
					Nodo currentNode = getNode ();
		
					if (currentNode == goal) 
					{
						path.Add (currentNode);
						while(currentNode.Padre != null)
						{
							path.Add (currentNode.Padre);
							currentNode.Padre = currentNode.Padre.Padre;
						}
						path.Reverse ();
						root = null;
						goal = null;
						return path;
					}
		
					CloseNode(currentNode);

					//currentNode.Adj = Grid.sharedInstance.GetAdj (currentNode);
		
					for (int i = 0; i < currentNode.Adj.Count; i++) 
					{
						Nodo n = currentNode.Adj [i];
						if ( !n.walkable ||!closed.Contains (n)) 
						{
							OpenNode(n);
							n.Padre = currentNode;
						}
					}
				}
				return null;
			}
	}



//	List<Nodo> closed = new List<Nodo> ();
//	List<Nodo> open = new List<Nodo>();
//
//	public List<Nodo> GetPath(Nodo root, Nodo goal)
//	{
//		Queue<Nodo> closed = new Queue<Nodo> ();
//		Queue<Nodo> open = new Queue<Nodo>();
//
//		List<Nodo> path = new List<Nodo>();
//
//		open.Enqueue (root);
//	
//		while (open.Count > 0) 
//		{
//			Nodo currentNode = open.Dequeue ();
//
//			if (currentNode == goal) 
//			{
//				path.Add (currentNode);
//				while(currentNode.Padre != null)
//				{
//					path.Add (currentNode.Padre);
//					currentNode.Padre = currentNode.Padre.Padre;
//				}
//				path.Reverse ();
//				return path;
//			}
//
//			closed.Enqueue (currentNode);
//			currentNode.Adj = Grid.sharedInstance.GetAdj (currentNode);
//
//			for (int i = 0; i < currentNode.Adj.Count; i++) 
//			{
//				Nodo n = currentNode.Adj [i];
//				if (!closed.Contains (n)) 
//				{
//					//closed.Enqueue (n);
//					open.Enqueue (n);
//					n.Padre = currentNode;
//				}
//			}
//		}
//		return null;
//	}
//
//
//	
//
//
//	public void OpenNode(Nodo n)
//	{
//		open.Add (n);
//
//		if (closed.Contains (n)) 
//		{
//			closed.Remove (n);
//		}
//	}
//
//	public void CloseNode(Nodo n)
//	{
//		closed.Add (n);
//
//		if (open.Contains (n)) 
//		{
//			open.Remove (n);
//		}
//
//	}
//	public void GetPathButtom()
//	{
//		List<Nodo> Path; 
//		Path = Dijktra (Grid.sharedInstance.cellsArray [1, 4], Grid.sharedInstance.cellsArray [5, 7]);
//		//Path = GetPath (Grid.sharedInstance.cellsArray[1,4], Grid.sharedInstance.cellsArray[5,7]);
//	
//		Debug.Log (Path.Count);
//		for (int i = 0; i < Path.Count; i++) 
//		{
//			Nodo n = Path [i];
//			if(n != Grid.sharedInstance.cellsArray[1,4] && n != Grid.sharedInstance.cellsArray[5,7])
//				Path [i].GetComponent<Renderer> ().material.color = Color.green;
//		}
//			
//	}
//}
