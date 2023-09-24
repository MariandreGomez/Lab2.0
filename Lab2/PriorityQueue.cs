using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Lab2
{

    class PriorityQueue<T>
    {
        private List<T> data;
        private readonly IComparer<T> comparer;

        public PriorityQueue() : this(null)
        {
        }

        public PriorityQueue(IComparer<T> comparer)
        {
            data = new List<T>();
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        public int Count
        {
            get { return data.Count; }
        }

        public void Enqueue(T item)
        {
            data.Add(item);
            int childIndex = data.Count - 1;

            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;

                if (comparer.Compare(data[childIndex], data[parentIndex]) >= 0)
                    break;

                T tmp = data[childIndex];
                data[childIndex] = data[parentIndex];
                data[parentIndex] = tmp;

                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            if (data.Count == 0)
                throw new InvalidOperationException("Queue is empty.");

            int lastIndex = data.Count - 1;
            T frontItem = data[0];
            data[0] = data[lastIndex];
            data.RemoveAt(lastIndex);

            lastIndex--;

            int parentIndex = 0;

            while (true)
            {
                int leftChildIndex = parentIndex * 2 + 1;
                int rightChildIndex = parentIndex * 2 + 2;

                if (leftChildIndex > lastIndex)
                    break;

                int minChildIndex = leftChildIndex;

                if (rightChildIndex <= lastIndex && comparer.Compare(data[rightChildIndex], data[leftChildIndex]) < 0)
                    minChildIndex = rightChildIndex;

                if (comparer.Compare(data[parentIndex], data[minChildIndex]) <= 0)
                    break;

                T tmp = data[parentIndex];
                data[parentIndex] = data[minChildIndex];
                data[minChildIndex] = tmp;

                parentIndex = minChildIndex;
            }

            return frontItem;
        }
    }



}