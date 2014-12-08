using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.UnionFind
{
	public class UnionFind<T>
	{
		private Dictionary<T, T> parentMap;
		private Dictionary<T, int> rankMap;

		public int Count { get; private set; }

		public UnionFind()
		{
			parentMap = new Dictionary<T, T>();
			rankMap = new Dictionary<T, int>();
		}

		public UnionFind(ISet<T> elements) : this()
		{
			if (elements == null)
			{
				throw new ArgumentNullException();
			}

			foreach (T element in elements)
			{
				parentMap.Add(element, element);
				rankMap.Add(element, 1);
			}
		}

		public void Add(T element)
		{
			parentMap.Add(element, element);
			rankMap.Add(element, 1);
		}

		public T Find(T element)
		{
			if (!parentMap.ContainsKey(element))
			{
				throw new InvalidOperationException("Specified element is not found.");
			}

			T parent = element;
			while (!parent.Equals(parentMap[parent]))
			{
				parent = parentMap[parent];
			}

			return parent;
		}

		public bool AreConnected(T element1, T element2)
		{
			T parent1 = Find(element1);
			T parent2 = Find(element2);
			return parent1.Equals(parent2);
		}

		public void Union(T element1, T element2)
		{
			T parent1 = Find(element1);
			T parent2 = Find(element2);
			if (!parent1.Equals(parent2))
			{
				int rank1 = rankMap[parent1];
				int rank2 = rankMap[parent2];
				if (rank1 < rank2)
				{
					parentMap[parent1] = parent2;
					rankMap[parent2] = rank1 + rank2;
				}
				else
				{
					parentMap[parent2] = parent1;
					rankMap[parent1] = rank1 + rank2;
				}
			}
		}
	}
}
