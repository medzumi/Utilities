using System;
using System.Collections.Generic;

namespace medzumi.Utilities.Pooling
{
    public class Pool<T> : IPool<T> where T : class
    {
        private readonly object _synchronizer = new object();
        private readonly Stack<T> _pool;
        private readonly Func<T> _createFunc;
        private int _leak;
        private Action<T> _resolveAction;
        private Action<T> _releaseAction;

        public Pool(int capacity, Func<T> createFunc)
        {
            _pool = new Stack<T>(capacity);
            _createFunc = createFunc;
            for (int i = 0; i < capacity; i++)
            {
                _pool.Push(createFunc.Invoke());
            }
        }
        
        public Pool(int capacity, Func<T> createFunc, Action<T> resolveAction, Action<T> releaseAction)
        {
            _pool = new Stack<T>(capacity);
            _createFunc = createFunc;
            for (int i = 0; i < capacity; i++)
            {
                _pool.Push(createFunc.Invoke());
            }

            _releaseAction = releaseAction;
            _resolveAction = resolveAction;
        }

        public int GetLeak()
        {
            return _leak;
        }

        public int GetCount()
        {
            lock (_synchronizer)
            {
                return _pool.Count;
            }
        }

        public T Get()
        {
            lock (_synchronizer)
            {
                _leak++;
                var result = _pool.Count > 0 ? _pool.Pop() : _createFunc.Invoke();
                _resolveAction?.Invoke(result);
                return result;
            }
        }

        public void Release(T tObject)
        {
            lock (_synchronizer)
            {
                _releaseAction?.Invoke(tObject);
                _pool.Push(tObject);
                _leak--;
            }
        }
    }
}