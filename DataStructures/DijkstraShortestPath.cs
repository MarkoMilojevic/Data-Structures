using DataStructures.PriorityQueues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public class DijkstraShortestPath<TVertex>
	{
		private class QueueItem : IComparable<QueueItem>
		{
			public TVertex Vertex;
			public TVertex Predecessor;
			public double DijkstrasGreedyScore;

			public QueueItem(TVertex vertex, TVertex predecessor, double dijkstrasGreedyScore)
			{
				this.Vertex = vertex;
				this.Predecessor = predecessor;
				this.DijkstrasGreedyScore = dijkstrasGreedyScore;
			}

			public int CompareTo(QueueItem key)
			{
				double result = this.DijkstrasGreedyScore - key.DijkstrasGreedyScore;
				if (result < 0)
				{
					return -1;
				}

				if (result == 0)
				{
					return 0;
				}

				return 1;
			}
		}

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

			Dictionary<TVertex, TVertex> parentMap = new Dictionary<TVertex, TVertex>();
			Dictionary<TVertex, QueueItem> itemsMap = new Dictionary<TVertex, QueueItem>();
			PriorityQueue<QueueItem> queue = new PriorityQueue<QueueItem>();
			foreach (TVertex vertex in this.graph.GetVertices().Where(v => !v.Equals(source)))
			{
				double edgeWeightFromSource = this.graph.GetEdgeWeight(source, vertex);
				TVertex vertexPredecessor = edgeWeightFromSource < double.PositiveInfinity ? source : default(TVertex);
				QueueItem item = new QueueItem(vertex, vertexPredecessor, edgeWeightFromSource);
				queue.Enqueue(item);
				itemsMap.Add(vertex, item);
				if (edgeWeightFromSource < double.PositiveInfinity)
				{
					parentMap.Add(vertex, source);
				}
			}

			while (!queue.IsEmpty())
			{
				QueueItem toSpan = queue.Dequeue();
				if (Double.IsPositiveInfinity(toSpan.DijkstrasGreedyScore))
				{
					break;
				}

				parentMap[toSpan.Vertex] = toSpan.Predecessor;
				itemsMap.Remove(toSpan.Vertex);
				foreach (TVertex vertex in this.graph.GetNeighbours(toSpan.Vertex).Where(v => itemsMap.ContainsKey(v)))
				{
					QueueItem toRelax = queue.Dequeue(itemsMap[vertex]);
					double edgeCost = this.graph.GetEdgeWeight(toSpan.Vertex, vertex);
					if (toSpan.DijkstrasGreedyScore + edgeCost < toRelax.DijkstrasGreedyScore)
					{
						toRelax.DijkstrasGreedyScore = toSpan.DijkstrasGreedyScore + edgeCost;
						toRelax.Predecessor = toSpan.Vertex;
					}

					queue.Enqueue(toRelax);
				}
			}

			return parentMap;
		}
	}
}
