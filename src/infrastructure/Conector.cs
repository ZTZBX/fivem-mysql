using CitizenFX.Core;
using MySqlConnector;

namespace fivem_mysql.Server
{
    
    public class Conector : Conection
    {
        public MySqlConnection connection;

        public Conector(string server, int port, string username, string password, string database)
        {

            Debug.WriteLine(@"
            
            MYSQL Database Connector

            ");
            string connectionString = $"Server={server};User ID={username};Password={password};Database={database}";
            this.connection = new MySqlConnection(connectionString);
        }

        public void Open()
        {
            try
            {
                if (this.is_conected == false)
                {
                    this.connection.Open();
                    this.is_conected = true;
                }
                
            }
            catch (MySqlException ex )
            {

                switch (ex.Number)
                {
                    case 0:
                        Debug.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Debug.WriteLine("Invalid username/password, please try again");
                        break;

                    default:
                        Debug.WriteLine(ex.Message);
                        break;
                }
            }
        }

        public void Close()
        {
            try
            {
                if (this.is_conected)
                {
                    this.connection.Close();
                    this.is_conected = false;
                }
            }
            catch (MySqlException ex )
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}