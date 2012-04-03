using System.Collections.Generic;
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
    ],
    nested : {
        child: {
            name: ""Something"",
            surnames: [
                ""Surname 1"",
                ""Surname 2""
            ]
        }
    }
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

        [Test]
        public void TryConvert_ToAClassWithFields_ShouldGetCorrectObject()
        {
            dynamic jsonObj = GetJsonObject(Json);

            var server = (ServerWithFields)jsonObj.servers[0];
            
            Assert.IsNotNull(server);
            Assert.AreEqual("192.168.0.1", server.host);
            Assert.AreEqual(80, server.port);
        }

        [Test]
        public void TryConvert_ToAClassWithProperties_ShouldGetCorrectObject()
        {
            dynamic jsonObj = GetJsonObject(Json);

            var server = (ServerWithProperties)jsonObj.servers[0];

            Assert.IsNotNull(server);
            Assert.AreEqual("192.168.0.1", server.host);
            Assert.AreEqual(80, server.port);
        }

        [Test]
        public void TryConvert_ToAClassWithUpperCasedFields_ShouldGetCorrectObject()
        {
            dynamic jsonObj = GetJsonObject(Json);

            var server = (ServerWithUpperCasedFields)jsonObj.servers[0];

            Assert.IsNotNull(server);
            Assert.AreEqual("192.168.0.1", server.Host);
            Assert.AreEqual(80, server.Port);
        }

        [Test]
        public void TryConvert_ToAClassWithUpperCasedProperties_ShouldGetCorrectObject()
        {
            dynamic jsonObj = GetJsonObject(Json);

            var server = (ServerWithUpperCasedProperties)jsonObj.servers[0];

            Assert.IsNotNull(server);
            Assert.AreEqual("192.168.0.1", server.Host);
            Assert.AreEqual(80, server.Port);
        }

        [Test]
        public void TryConvert_ShouldGetCorrectObject()
        {
            dynamic jsonObj = GetJsonObject(Json);


            var obj = (JsonObject)jsonObj;


            Assert.IsNotNull(obj);
            Assert.AreEqual("Nice name", obj.Name);

            Assert.AreEqual(2, obj.Servers.Count);
            Assert.AreEqual("192.168.0.1", obj.Servers[0].Host);
            Assert.AreEqual(80, obj.Servers[0].Port);
            Assert.AreEqual("192.168.0.2", obj.Servers[1].Host);
            Assert.AreEqual(8080, obj.Servers[1].Port);

            Assert.IsNotNull(obj.Nested);
            Assert.IsNotNull(obj.Nested.Child);
            Assert.AreEqual("Something", obj.Nested.Child.Name);
            Assert.AreEqual(2, obj.Nested.Child.Surnames.Count);
            Assert.AreEqual("Surname 1", obj.Nested.Child.Surnames[0]);
            Assert.AreEqual("Surname 2", obj.Nested.Child.Surnames[1]);
        }

        [Test]
        public void ToString_ShouldReturnCorrectResult()
        {
            dynamic jsonObj = GetJsonObject(Json);

            const string expected = @"{name:""Nice name"",servers:[{host:""192.168.0.1"",port:80},{host:""192.168.0.2"",port:8080}],nested:{child:{name:""Something"",surnames:[""Surname 1"",""Surname 2""]}}}";
            var result = jsonObj.ToString();

            Assert.AreEqual(expected, result);
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

    #region Mapping Classes

    public class ServerWithFields
    {
        public string host;
        public int port;
    }

    public class ServerWithProperties
    {
        public string host { get; set; }

        public int port { get; set; }
    }

    public class ServerWithUpperCasedFields
    {
        public string Host;
        public int Port;
    }

    public class ServerWithUpperCasedProperties
    {
        public string Host { get; set; }

        public int Port { get; set; }
    }



    public class JsonObject
    {
        public string Name { get; set; }

        public IList<ServerWithUpperCasedProperties> Servers { get; set; }

        public Parent Nested { get; set; }
    }

    public class Parent
    {
        public Child Child { get; set; }
    }

    public class Child
    {
        public string Name { get; set; }
        public IList<string> Surnames { get; set; }
    }

    #endregion
}
