using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicList
{
    public class DynamicListEnum<T> : IEnumerator<T>
    {
        private T[] _items;

        // Is positioned before the first element 
        // until the first MoveNext() call
        private int position = -1;

        public DynamicListEnum(T[] items)
        {
            _items = items;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _items.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        T IEnumerator<T>.Current
        {
            get
            {
                try
                {
                    return _items[position];
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public object Current
        {
            get
            {
                try
                {
                    return _items[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose() { }
    }
}
