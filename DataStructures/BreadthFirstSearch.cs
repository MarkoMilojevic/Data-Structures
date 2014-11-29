using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class BreadthFirstSearch<TVertex>
	{
		private IGraph<TVertex> graph;

		public BreadthFirstSearch(IGraph<TVertex> graph)
		{
			this.graph = graph;
		}

		public IEnumerable<TVertex> GetReachableVertices(TVertex source)
		{
			if (this.graph == null || source == null)
			{
				throw new ArgumentNullException();
			}

			HashSet<TVertex> visited = new HashSet<TVertex> { source };
			Queue<TVertex> queue = new Queue<TVertex>();
			queue.Enqueue(source);
			while (queue.Count > 0)
			{
				TVertex nextVertexToExpand = queue.Dequeue();
				yield return nextVertexToExpand;
				foreach (TVertex vertex in graph.GetNeighbours(nextVertexToExpand).Where(vertex => !visited.Contains(vertex)))
				{
					visited.Add(vertex);
					queue.Enqueue(vertex);
				}
			}
		}
	}
}
