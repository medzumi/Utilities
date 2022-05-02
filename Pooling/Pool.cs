using System;
using System.Collections.Generic;

namespace Utilities.Pooling
{
    public class Pool<T> : IPool<T>
    {
        private readonly Stack<T> _pool;
        private readonly Func<T> _createFunc;
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

        public T Get()
        {
            var result = _pool.Count > 0 ? _pool.Pop() : _createFunc.Invoke();
            _resolveAction?.Invoke(result);
            return result;
        }

        public void Release(T tObject)
        {
            _releaseAction?.Invoke(tObject);
            _pool.Push(tObject);
        }
    }
}