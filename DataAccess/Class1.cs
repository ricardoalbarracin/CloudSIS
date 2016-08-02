using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Data.Common;


using System.Collections;
using System.Configuration;
using System.Threading;
using System.Collections.Specialized;

namespace DataAccess
{
    public class PostgrePostgreSQLAccess
    {

    }

    class DataAccess
    {
        private string _ConnectionString = "";

        public string ConnectionString
        {

            get { return this._ConnectionString; }
            set
            {
                this._ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;
               // connection = new OracleConnection(connectionString);
                this._ConnectionString = value;
            }
        }

        public DataAccess(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        private DbConnection GetConnection()
        {
            NpgsqlConnection curConn = new NpgsqlConnection(this.ConnectionString);
            return curConn;
        }
        public void cc()
        {
            
            DataAccess ourDB = new DataAccess("server=dbserver;Port=5432;User Id=user;Password=;Database=our_db");
            //create the DataReader
            DbDataReader table1 = ourDB.ExecuteReader("SELECT field1 FROM table1;");
            //Read through the table and print field1
            while (table1.Read())
            {
                Console.WriteLine(table1["field1"].ToString());
            }
            //dispose of the DataReader
            table1.Close();
            table1.Dispose();
        }

        public DbDataReader ExecuteReader(string cmdText)
        {
            NpgsqlConnection conn = null;
            NpgsqlCommand cmd = null;

            try
            {
                conn = this.GetConnection() as NpgsqlConnection;
                cmd = new NpgsqlCommand(cmdText, conn);
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
