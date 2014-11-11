# JsonConfig 

JsonConfig is a simple configuration framework based on json and the dynamic type avaible in .NET Framework 4.0+ 


## Getting Started

1) In your project, add reference to JsonConfig.dll


2) Add a file to your project named "app.json.config" with your configuration in a json format. Example:

```javascript
{
  name: "My Project",
  host: "localhost",
  port: 80
}
```


3) If your project is not a web project, you need to set the file to "Copy to the output directory if changed".


4) Just use the dynamic config object in your code as follows:

```csharp
Console.WriteLine("name: {0}", JsonConfigManager.DefaultConfig.name);
Console.WriteLine("host: {0}", JsonConfigManager.DefaultConfig.host);
Console.WriteLine("port: {0}", JsonConfigManager.DefaultConfig.port);
```


## Config file

You can use any config file name you want. Checkout the unit tests to find out how.


## Licensing

Released under the MIT license.
