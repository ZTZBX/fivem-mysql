using System.IO;
using static CitizenFX.Core.Native.API;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace fivem_mysql.Server
{
    public class DataBaseConfiguration
    {
        public string hostname { get; set; }
        public int port { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string database { get; set; }
    }

    public class YamlConfig
    {
        public DataBaseConfiguration DataBase;
        public YamlConfig(string filePath)
        {
            string contents = File.ReadAllText($"{GetResourcePath(GetCurrentResourceName())}/{filePath}");
            var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
            DataBase = deserializer.Deserialize<DataBaseConfiguration>(contents);
        }
    }
}