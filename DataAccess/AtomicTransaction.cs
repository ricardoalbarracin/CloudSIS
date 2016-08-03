using System;
using System.Data.Common;

namespace DataAccess
{
    
    /// <summary>
    ///  clase que maneja las Transacción atomicas
    /// </summary>    
    public class AtomicTransaction: IDisposable
    {
        public DbConnection Conn { get; set; }

        public DbTransaction Trans { get; set; }

        public DbCommand Cmd { get; set; }

        public AtomicTransaction(DbConnection conn, DbTransaction trans, DbCommand cmd)
        {
            Conn = conn;
            Trans = trans;
            Cmd = cmd;
        }

        /// <summary>
        /// Metodo que realiza la persistencia de los datos
        /// </summary>
        public void Commit()
        {
            Trans.Commit();
            Dispose();
        }

        /// <summary>
        /// Metodo que deshace la persistencia de los datos
        /// </summary>
        public void Rollback()
        {
            Trans.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            Conn.Close();
            Cmd.Dispose();
            Conn.Dispose();
        }
    }
}
