using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public interface IUndirectedGraph<TVertex> : IGraph<TVertex>
	{
		bool AreConnected(TVertex vertex1, TVertex vertex2);
	}
}
