using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Slapper;

namespace DataAccess
{
    public class modelo
    {
        public int id { get; set; }
        public string cargo { get; set; }
        public string sede { get; set; }
        public int salario { get; set; }
    }
    public class DataAccessObject
    {
        private string _ConnectionString = "";
        private string connectionStringName = "";
        private string providerName = "";
        DbProviderFactory factory;

        public string ConnectionStringName
        {
            get { return connectionStringName; }
        }

        public DataAccessObject(string ConnectionStringName)
        {
            connectionStringName = ConnectionStringName;
            var a = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            _ConnectionString = a.ConnectionString;
            providerName = a.ProviderName;
            factory = DbProviderFactories.GetFactory(providerName);
        }

        public DbConnection GetConnection()
        {
            DbConnection curConn = factory.CreateConnection();
            curConn.ConnectionString = _ConnectionString;
            return curConn;
        }

        public IEnumerable<T> ExecuteReader<T>(string cmdText)
        {
            
            DbConnection conn = GetConnection();
            DbCommand cmd = factory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            try
            {
                conn.Open();
                DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                
                var list = new List<IDictionary<string, object>>();
                
                if (reader.Read())
                {
                    
                    var count = reader.FieldCount;
                    string rowName, rowType;
                    dynamic rowValue;

                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();

                        for (int i = 0; i < count; i++)
                        {
                            rowName = reader.GetName(i);
                            rowValue = reader.GetValue(i);
                            rowType = reader.GetDataTypeName(i);

                            switch (rowType)
                            {
                                case "Decimal":
                                    if (reader.IsDBNull(i)) row[rowName] = null;
                                    else row[rowName] = (double)rowValue;
                                    break;

                                case "Date":
                                    if (reader.IsDBNull(i)) row[rowName] = null;
                                    else row[rowName] = (DateTime)rowValue;
                                    break;

                                default:
                                    row[rowName] = reader.IsDBNull(i) ? null : rowValue;
                                    break;
                            }
                        }

                        list.Add(row);
                    }

                    reader.Dispose();
                }
                reader.Close();
                reader.Dispose();
                
                return list.Select(item => AutoMapper.Map<T>(item)); ;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
    }
}
