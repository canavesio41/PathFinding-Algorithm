using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStart : PathFinding
{
	List<Nodo> closed = new List<Nodo> ();
	List<Nodo> open = new List<Nodo>();
	List<Nodo> path = new List<Nodo>();


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

	public int GetDistance(Nodo a, Nodo b)
	{
		int distX = Mathf.Abs (a.Row - b.Row);
		int distY = Mathf.Abs (a.Col - b.Col);

		if (distX > distY) 
		{
			return Grid.sharedInstance.horCells * distY + Grid.sharedInstance.verCells * (distX - distY);
		}
		return Grid.sharedInstance.horCells * distX + Grid.sharedInstance.verCells * (distY - distX);
	}

	public override List<Nodo> GetPath (Nodo root, Nodo goal)
	{

		OpenNode(root);
		while (open.Count > 0) 
		{
			Nodo currentNode = getNode ();
			for (int i = 1; i < open.Count; i++) 
			{	
				if (open [i].Weight < currentNode.Weight || open[i].Weight == currentNode.Weight)
				{
					if(open[i].h < currentNode.h)
					{
						currentNode = open[i];
					}
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
				int newCostToNeighbour = n.Weight + GetDistance(currentNode, n);
				if (newCostToNeighbour < n.Weight || !open.Contains (n)) 
				{
					n.Padre = currentNode;
					n.Weight = newCostToNeighbour;
					n.h = GetDistance (n, goal);

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

