using DataStructures.PriorityQueues;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public class PrimsMST<TVertex>
	{
		private class QueueItem : IComparable<QueueItem>
		{
			public TVertex VertexToSpan;
			public TVertex Source;
			public double CostToSpan;

			public QueueItem(TVertex target, TVertex source, double costToSpan)
			{
				this.VertexToSpan = target;
				this.Source = source;
				this.CostToSpan = costToSpan;
			}

			public int CompareTo(QueueItem key)
			{
				return this.CostToSpan.CompareTo(key.CostToSpan);
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
				throw new ArgumentException("Graph is not connected.");
			}

			TVertex source = graph.GetVertices().First();
			PriorityQueue<QueueItem> queue = new PriorityQueue<QueueItem>();
			Dictionary<TVertex, QueueItem> notSpanned = new Dictionary<TVertex, QueueItem>();
			foreach(TVertex vertex in graph.GetVertices().Where(v => !v.Equals(source)))
			{
				double costToSpan = graph.GetEdgeWeight(source, vertex);
				TVertex src = costToSpan < double.PositiveInfinity ? source : default(TVertex);
				QueueItem item = new QueueItem(vertex, src, costToSpan);
				notSpanned.Add(vertex, item);
				queue.Enqueue(item);
			}

			List<Tuple<TVertex, TVertex>> mst = new List<Tuple<TVertex, TVertex>>();
			while (!queue.IsEmpty())
			{
				QueueItem toSpan = queue.Dequeue();
				yield return Tuple.Create(toSpan.Source, toSpan.VertexToSpan);
				notSpanned.Remove(toSpan.VertexToSpan);
				foreach (TVertex vertex in graph.GetNeighbours(toSpan.VertexToSpan).Where(v => notSpanned.ContainsKey(v)))
				{
					QueueItem toRelax = queue.Dequeue(notSpanned[vertex]);
					double costToSpan = graph.GetEdgeWeight(toSpan.VertexToSpan, vertex);
					if (costToSpan < toRelax.CostToSpan)
					{
						toRelax.CostToSpan = costToSpan;
						toRelax.Source = toSpan.VertexToSpan;
					}

					queue.Enqueue(toRelax);
				}
			}
		}
	}
}
