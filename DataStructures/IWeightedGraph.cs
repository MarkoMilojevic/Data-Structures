using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public interface IWeightedGraph<TVertex> : IGraph<TVertex>
	{
		double GetEdgeWeight(TVertex source, TVertex target);
	}
}
