using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO;
using Ingenio.BLL;
using System.Globalization;
using Ingenio.Filters;
using Ingenio.BO.Ingenio;
using Ingenio.Models;
using System.Diagnostics;

namespace Ingenio.PortalWebExterno.Controllers
{
    
    public class AsociadoController : Controller
    {
        // GET: Asociado

        AsociadoBLL asocBll = new AsociadoBLL();
        ToolBLL toolBll = new ToolBLL();
        [Allow(action = "LISTA_ASOCIADOS")]
        public ActionResult index()
        {
            ICollection<Asociados_Ingenio> AsociadoIngenio = asocBll.asociados();
            return View("", AsociadoIngenio);
        }
        // POST: PersonalDetails/Delete/5
       
        public JsonResult EliminarAso(int id)
        {
            bool a= asocBll.EliminarAso(id);
            return Json(new
            {
                estado = a,
                mensaje = "Asociado eliminado",
            }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra datos personales del asociado para luego ser consultados los historicos de pago cuentas
        /// </summary>
        // GET: Asociado/Details/5
        [Allow(action = "AsociadosModulo")]
        public ActionResult Details()
        {
            AccountModels cuentaUsuario = new AccountModels();
            int id = cuentaUsuario.Identificacion;
            asociados aso = asocBll.GetAsociado(id);
            ViewBag.Aso = aso;
            TimeSpan dias = DateTime.Now - Convert.ToDateTime(aso.nits.fechaingreso);
            ViewBag.antiguedad = Math.Abs((aso.nits.fechaingreso.Month - DateTime.Now.Month) + 12 * (aso.nits.fechaingreso.Year - DateTime.Now.Year));
            ViewBag.agencia = aso.nits.agencia + "-" + asocBll.GetAgencias(aso);
            if (aso.periododeduce == "M") ViewBag.periododeduce = "Mensual";
            if (aso.periododeduce == "Q") ViewBag.periododeduce = "Quiencenal";
            if (aso.periododeduce == "O") ViewBag.periododeduce = "Catorcenal";
            if (aso.periododeduce == "D") ViewBag.periododeduce = "Decadal";
            if (aso.periododeduce == "E") ViewBag.periododeduce = "Semanal";
            return View();
        }
        [Allow(action = "AsociadosModulo")]
        public class CAhorros
        {
            public string tipo_cuenta { get; set; }
            public int año { get; set; }
            public int mes { get; set; }
            public string nombre { get; set; }
            public string numerocuenta { get; set; }
            public long cedulasociado { get; set; }
            public string codlinea { get; set; }
            public string fechainicio { get; set; }
            public string fechavencimiento { get; set; }
            public string anulidad { get; set; }
            public string saldoponersedia { get; set; }
            public decimal pagare { get; set; }
            public string estado { get; set; }
            public string cuota { get; set; }
            public decimal cuotamora { get; set; }
            public string saldototal { get; set; }
            public decimal interes { get; set; }

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los historicos de los movimientos  de cuentas
        /// </summary>
        /// <param name="anio">argumento tipo numero es enviado desde details formulario</param>
        /// <param name="mes">argumento tipo numero es enviado desde details formulario</param>
        /// <param name="nit">argumento tipo numero es enviado desde details formulario</param>
        [Allow(action = "AsociadosModulo")]
        public ActionResult GetAhorros(int anio, int mes, int nit)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            dynamic ahorro = asocBll.GetAhorros(anio, mes, nit);

            try
            {
                var permanente = ahorro.ahorroPermanente;// permanentes
                var AhorroSocial = ahorro.ahorroSocial;// social
                var AhorroATermino = ahorro.ahorroAtermino;// a termino
                var AhorroAVista = ahorro.ahorroVista;// a termino
                var AhorroContractual = ahorro.ahorroContractual;// a termino
                var Creditos = ahorro.Creditos;
                List<CAhorros> parts = new List<CAhorros>();

                foreach (var item in permanente)
                {
                    parts.Add(new CAhorros() { tipo_cuenta = "P", año = item.año, mes = item.mes, codlinea = item.codlinea, numerocuenta = item.numerocuenta, nombre = item.nombre, fechainicio = Convert.ToString(item.fechainicio), fechavencimiento = Convert.ToString(item.fechavencimiento), cuota = item.cuota.ToString("C", nfi), saldototal = item.saldototal.ToString("C", nfi), interes = item.interes });
                }
                foreach (var item in AhorroSocial)
                {
                    parts.Add(new CAhorros() { tipo_cuenta = "S", año = item.año, mes = item.mes, codlinea = item.codlinea, numerocuenta = item.numerocuenta, nombre = item.nombre, fechainicio = Convert.ToString(item.fechainicio), fechavencimiento = Convert.ToString(item.fechavencimiento), cuota = item.cuota.ToString("C", nfi), saldototal = item.saldototal.ToString("C", nfi), interes = item.interes });
                }
                foreach (var item in AhorroATermino)
                {
                    parts.Add(new CAhorros() { tipo_cuenta = "AT", año = item.año, mes = item.mes, codlinea = item.codlinea, numerocuenta = item.numerocuenta, nombre = item.nombre, fechainicio = Convert.ToString(item.fechainicio), fechavencimiento = Convert.ToString(item.fechavencimiento), cuota = item.cuota.ToString("C", nfi), saldototal = item.saldototal.ToString("C", nfi), interes = item.interes });
                }
                foreach (var item in AhorroContractual)
                {
                    parts.Add(new CAhorros() { tipo_cuenta = "AC", año = item.año, mes = item.mes, codlinea = item.codlinea, numerocuenta = item.numerocuenta, nombre = item.nombre, fechainicio = Convert.ToString(item.fechainicio), fechavencimiento = Convert.ToString(item.fechavencimiento), cuota = item.cuota.ToString("C", nfi), saldototal = item.saldototal.ToString("C", nfi), interes = item.interes });
                }
                foreach (var item in AhorroAVista)
                {
                    parts.Add(new CAhorros() { tipo_cuenta = "AV", año = item.año, mes = item.mes, codlinea = item.codlinea, numerocuenta = item.numerocuenta, nombre = item.nombre, fechainicio = Convert.ToString(item.fechainicio), fechavencimiento = Convert.ToString(item.fechavencimiento), cuota = item.cuota.ToString("C", nfi), saldototal = item.saldototal.ToString("C", nfi), interes = item.interes });
                }
                foreach (var item in Creditos)
                {
                    parts.Add(new CAhorros() { tipo_cuenta = "C", año = item.año, mes = item.mes, cedulasociado = item.cedulasociado, codlinea = item.codlinea, nombre = item.nombre, fechainicio = Convert.ToString(item.fechainicio), fechavencimiento = Convert.ToString(item.fechavencimiento), cuotamora = item.cuotasmora, anulidad = item.anulidad.ToString("C", nfi), saldototal = item.saldocapital.ToString("C", nfi), saldoponersedia = item.saldoponersedia.ToString("C", nfi), pagare = item.pagare, estado = item.estado == "V" ? "Vigente" : item.estado });
                }

                return Json(parts, JsonRequestBehavior.AllowGet);
            }
            catch (Exception er) {
                Trace.WriteLine("Controlador Asociado, funcion GetAhorros " + er.Message.ToString(), "Error " + DateTime.Now);
                return Json(false, JsonRequestBehavior.AllowGet); }
        }

        [Allow(action = "AsociadosModulo")]
        // GET: Asociado/Create
        public ActionResult Create()
        {
            return View();
        }
        [Allow(action = "AsociadosModulo")]
        // POST: Asociado/Create
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
        [Allow(action = "AsociadosModulo")]
        //autocomplete profesiones
        public JsonResult AutoCompleteProfesion(string term)
        {
            ICollection<profesiones> profesiones = asocBll.GetProfesiones(term);
            List<object> profesiones2 = new List<object>();
            foreach (var item in profesiones)
            {
                profesiones2.Add(new
                {
                    value = item.codprofesion,
                    label = item.nombreprofesion.Trim()

                });
            }
            return Json(profesiones2, JsonRequestBehavior.AllowGet);
        }
        [Allow(action = "AsociadosModulo")]
        //autocomplete empresas
        public JsonResult AutoCompleteEmpresa(string term)
        {
            ICollection<empresas> empresas = asocBll.GetEmpresas(term);
            List<object> empresas2 = new List<object>();
            foreach (var item in empresas)
            {
                empresas2.Add(new
                {
                    value = item.codempresa,
                    label = item.nombreempresa.Trim()

                });
            }
            return Json(empresas2, JsonRequestBehavior.AllowGet);
        }
        [Allow(action = "AsociadosModulo")]
        //autocomplete depedencias
        public JsonResult AutoCompleteDependencias(string term, string id)
        {
            ICollection<dependenciasempresas> dependecias = asocBll.GetDependencias(term, id);
            List<object> dependecias2 = new List<object>();
            foreach (var item in dependecias)
            {
                dependecias2.Add(new
                {
                    value = item.coddependencia,
                    label = item.nombredependencia.Trim()

                });
            }
            return Json(dependecias2, JsonRequestBehavior.AllowGet);
        }
        [Allow(action = "AsociadosModulo")]
        //autocomplete empresas
        public JsonResult AutoCompleteCiiu(string term)
        {
            ICollection<ciiu> ciiu1 = asocBll.GetCiiu(term);
            List<object> ciiu2 = new List<object>();
            foreach (var item in ciiu1)
            {
                ciiu2.Add(new
                {
                    value = item.codciiu,
                    label = item.nombre.Trim()

                });
            }
            return Json(ciiu2, JsonRequestBehavior.AllowGet);
        }
        [Allow(action = "AsociadosModulo")]
        //autocomplete depedencias
        public JsonResult AutoCompleteDivision(string term, string id)
        {
            ICollection<divisionciiu> divciiu = asocBll.GetDivisionciiu(term, id);
            List<object> divciiu2 = new List<object>();
            foreach (var item in divciiu)
            {
                divciiu2.Add(new
                {
                    value = item.coddivision,
                    label = item.nombre.Trim()

                });
            }
            return Json(divciiu2, JsonRequestBehavior.AllowGet);
        }
     //   [Allow(action = "AsociadosModulo")]
        [HandleError]
        // GET: Asociado/Edit/5
        public ActionResult Edit(int id)
        {
            asociados aso = asocBll.GetAsociado(id);

            try
            {
                ViewBag.depart = toolBll.GetDepartamento(aso.nits.coddepartamento);
                ViewBag.ciudad = toolBll.GetCiudad(aso.nits.codciudad);
                ViewBag.zona = toolBll.GetZona(aso.nits.codzona);
                ViewBag.comuna = toolBll.GetComuna(aso.nits.codcomuna);
                ViewBag.barrio = toolBll.GetBarrio(aso.nits.codbarrio);
                ViewBag.empresalabora = asocBll.GetEmpresaLabora(aso.codempresalabora);
                ViewBag.Aso = aso;
                return View("Edit",aso.nits);
            }
            catch ( Exception e )
            {

                throw ;
            }
        }
     //   [Allow(action = "AsociadosModulotyutu")]
     ////   POST: Asociado/Edit/5
     //   [HttpPost]
     //   public JsonResult Edit(int id, asociados aso, nits nit)
     //   {
     //       try
     //       {
     //           bool retorno = asocBll.Edit(id, nit, aso);
     //           return Json(new { estado = true }, JsonRequestBehavior.AllowGet);

     //       }
     //       catch (Excepciones e)
     //       {
     //           Trace.WriteLine("Controlador Asociado, funcion Edit " + e.Message.ToString(), "Error " + DateTime.Now);
     //           return Json(new
     //           {
     //               estado = false,
     //               mensaje = e.Descripcion,
     //           }, JsonRequestBehavior.AllowGet);
     //       }
     //   }


    }
}
