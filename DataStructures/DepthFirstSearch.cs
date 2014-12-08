using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public class DepthFirstSearch<TVertex>
	{
		public static IEnumerable<TVertex> GetReachableVertices(IGraph<TVertex> graph, TVertex source)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph", "Specify a non-null argument.");
			}

			if (!graph.ContainsVertex(source))
			{
				throw new InvalidOperationException("Graph does not contain vertex 'source'.");
			}

			HashSet<TVertex> visited = new HashSet<TVertex>();
			Stack<TVertex> stack = new Stack<TVertex>();
			stack.Push(source);
			while (stack.Count != 0)
			{
				TVertex toSpan = stack.Pop();
				if (!visited.Contains(toSpan))
				{
					visited.Add(toSpan);
					yield return toSpan;
					IEnumerable<TVertex> neighbours = graph.GetNeighbours(toSpan).Where(v => !visited.Contains(v));
					foreach (TVertex neighbour in neighbours.Reverse())
					{
						stack.Push(neighbour);
					}
				}
			}
		}
	}
}
