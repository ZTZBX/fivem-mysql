using System;

using System.Collections.Generic;
using MySqlConnector;

namespace fivem_mysql.Server
{

    static public class SerializerDatabase
    {
        static public List<string[]> Query(MySqlDataReader data)
        {

            List<string[]> result = new List<string[]>();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    string[] q_values = new String[data.FieldCount];

                    for (int i = 0; i < data.FieldCount; i++)
                    {
                        q_values.SetValue(data.GetValue(i).ToString(), i);
                    }
                    result.Add(q_values);
                }
            }

            return result;
        }
    }
    
}