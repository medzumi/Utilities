using System;
using System.Collections.Generic;

namespace Utilities.Pooling
{
    public class Pool<T> : IPool<T>
    {
        private readonly Stack<T> _pool;
        private readonly Func<T> _createFunc;

        public Pool(int capacity, Func<T> createFunc)
        {
            _pool = new Stack<T>(capacity);
            _createFunc = createFunc;
            for (int i = 0; i < capacity; i++)
            {
                _pool.Push(createFunc.Invoke());
            }
        }

        public T Get()
        {
            return _pool.Count > 0 ? _pool.Pop() : _createFunc.Invoke();
        }

        public void Release(T tObject)
        {
            _pool.Push(tObject);
        }
    }
}