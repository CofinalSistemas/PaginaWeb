using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO;
using Ingenio.BLL;
using Ingenio.DLL;
using Ingenio.Filters;
using System.Diagnostics;

namespace Ingenio.PortalWebExterno.Controllers
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// Estado de cuenta, muestra los movimientos de ahorro y creditos segun numero de cuenta y pagare
    /// </summary>
    [Allow(action = "AsociadosModulo")]
    public class EstadodeCuentaController : Controller
    {
        AsociadoBLL asocBll = new AsociadoBLL();
        ExtractosBLL extBll = new ExtractosBLL();
        // GET: EstadodeCuenta
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos de ahorro y credito 
        /// </summary>
        /// <param name="id">argumento tipo numero es enviado desde navbar</param>
        // GET: EstadodeCuenta/Details/5
        public ActionResult Details(int id)
        {
            try { 
            asociados aso = asocBll.GetAsociado(id);
            ViewBag.Aso = aso;
                if (ViewBag.Aso != null)
                {
                    TimeSpan dias = DateTime.Now - Convert.ToDateTime(aso.nits.fechaingreso);
                    ViewBag.antiguedad = Math.Abs((aso.nits.fechaingreso.Month - DateTime.Now.Month) + 12 * (aso.nits.fechaingreso.Year - DateTime.Now.Year));
                    ViewBag.agencia = aso.nits.agencia + "-" + asocBll.GetAgencias(aso);
                    if (aso.periododeduce == "M") ViewBag.periododeduce = "Mensual";
                    if (aso.periododeduce == "Q") ViewBag.periododeduce = "Quiencenal";
                    if (aso.periododeduce == "O") ViewBag.periododeduce = "Catorcenal";
                    if (aso.periododeduce == "D") ViewBag.periododeduce = "Decadal";
                    if (aso.periododeduce == "E") ViewBag.periododeduce = "Semanal";

                    if (aso.estado == "A") ViewBag.activado = "Activo";
                    if (aso.estado == "I") ViewBag.activado = "Inactivo";
                }
                else {
                    nits nitaso = asocBll.GetNitsAsociado(id);
                    ViewBag.Nitaso = nitaso;
                    TimeSpan dias = DateTime.Now - Convert.ToDateTime(nitaso.fechaingreso);
                    ViewBag.antiguedad = Math.Abs((nitaso.fechaingreso.Month - DateTime.Now.Month) + 12 * (nitaso.fechaingreso.Year - DateTime.Now.Year));
                    ViewBag.agencia = nitaso.agencia + "-" + asocBll.GetAgenciasxNit(nitaso.agencia);
                    ViewBag.periododeduce = "";

                    if (nitaso.estado == "A") ViewBag.activado = "Activo";
                    if (nitaso.estado == "I") ViewBag.activado = "Inactivo";
                }

            dynamic cuentas = extBll.GetCuentas(id);

            var permanente = cuentas.Perm;// permanentes
            var Alavista = cuentas.Alavista;// social
            var AhorroATermino = cuentas.Termino;// a termino
            var Contractual = cuentas.Contractual;
            var Aportes = cuentas.Aportes;
            var Creditos = cuentas.Creditos;

            ICollection<dynamic> aho_perman = new List<dynamic>();

            Decimal SALDOTOTAL = 0, SALDOTOTALCRE=0;

            foreach (var item in permanente)
            {
                dynamic b = new ExpandoObject();
                               
                b.numerocuenta = item.numerocuenta;
                b.fechainicio = item.fechainicio.ToString("yyyy-MM-dd");
                b.valor = item.valor;
                b.saldoTotal = item.saldoTotal;
                SALDOTOTAL=SALDOTOTAL + item.saldoTotal;
                if (item.estado == "A") b.estado = "Activo";
                if (item.estado == "I") b.estado = "Inactivo";

                aho_perman.Add(b);
            }

            ICollection<dynamic> aho_alavista = new List<dynamic>();

            foreach (var item in Alavista)
            {
                dynamic b = new ExpandoObject();

                b.numerocuenta = item.numerocuenta;
                b.fechainicio = item.fechainicio.ToString("yyyy-MM-dd");
                b.valor = item.valor;
                b.saldoTotal = item.saldoTotal;
                SALDOTOTAL = SALDOTOTAL + item.saldoTotal;
                if (item.estado == "A") b.estado = "Activo";
                if (item.estado == "I") b.estado = "Inactivo";

                aho_alavista.Add(b);
            }

            ICollection<dynamic> aho_AhorroATermino = new List<dynamic>();

            foreach (var item in AhorroATermino)
            {
                dynamic b = new ExpandoObject();

                b.numerocuenta = item.numerocuenta;
                b.fechainicio = item.fechainicio.ToString("yyyy-MM-dd");
                b.valor = item.valor;
                b.saldoTotal = item.saldoTotal;
                SALDOTOTAL = SALDOTOTAL + item.saldoTotal;
                if (item.estado == "V") b.estado = "Activo";
                if (item.estado == "I") b.estado = "Inactivo";

                aho_AhorroATermino.Add(b);
            }

            ICollection<dynamic> aho_Contractual = new List<dynamic>();

            foreach (var item in Contractual)
            {
                dynamic b = new ExpandoObject();

                b.numerocuenta = item.numerocuenta;
                b.fechainicio = item.fechainicio.ToString("yyyy-MM-dd");
                b.valor = item.valor;
                b.saldoTotal = item.saldoTotal;
                SALDOTOTAL = SALDOTOTAL + item.saldoTotal;
                if (item.estado == "A") b.estado = "Activo";
                if (item.estado == "I") b.estado = "Inactivo";

                aho_Contractual.Add(b);
            }

            ICollection<dynamic> aho_Aportes = new List<dynamic>();

            foreach (var item in Aportes)
            {
                dynamic b = new ExpandoObject();

                b.numerocuenta = item.numerocuenta;
                b.fechainicio = item.fechaapertura.ToString("yyyy-MM-dd");
                b.valor = item.valorcuota;
                b.saldoTotal = item.saldoTotal;
                SALDOTOTAL = SALDOTOTAL + item.saldoTotal;
                if (item.estado == "A") b.estado = "Activo";
                if (item.estado == "I") b.estado = "Inactivo";
                aho_Aportes.Add(b);
            }

            ICollection<dynamic> aho_Creditos = new List<dynamic>();

            foreach (var item in Creditos)
            {
                dynamic b = new ExpandoObject();

                b.pagare = item.pagare;
                b.coddestino = item.coddestino;
                b.F_iniciofinanciacion = item.F_iniciofinanciacion.ToString("yyyy-MM-dd");
                b.cuotasmora = item.cuotasmora;
                b.anualidad = item.anualidad;
                b.saldocapital = item.saldocapital+item.intcorriente;
                SALDOTOTALCRE = SALDOTOTALCRE + item.saldocapital;
                if (item.cuotasmora != 0) { b.estado = "MORA"; }
                else { b.estado = "AL DIA"; }
                aho_Creditos.Add(b);
            }

            ViewBag.Apermanentes = aho_perman;
            ViewBag.AAtermino = aho_AhorroATermino;
            ViewBag.Asocial = aho_Aportes;
            ViewBag.Credit = aho_Creditos;
            ViewBag.Contractual = aho_Contractual;
            ViewBag.Alavista = aho_alavista;

            //decimal Totales = 0;
            //decimal cuotatotales = 0;

            //dynamic ahorro = asocBll.GetAhorros(1900, 00, id);

            //try
            //{
            //    var permanente = ahorro.ahorroPermanente;// permanentes
            //    var AhorroSocial = ahorro.ahorroSocial;// social
            //    var AhorroATermino = ahorro.ahorroAtermino;// a termino
            //    var Creditos = ahorro.Creditos;

            //    ICollection<dynamic> aho_perman = new List<dynamic>();

            //    foreach (var item in permanente)
            //    {
            //        dynamic b = new ExpandoObject();

            //        b.año = item.año;
            //        b.mes = item.mes;
            //        b.codlinea = item.codlinea;
            //        b.numerocuenta = item.numerocuenta;
            //        b.nombre = item.nombre;
            //        b.fechainicio = Convert.ToString(item.fechainicio);
            //        b.fechavencimiento = Convert.ToString(item.fechavencimiento);
            //        b.cuota = item.cuota;
            //        b.saldototal = item.saldototal;
            //        Totales = Totales + Convert.ToDecimal(item.saldototal);
            //        cuotatotales= cuotatotales+ Convert.ToDecimal(item.cuota);
            //        b.interes = item.interes;
            //        aho_perman.Add(b);
            //    }

            //    ICollection<dynamic> aho_social = new List<dynamic>();

            //    foreach (var item in AhorroSocial)
            //    {
            //        dynamic b = new ExpandoObject();

            //        b.año = item.año;
            //        b.mes = item.mes;
            //        b.codlinea = item.codlinea;
            //        b.numerocuenta = item.numerocuenta;
            //        b.nombre = item.nombre;
            //        b.fechainicio = Convert.ToString(item.fechainicio);
            //        b.fechavencimiento = Convert.ToString(item.fechavencimiento);
            //        b.cuota = item.cuota;
            //        b.saldototal = item.saldototal;
            //        Totales = Totales + Convert.ToDecimal(item.saldototal);
            //        cuotatotales = cuotatotales + Convert.ToDecimal(item.cuota);
            //        b.interes = item.interes;
            //        aho_social.Add(b);
            //    }

            //    ICollection<dynamic> aho_ATermino = new List<dynamic>();

            //    foreach (var item in AhorroATermino)
            //    {
            //        dynamic b = new ExpandoObject();

            //        b.año = item.año;
            //        b.mes = item.mes;
            //        b.codlinea = item.codlinea;
            //        b.numerocuenta = item.numerocuenta;
            //        b.nombre = item.nombre;
            //        b.fechainicio = Convert.ToString(item.fechainicio);
            //        b.fechavencimiento = Convert.ToString(item.fechavencimiento);
            //        b.cuota = item.cuota;
            //        b.saldototal = item.saldototal;
            //        Totales = Totales + Convert.ToDecimal(item.saldototal);
            //        cuotatotales = cuotatotales + Convert.ToDecimal(item.cuota);
            //        b.interes = item.interes;
            //        aho_ATermino.Add(b);
            //    }

            //    ICollection<dynamic> cre = new List<dynamic>();

            //    foreach (var item in Creditos)
            //    {
            //        dynamic b = new ExpandoObject();
            //        b.año = item.año;
            //        b.mes = item.mes;
            //        b.cuotasmora = item.cuotasmora;
            //        b.cedulasociado = item.cedulasociado;
            //        b.codlinea = item.codlinea;
            //        b.nombre = item.nombre;
            //        b.fechainicio = Convert.ToString(item.fechainicio);
            //        b.fechavencimiento = Convert.ToString(item.fechavencimiento);
            //        b.anulidad = item.anulidad;
            //        b.saldoponersedia = item.saldoponersedia;
            //        b.pagare = item.pagare;
            //        b.estado = item.estado == "V" ? "Vigente" : item.estado; 
            //        cre.Add(b);
            //    }

            //    ViewBag.Apermanentes = aho_perman;
            //    ViewBag.AAtermino = aho_ATermino;
            //    ViewBag.Asocial = aho_social;
            //    ViewBag.Credit = cre;
            ViewBag.Totales = SALDOTOTAL;
            ViewBag.Obligaciones = SALDOTOTALCRE;
            //    ViewBag.CuotaTotal = cuotatotales;
            return View();
            }
            catch (Exception er) {
                Trace.WriteLine("Controlador Estadodecuenta, funcion Details " + er.Message.ToString(), "Error " + DateTime.Now);

                return View(); }

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos de aportes socciales segun nro. cuenta 
        /// </summary>
        /// <param name="nroc">argumento tipo numero es enviado desde details formulario</param>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult GetEspecificoAsociales(string nroc, int id)
        {
            dynamic cuentas = extBll.GetEspecificoAsociales(nroc, id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos de ahorro permanente segun nro. cuenta 
        /// </summary>
        /// <param name="nroc">argumento tipo numero es enviado desde details formulario</param>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult GetEspecificoPermanente(int nroc, int id)
        {
            dynamic cuentas = extBll.GetEspecificoPermanente(nroc, id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos de ahorro contractual segun nro. cuenta 
        /// </summary>
        /// <param name="nroc">argumento tipo numero es enviado desde details formulario</param>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult GetEspecificoContractual(int nroc, int id)
        {
            dynamic cuentas = extBll.GetEspecificoContractual(nroc, id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos de ahorro a la vista segun nro. cuenta 
        /// </summary>
        /// <param name="nroc">argumento tipo numero es enviado desde details formulario</param>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult GetEspecifico(int nroc,int id)
        {
            dynamic cuentas = extBll.GetEspecifico(nroc,id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos de ahorro a termino segun nro. cuenta 
        /// </summary>
        /// <param name="nroc">argumento tipo numero es enviado desde details formulario</param>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult GetEspecificoAtermino(int nroc, int id)
        {
            dynamic cuentas = extBll.GetEspecificoAtermino(nroc, id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los detalles nro. cuenta  de aho. vista
        /// </summary>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult Ah_vista(int id)
        {
            dynamic cuentas = extBll.GetAlaVista(id,1);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los detalles nro. cuenta  de aho. termino
        /// </summary>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult Ah_termino(int id)
        {
            dynamic cuentas = extBll.GetAtermino(id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los detalles nro. cuenta  de aho. permanente
        /// </summary>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult Ah_permanente(int id)
        {
            dynamic cuentas = extBll.Getapermanente(id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los detalles nro. cuenta  de aho. contractual
        /// </summary>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult Ah_contractual(int id)
        {
            dynamic cuentas = extBll.Getacontractual(id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los detalles nro. cuenta  de aporte social
        /// </summary>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult Ah_sociales(int id)
        {
            dynamic cuentas = extBll.Getasociales(id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos por nro. cuenta
        /// </summary>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult GetMovimientos(string id)
        {
            
            dynamic query = extBll.GetMovimientos(id);

            return Json(query, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimiento por pagare
        /// </summary>
        ///  /// <param name="id">argumento tipo numero es enviado desde details formulario</param>
        public JsonResult GetMov(int id)
        {
           
            dynamic query = extBll.GetMov(id);

            return Json(query, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SdoCanje(int id,string nit)
        {
            int _nit = Encriptar.decryptint(nit.ToString());
            asociados aso = asocBll.GetAsociado(_nit);
            ViewBag.Aso = aso;
            TimeSpan dias = DateTime.Now - Convert.ToDateTime(aso.nits.fechaingreso);
            ViewBag.antiguedad = Math.Abs((aso.nits.fechaingreso.Month - DateTime.Now.Month) + 12 * (aso.nits.fechaingreso.Year - DateTime.Now.Year));
            ViewBag.agencia = aso.nits.agencia + "-" + asocBll.GetAgencias(aso);
            if (aso.periododeduce == "M") ViewBag.periododeduce = "Mensual";
            if (aso.periododeduce == "Q") ViewBag.periododeduce = "Quiencenal";
            if (aso.periododeduce == "O") ViewBag.periododeduce = "Catorcenal";
            if (aso.periododeduce == "D") ViewBag.periododeduce = "Decadal";
            if (aso.periododeduce == "E") ViewBag.periododeduce = "Semanal";

            if (aso.estado == "A") ViewBag.activado = "Activo";
            if (aso.estado == "I") ViewBag.activado = "Inactivo";
            return View();
        }
        public ActionResult Seguimientos(int id, int nit)
        {
            asociados aso = asocBll.GetAsociado(nit);
            ViewBag.Aso = aso;
            TimeSpan dias = DateTime.Now - Convert.ToDateTime(aso.nits.fechaingreso);
            ViewBag.antiguedad = Math.Abs((aso.nits.fechaingreso.Month - DateTime.Now.Month) + 12 * (aso.nits.fechaingreso.Year - DateTime.Now.Year));
            ViewBag.agencia = aso.nits.agencia + "-" + asocBll.GetAgencias(aso);
            
            return View();
        }

    }
}
