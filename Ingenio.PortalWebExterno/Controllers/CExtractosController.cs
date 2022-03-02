using Ingenio.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenio.DLL;
using System.Dynamic;
using Ingenio.PortalWebExterno.Models;
using Ingenio.BLL;
using System.IO;
using Ingenio.Models;
using Ingenio.Filters;
using System.Diagnostics;

namespace Ingenio.PortalWebExterno.Controllers
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// Extracto de credito
    /// </summary>
    public class CExtractosController : Controller
    {
        ExtractosBLL extBll = new ExtractosBLL();
        AsociadoBLL asocBll = new AsociadoBLL();
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Formulario para subir una imagen de mercado que aparece en el extracto de credito al imprimir
        /// </summary>
        // GET: CExtractos
        [Allow(action = "CONFIGURACION_PUBLICIDAD_MERCADEO")]
        [HttpGet]
        public ActionResult Index()
        {        
            
            return View();
        }
        [Allow(action = "CONFIGURACION_PUBLICIDAD_MERCADEO")]
        public ActionResult ImagenPrevia()
        {
            var path = Server.MapPath("~/Publicacion");
            var file = string.Format("cofinalextracto.jpg");
            var fullPath = Path.Combine(path, file);
            return File(fullPath, "image/jpg", file);
        }
        public JsonResult Ah_creditos(int id)
        {
            dynamic cuentas = extBll.Getpagares(id);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Guarda la imagen en la carpeta de servidor
        /// </summary>
        [Allow(action = "CONFIGURACION_PUBLICIDAD_MERCADEO")]
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase Imagen)
        {
            //elimino
            var file = Path.Combine(HttpContext.Server.MapPath("~/Publicacion"), "cofinalextracto.jpg");
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);

            //creo
            try
            {
                var path = "";
                if (Imagen != null && Imagen.ContentLength > 0)
                {
                    var nombreArchivo = Path.GetFileName(Imagen.FileName);
                    path = Path.Combine(Server.MapPath("~/Publicacion"), "cofinalextracto.jpg");
                    Imagen.SaveAs(path);
                }
               
            }
            catch (Exception a) {
                Trace.WriteLine("Controlador CExtractos, funcion Index " + a.Message.ToString(), "Error "+DateTime.Now);
            }
            return View("Index");
        }

        [Allow(action = "AsociadosModulo")]
        public ActionResult Imagen()
        {
            var path = Server.MapPath("~/Publicacion");
            var file = string.Format("cofinalextracto.jpg");
            var fullPath = Path.Combine(path, file);
            return File(fullPath, "image/jpg", file);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra el formulario
        /// </summary>
        [Allow(action = "AsociadosModulo")]
        // GET: CExtractos/Details/5
        public ActionResult Details()
        {
            return View();

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos de creditos 
        /// </summary>
        /// <param name="txtpagare">argumento tipo numero es enviado desde details formulario</param>
        /// <param name="txtcedula">argumento tipo numero es enviado desde details formulario</param>
        /// <param name="txtFechaI">argumento tipo fecha es enviado desde details formulario</param>
        /// <param name="txtFechaF">argumento tipo fecha es enviado desde details formulario</param>
        [Allow(action = "AsociadosModulo")]
        public ActionResult Calcular(int txtpagare, int txtcedula, DateTime txtFechaI, DateTime txtFechaF)
        {
            ViewBag.JumpTo = "AtBottomOfForm";
            try
            {
                AccountModels cuentaUsuario = new AccountModels();
                txtcedula = cuentaUsuario.Identificacion;
                asociados aso = asocBll.GetAsociado(txtcedula);
                ViewBag.Aso = aso;
                if (ViewBag.Aso== null)
                {
                    TimeSpan dias = DateTime.Now - Convert.ToDateTime(aso.nits.fechaingreso);
                    ViewBag.antiguedad = Math.Abs((aso.nits.fechaingreso.Month - DateTime.Now.Month) + 12 * (aso.nits.fechaingreso.Year - DateTime.Now.Year));
                    ViewBag.agencia = aso.nits.agencia + "-" + asocBll.GetAgencias(aso);
                }
                else
                {
                    nits nitaso = asocBll.GetNitsAsociado(txtcedula);
                    ViewBag.Nitaso = nitaso;
                    TimeSpan dias = DateTime.Now - Convert.ToDateTime(nitaso.fechaingreso);
                    ViewBag.antiguedad = Math.Abs((nitaso.fechaingreso.Month - DateTime.Now.Month) + 12 * (nitaso.fechaingreso.Year - DateTime.Now.Year));
                    ViewBag.agencia = nitaso.agencia + "-" + asocBll.GetAgenciasxNit(nitaso.agencia);
                }
                    
                //verifica si tiene pago
                seguimientocredito seg = extBll.GetSeguimiento(txtpagare, txtcedula);
                ViewBag.seguimiento = seg;
                //trae info general del credito 
                creditos cred = extBll.GetCreaditos(txtpagare, txtcedula);
                ViewBag.Creditos = cred;

                //ViewBag.Cuotaspagadas = extBll.CuotasPagadas(txtpagare, txtcedula);
                //trae los pagos de credito mes a mes
                //dynamic hcred = extBll.Historicodepagoscreditos(txtpagare, txtcedula);
                //var his_hcred = hcred.Datos;

                ICollection<dynamic> saldocap = new List<dynamic>();
                decimal abom = 0, aboc = 0, abok = 0, cngc = 0, RCNG = 0, AUX_ANT_ABOK = 0;
                //foreach (var item in his_hcred)
                //{
                //    dynamic d = new ExpandoObject();
                //    d.saldocapital = item.saldocapital;
                //    string ultimo = item.f_ultimopago.ToString("yyyy-MM-dd");
                //    //d.f_ultimopago = item.f_ultimopago;
                //    DateTime ultimo1;
                //    ultimo1 = Convert.ToDateTime(ultimo.ToString().Substring(0, 10) + " 00:01:00.000");
                //    DateTime ultimo2;
                //    ultimo2=Convert.ToDateTime(ultimo.ToString().Substring(0, 10) + " 23:59:00.000");

                //    string rec_pago = "";
                //    int bandera = 0;
                //    dynamic pagos_cuota = extBll.GetPagos_cuota(txtcedula, txtpagare, ultimo2, ultimo1); //busco solo pagos de cuota
                //    var pagos = pagos_cuota;
                //    foreach (var item2 in pagos)
                //    {
                //        d.fechatrabajo = item2.fechaTrabajo.ToString("yyyy-MM-dd");
                //        d.descripcion = "Consignacion Total";
                //        rec_pago= item2.recibopago;
                //        if (rec_pago.Trim() != "") {
                //            d.recibopago = item2.recibopago;
                //            bandera = 1;
                //        }


                //        d.agencia = item2.agencia;

                //        if (item2.natura == "ABOC") { aboc = item2.totalefectivo; d.aboc = aboc; }//"Abono Corriente";
                //        if (item2.natura == "CNGC") { cngc = item2.totalefectivo; d.cngc = cngc; }// "Consignacion Total";
                //        if (item2.natura == "ABOK") { abok = item2.totalefectivo; d.abok = abok; }//"Abono Capital";
                //        if (item2.natura == "ABOM") { abom = item2.totalefectivo; d.abom = abom; }//"Abono Mora";

                //        if (aboc == 0) d.aboc = 0;
                //        if (abok == 0) d.abok = 0;
                //        if (abom == 0) d.abom = 0;
                //        if (cngc == 0) d.cngc = 0;



                //        //saldocap.Add(d);
                //    }
                //    if (bandera == 0) d.recibopago = "";
                //    saldocap.Add(d);
                //    abom = 0;
                //    aboc = 0;
                //    abok = 0;
                //}

                ViewBag.Saldocap = saldocap;
                //trase el nombre de destino
                dynamic querynom = extBll.DestinosReporte(cred.coddestino);
                var inf = querynom.Des_reportes;
                foreach (var item in inf)
                {
                    ViewBag.nombrecuenta = item.nombredestino;
                }

                if (ViewBag.Creditos.periodointeres == "M") ViewBag.periododeduce = "Mensual";
                if (ViewBag.Creditos.periodointeres == "Q") ViewBag.periododeduce = "Quiencenal";
                if (ViewBag.Creditos.periodointeres == "O") ViewBag.periododeduce = "Catorcenal";
                if (ViewBag.Creditos.periodointeres == "D") ViewBag.periododeduce = "Decadal";
                if (ViewBag.Creditos.periodointeres == "E") ViewBag.periododeduce = "Semanal";
                if (ViewBag.Creditos.periodointeres == "S") ViewBag.periododeduce = "Semestral";

                //obtengo los movimientos
                dynamic cree = extBll.GetCreditos(txtcedula, txtpagare);

                decimal saldocapital = ViewBag.Creditos.capitalinicial;

                var Creditos = cree.Creditos;
                var Costos = cree.Costos;
                ICollection<dynamic> creditomov = new List<dynamic>();
                ICollection<dynamic> costomov = new List<dynamic>();

                int bandera = 0;
                try
                {
                    dynamic d = new ExpandoObject();

                    string rec_pago = "", rec_pago_ant = "", saldototal = "";
                    int i = 0;
                    foreach (var item in Creditos)
                    {

                        //d.fechaTrabajo = item.fechaTrabajo;
                        //d.natura = item.natura;
                        //if (item.natura == "CARD") d.natura = "Cargo Directo a";
                        //if (item.natura == "CARC") d.natura = "Cargo Corriente";
                        //if (item.natura == "ABOC") d.natura = "Abono Corriente";
                        //if (item.natura == "CNGC") d.natura = "Consignacion Total";
                        //if (item.natura == "ABOK") d.natura = "Abono Capital"; 
                        //if (item.natura == "COTO") d.natura = "C.A.: POLIZA";

                        //d.agencia = item.agencia;

                        //d.totalefectivo = item.totalefectivo;
                        //if (item.natura == "ABOK")
                        //{
                        //    saldocapital = saldocapital - item.totalefectivo;
                        //    d.saldocapital = saldocapital;
                        //}
                        //else
                        //{
                        //    d.saldocapital = 0;
                        //}


                        if (item.natura == "RCNG")
                        {
                            RCNG = AUX_ANT_ABOK;
                            saldocapital = saldocapital + RCNG;
                            d.saldocap = saldocapital;


                        } //"Abono Capital";

                        rec_pago = item.recibopago.ToString().Trim();

                        if (cree.Creditos.IndexOf(item) == cree.Creditos.Count - 1)
                        {
                            rec_pago = "";
                            if (item.natura == "ABOC") { aboc = item.totalefectivo; d.aboc = aboc; }//"Abono Corriente";
                            if (item.natura == "CNGC") { d.cngc = item.totalefectivo; }// "Consignacion Total";
                            if (item.natura == "ABOK")
                            {
                                abok = item.totalefectivo; d.abok = abok;
                                saldocapital = saldocapital - abok;
                                d.saldocap = saldocapital;
                                AUX_ANT_ABOK = abok;
                            }//"Abono Capital";
                            if (item.natura == "ABOM") { abom = item.totalefectivo; d.abom = abom; }//"Abono Mora";
                        }
                        if (rec_pago != rec_pago_ant)
                        {
                            rec_pago_ant = item.recibopago.ToString().Trim();
                            if (rec_pago != "")
                            {
                                if (bandera == 1)
                                {
                                    if (aboc == 0) d.aboc = 0;
                                    if (abok == 0)
                                    {
                                        d.abok = 0;
                                        d.saldocap = saldocapital;
                                    }
                                    if (abom == 0) d.abom = 0;
                                    creditomov.Add(d);
                                    bandera = 0;
                                    abom = 0;
                                    aboc = 0;
                                    abok = 0;
                                    d = new ExpandoObject();
                                }
                            }
                        }
                        if (rec_pago != "")
                        {
                            bandera = 1;
                            d.fechatrabajo = item.fechaTrabajo;
                            d.descripcion = "Abono de credito";
                            d.recibopago = item.recibopago;
                            d.agencia = item.agencia;

                            if (item.natura == "ABOC") { aboc = item.totalefectivo; d.aboc = aboc; }//"Abono Corriente";
                            if (item.natura == "CNGC") { d.cngc = item.totalefectivo; }// "Consignacion Total";
                            if (item.natura == "ABOK")
                            {
                                abok = item.totalefectivo; d.abok = abok;
                                saldocapital = saldocapital - abok;
                                d.saldocap = saldocapital;
                                AUX_ANT_ABOK = abok;

                            } //"Abono Capital";
                            if (item.natura == "ABOM") { abom = item.totalefectivo; d.abom = abom; }//"Abono Mora";

                        }
                        else
                        {
                            if (bandera == 1)
                            {
                                if (aboc == 0) d.aboc = 0;
                                if (abok == 0)
                                {
                                    d.abok = 0;
                                    d.saldocap = saldocapital;
                                }
                                if (abom == 0) d.abom = 0;

                                creditomov.Add(d);
                                bandera = 0;
                                abom = 0;
                                aboc = 0;
                                abok = 0;
                                d = new ExpandoObject();
                            }
                        }


                    }
                    /////////////////////////////////////
                    foreach (var item in Costos)
                    {

                        ViewBag.Costoadicional = item.valorcosto;

                    }
                    i++;
                }
                catch (Exception er)
                {
                    Trace.WriteLine("Controlador CExtractos, funcion Calcular ln.208 " + er.Message.ToString(), "Error " + DateTime.Now);
                    return View();
                }

                txtFechaI = Convert.ToDateTime(txtFechaI.ToString().Substring(0, 10) + " 00:01:00.000");
                txtFechaF = Convert.ToDateTime(txtFechaF.ToString().Substring(0, 10) + " 23:59:00.000");

                ViewBag.Cuotaspagadas = creditomov.Count();

                var qq = (from ab in creditomov
                          where ab.fechatrabajo >= txtFechaI && ab.fechatrabajo <= txtFechaF
                          select ab).ToList();


                decimal interesperiodo = 0;
                decimal diascalculados = 0;
                decimal interescorriente = 0;

                DateTime a = ViewBag.Creditos.f_ultimopago;

                diascalculados = 30 - a.Day;
                interesperiodo = ((saldocapital * (ViewBag.Creditos.tasacolocacion / 100)) / 30) * diascalculados;

                decimal deudatotal = 0;
                deudatotal = ViewBag.Creditos.saldocapital;
                interescorriente = ViewBag.Creditos.intcorriente;


                ViewBag.deudatotal = deudatotal + ViewBag.Costoadicional + interesperiodo;
                ViewBag.saldocapital = saldocapital;

                ViewBag.interesperiodo = interesperiodo;
                ViewBag.fi = txtFechaI;
                ViewBag.ff = txtFechaF;
                ViewBag.pagare = txtpagare;
                ViewBag.cedula = txtcedula;
                ViewBag.Credit = qq;

                return View("Details");
            }
            catch (Exception er) {
                Trace.WriteLine("Controlador CExtractos, funcion Calcular  " + er.Message.ToString(), "Error " + DateTime.Now);
                return View("Details"); }
        }




    }


}
