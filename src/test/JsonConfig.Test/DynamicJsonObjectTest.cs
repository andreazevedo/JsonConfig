using System.Web.Script.Serialization;
using NUnit.Framework;

namespace JsonConfig.Test
{
    [TestFixture]
    public class DynamicJsonObjectTest
    {
        #region Constants

        const string Json =
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

        #endregion

        #region Test Methods

        [Test]
        public void TryGetMember_UsingDynamicTyping_OnSimpleProperty_ShouldReturnCorrectResult()
        {
            dynamic jsonObj = GetJsonObject(Json);

            string name = jsonObj.name;
            Assert.AreEqual("Nice name", name);
        }

        [Test]
        public void TryGetMember_UsingDynamicTyping_OnArrayLength_ShouldReturnCorrectResult()
        {

            dynamic jsonObj = GetJsonObject(Json);

            int numOfServers = jsonObj.servers.Count;
            Assert.AreEqual(2, numOfServers);
        }

        [Test]
        public void TryGetMember_UsingDynamicTyping_OnArrayItem_ShouldReturnCorrectResult()
        {

            dynamic jsonObj = GetJsonObject(Json);

            string firstServerHost = jsonObj.servers[0].host;
            Assert.AreEqual("192.168.0.1", firstServerHost);
        }

        #endregion

        #region Private Methods

        private dynamic GetJsonObject(string json)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            return serializer.Deserialize(json, typeof(object));
        }

        #endregion
    }
}
