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

            Assert.AreEqual("Andre Azevedo", config.name);
            Assert.AreEqual(26, config.age);
            Assert.IsNull(config.sex);
        }

        /// <summary>
        /// This test actually reads the file app.json.config located at the root of this test project.
        /// </summary>
        [Test]
        public void DefaultConfig_ShouldReturnCorrectObject()
        {
            var config = JsonConfigManager.DefaultConfig;

            Assert.AreEqual("Andre Azevedo", config.name);
            Assert.AreEqual(26, config.age);
            Assert.AreEqual("M", config.sex);
        }

        /// <summary>
        /// This test actually reads the file anotherConfig.js located at the root of this test project.
        /// </summary>
        [Test]
        public void LoadConfig_ShouldReturnCorrectObject()
        {
            var directory = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            var filePath = Path.Combine(directory, "anotherConfig.json");

            var config = JsonConfigManager.LoadConfig(filePath);

            Assert.AreEqual("another config", config.name);
            Assert.IsNull(config.age);
        }
    }
}
