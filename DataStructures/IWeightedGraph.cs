using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public interface IWeightedGraph<TVertex> : IGraph<TVertex>
	{
		double GetEdgeWeight(TVertex source, TVertex target);
	}
}
