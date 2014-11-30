using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.PriorityQueues
{
	public class PriorityQueue<T> where T : IComparable<T>
	{
		private const int InitialCapacity = 100;
		private T[] queue;
		private Dictionary<T, int> indices;
		private IComparer<T> comparer;
		public int Count { get; private set; }

		public PriorityQueue()
		{
			this.queue = new T[InitialCapacity];
			this.indices = new Dictionary<T, int>();
			this.Count = 0;
			this.comparer = null;
		}

		public PriorityQueue(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException();
			}

			this.queue = new T[InitialCapacity];
			this.indices = new Dictionary<T, int>();
			this.Count = 0;
			this.comparer = comparer;
		}

		public PriorityQueue(T[] keys)
		{
			if (keys == null)
			{
				throw new ArgumentNullException();
			}

			this.queue = new T[InitialCapacity];
			this.indices = new Dictionary<T, int>();
			this.Count = 0;
			this.comparer = null;
			foreach (T key in keys)
			{
				this.Enqueue(key);
			}
		}

		public PriorityQueue(IEnumerable<T> keys)
		{
			if (keys == null)
			{
				throw new ArgumentNullException();
			}

			this.queue = new T[InitialCapacity];
			this.indices = new Dictionary<T, int>();
			this.Count = 0;
			this.comparer = null;
			foreach (T key in keys)
			{
				this.Enqueue(key);
			}
		}

		public bool IsEmpty()
		{
			return this.Count == 0;
		}

		public T Peek()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Priority queue underflow");
			}
			
			return this.queue[1];
		}

		public void Enqueue(T key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}

			if (isFull())
			{
				resize(2 * this.queue.Length);
			}

			this.queue[++this.Count] = key;
			this.indices[key] = this.Count;
			swim(this.Count);
		}

		public T Dequeue()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Priority queue underflow");
			}

			swap(1, this.Count);
			T result = this.queue[this.Count--];
			sink(1);
			this.indices.Remove(result);
			this.queue[this.Count + 1] = default(T);
			if (this.Count > 0 && this.Count == (this.queue.Length - 1) / 4)
			{
				resize(this.queue.Length / 2);
			}

			return result;
		}

		public T Dequeue(T key)
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Priority queue underflow");
			}

			if (!this.indices.ContainsKey(key))
			{
				throw new ArgumentException("No such key");
			}

			int pos = this.indices[key];
			swap(pos, this.Count);
			T result = this.queue[this.Count--];
			sink(pos);
			this.queue[this.Count + 1] = default(T);
			this.indices.Remove(result);
			if (this.Count > 0 && this.Count == (this.queue.Length - 1) / 4)
			{
				resize(this.queue.Length / 2);
			}

			return result;
		}

		private bool isFull()
		{
			return this.Count == this.queue.Length - 1;
		}

		private void resize(int capacity)
		{
			T[] newQueue = new T[capacity];
			for (int i = 1; i <= this.Count; i++)
			{
				newQueue[i] = this.queue[i];
			}

			this.queue = newQueue;
		}

		private void sink(int k)
		{
			while (2 * k <= this.Count)
			{
				int i = 2 * k;
				if (i < this.Count && isGreater(i, i + 1))
				{
					i++;
				}

				if (!isGreater(k, i))
				{
					break;
				}

				swap(k, i);
				k = i;
			}
		}		

		private void swim(int k)
		{
			while (k > 1 && isGreater(k / 2, k))
			{
				swap(k, k / 2);
				k = k / 2;
			}
		}

		private bool isGreater(int i, int j)
		{
			if (this.comparer == null)
			{
				return (this.queue[i]).CompareTo(this.queue[j]) > 0;
			}
			else
			{
				return comparer.Compare(this.queue[i], this.queue[j]) > 0;
			}
		}

		private void swap(int i, int j)
		{
			T swapper = this.queue[i];
			this.queue[i] = this.queue[j];
			this.queue[j] = swapper;
			this.indices[this.queue[i]] = i;
			this.indices[this.queue[j]] = j;
		}
	}
}
