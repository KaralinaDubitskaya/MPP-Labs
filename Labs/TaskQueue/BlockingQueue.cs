using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPool
{
    public class BlockingQueue<T> : Queue<T>
    {
        private object SyncRoot = new object();

        public bool IsEmpty { get { return (this.Count == 0); } }

        public BlockingQueue() : base() { }

        // add item to the queue
        public new void Enqueue(T item)
        {
            lock (SyncRoot)
            {
                base.Enqueue(item);
            }
        }

        // get item from the queue
        public new T Dequeue()
        {
            T item = default(T);

            lock (SyncRoot)
            {
                if (!IsEmpty)
                {
                    item = base.Dequeue();
                }
            }

            return item;
        }
    }
}
