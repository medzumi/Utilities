using System;
using System.Collections.Generic;

namespace medzumi.Utilities
{
    public class DisposeHandler : IDisposeHandler
    {
        private bool _isDisposed = false;
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public void Reset()
        {
            _disposables.Clear();
            _isDisposed = false;
        }

        public void Dispose()
        {
            if(_isDisposed)
                return;
            _isDisposed = true;
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }

        public void OnStop(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    }
}