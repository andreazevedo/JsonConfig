using System;
using NUnit.Framework;

namespace JsonConfig.Test
{
    [TestFixture]
    public class TypeInfoTest
    {
        #region Test Methods

        [Test]
        public void ExistsFieldOrProperty_OnAnExistingField_ShouldReturnTrue()
        {
            var typeInfo = GetTypeInfo(typeof (TypeInfoTestType));

            var result = typeInfo.ExistsFieldOrProperty("myField");

            Assert.IsTrue(result);
        }

        [Test]
        public void ExistsFieldOrProperty_OnAnExistingProperty_ShouldReturnTrue()
        {
            var typeInfo = GetTypeInfo(typeof(TypeInfoTestType));

            var result = typeInfo.ExistsFieldOrProperty("myProperty");

            Assert.IsTrue(result);
        }

        [Test]
        public void ExistsFieldOrProperty_OnAnUnexistingMember_ShouldReturnFalse()
        {
            var typeInfo = GetTypeInfo(typeof(TypeInfoTestType));

            var result = typeInfo.ExistsFieldOrProperty("unexisting");

            Assert.IsFalse(result);
        }

        [Test]
        public void GetReturnType_OnAField_ShouldReturnCorrectType()
        {
            var typeInfo = GetTypeInfo(typeof (TypeInfoTestType));

            var type = typeInfo.GetReturnType("myField");

            Assert.AreEqual(typeof(int), type);
        }

        [Test]
        public void GetReturnType_OnAProperty_ShouldReturnCorrectType()
        {
            var typeInfo = GetTypeInfo(typeof(TypeInfoTestType));

            var type = typeInfo.GetReturnType("myProperty");

            Assert.AreEqual(typeof(string), type);
        }

        [Test]
        public void GetReturnType_OnAnUnexistingMember_ShouldReturnNull()
        {
            var typeInfo = GetTypeInfo(typeof(TypeInfoTestType));

            var type = typeInfo.GetReturnType("unexisting");

            Assert.IsNull(type);
        }

        [Test]
        public void SetValue_OnAField_ShouldSetValue()
        {
            var typeInfo = GetTypeInfo(typeof(TypeInfoTestType));
            var obj = new TypeInfoTestType();

            Assert.AreEqual(0, obj.myField);
            bool valueSetted = typeInfo.SetValue("myField", obj, 80);
            Assert.IsTrue(valueSetted);
            Assert.AreEqual(80, obj.myField);
        }

        [Test]
        public void SetValue_OnAProperty_ShouldSetValue()
        {
            var typeInfo = GetTypeInfo(typeof(TypeInfoTestType));
            var obj = new TypeInfoTestType();

            Assert.IsNull(obj.MyProperty);
            bool valueSetted = typeInfo.SetValue("myProperty", obj, "New value");
            Assert.IsTrue(valueSetted);
            Assert.AreEqual("New value", obj.MyProperty);
        }

        [Test]
        public void SetValue_OnAnUnexistingMember_ShouldDoNothing()
        {
            var typeInfo = GetTypeInfo(typeof(TypeInfoTestType));
            var obj = new TypeInfoTestType();

            Assert.AreEqual(0, obj.myField);
            Assert.IsNull(obj.MyProperty);
            bool valueSetted = typeInfo.SetValue("unexisting", obj, "...");
            Assert.IsFalse(valueSetted);
            Assert.AreEqual(0, obj.myField);
            Assert.IsNull(obj.MyProperty);
        }

        #endregion

        #region Private Methods

        private TypeInfo GetTypeInfo(Type type)
        {
            return ReflectionHelper.GetTypeInfo(type);
        }

        #endregion
    }

    #region Types

    public class TypeInfoTestType
    {
        public int myField;

        public string MyProperty { get; set; }
    }

    #endregion
}
