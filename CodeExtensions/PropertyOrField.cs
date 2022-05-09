using System;
using System.Reflection;

namespace Utilities.CodeExtensions
{
    public readonly struct PropertyOrField
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly FieldInfo _fieldInfo;

        public PropertyOrField(PropertyInfo propertyInfo, FieldInfo fieldInfo)
        {
            _propertyInfo = propertyInfo;
            _fieldInfo = fieldInfo;
        }

        public T Get<T>(object value)
        {
            if (_propertyInfo != null)
                return (T)_propertyInfo.GetValue(value);
            if (_fieldInfo != null)
                return (T) _fieldInfo.GetValue(value);
            return default;
        }

        public string GetName()
        {
            if (_propertyInfo != null)
                return _propertyInfo.Name;
            if (_fieldInfo != null)
                return _fieldInfo.Name;
            return string.Empty;
        }

        public Type GetType()
        {
            if (_propertyInfo.IsNotNull())
                return _propertyInfo.PropertyType;
            if (_fieldInfo.IsNotNull())
                return _fieldInfo.FieldType;
            return null;
        }
    }
}