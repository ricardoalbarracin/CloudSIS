using BLComponents;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBL
{
    [AspectLogger()]
    public class AccountBl
    {
        public dynamic GetUserByName(dynamic data)
        {
            TransactionResult result = new TransactionResult();
            DataAccessObject ourDB = new DataAccessObject("DBModelsAWS");
            var a = ourDB.ExecuteReader(
           @"SELECT p.id, p.nombres , p.clave, p.documento FROM
            agendamiento.afiliado p where p.email=@Email and p.clave=@Clave;", data,false );           
            result.DataObject = a;
            result.Message = "asdasdasd";
            return result;
        }
    }
}
