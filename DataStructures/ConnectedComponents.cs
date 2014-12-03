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
				throw new ArgumentNullException();
			}

			HashSet<TVertex> visited = new HashSet<TVertex>();
			int componentsCount = 0;
			foreach (TVertex vertex in graph.GetVertices().Where(v => !visited.Contains(v)))
			{
				IEnumerable<TVertex> reachable = DepthFirstSearch<TVertex>.GetReachableVertices(graph, vertex);
				visited.UnionWith(reachable);
				componentsCount++;
			}

			return componentsCount;
		}
	}
}
