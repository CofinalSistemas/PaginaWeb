using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ingenio.DAL;
using Ingenio.DAL.Seguridad;
using Ingenio.BO;
using Ingenio.BO.Ingenio;

namespace Ingenio.BLL.Seguridad
{
    public class AccountBll
    {
        AccountDal cuentaDal = new AccountDal();
        public dynamic Login(string user, string password, DateTime fechaAcceso,String ip)
        {
            user = user.ToUpper().Trim();
            return cuentaDal.Login(user, password, fechaAcceso,ip);
        }      

        public bool AccesoAModulo(int userId, string modulo)
        {
            return cuentaDal.AccesoAModulo(userId, modulo);
        }

        public ICollection<Modulos> GetModulos(int id)
        {
            return cuentaDal.GetModulos(id);
        }
      
    }
}
