using Ingenio.Controllers.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO;
using Ingenio.BLL.Seguridad;
using Ingenio.BO.Ingenio;
using Ingenio.Models;

namespace Ingenio.Filters
{
    public class AllowAttribute : ActionFilterAttribute
    {
        Cifrado cifrado = new Cifrado();
        
        public string action { get; set; }
        public string actionDos { get; set; }
        public string http { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            AccountModels cuenta = new AccountModels();
            var controller = filterContext.Controller;
            string act = filterContext.ActionDescriptor.ActionName;
            string con = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string urlReturn = "/" + con + "/" + act;
            var aaaa = filterContext.HttpContext.Handler;
            var contex = controller.ControllerContext.HttpContext;
            var g = filterContext.RequestContext;

            try
            {
                    UsuarioBll usuariobll = new UsuarioBll();
                    Usuarios u = cuenta.GerUser();
                if (u == null)
                {
                    
                    Redirect(contex, filterContext, "/Home/Index");
                    return;
                }             
                AccountBll accountbll = new AccountBll();
                ICollection<Modulos> modulos;
                var PR = HttpContext.Current.Session["asociadoInfo"];
                if (HttpContext.Current.Session["asociadoInfo"] != null)
                {
                    modulos = cuenta._Modulos_Usu;                   
                }
                else
                {
                    modulos = accountbll.GetModulos(u.Id);
                    Usuarios usu = usuariobll.GetUserByName(u.UserName);

                    if (u.Activo==false || usu==null)
                    {
                        contex.Response.Redirect("/Account/AccessDenied");
                        filterContext.Result = new RedirectResult("/Account/AccessDenied");
                        return;
                    }
                    //modulos = accountbll.GetModulos(u.Id);//mirar que paso aqui
                }

                if (modulos.Where(x => (x.Nombre == action)).Select(x => x).Count() == 0 && action != null || !u.Activo )
                {
                    contex.Response.Redirect("/Account/AccessDenied");
                    filterContext.Result = new RedirectResult("/Account/AccessDenied");
                    return;
                }

                if (modulos.Where(x => (x.Nombre == actionDos)).Select(x => x).Count() == 0 && actionDos != null || !u.Activo)
                {
                    contex.Response.Redirect("/Account/AccessDenied");
                    filterContext.Result = new RedirectResult("/Account/AccessDenied");
                    return;
                }
            }
            catch (Exception e)
            {

            }
        }

        private void Redirect(HttpContextBase contex, ActionExecutingContext filterContext, string ruta)
        {
            contex.Response.Cookies["urlReturn"].Value = ruta;
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }
}