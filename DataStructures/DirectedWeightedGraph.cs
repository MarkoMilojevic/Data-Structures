using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class DirectedWeightedGraph<TVertex> : IGraph<TVertex>
	{
		private Dictionary<TVertex, List<DirectedWeightedEdge<TVertex>>> adjacencyList;

		public int VertexCount { get; protected set; }

		public int EdgeCount { get; protected set; }

		public DirectedWeightedGraph()
		{
			this.adjacencyList = new Dictionary<TVertex, List<DirectedWeightedEdge<TVertex>>>();
			this.VertexCount = 0;
			this.EdgeCount = 0;
		}

		public IEnumerable<TVertex> GetVertices()
		{
			foreach (TVertex vertex in this.adjacencyList.Keys)
			{
				yield return vertex;
			}
		}

		public void AddVertex(TVertex vertex)
		{
			if (!this.ContainsVertex(vertex))
			{
				this.adjacencyList.Add(vertex, new List<DirectedWeightedEdge<TVertex>>());
				this.VertexCount++;
			}
		}

		public void RemoveVertex(TVertex vertex)
		{
			if (this.ContainsVertex(vertex))
			{
				foreach (List<DirectedWeightedEdge<TVertex>> edges in this.adjacencyList.Values)
				{
					edges.RemoveAll(e => e.Target.Equals(vertex));
				}

				this.adjacencyList.Remove(vertex);
				this.VertexCount--;
			}
		}

		public bool ContainsVertex(TVertex vertex)
		{
			if (vertex == null)
			{
				throw new ArgumentNullException();
			}

			return this.adjacencyList.ContainsKey(vertex);
		}

		public IEnumerable<TVertex> GetNeighbours(TVertex vertex)
		{
			return this.ContainsVertex(vertex) ? this.adjacencyList[vertex].Select(edge => edge.Target) : Enumerable.Empty<TVertex>();
		}

		public void AddEdge(TVertex source, TVertex target, double weight)
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
				DirectedWeightedEdge<TVertex> edge = new DirectedWeightedEdge<TVertex>(source, target, weight);
				this.adjacencyList[source].Add(edge);
				this.EdgeCount++;
			}
		}

		public void RemoveEdge(TVertex source, TVertex target)
		{
			if (this.ContainsEdge(source, target))
			{
				this.adjacencyList[source].RemoveAll(e => e.Target.Equals(target));
				this.EdgeCount--;
			}
		}

		public double GetEdgeWeight(TVertex source, TVertex target)
		{
			if (source == null || target == null)
			{
				throw new ArgumentNullException();
			}

			if (source.Equals(target))
			{
				return 0;
			}

			if (!this.ContainsEdge(source, target))
			{
				return double.PositiveInfinity;
			}

			return this.adjacencyList[source].Find(e => e.Target.Equals(target)).Weight;
		}

		public bool ContainsEdge(TVertex source, TVertex target)
		{
			return this.ContainsVertex(source) && this.ContainsVertex(target)
				&& this.adjacencyList[source].Any(e => e.Target.Equals(target));
		}
	}
}
