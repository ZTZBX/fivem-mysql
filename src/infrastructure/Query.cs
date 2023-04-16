using System.Collections.Generic;
using CitizenFX.Core;

using MySqlConnector;

namespace fivem_mysql.Server
{

    public class Query : Conector
    {

        private List<string[]> emptyData = new List<string[]>();


        public Query(string server, int port, string username, string password, string database) : base(server, port, username, password, database) { }

        public List<string[]> Raw(string raw_query)
        {
            this.Open();
            if (this.is_conected)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(raw_query, this.connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    List<string[]> result = SerializerDatabase.Query(dataReader);
                    this.Close();
                    return result;
                }
                catch (MySqlException ex)
                {
                    this.Close();
                    Debug.WriteLine(ex.Message);
                }
            }
            return this.emptyData;
        }

        public List<string[]> Select(string tableName, List<string> tableAtributes, string condition = null)
        {
            this.Open();
            if (this.is_conected)
            {
                string current_atributes;
                string query;

                if (tableAtributes.Capacity == 0)
                {
                    current_atributes = "*";
                }
                else
                {
                    current_atributes = string.Join(",", tableAtributes);
                }

                if (condition == null)
                {
                    query = $@"
                    SELECT {current_atributes} FROM {tableName}
                    ";
                }
                else
                {
                    query = $@"
                    SELECT {current_atributes} FROM {tableName}
                    WHERE {condition}
                    ";
                }

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    List<string[]> result = SerializerDatabase.Query(dataReader);

                    this.Close();
                    return result;

                }
                catch (MySqlException ex)
                {
                    this.Close();
                    Debug.WriteLine(ex.Message);
                }
            }
            
            return this.emptyData;
        }

        public void Insert(string tableName, List<string> tableAtributes, List<List<string>> rows)
        {
            this.Open();
            if (this.is_conected)
            {
                string query = $@"
            INSERT INTO {tableName} ({string.Join(",", tableAtributes)})
            VALUES
            ";

                string values = "";

                foreach (List<string> value in rows)
                {
                    string current_row = $"({string.Join(",", value)}),\n";
                    values += current_row;
                }

                query += values;
                query = query.Remove(query.Length - 2);

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);
                    cmd.ExecuteNonQuery();
                    
                    this.Close();
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                    this.Close();
                }
            }
        }
    }
}