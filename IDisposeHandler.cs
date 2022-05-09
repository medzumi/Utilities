using System;

namespace Utilities
{
    public interface IDisposeHandler
    {
        void Reset();
        
        void Subscribe(IDisposable disposable);
    }
}