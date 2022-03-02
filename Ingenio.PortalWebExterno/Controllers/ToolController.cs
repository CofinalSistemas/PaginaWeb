using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO;
using Ingenio.BLL;

namespace Ingenio.PortalWebExterno.Controllers
{
    public class ToolController : Controller
    {
        // GET: Tool
        ToolBLL toolBll = new ToolBLL();
      
        public JsonResult AutoCompletePais(string term)
        {
            ICollection<pais> paises = toolBll.GetPais(term);
            List<object> paises2 = new List<object>();
            foreach (var item in paises)
            {
                paises2.Add(new
                {
                    value = item.codpais,
                    label = item.nombre.Trim()

                });
            }
            return Json(paises2, JsonRequestBehavior.AllowGet);
          
        }

        public JsonResult AutoCompleteDepartamentos(string term, string id)
        {
            ICollection<departamentos> departamentos = toolBll.GetDepartamentos(term, id);
            List<object> departamentos2 = new List<object>();
            foreach (var item in departamentos)
            {
                departamentos2.Add(new
                {
                    value = item.coddepartamento,
                    label = item.nombredepartamento.Trim()

                });
            }
            return Json(departamentos2, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AutoCompleteCiudad(string term, string id)
        {
            ICollection<ciudades> ciudades = toolBll.GetCiudades(term, id);
            List<object> ciudades2 = new List<object>();
            foreach (var item in ciudades)
            {
                ciudades2.Add(new
                {
                    value = item.codciudad,
                    label = item.nombreciudad.Trim()

                });
            }
            return Json(ciudades2, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AutoCompleteZona(string term, string id)
        {
            ICollection<zonasgeograficas> zonas = toolBll.GetZonas(term, id);
            List<object> zonas2 = new List<object>();
            foreach (var item in zonas)
            {
                zonas2.Add(new
                {
                    value = item.codzona,
                    label = item.nombrezona.Trim()

                });
            }
            return Json(zonas2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoCompleteComuna(string term, string id)
        {
            ICollection<comunas> comunas = toolBll.GetComunas(term, id);
            List<object> comunas2 = new List<object>();
            foreach (var item in comunas)
            {
                comunas2.Add(new
                {
                    value = item.codcomuna,
                    label = item.nombrecomuna.Trim()

                });
            }
            return Json(comunas2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoCompleteBarrio(string term, string id)
        {
            ICollection<barrios> barrios = toolBll.GetBarrios(term, id);
            List<object> barrios2 = new List<object>();
            foreach (var item in barrios)
            {
                barrios2.Add(new
                {
                    value = item.codbarrio,
                    label = item.nombre.Trim()

                });
            }
            return Json(barrios2, JsonRequestBehavior.AllowGet);
        }
    }
}
