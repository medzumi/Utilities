using System;
using System.Collections.Generic;
using System.Reflection;

namespace medzumi.Utilities
{
    public static class TypeExtensions
    {
        public static T FindWithRecursiveType<T>(this Dictionary<Type, T> dictionary, Type type)
        {
            while (type != null)
            {
                if (dictionary.TryGetValue(type, out var value))
                {
                    return value;
                }

                if (type.IsGenericType && !type.IsGenericTypeDefinition && dictionary.TryGetValue(type.GetGenericTypeDefinition(), out value))
                {
                    return value;
                }
                else
                {
                    type = type.BaseType;
                }
            }
            return default(T);
        }
        
        public static bool TryFindWithRecursiveType<T>(this Dictionary<Type, T> dictionary, Type type, out T value)
        {
            while (type != null)
            {
                if (dictionary.TryGetValue(type, out value))
                {
                    return true;
                }
                if (type.IsGenericType && !type.IsGenericTypeDefinition && dictionary.TryGetValue(type.GetGenericTypeDefinition(), out value))
                {
                    return true;
                }
                else
                {
                    type = type.BaseType;
                }
            }

            value = default(T);
            return false;
        }

        public static bool TryGetFieldInfoRecursive(this Type type, string name, out FieldInfo fieldInfo) 
        {
            fieldInfo = null;
            while (fieldInfo == null && type != null)
            {
                fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }

            return fieldInfo != null;
        }
        
        public static FieldInfo GetFieldInfoRecursive(this Type type, string name) 
        {
            FieldInfo fieldInfo = null;
            while (fieldInfo == null && type != null)
            {
                fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }

            return fieldInfo;
        }

        public static string GetTypeNameWithoutNameSpace(string @string)
        {
            var split = @string.Split('[');
            split = split[0].Split('.');
            return split[^1];
        }

        public static string GetBeautifulFullName(this Type type, bool withNameSpace = true)
        {
            string result = string.Empty;
            if (type.IsGenericType)
            {
                result = withNameSpace ? type.GetGenericTypeDefinition().FullName : type.GetGenericTypeDefinition().Name;
                result = result.Substring(0, result.IndexOf('`'));
                result += '[';
                var genericArguments = type.GetGenericArguments();
                for (int i = 0; i < genericArguments.Length; i++)
                {
                    result += genericArguments[i].GetBeautifulFullName(withNameSpace);
                    if (i < genericArguments.Length - 1)
                    {
                        result += ',';
                    }
                }

                result += ']';
            }
            else
            {
                result = withNameSpace ? type.FullName : type.Name;
            }

            return result;
        }
    }
}