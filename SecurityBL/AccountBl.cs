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

        public dynamic GetCitasAsignadas(dynamic data)
        {
            TransactionResult result = new TransactionResult();
            DataAccessObject ourDB = new DataAccessObject("DBModelsAWS");
            var a = ourDB.ExecuteReader(
           @"SELECT m.nombres as medico, a.fecha ,  con.nombre as consultorio, e.nombre as esm, esp.nombres as especialidad  FROM agendamiento.citas c
                inner join agendamiento.agenda a on c.idagenda=a.id
                inner join agendamiento.medico m on a.idmedico=m.id
                inner join agendamiento.afiliado afi on c.idafiliado=afi.id
                inner join agendamiento.consultorio con on a.idconsultorio=con.id
                inner join agendamiento.esm e on (a.idesm=e.id)
                inner join agendamiento.especialidad esp on (a.idespecialidad=esp.id)
                where c.idafiliado = @Idafiliado;", data, true);
            result.DataObject = a;
            result.Message = "asdasdasd";
            return result;
        }
    }
}
