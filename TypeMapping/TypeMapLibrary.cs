using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities.TypeMapping
{
    [Obsolete("Doesn't tested")]
    public partial class TypeMapLibrary<TAttribute>
        where TAttribute : Attribute, ITypeMapAttribute
    {
        private readonly Dictionary<string, Type> _typeDictionary = new Dictionary<string, Type>();

        public TypeMapLibrary()
        {
            var attributeType = typeof(TAttribute);
            var linq = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Select(type => (type.GetCustomAttribute(attributeType) as TAttribute, type))
                .Where(tuple => tuple.Item1 != null);
            foreach (var valueTuple in linq)
            {
                _typeDictionary[valueTuple.Item1.Key] = valueTuple.type;
            }
        }
    }

    
}