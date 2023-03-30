using System;

using CitizenFX.Core;
using System.Collections.Generic;

namespace fivem_mysql.Server
{
    public class ServerMain : BaseScript
    {

        private DataBase newDatabaseConnection = new DataBase();

        public ServerMain(){
            Exports.Add("raw", new Func<string, IEnumerable<string[]>>(raw));
            Exports.Add("select", new Func<string, dynamic, string, IEnumerable<string[]>>(select));
            Exports.Add("insert", new Action<string, dynamic, dynamic>(insert));
        }

        private IEnumerable<string[]> select(string tableName, dynamic tableAtributes, string condition)
        {
            List<string> atri = new List<string> {};
            if (tableAtributes.Capacity != 0)
            {
                foreach (object element in tableAtributes)
                {
                    atri.Add(element.ToString());
                }
            }

            IEnumerable<string[]> result  = newDatabaseConnection.query.Select(tableName, atri, condition);
            return result;
        }

        private void insert(string tableName, dynamic tableAtributes, dynamic rows)
        {

            List<string> atri = new List<string> {};
            if (tableAtributes.Capacity != 0)
            {
                foreach (object element in tableAtributes)
                {
                    atri.Add(element.ToString());
                }
            }

            List<List<string>> rowsToAdd = new List<List<string>>();

            if (rows.Capacity != 0)
            {
                foreach (object row in rows)
                {
                    List<object> currentRow = row as List<object>;
                    List<string> currentRowResult = new List<string>();

                    foreach (object valueRow in currentRow)
                    {
                        currentRowResult.Add(valueRow.ToString());
                    }

                    rowsToAdd.Add(currentRowResult);
                }
            }

            newDatabaseConnection.query.Insert(tableName, atri, rowsToAdd);
        }

        private IEnumerable<string[]> raw(string query)
        {
            // this need to be Covariance and contravariance return data type
            IEnumerable<string[]> result = newDatabaseConnection.query.Raw(query);
            return result;
        }
    }
}