using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorController : Controller
    {
        //GET: Error
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 500:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Error 500 -Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;
                case 404:
                    ViewBag.Title = "Lo sentimos - esta página ya no está aquí";
                    ViewBag.Description = "Error 404 - Página no encontrada";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    break;
            }

            return View("~/views/error/PaginaError.cshtml");
        }
    }
}