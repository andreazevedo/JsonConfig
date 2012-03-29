using System.IO;
using NUnit.Framework;

namespace JsonConfig.Test
{
    [TestFixture]
    internal class ConfigFileLoaderTest
    {
        [Test]
        public void GetDefaultConfigFilePath_ShouldReturnCorrectResult()
        {
            var filePath = new ConfigFileLoader().GetDefaultConfigFilePath();
            var fileName = Path.GetFileName(filePath);

            Assert.AreEqual(fileName, ConfigFileLoader.DefaultConfigFileName);
            Assert.AreNotEqual(filePath.Length, fileName.Length);
        }
    }
}
