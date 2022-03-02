using Ingenio.BO.Ingenio;
using Ingenio.DLL;
using Ingenio.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ingenio.PortalWebExterno.Controllers
{
    
    public class DianController : Controller
    {
        ExtractosBLL extBll = new ExtractosBLL();

        // GET: Dian
        [Allow(action = "AsociadosModulo")]
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Certificado()
        {
          
            ViewBag.Cert = "1";
            return View("Index");
        }

        // GET: Dian/Details/5
        [Allow(action = "REPORTES_UIAF")]
        public ActionResult Details()
        {
            ICollection<Param_dian> Intereses = extBll.listinteres();
            return View("", Intereses);
            
        }
        
        public JsonResult Guarda(string paga, string dedu)
        {
            try {
                
                Param_dian dat = new Param_dian();
                dat.INTERES_PAGADOS = paga;
                dat.INTERES_DEDUCIBLES = dedu;

                extBll.SaveInteres(dat);

                return Json(new
                {
                    estado = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception a)
            {
                return Json(new
                {
                    estado = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Dian/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dian/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dian/Edit/5
        [Allow(action = "REPORTES_UIAF")]
        public ActionResult Edit(int id)
        {
            Param_dian Intereses = extBll.actuainteres(id);
            return View(Intereses);
            
        }

        // POST: Dian/Edit/5
        [Allow(action = "REPORTES_UIAF")]
        [HttpPost]
        public ActionResult Edit(int id, Param_dian dat)
        {
            try
            {
                // TODO: Add update logic here
                

                extBll.SaveInteres(dat);

                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dian/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dian/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
