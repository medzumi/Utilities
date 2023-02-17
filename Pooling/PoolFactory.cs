using System;
using System.Collections.Generic;
using System.Linq;
using medzumi.Utilities.CodeExtensions;
using medzumi.Utilities.GenericPatterns;

namespace medzumi.Utilities.Pooling
{
    public interface IPoolConfiguration<out TOut, in TIn>
    {
        public TOut Create(TIn tObject, IPoolReleaser<TOut> releaser);
        public void ResolveAction(TIn tObject);
        public void ReleaseAction(TIn tObject);
    }

    public class PoolFactory : Singletone<PoolFactory>
    {
        private readonly Dictionary<object, Dictionary<Type, object>> _pools = new Dictionary<object, Dictionary<Type, object>>();
        private readonly Dictionary<Type, object> _factories = new Dictionary<Type, object>();

        private readonly Dictionary<Type, object> _commonPools = new Dictionary<Type, object>();

        public void SetPoolConfiguration<T>(IPoolConfiguration<T, T> poolConfiguration)
        {
            _factories[typeof(T)] = poolConfiguration;
        }

        public IPool<T> GetPoolForNew<T>() where T : class, new()
        {
            if(!_commonPools.TryGetValue(typeof(T), out var value)){
                _commonPools[typeof(T)] = value = new Pool<T>(0, x => new T(), null, null);
            }
            return value as IPool<T>;
        }

        public IPool<T> GetPollForObject<T>(T tObject) where T : class
        {
            if (!_pools.TryGetValue(tObject, out var oPools))
            {
                _pools[tObject] = oPools = new Dictionary<Type, object>();
            }
            if(!oPools.TryGetValue(typeof(T), out var oPool))
            {
                var type = tObject.GetType();
                object factory = null;
                while (!_factories.TryGetValue(type, out factory) && type.IsNotNull())
                {
                    type = type.BaseType;
                }

                if (factory == null)
                {
                    return null;
                }
                else
                {
                    var typeFactory = factory as IPoolConfiguration<T, T>;
                    oPools[typeof(T)] = oPool = new Pool<T>(0, (x) => typeFactory.Create(tObject, x) as T, typeFactory.ResolveAction, typeFactory.ReleaseAction);
                    return oPool as IPool<T>;
                }
            }
            else
            {
                return oPool as IPool<T>;
            }
        }

        public IPool<T> GetPoolForObject<T>(T tObject, Action<T> resolveAction, Action<T> releaseAction) where T : class
        {
            var pool = GetPollForObject(tObject);
            return new FakePool<T>(pool, () =>
            {
                var o = pool.Get();
                resolveAction.Invoke(o);
                return o;
            }, obj =>
            {
                releaseAction.Invoke(obj);
                pool.Release(obj);
            });
        }
    }
    
    public static class PoolExtensions
    {
        public static IPool<T> GetPool<T>(this T tObj) where T : class
        {
            return PoolFactory.instance.GetPollForObject(tObj);
        }

        public static IPool<T> GetPool<T>() where T : class, new(){
            return PoolFactory.instance.GetPoolForNew<T>();
        }
    }
}