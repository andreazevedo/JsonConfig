using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace JsonConfig.Test
{
    [TestFixture]
    public class ReflectionHelperTest
    {
        #region Test Methods

        [Test]
        public void Instantiate_ShouldReturnInstance()
        {
            var obj = (ReflectionHelperTestType)ReflectionHelper.Instantiate(typeof(ReflectionHelperTestType));

            Assert.IsNotNull(obj);
        }

        [Test]
        public void GetTypeInfo_ShouldNotReturnNull()
        {
            var typeInfo = ReflectionHelper.GetTypeInfo(typeof (ReflectionHelperTestType));

            Assert.IsNotNull(typeInfo);
        }

        [Test]
        public void InstantiateGenericList_ShouldCreateListWithCorrectTypeArg()
        {
            var list = ReflectionHelper.InstantiateGenericList(typeof(ReflectionHelperTestType));

            Assert.IsNotNull(list);
            list.Add(new ReflectionHelperTestType()); // should work
            Assert.Throws<ArgumentException>(() => list.Add("ola!!!")); // should throw exception
        }

        [Test]
        public void ConstructGenericListType_ShouldReturnCorrectType()
        {
            var type = ReflectionHelper.ConstructGenericListType(typeof (ReflectionHelperTestType));

            Assert.AreEqual(type, typeof(List<ReflectionHelperTestType>));
        }

        #endregion
    }

    #region Test Types

    public class ReflectionHelperTestType
    {
    }

    #endregion
}
