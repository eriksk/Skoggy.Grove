using System;

namespace Skoggy.Grove.Collections
{
    public class Pool<T> where T : class, new()
    {
        private T[] _items;
        private int _count;
        private readonly int _capacity;

        public Pool(int capacity)
        {
            _capacity = capacity;
            _items = new T[capacity];
            _count = 0;
            for (var i = 0; i < _capacity; i++)
            {
                _items[i] = new T();
            }
        }

        public int Count => _count;
        public T this[int index] => _items[index];

        public void Clear()
        {
            _count = 0;
        }

        public T Pop()
        {
            if (_count > _capacity - 1)
            {
                throw new InvalidOperationException("Pool is empty");
            }

            return _items[_count++];
        }

        public void Push(int index)
        {
            if (_count == 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (index > _count - 1) throw new ArgumentOutOfRangeException(nameof(index));

            var temp = _items[_count - 1];
            _items[_count - 1] = _items[index];
            _items[index] = temp;
            _count--;
        }
    }
}