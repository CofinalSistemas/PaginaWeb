using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO;

namespace Ingenio.DAL
{
    public class PersCargoDAL
    {
        ModelContainer db = new ModelContainer();

        public ICollection<personacargo> GetPersCargo(int id)
        {
            ICollection<personacargo> personascargo = (from p in db.personacargo
                                                       where p.cedulaAsociado == id
                                                       select p).ToList();
            return personascargo;

        }
       
    }
}
