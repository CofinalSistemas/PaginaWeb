using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO;
using Ingenio.BLL;
using Ingenio.Filters;
using Ingenio.Models;

namespace Ingenio.PortalWebExterno.Controllers
{
    [Allow(action = "AsociadosModulo")]
    public class PersCargoController : Controller
    {
        AccountModels cuentaUsuario = new AccountModels();
        PersCargoBLL PersCargBll = new PersCargoBLL();
        public ActionResult Index()
        {
            int id = cuentaUsuario.Identificacion;        
            ICollection<personacargo> perCargo = PersCargBll.GetPersCargo(id);
            return View("",perCargo);
        }
         
    }
}
