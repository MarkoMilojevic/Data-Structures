using DataStructures.PriorityQueues;
using DataStructures.UnionFind;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public class KruskalsMST<TVertex>
	{
		private class Edge : IComparable<Edge>
		{
			public TVertex Vertex1;
			public TVertex Vertex2;
			public double Weight;

			public Edge(TVertex vertex1, TVertex vertex2, double weight)
			{
				this.Vertex1 = vertex1;
				this.Vertex2 = vertex2;
				this.Weight = weight;
			}

			public int CompareTo(Edge edge)
			{
				return this.Weight.CompareTo(edge.Weight);
			}
		}

		public static IEnumerable<Tuple<TVertex, TVertex>> GetMST(UndirectedWeightedGraph<TVertex> graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph", "Specify a non-null argument.");
			}

			if (graph.VertexCount == 0)
			{
				yield break;
			}

			if (ConnectedComponents<TVertex>.GetConnectedComponentsCount(graph) > 1)
			{
				throw new InvalidOperationException("Graph is not connected.");
			}

			PriorityQueue<Edge> queue = new PriorityQueue<Edge>();
			UnionFind<TVertex> connectedComponents = new UnionFind<TVertex>();
			foreach(TVertex vertex in graph.GetVertices())
			{
				connectedComponents.Add(vertex);
				foreach(TVertex neighbour in graph.GetNeighbours(vertex))
				{
					queue.Enqueue(new Edge(vertex, neighbour, graph.GetEdgeWeight(vertex, neighbour)));
				}
			}

			List<Tuple<TVertex, TVertex>> mst = new List<Tuple<TVertex, TVertex>>();
			while (!queue.IsEmpty())
			{
				Edge minCostEdge = queue.Dequeue();
				if (!connectedComponents.AreConnected(minCostEdge.Vertex1, minCostEdge.Vertex2))
				{
					yield return Tuple.Create(minCostEdge.Vertex1, minCostEdge.Vertex2);
					connectedComponents.Union(minCostEdge.Vertex1, minCostEdge.Vertex2);
				}
			}
		}
	}
}
