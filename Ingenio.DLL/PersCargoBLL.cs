using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO;
using Ingenio.DAL;

namespace Ingenio.BLL
{
    public class PersCargoBLL
    {
        PersCargoDAL PersCargDAL = new PersCargoDAL();

        public ICollection<personacargo> GetPersCargo(int id)
        {
            return PersCargDAL.GetPersCargo(id);
        }      
     
    }
}
