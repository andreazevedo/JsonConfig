using System.Web.Script.Serialization;
using NUnit.Framework;

namespace JsonConfig.Test
{
    [TestFixture]
    public class DynamicJsonConverterTest
    {
        [Test]
        public void Deserialize_ShouldNotReturnNull_NorThrowException()
        {
            const string json = 
@"{
    name : ""Nice name"",
    servers : [
        {
            host: ""192.168.0.1"",
            port: 80
        },
        {
            host: ""192.168.0.2"",
            port: 8080
        }
    ]
}";
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            dynamic obj = serializer.Deserialize(json, typeof (object));

            Assert.IsNotNull(obj);
        }

        [Test]
        public void Deserialize_ShouldReturnNull()
        {
            const string json = "";

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            dynamic obj = serializer.Deserialize(json, typeof(object));

            Assert.IsNull(obj);
        }
    }
}
