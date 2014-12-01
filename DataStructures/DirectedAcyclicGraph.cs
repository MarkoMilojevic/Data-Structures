using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class DirectedAcyclicGraph<TVertex> : IDirectedGraph<TVertex>
	{
		private Dictionary<TVertex, HashSet<TVertex>> adjacencyList;
		public int VertexCount { get { return adjacencyList.Count; } }
		public int EdgeCount { get; protected set; }

		public DirectedAcyclicGraph()
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
					if (this.ContainsEdge(v, vertex))
					{
						this.RemoveEdge(v, vertex);
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

		public void AddEdge(TVertex source, TVertex target)
		{
			if (!this.ContainsVertex(source) || !this.ContainsVertex(target))
			{
				throw new ArgumentException("Vertices not contained in graph");
			}

			if (source.Equals(target))
			{
				throw new ArgumentException("Self-loops not allowed");
			}

			if (!this.ContainsEdge(source, target))
			{
				this.adjacencyList[source].Add(target);
				this.EdgeCount++;
			}
		}

		public void RemoveEdge(TVertex source, TVertex target)
		{
			if (this.ContainsEdge(source, target))
			{
				this.adjacencyList[source].Remove(target);
				this.EdgeCount--;
			}
		}

		public bool ContainsEdge(TVertex source, TVertex target)
		{
			return this.ContainsVertex(source) && this.ContainsVertex(target)
				&& this.adjacencyList[source].Contains(target);
		}
	}
}
