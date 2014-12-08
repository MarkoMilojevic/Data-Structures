using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public interface IUndirectedGraph<TVertex> : IGraph<TVertex>
	{
		bool AreConnected(TVertex vertex1, TVertex vertex2);
	}
}
