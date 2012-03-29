using System;
using System.IO;
using NUnit.Framework;

namespace JsonConfig.Test
{
    [TestFixture]
    public class JsonConfigManagerTest
    {
        #region Constants

        private const string SimpleJson = @"{name:""Andre Azevedo"",age:26}";

        #endregion

        [Test]
        public void GetConfig_ShouldReturnCorrectObject()
        {
            var config = JsonConfigManager.GetConfig(SimpleJson);

            var name = config.name;
            Assert.AreEqual("Andre Azevedo", name);

            var age = config.age;
            Assert.AreEqual(26, age);

            var sex = config.sex;
            Assert.IsNull(sex);
        }

        /// <summary>
        /// This test actually reads the file app.json.config located at the root of this test project.
        /// </summary>
        [Test]
        public void DefaultConfig_ShouldReturnCorrectObject()
        {
            var name = JsonConfigManager.DefaultConfig.name;
            Assert.AreEqual("Andre Azevedo", name);

            var age = JsonConfigManager.DefaultConfig.age;
            Assert.AreEqual(26, age);

            var sex = JsonConfigManager.DefaultConfig.sex;
            Assert.AreEqual("M", sex);
        }

        /// <summary>
        /// This test actually reads the file anotherConfig.js located at the root of this test project.
        /// </summary>
        [Test]
        public void LoadConfig_ShouldReturnCorrectObject()
        {
            var directory = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            var filePath = Path.Combine(directory, "anotherConfig.js");

            var config = JsonConfigManager.LoadConfig(filePath);

            var name = config.name;
            Assert.AreEqual("another config", name);

            var age = config.age;
            Assert.IsNull(age);
        }
    }
}
