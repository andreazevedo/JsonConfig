namespace JsonConfig
{
    internal interface IConfigFileLoader
    {
        string LoadConfigFile(string path);

        string LoadDefaultConfigFile();
    }
}
