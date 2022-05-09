using System;

namespace Components
{
    public interface IDisposeHandler
    {
        void Subscribe(IDisposable disposable);
    }
}