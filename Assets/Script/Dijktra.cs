using System;
using UnityEngine;
using System.Collections.Generic;

	public class Dijktra : PathFinding
	{
		List<Nodo> closed = new List<Nodo> ();
		List<Nodo> open = new List<Nodo>();

	public void OpenNode (Nodo n)
	{
		open.Add(n);
	}

	public void CloseNode(Nodo n)
	{
		closed.Add(n);
	}

	public Nodo getNode()
	{
		return open [0];
	}

//	public List<Nodo> RetracePath(Nodo root, Nodo goal) 
//	{
//		List<Nodo> path = new List<Nodo>();
//		Nodo currentNode = goal;
//
//		while (currentNode != root) 
//		{
//			path.Add(currentNode);
//			currentNode = currentNode.Padre;
//		}
//		path.Reverse();
//		return path;
//	}

	public override List<Nodo> GetPath (Nodo root, Nodo goal)
	{
		List<Nodo> path = new List<Nodo>();

		path.Clear ();
		OpenNode(root);
		while (open.Count > 0) 
		{
			Nodo currentNode = getNode ();
			for (int i = 1; i < open.Count; i++) 
			{	
				if (open [i].Weight < currentNode.Weight || open[i].Weight == currentNode.Weight)
				{
					currentNode = open[i];
				}
			}
			open.Remove (currentNode);
			CloseNode (currentNode);

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
			for (int i = 0; i < currentNode.Adj.Count; i++) 
			{
				Nodo n = currentNode.Adj [i];
				if (!n.walkable || closed.Contains (n))
				{
					continue;
				}
				int newCostToNeighbour = n.Weight + currentNode.Weight;
				if (newCostToNeighbour < n.Weight || !open.Contains (n)) 
				{
					n.Padre = currentNode;
					n.Weight = newCostToNeighbour;
					if (!open.Contains (n)) 
					{
						OpenNode(n);
					}
				}
			}
		}
		return null;
	}
}
	

