using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public class ConnectedComponents<TVertex>
	{
		public static int GetConnectedComponentsCount(IGraph<TVertex> graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph", "Specify a non-null argument.");
			}

			HashSet<TVertex> visited = new HashSet<TVertex>();
			int count = 0;
			foreach (TVertex vertex in graph.GetVertices().Where(v => !visited.Contains(v)))
			{
				IEnumerable<TVertex> reachable = DepthFirstSearch<TVertex>.GetReachableVertices(graph, vertex);
				visited.UnionWith(reachable);
				count++;
			}

			return count;
		}
	}
}
