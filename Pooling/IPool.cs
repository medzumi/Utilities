using System;

namespace medzumi.Utilities.Pooling
{
    public interface IPoolInformation
    {
        int GetLeak();
        int GetCount();
    }
    
    public interface IPool<T> : IPoolInformation, IPoolGetter<T>, IPoolReleaser<T> where T : class
    {
    }

    public interface IPoolGetter<out T>
    {
        T Get();
    }

    public interface IPoolReleaser<in T>
    {
        void Release(T tObject);
    }

    public class FakePool<T> : IPool<T> where T : class
    {
        private readonly IPoolInformation _poolInformation;
        private readonly Action<T> _releaseAction;
        private readonly Func<T> _getFunc;

        public FakePool(IPoolInformation poolInformation, Func<T> getFunc, Action<T> releaseAction)
        {
            _poolInformation = poolInformation;
            _getFunc = getFunc;
            _releaseAction = releaseAction;
        }
        
        public T Get()
        {
            return _getFunc.Invoke();
        }

        public void Release(T tObject)
        {
            _releaseAction.Invoke(tObject);
        }

        public int GetLeak()
        {
            return _poolInformation.GetLeak();
        }

        public int GetCount()
        {
            return _poolInformation.GetCount();
        }
    }
}