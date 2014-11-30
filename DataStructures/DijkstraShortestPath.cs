using DataStructures.PriorityQueues;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public class DijkstraShortestPath<TVertex>
	{
		private class QueueItem : IComparable<QueueItem>
		{
			public TVertex Vertex;
			public double PathCostFromSource;

			public QueueItem(TVertex vertex, double pathCostFromSource)
			{
				this.Vertex = vertex;
				this.PathCostFromSource = pathCostFromSource;
			}

			public int CompareTo(QueueItem key)
			{
				return this.PathCostFromSource.CompareTo(key.PathCostFromSource);
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

			PriorityQueue<QueueItem> queue = new PriorityQueue<QueueItem>();
			Dictionary<TVertex, QueueItem> itemsMap = new Dictionary<TVertex, QueueItem>();
			Dictionary<TVertex, TVertex> parentsMap = new Dictionary<TVertex, TVertex>();
			foreach (TVertex vertex in this.graph.GetVertices().Where(v => !v.Equals(source)))
			{
				double pathCostFromSource = this.graph.GetEdgeWeight(source, vertex);
				QueueItem item = new QueueItem(vertex, pathCostFromSource);
				queue.Enqueue(item);
				itemsMap.Add(vertex, item);
				if (pathCostFromSource < double.PositiveInfinity)
				{
					parentsMap.Add(vertex, source);
				}
			}

			while (!queue.IsEmpty())
			{
				QueueItem toSpan = queue.Dequeue();
				if (Double.IsPositiveInfinity(toSpan.PathCostFromSource))
				{
					break;
				}
				
				itemsMap.Remove(toSpan.Vertex);
				foreach (TVertex vertex in this.graph.GetNeighbours(toSpan.Vertex).Where(v => itemsMap.ContainsKey(v)))
				{
					QueueItem toRelax = queue.Dequeue(itemsMap[vertex]);
					double edgeWeight = this.graph.GetEdgeWeight(toSpan.Vertex, vertex);
					if (toSpan.PathCostFromSource + edgeWeight < toRelax.PathCostFromSource)
					{
						toRelax.PathCostFromSource = toSpan.PathCostFromSource + edgeWeight;
						parentsMap[toRelax.Vertex] = toSpan.Vertex;
					}

					queue.Enqueue(toRelax);
				}
			}

			return parentsMap;
		}
	}
}
