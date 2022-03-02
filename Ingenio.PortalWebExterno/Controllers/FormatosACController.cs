using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ingenio.PortalWebExterno.Controllers
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// Muestra los pasos para afiliarse y pedir creditos de cofinal
    /// </summary>
    ///<param name="archivo">argumento tipo FILE es enviado desde index formulario</param>
    public class FormatosACController : Controller
    {
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra contenido de como afiliarse, con sus respectivos formularios y requisitos iniciales
        /// </summary>
        // GET: FormatosAC
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra contenido de como pedir un credito , los requisitos iniciales
        /// </summary>        
        // GET: FormatosAC/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: FormatosAC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormatosAC/Create
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

        // GET: FormatosAC/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FormatosAC/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FormatosAC/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FormatosAC/Delete/5
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
