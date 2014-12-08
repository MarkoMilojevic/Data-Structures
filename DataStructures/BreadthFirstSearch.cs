using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public class BreadthFirstSearch<TVertex>
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

			HashSet<TVertex> visited = new HashSet<TVertex> { source };
			Queue<TVertex> queue = new Queue<TVertex>();
			queue.Enqueue(source);
			while (queue.Count > 0)
			{
				TVertex toSpan = queue.Dequeue();
				yield return toSpan;
				foreach (TVertex vertex in graph.GetNeighbours(toSpan).Where(v => !visited.Contains(v)))
				{
					visited.Add(vertex);
					queue.Enqueue(vertex);
				}
			}
		}
	}
}
