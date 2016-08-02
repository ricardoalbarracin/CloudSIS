using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

using System.Collections.Specialized;

namespace DataAccess
{
    public class DataAccessObject
    {
        private string _ConnectionString = "";

        private string connectionStringName = "";
        private string providerName = "";
        DbProviderFactory factory;
        public string ConnectionStringName
        {

            get { return this.connectionStringName; }
            set
            {
                var a = ConfigurationManager.ConnectionStrings[value];
                this._ConnectionString =a.ConnectionString;
                this.providerName = a.ProviderName;
                factory =
                DbProviderFactories.GetFactory(providerName);
            }
        }

        public DataAccessObject(string ConnectionStringName)
        {
            this.ConnectionStringName = ConnectionStringName;
        }

        public DbConnection GetConnection()
        {
            DbConnection curConn = factory.CreateConnection();
            curConn.ConnectionString = _ConnectionString;
            return curConn;
        }
        public void cc()
        {
            DataAccessObject ourDB = new DataAccessObject("DBModels");
            DbDataReader table1 = ourDB.ExecuteReader("SELECT * FROM cargos; ");
            while (table1.Read())
            {
                string ccc = table1["cargo"].ToString();
                Console.WriteLine(ccc);
            }
            //dispose of the DataReader
            table1.Close();
            table1.Dispose();
        }

        public DbDataReader ExecuteReader(string cmdText)
        {
            DbConnection conn = this.GetConnection();
            DbCommand cmd = factory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
    }
}
