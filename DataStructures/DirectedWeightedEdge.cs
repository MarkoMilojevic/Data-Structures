using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Graphs
{
	internal class DirectedWeightedEdge<TVertex> : DirectedEdge<TVertex>
	{
		public double Weight { get; private set; }

		public DirectedWeightedEdge(TVertex source, TVertex target, double weight)
				: base(source, target)
		{
			if (weight <= 0)
			{
				throw new ArgumentException("Edge weight must be positive");
			}

			this.Weight = weight;
		}
	}
}
