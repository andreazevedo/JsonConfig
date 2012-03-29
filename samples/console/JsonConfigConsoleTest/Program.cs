using System;
using JsonConfig;

namespace JsonConfigConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("name: {0}", JsonConfigManager.DefaultConfig.name);
            Console.WriteLine("host: {0}", JsonConfigManager.DefaultConfig.host);
            Console.WriteLine("port: {0}", JsonConfigManager.DefaultConfig.port);

            Console.ReadKey();
        }
    }
}
