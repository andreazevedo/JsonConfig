using System.Web.Script.Serialization;

namespace JsonConfig
{
    public static class JsonConfigManager
    {
        #region Fields

        private static readonly IConfigFileLoader ConfigFileLoader = new ConfigFileLoader();
        private static dynamic _defaultConfig;

        #endregion

        #region Public Members

        public static dynamic DefaultConfig
        {
            get
            {
                if (_defaultConfig == null)
                {
                    _defaultConfig = GetConfig(ConfigFileLoader.LoadDefaultConfigFile());
                }
                return _defaultConfig;
            }
        }

        public static dynamic GetConfig(string json)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            return serializer.Deserialize(json, typeof(object));
        }

        public static dynamic LoadConfig(string filePath)
        {
            return GetConfig(ConfigFileLoader.LoadConfigFile(filePath));
        }

        #endregion
    }
}
