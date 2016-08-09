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
           @"SELECT p.id, p.primer_nombre || ' ' ||p.primer_apellido as nombre, u.id, u.nombre as usuario, u.clave, u.persona, u.estado FROM
            seg.usuarios u INNER JOIN seg.personas p  ON(p.id = u.persona)  where u.nombre=@Email;", data,false );           
            result.DataObject = a;
            result.Message = "asdasdasd";
            return result;
        }
    }
}
