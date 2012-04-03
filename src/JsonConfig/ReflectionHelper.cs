using System;
using System.Collections;
using System.Collections.Generic;

namespace JsonConfig
{
    internal static class ReflectionHelper
    {
        internal static object Instantiate(Type type)
        {
            return Activator.CreateInstance(type);
        }

        internal static TypeInfo GetTypeInfo(Type type)
        {
            return new TypeInfo(type);
        }

        internal static IList InstantiateGenericList(Type typeArg)
        {
            return (IList)Instantiate(ConstructGenericListType(typeArg));
        }

        internal static Type ConstructGenericListType(Type typeArg)
        {
            var genericListType = typeof(List<>);
            var listTypeArgs = new Type[] { typeArg };
            return genericListType.MakeGenericType(listTypeArgs);
        }
    }
}
