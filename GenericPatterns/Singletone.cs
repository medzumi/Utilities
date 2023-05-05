using System;

namespace medzumi.Utilities.GenericPatterns
{
    public class Singletone<T>
    {
        private static T _instance;
        
        public static T instance
        {
            get
            {
                try
                {
                    return _instance ??= Activator.CreateInstance<T>();
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                    return default;
                }
                
            }
            set
            {
                if (_instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                _instance = value;
            }
        }
    }
}