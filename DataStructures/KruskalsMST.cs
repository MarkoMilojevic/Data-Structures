using DataStructures.PriorityQueues;
using DataStructures.UnionFind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		private UndirectedAcyclicWeightedGraph<TVertex> graph;

		public KruskalsMST(UndirectedAcyclicWeightedGraph<TVertex> graph)
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
			PriorityQueue<Edge> queue = new PriorityQueue<Edge>();
			UnionFind<TVertex> connectedComponents = new UnionFind<TVertex>();
			foreach(TVertex vertex in this.graph.GetVertices())
			{
				connectedComponents.Add(vertex);
				foreach(TVertex neighbour in this.graph.GetNeighbours(vertex))
				{
					queue.Enqueue(new Edge(vertex, neighbour, this.graph.GetEdgeWeight(vertex, neighbour)));
				}
			}

			while (!queue.IsEmpty())
			{
				Edge minCostEdge = queue.Dequeue();
				if (!connectedComponents.AreConnected(minCostEdge.Vertex1, minCostEdge.Vertex2))
				{
					mst.Add(new Tuple<TVertex, TVertex>(minCostEdge.Vertex1, minCostEdge.Vertex2));
					connectedComponents.Union(minCostEdge.Vertex1, minCostEdge.Vertex2);
				}
			}

			if (connectedComponents.Count > 1)
			{
				throw new ArgumentException("Graph not connected");
			}

			return mst;
		}
	}
}
