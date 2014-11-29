using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class DijkstraShortestPath<TVertex>
	{
		private DirectedWeightedGraph<TVertex> graph;

		public DijkstraShortestPath(DirectedWeightedGraph<TVertex> graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException();
			}

			this.graph = graph;
		}

		public Dictionary<TVertex, TVertex> GetParentsMap(TVertex source)
		{
			if (!this.graph.ContainsVertex(source))
			{
				throw new ArgumentException("Vertex not contained in graph");
			}

			Dictionary<TVertex, double> shortestPathsFromSource = new Dictionary<TVertex, double>();
			shortestPathsFromSource.Add(source, 0);
			Dictionary<TVertex, TVertex> parentMap = new Dictionary<TVertex, TVertex>();
			foreach (TVertex vertex in this.graph.GetVertices().Where(v => !v.Equals(source)))
			{
				double edgeWeightFromSource = this.graph.GetEdgeWeight(source, vertex);
				shortestPathsFromSource.Add(vertex, edgeWeightFromSource);
				if (edgeWeightFromSource < double.PositiveInfinity)
				{
					parentMap.Add(vertex, source);
				}
			}

			HashSet<TVertex> visited = new HashSet<TVertex> { source };
			while (visited.Count < this.graph.VertexCount)
			{
				TVertex closestVertex = default(TVertex);
				TVertex closestVertexPredecessor = default(TVertex);
				double distanceToClosestVertex = double.PositiveInfinity;
				bool expanded = false;
				foreach (TVertex vertex in this.graph.GetVertices().Where(v => visited.Contains(v)))
				{
					foreach (TVertex neighbour in this.graph.GetNeighbours(vertex).Where(v => !visited.Contains(v)))
					{
						double edgeWeight = this.graph.GetEdgeWeight(vertex, neighbour);
						if (shortestPathsFromSource[vertex] + edgeWeight < distanceToClosestVertex)
						{
							distanceToClosestVertex = shortestPathsFromSource[vertex] + edgeWeight;
							closestVertex = neighbour;
							closestVertexPredecessor = vertex;
							expanded = true;
						}
					}
				}

				if (!expanded)
				{
					break;
				}
				
				visited.Add(closestVertex);
				shortestPathsFromSource[closestVertex] = distanceToClosestVertex;
				if (parentMap.ContainsKey(closestVertex))
				{
					parentMap[closestVertex] = closestVertexPredecessor;
				}
				else
				{
					parentMap.Add(closestVertex, closestVertexPredecessor);
				}
			}

			return parentMap;
		}
	}
}
