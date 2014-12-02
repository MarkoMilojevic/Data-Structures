using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class DepthFirstSearch<TVertex>
	{
		public static IEnumerable<TVertex> GetReachableVertices(IGraph<TVertex> graph, TVertex source)
		{
			if (graph == null)
			{
				throw new ArgumentNullException();
			}

			if (!graph.ContainsVertex(source))
			{
				throw new ArgumentException("Vertex not contained in graph");
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
					foreach (var neighbour in neighbours.Reverse())
					{
						stack.Push(neighbour);
					}
				}
			}
		}
	}
}
