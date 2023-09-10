using System;

namespace medzumi.Utilities
{
    public interface IDisposeHandler
    {
        void OnDispose(IDisposable disposable);
    }
}