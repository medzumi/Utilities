using System;
using System.Collections.Generic;

namespace medzumi.Utilities.CodeExtensions
{
    public static class EnumerableExtensions
    {
        private static readonly Dictionary<Type, object> _listDict = new Dictionary<Type, object>();
        
        public static List<T> RemoveZeroAlloc<T>(this List<T> list, T example)
        {
            var bufferList = GetOrCreate<List<T>>();
            lock (bufferList)
            {
                bufferList.Clear();
                bufferList.AddRange(list);
                lock (list)
                {
                    list.Clear();
                    var index = bufferList.IndexOf(example);
                    for (int i = 0; i < bufferList.Count; i++)
                    {
                        if (i != index)
                        {
                            list.Add(bufferList[i]);
                        }
                    }
                }

                return list;
            }
        }

        public static List<T> AddAsUnique<T>(this List<T> list, T example)
        {
            if (list.IndexOf(example) == -1)
            {
                list.Add(example);
            }

            return list;
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                dictionary[key] = value = new TValue();
            }

            return value;
        }

        private static T GetOrCreate<T>() where T : class, new()
        {
            lock (_listDict)
            {
                var type = typeof(T);
                if (!_listDict.TryGetValue(type, out var obj))
                {
                    _listDict[type] = obj = new T();
                }

                return obj as T;
            }
        }
    }
}