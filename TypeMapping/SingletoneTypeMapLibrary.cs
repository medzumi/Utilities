using System;
using medzumi.Utilities.GenericPatterns;

namespace medzumi.Utilities.TypeMapping
{
    public partial class TypeMapLibrary<TAttribute>
    {
        public static Type GetType(string key)
        {
            return Singletone<TypeMapLibrary<TAttribute>>.instance._typeDictionary[key];
        }
    }
}