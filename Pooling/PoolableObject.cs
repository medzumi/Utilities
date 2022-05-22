using System;
using System.Collections.Generic;

namespace Utilities.Pooling
{
    public abstract class PoolableObject<T> : IDisposable where T : PoolableObject<T>, new()
    {
        private static readonly Pool<T> _pool;

        private bool _isDisposed = false;
        
        static PoolableObject()
        {
            _pool = new Pool<T>(0, () => new T());
        }

        protected PoolableObject()
        {
            
        }

        public static T Create()
        {
            var obj = _pool.Get();
            obj._isDisposed = false;
            return obj;
        }

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public IDisposable AddTo(IDisposable disposable)
        {
            _disposables.Add(disposable);
            return disposable;
        }
        
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                foreach (var disposable in _disposables)
                {
                    disposable?.Dispose();
                }    
                _disposables.Clear();
                DisposeHandler();
                _pool.Release(this as T);
            }
        }

        protected virtual void DisposeHandler()
        {
        }
    }
}