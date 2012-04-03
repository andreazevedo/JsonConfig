using System;
using System.Collections.Generic;
using System.Reflection;

namespace JsonConfig
{
    internal class TypeInfo
    {
        #region Fields

        private readonly Type _type;
        private readonly IDictionary<string, FieldInfo> _fields;
        private readonly IDictionary<string, PropertyInfo> _properties;

        #endregion

        #region Constructors

        public TypeInfo(Type type)
        {
            _type = type;

            var fields = type.GetFields();
            _fields = new Dictionary<string, FieldInfo>(fields.Length);
            foreach (var fieldInfo in fields)
            {
                _fields.Add(fieldInfo.Name, fieldInfo);
            }

            var properties = type.GetProperties();
            _properties = new Dictionary<string, PropertyInfo>(properties.Length);
            foreach (var propertyInfo in properties)
            {
                _properties.Add(propertyInfo.Name, propertyInfo);
            }
        }

        #endregion

        #region Methods

        internal bool SetValue(string name, object obj, object value)
        {
            FieldInfo fieldInfo;
            PropertyInfo propertyInfo;

            string nameUpperCased = name.UpperCaseFirstChar();

            if (_fields.TryGetValue(name, out fieldInfo))
            {
                fieldInfo.SetValue(obj, value);
                return true;
            }

            if (_properties.TryGetValue(name, out propertyInfo))
            {
                propertyInfo.SetValue(obj, value, new object[0]);
                return true;
            }

            if (!name.Equals(nameUpperCased))
            {
                if (_fields.TryGetValue(nameUpperCased, out fieldInfo))
                {
                    fieldInfo.SetValue(obj, value);
                    return true;
                }

                if (_properties.TryGetValue(nameUpperCased, out propertyInfo))
                {
                    propertyInfo.SetValue(obj, value, new object[0]);
                    return true;
                }
            }

            return false;
        }

        internal Type GetReturnType(string name)
        {
            FieldInfo fieldInfo;
            PropertyInfo propertyInfo;

            string nameUpperCased = name.UpperCaseFirstChar();

            if (_fields.TryGetValue(name, out fieldInfo))
            {
                return fieldInfo.FieldType;
            }

            if (_properties.TryGetValue(name, out propertyInfo))
            {
                return propertyInfo.PropertyType;
            }

            if (!name.Equals(nameUpperCased))
            {
                if (_fields.TryGetValue(nameUpperCased, out fieldInfo))
                {
                    return fieldInfo.FieldType;
                }

                if (_properties.TryGetValue(nameUpperCased, out propertyInfo))
                {
                    return propertyInfo.PropertyType;
                }
            }

            return null;
        }

        internal bool ExistsFieldOrProperty(string name)
        {
            string nameUpperCased = name.UpperCaseFirstChar();
            return (_fields.ContainsKey(name) || _properties.ContainsKey(name) || _fields.ContainsKey(nameUpperCased) ||
                    _properties.ContainsKey(nameUpperCased));
        }

        #endregion
    }
}
