using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Slapper;
using System.Globalization;
using System.Data.SqlClient;

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
        TextInfo textInfo;

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
            textInfo = new CultureInfo("en-US").TextInfo;
        }

        public AtomicTransaction CreateAtomicTransaction()
        {
            DbConnection conn = GetConnection();
            DbCommand cmd = factory.CreateCommand();
            cmd.Connection = conn;
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            AtomicTransaction atomicTransaction = new AtomicTransaction(conn, trans, cmd);
            return atomicTransaction;
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
                if (reader.HasRows)
                {
                    var count = reader.FieldCount;
                    string rowName, rowType;
                    dynamic rowValue;
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < count; i++)
                        {
                            rowName = ToTitleCase(reader.GetName(i), textInfo);
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
                throw ex;
            }
        }

        public int ExecuteNonQuery(string cmdText , object parameters =null, AtomicTransaction atom=null )
        {
            DbConnection conn;
            DbCommand cmd;
            if (atom != null)
            {
                conn = atom.Conn;
                cmd = atom.Cmd;
            }
            else
            {
                conn = GetConnection();
                cmd = factory.CreateCommand();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            
            string[] propertyNames = parameters.GetType().GetProperties().Select(p => p.Name).ToArray();
            foreach (var prop in propertyNames)
            {
                object propValue = parameters.GetType().GetProperty(prop).GetValue(parameters, null);
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = "@" + prop;
                param.Value = propValue;
                cmd.Parameters.Add(param);
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Metodo que convierte cadena de texto a formato Pascal Casing
        /// </summary>
        /// <param name="value">Cadena de texto a convertir</param>
        /// <param name="textInfo">Objeto culturizado para la conversion</param>
        /// <returns></returns>
        public static string ToTitleCase(string value, TextInfo textInfo)
        {
            string result = textInfo.ToTitleCase(value.ToLower()).Replace("_", string.Empty);
            return result;
        }
    }
}
