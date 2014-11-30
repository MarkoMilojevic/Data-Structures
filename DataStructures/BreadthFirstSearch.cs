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
			if (graph == null)
			{
				throw new ArgumentNullException();
			}

			this.graph = graph;
		}

		public IEnumerable<TVertex> GetReachableVertices(TVertex source)
		{
			if (!this.graph.ContainsVertex(source))
			{
				throw new ArgumentException("Vertex not contained in graph");
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
