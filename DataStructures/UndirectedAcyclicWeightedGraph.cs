using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class UndirectedAcyclicWeightedGraph<TVertex> : IUndirectedGraph<TVertex>, IWeightedGraph<TVertex>
	{
		private Dictionary<TVertex, Dictionary<TVertex, double>> adjacencyList;
		public int VertexCount { get { return adjacencyList.Count; } }
		public int EdgeCount { get; protected set; }

		public UndirectedAcyclicWeightedGraph()
		{
			this.adjacencyList = new Dictionary<TVertex, Dictionary<TVertex, double>>();
			this.EdgeCount = 0;
		}

		public IEnumerable<TVertex> GetVertices()
		{
			return this.adjacencyList.Keys;
		}

		public void AddVertex(TVertex vertex)
		{
			if (!this.ContainsVertex(vertex))
			{
				this.adjacencyList.Add(vertex, new Dictionary<TVertex, double>());
			}
		}

		public void RemoveVertex(TVertex vertex)
		{
			if (this.ContainsVertex(vertex))
			{
				this.adjacencyList.Remove(vertex);
				foreach (TVertex v in this.adjacencyList.Keys)
				{
					if (this.containsEdge(v, vertex))
					{
						this.removeEdge(v, vertex);
					}
				}
			}
		}

		public bool ContainsVertex(TVertex vertex)
		{
			return this.adjacencyList.ContainsKey(vertex);
		}

		public IEnumerable<TVertex> GetNeighbours(TVertex vertex)
		{
			if (!this.ContainsVertex(vertex))
			{
				throw new ArgumentException("Vertex not contained in graph");
			}

			return this.adjacencyList[vertex].Keys;
		}

		public void Connect(TVertex vertex1, TVertex vertex2, double weight)
		{
			this.addEdge(vertex1, vertex2, weight);
			this.addEdge(vertex2, vertex1, weight);
		}

		private void addEdge(TVertex source, TVertex target, double weight)
		{
			if (!this.ContainsVertex(source) || !this.ContainsVertex(target))
			{
				throw new ArgumentException("Vertices not contained in graph");
			}

			if (source.Equals(target))
			{
				throw new ArgumentException("Self-loops not allowed");
			}

			if (!this.containsEdge(source, target))
			{
				this.adjacencyList[source].Add(target, weight);
				this.EdgeCount++;
			}
		}

		public void Disconnect(TVertex vertex1, TVertex vertex2)
		{
			this.removeEdge(vertex1, vertex2);
			this.removeEdge(vertex2, vertex1);
		}

		private void removeEdge(TVertex source, TVertex target)
		{
			if (this.containsEdge(source, target))
			{
				this.adjacencyList[source].Remove(target);
				this.EdgeCount--;
			}
		}

		public double GetEdgeWeight(TVertex source, TVertex target)
		{
			if (!this.containsEdge(source, target))
			{
				return double.PositiveInfinity;
			}

			if (source.Equals(target))
			{
				return 0;
			}

			return this.adjacencyList[source][target];
		}

		public bool AreConnected(TVertex vertex1, TVertex vertex2)
		{
			return this.containsEdge(vertex1, vertex2) && this.containsEdge(vertex2, vertex1);
		}

		private bool containsEdge(TVertex source, TVertex target)
		{
			return this.ContainsVertex(source) && this.ContainsVertex(target)
				&& this.adjacencyList[source].ContainsKey(target);
		}
	}
}
