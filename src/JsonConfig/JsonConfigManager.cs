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

        /// <summary>
        /// Gets the default config dynamic object, which should be a file named app.json.config located at the root of your project
        /// </summary>
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

        /// <summary>
        /// Get a config dynamic object from the json
        /// </summary>
        /// <param name="json">Json string</param>
        /// <returns>The dynamic config object</returns>
        public static dynamic GetConfig(string json)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            return serializer.Deserialize(json, typeof(object));
        }

        /// <summary>
        /// Load a config from a specified file
        /// </summary>
        /// <param name="filePath">Config file path</param>
        /// <returns>The dynamic config object</returns>
        public static dynamic LoadConfig(string filePath)
        {
            return GetConfig(ConfigFileLoader.LoadConfigFile(filePath));
        }

        #endregion
    }
}
