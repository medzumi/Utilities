using System;

namespace Utilities
{
    public interface IDisposeHandler
    {
        void Subscribe(IDisposable disposable);
    }
}