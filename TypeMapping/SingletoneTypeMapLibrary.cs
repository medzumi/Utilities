using System;
using Utilities.GenericPatterns;

namespace Utilities.TypeMapping
{
    public partial class TypeMapLibrary<TAttribute>
    {
        public static Type GetType(string key)
        {
            return Singletone<TypeMapLibrary<TAttribute>>.instance._typeDictionary[key];
        }
    }
}