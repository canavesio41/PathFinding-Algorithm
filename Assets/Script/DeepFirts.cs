using System;
using System.Collections.Generic;

	public class DeepFirts : PathFinding
	{
		Stack<Nodo> closed = new Stack<Nodo> ();
		Stack<Nodo> open = new Stack<Nodo>();
		List<Nodo> path = new List<Nodo>();

		public void OpenNode (Nodo n)
		{
			open.Push(n);
		}

		public void CloseNode(Nodo n)
		{
			closed.Push(n);
		}

		public Nodo getNode()
		{
			return open.Pop();
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

				for (int i = 0; i < currentNode.Adj.Count; i++) 
				{
					Nodo n = currentNode.Adj [i];
					if (!n.walkable || closed.Contains (n))
					{
						OpenNode(n);
						n.Padre = currentNode;
					}
				}
			}
			return null;
	}

}

