﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public interface IDirectedGraph<TVertex> : IGraph<TVertex>
	{
		bool ContainsEdge(TVertex source, TVertex target);
	}
}
