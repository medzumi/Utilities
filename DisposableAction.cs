using System;

namespace medzumi.Utilities
{
    public class DisposableAction : IDisposable
    {
        private Action _action;
        private bool _isDisposed = false;

        public DisposableAction(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            if(_isDisposed)
                return;
            _isDisposed = true;
            _action.Invoke();
            _action = null;
        }
        
        public static implicit operator DisposableAction(Action action)
        {
            return new DisposableAction(action);
        }
    }
}