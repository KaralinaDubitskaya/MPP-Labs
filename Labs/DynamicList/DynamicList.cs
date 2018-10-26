using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicList
{
    public class DynamicList<T> : IEnumerable<T>
    {
        private T[] _items;
        private const int INITIAL_SIZE = 4;

        public int Count { get; private set; }

        public DynamicList()
        {
            _items = new T[INITIAL_SIZE];
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    return _items[index];
                }
            }
            set
            {
                if (index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _items[index] = value;
                }
            }
        }

        public void Add(T item)
        {
            ResizeArray();
            _items[Count++] = item;
        }

        public bool Remove(T item)
        {
            int index = Array.IndexOf(_items, item, 0, Count);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Count--;
                Array.Copy(_items, index + 1, _items, index, Count - index);
            }
        }

        public void Clear()
        {
            Array.Clear(_items, 0, Count);
            Count = 0;
        }

        private void ResizeArray()
        {
            if (Count == _items.Length)
            {
                Array.Resize<T>(ref _items, _items.Length * 2);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var array = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                array[i] = _items[i];
            }
            return new DynamicListEnum<T>(array);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var array = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                array[i] = _items[i];
            }
            return new DynamicListEnum<T>(array);
        }
    }
}
