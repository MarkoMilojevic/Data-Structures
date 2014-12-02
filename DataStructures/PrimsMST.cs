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

		private UndirectedAcyclicWeightedGraph<TVertex> graph;

		public PrimsMST(UndirectedAcyclicWeightedGraph<TVertex> graph)
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
			if (this.graph.VertexCount == 0)
			{
				return mst;
			}

			TVertex source = this.graph.GetVertices().First();
			PriorityQueue<QueueItem> queue = new PriorityQueue<QueueItem>();
			Dictionary<TVertex, QueueItem> notSpanned = new Dictionary<TVertex, QueueItem>();
			foreach(TVertex vertex in this.graph.GetVertices().Where(v => !v.Equals(source)))
			{
				double costToSpan = this.graph.GetEdgeWeight(source, vertex);
				TVertex src = costToSpan < double.PositiveInfinity ? source : default(TVertex);
				QueueItem item = new QueueItem(vertex, src, costToSpan);
				queue.Enqueue(item);
				notSpanned.Add(vertex, item);
			}

			while (!queue.IsEmpty())
			{
				QueueItem toSpan = queue.Dequeue();
				if (Double.IsPositiveInfinity(toSpan.CostToSpan))
				{
					throw new ArgumentException("Graph not connected");
				}

				mst.Add(new Tuple<TVertex, TVertex>(toSpan.Source, toSpan.VertexToSpan));
				notSpanned.Remove(toSpan.VertexToSpan);
				foreach (TVertex vertex in this.graph.GetNeighbours(toSpan.VertexToSpan).Where(v => notSpanned.ContainsKey(v)))
				{
					QueueItem toRelax = queue.Dequeue(notSpanned[vertex]);
					double costToSpan = this.graph.GetEdgeWeight(toSpan.VertexToSpan, vertex);
					if (costToSpan < toRelax.CostToSpan)
					{
						toRelax.CostToSpan = costToSpan;
						toRelax.Source = toSpan.VertexToSpan;
					}

					queue.Enqueue(toRelax);
				}
			}

			return mst;
		}
	}
}
