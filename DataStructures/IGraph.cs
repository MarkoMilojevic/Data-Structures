using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	public interface IGraph<TVertex>
	{
		bool ContainsVertex(TVertex vertex);

		IEnumerable<TVertex> GetNeighbours(TVertex vertex);
	}
}
