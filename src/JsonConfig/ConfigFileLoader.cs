using System;
using System.IO;

namespace JsonConfig
{
    internal class ConfigFileLoader : IConfigFileLoader
    {
        #region Constants

        internal const string DefaultConfigFileName = "app.json.config";

        #endregion

        #region IConfigLoader Members

        public string LoadConfigFile(string filePath)
        {
            return LoadFileContent(filePath);
        }

        public string LoadDefaultConfigFile()
        {
            return LoadConfigFile(GetDefaultConfigFilePath());
        }

        #endregion

        #region Private/Internal Methods

        private string LoadFileContent(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                using (var fileReader = new StreamReader(fileStream))
                {
                    return fileReader.ReadToEnd();
                }
            }
        }

        internal string GetDefaultConfigFilePath()
        {
            var configFileFullPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            var configFileDirectory = Path.GetDirectoryName(configFileFullPath);

            return Path.Combine(configFileDirectory, DefaultConfigFileName);
        }

        #endregion
    }
}
