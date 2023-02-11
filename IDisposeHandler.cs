using System;

namespace medzumi.Utilities
{
    public interface IDisposeHandler
    {
        void OnStop(IDisposable disposable);
    }
}