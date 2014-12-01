using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class PrimsMST<TVertex>
	{
		private UndirectedAcyclicWeightedGraph<TVertex> graph;

		public PrimsMST(UndirectedAcyclicWeightedGraph<TVertex> graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException();
			}

			this.graph = graph;
		}

		public List<Tuple<TVertex, TVertex>> GetMST()
		{
			List<Tuple<TVertex, TVertex>> mst = new List<Tuple<TVertex, TVertex>>();
			if (this.graph.VertexCount == 0)
			{
				return mst;
			}

			TVertex source = this.graph.GetVertices().First();
			HashSet<TVertex> visited = new HashSet<TVertex> { source };
			bool expanding = true;
			while (expanding && visited.Count < this.graph.VertexCount)
			{
				double minCost = double.PositiveInfinity;
				Tuple<TVertex, TVertex> edge = new Tuple<TVertex, TVertex>(default(TVertex), default(TVertex));
				expanding = false;
				foreach (TVertex vertex in this.graph.GetVertices().Where(v => visited.Contains(v)))
				{
					foreach (TVertex neighbour in this.graph.GetNeighbours(vertex).Where(v => !visited.Contains(v)))
					{
						double edgeCost = this.graph.GetEdgeWeight(vertex, neighbour);
						if (edgeCost < minCost)
						{
							minCost = edgeCost;
							edge = new Tuple<TVertex, TVertex>(vertex, neighbour);
							expanding = true;
						}
					}
				}

				mst.Add(edge);
				visited.Add(edge.Item2);
			}

			return mst;
		}
	}
}
