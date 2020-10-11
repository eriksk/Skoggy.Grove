using System.Collections;
using System.Collections.Generic;

namespace Skoggy.Grove.Contexts
{
    public class DesynchronizedList<T> : IEnumerable<T>
    {
        private bool _dirty;
        private List<T> _active;
        private List<T> _added;
        private List<T> _removed;

        public DesynchronizedList()
        {
            _active = new List<T>();
            _added = new List<T>();
            _removed = new List<T>();
        }

        public int Count => _active.Count;
        public int ActiveCount => _active.Count;
        public int DesyncCount => _added.Count + _removed.Count;
        public int TotalCount => ActiveCount + DesyncCount;
        public T this[int index] => _active[index];

        public void Clear()
        {
            _active.Clear();
            _added.Clear();
            _removed.Clear();
            _dirty = false;
        }

        public void Add(T item)
        {
            // TODO: We want this to be available when looping or searching - immidiately
            _added.Add(item);
            _dirty = true;
        }

        public void Remove(T item)
        {
            // TODO: We want this to not be available when looping or searching - immidiately
            _removed.Add(item);
            _dirty = true;
        }

        public void Sync()
        {
            if (!_dirty) return;

            foreach(var item in _added)
            {
                _active.Add(item);
            }
            _added.Clear();

            foreach(var item in _removed)
            {
                _active.Remove(item);
            }
            _removed.Clear();

            _dirty = false;
        }

        public IEnumerator<T> GetEnumerator() => _active.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _active.GetEnumerator();
    }
}
