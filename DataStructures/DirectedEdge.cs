using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	internal class DirectedEdge<TVertex>
	{
		public TVertex Source { get; private set; }
		public TVertex Target { get; private set; }

		public DirectedEdge(TVertex source, TVertex target)
		{
			if (source == null || target == null)
			{
				throw new ArgumentNullException();
			}

			this.Source = source;
			this.Target = target;
		}
	}
}
