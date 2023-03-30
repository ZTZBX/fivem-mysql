namespace fivem_mysql.Server
{  
    public class DataBase
    {
        public Query query;

        public DataBase()
        {
            YamlConfig config = new YamlConfig("Config.yaml");
            query = new Query(
                config.DataBase.hostname,
                config.DataBase.port,
                config.DataBase.username,
                config.DataBase.password,
                config.DataBase.database
            );
        }
    }
}