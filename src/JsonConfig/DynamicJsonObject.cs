/* 
 * This file/class is based on the solution shared by Shawn Weisfeld at 
 * http://www.drowningintechnicaldebt.com/ShawnWeisfeld/archive/2010/08/22/using-c-4.0-and-dynamic-to-parse-json.aspx
 * and improved by Drew Noakes at 
 * http://stackoverflow.com/questions/3142495/deserialize-json-into-c-sharp-dynamic-object/3806407#3806407
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace JsonConfig
{
    internal class DynamicJsonObject : DynamicObject
    {
        #region Fields

        private readonly IDictionary<string, object> _dictionary;

        #endregion

        #region Constructors

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            _dictionary = dictionary;
        }

        #endregion

        #region Methods

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetMember(_dictionary, binder.Name);
            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            try
            {
                result = Convert(_dictionary, binder.Type);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            ToString(sb);
            return sb.ToString();
        }

        #endregion

        #region Internal Methods

        internal void ToString(StringBuilder sb)
        {
            sb.Append("{");

            var firstInDictionary = true;
            foreach (var pair in _dictionary)
            {
                if (!firstInDictionary)
                    sb.Append(",");
                firstInDictionary = false;
                var value = pair.Value;
                var name = pair.Key;
                if (value is string)
                {
                    sb.AppendFormat("{0}:\"{1}\"", name, value);
                }
                else if (value is IDictionary<string, object>)
                {
                    sb.Append(name + ":");
                    new DynamicJsonObject((IDictionary<string, object>)value).ToString(sb);
                }
                else if (value is ArrayList)
                {
                    sb.Append(name + ":[");
                    var firstInArray = true;
                    foreach (var arrayValue in (ArrayList)value)
                    {
                        if (!firstInArray)
                            sb.Append(",");
                        firstInArray = false;
                        if (arrayValue is IDictionary<string, object>)
                            new DynamicJsonObject((IDictionary<string, object>)arrayValue).ToString(sb);
                        else if (arrayValue is string)
                            sb.AppendFormat("\"{0}\"", arrayValue);
                        else
                            sb.AppendFormat("{0}", arrayValue);

                    }
                    sb.Append("]");
                }
                else
                {
                    sb.AppendFormat("{0}:{1}", name, value);
                }
            }
            sb.Append("}");
        }

        #endregion

        #region Static Methods

        public static object Convert(IDictionary<string, object> dictionary, Type type)
        {
            var obj = ReflectionHelper.Instantiate(type);
            TypeInfo typeInfo = ReflectionHelper.GetTypeInfo(type);

            foreach (var key in dictionary.Keys)
            {
                var value = GetMember(dictionary, key);
                if (value != null)
                {
                    if (typeInfo.ExistsFieldOrProperty(key))
                    {
                        var valueAsDynamicJsonObject = value as DynamicJsonObject;
                        if (valueAsDynamicJsonObject != null)
                        {
                            var convertedValue = Convert(valueAsDynamicJsonObject._dictionary, typeInfo.GetReturnType(key));
                            typeInfo.SetValue(key, obj, convertedValue);
                        }
                        else
                        {
                            var valueAsListOfObjects = value as List<object>;
                            if (valueAsListOfObjects != null)
                            {
                                var listItemType = typeInfo.GetReturnType(key).GetGenericArguments()[0];
                                var list = ReflectionHelper.InstantiateGenericList(listItemType);

                                FillList(listItemType, list, valueAsListOfObjects);

                                typeInfo.SetValue(key, obj, list);
                            }
                            else
                            {
                                typeInfo.SetValue(key, obj, value);

                            }
                        }
                    }
                }
            }

            return obj;
        }

        private static void FillList(Type listItemType, IList list, List<object> valueAsListOfObjects)
        {
            if (valueAsListOfObjects[0] is DynamicJsonObject)
            {
                foreach (var listItem in valueAsListOfObjects)
                {
                    list.Add(Convert(((DynamicJsonObject) listItem)._dictionary, listItemType));
                }
            }
            else
            {
                foreach (var listItem in valueAsListOfObjects)
                {
                    list.Add(listItem);
                }
            }
        }

        private static object GetMember(IDictionary<string, object> dictionary, string name)
        {
            object result = null;
            if (!dictionary.TryGetValue(name, out result))
            {
                // return null to avoid exception.  caller can check for null this way...
                return null;
            }

            var resultAsDictionary = result as IDictionary<string, object>;
            if (resultAsDictionary != null)
            {
                return new DynamicJsonObject(resultAsDictionary);
            }

            var resultAsArrayList = result as ArrayList;
            if (resultAsArrayList != null && resultAsArrayList.Count > 0)
            {
                if (resultAsArrayList[0] is IDictionary<string, object>)
                    result = new List<object>(resultAsArrayList.Cast<IDictionary<string, object>>().Select(x => new DynamicJsonObject(x)));
                else
                    result = new List<object>(resultAsArrayList.Cast<object>());
            }

            return result;
        }

        #endregion
    }
}
