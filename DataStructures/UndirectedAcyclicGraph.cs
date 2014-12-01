using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class UndirectedAcyclicGraph<TVertex> : IUndirectedGraph<TVertex>
	{
		private Dictionary<TVertex, HashSet<TVertex>> adjacencyList;
		public int VertexCount { get { return adjacencyList.Count; } }
		public int EdgeCount { get; protected set; }

		public UndirectedAcyclicGraph()
		{
			this.adjacencyList = new Dictionary<TVertex, HashSet<TVertex>>();
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
				this.adjacencyList.Add(vertex, new HashSet<TVertex>());
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
			
			return this.adjacencyList[vertex];
		}

		public void Connect(TVertex vertex1, TVertex vertex2)
		{
			this.addEdge(vertex1, vertex2);
			this.addEdge(vertex2, vertex1);
		}

		private void addEdge(TVertex source, TVertex target)
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
				this.adjacencyList[source].Add(target);
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

		public bool AreConnected(TVertex vertex1, TVertex vertex2)
		{
			return this.containsEdge(vertex1, vertex2) && this.containsEdge(vertex2, vertex1);
		}

		private bool containsEdge(TVertex source, TVertex target)
		{
			return this.ContainsVertex(source) && this.ContainsVertex(target)
				&& this.adjacencyList[source].Contains(target);
		}
	}
}
