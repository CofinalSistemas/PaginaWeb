using Ingenio.BLL;
using Ingenio.BO;
using Ingenio.DLL;
using Ingenio.Filters;
using Ingenio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ingenio.PortalWebExterno.Controllers
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// EXtractos de ahorro
    /// </summary>
    [Allow(action = "AsociadosModulo")]
    public class AhExtractosController : Controller
    {
        ExtractosBLL extBll = new ExtractosBLL();
        AsociadoBLL asocBll = new AsociadoBLL();
        public static int tempid;
        // GET: AhExtractos
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// inicio de presentacion formulario con los datos del asociado y movimientos de ahorros a la vista del asociado
        /// </summary>
        // GET: AhExtractos/Details/5
        public ActionResult Details(int id)
        {
           
            try
            {
                tempid = id;

                AccountModels cuentaUsuario = new AccountModels();
                int txtcedula = cuentaUsuario.Identificacion;

                asociados aso = asocBll.GetAsociado(txtcedula);
                ViewBag.Aso = aso;

                 if (ViewBag.Aso != null) { 
                //validar nulos pais ciudad
                ViewBag.nombreciudad = ViewBag.aso.paisnace == null || ViewBag.aso.ciudadnace == null ? "" : aso.ciudades.nombreciudad;
                ViewBag.nombredepartamento = ViewBag.aso.dptonace == null ? "" : aso.ciudades.nombreciudad;// .departamentos.nombredepartamento;

                TimeSpan dias = DateTime.Now - Convert.ToDateTime(aso.nits.fechaingreso);
                ViewBag.antiguedad = Math.Abs((aso.nits.fechaingreso.Month - DateTime.Now.Month) + 12 * (aso.nits.fechaingreso.Year - DateTime.Now.Year));
                ViewBag.agencia = aso.nits.agencia + "-" + asocBll.GetAgencias(aso);

                }
                else {
                nits nitaso = asocBll.GetNitsAsociado(txtcedula);
                ViewBag.Nitaso = nitaso;
                    ViewBag.nombreciudad = "";
                    ViewBag.nombredepartamento = "";// .departamentos.nombredepartamento;

                    TimeSpan dias = DateTime.Now - Convert.ToDateTime(nitaso.fechaingreso);
                    ViewBag.antiguedad = Math.Abs((nitaso.fechaingreso.Month - DateTime.Now.Month) + 12 * (nitaso.fechaingreso.Year - DateTime.Now.Year));
                    ViewBag.agencia = nitaso.agencia + "-" + asocBll.GetAgenciasxNit(nitaso.agencia);

                }
                ViewBag.Aho = "";
                if (id == 1) { 
                ahorrosalavista aho = extBll.GetInfoAhorro(txtcedula);
                ViewBag.Aho = aho;
                ViewBag.nombrelinea = aho.parameahorroAlaVista.plancuentas.nombre;
                }
                if (id == 2)
                {
                    ahorrosAtermino aho = extBll.GetInfoAhorro1(txtcedula);
                    ViewBag.Aho = aho;
                    ViewBag.nombrelinea = "Certificado de deposito a termino";
                }
                if (id == 3)
                {
                    ahorrosContractual aho = extBll.GetInfoAhorro2(txtcedula);
                    ViewBag.Aho = aho;
                    ViewBag.nombrelinea = "Ahorro contractual";
                }
                if (id == 4)
                {
                    aportessociales aho = extBll.GetInfoAhorro3(txtcedula);
                    ViewBag.Aho = aho;
                    ViewBag.nombrelinea = "Aportes sociales";
                }
                dynamic perm = "";
                //if (ViewBag.Aho == null)//2016-04-06 se desactiva, solo va ahorro a la vista , segun don Alfredo Paz
                //{
                //    ahorrosAtermino aho1 = extBll.GetInfoAhorro1(txtcedula);
                //    ViewBag.Aho = aho1;
                //    perm = extBll.GetPermanentes(aho1.numerocuenta);
                //    if (ViewBag.Aho == null)
                //    {
                //        ahorrosContractual aho2 = extBll.GetInfoAhorro2(txtcedula);
                //        ViewBag.Aho = aho2;
                //        perm = extBll.GetPermanentes(aho2.numerocuenta);
                //    }
                //}
                //else
                //{
               
                ViewBag.cedula = txtcedula;
                //ViewBag.Ahperm = ahpermmov;

                return View();
            }
            catch (Exception er) {
                Trace.WriteLine("Controlador AhExtractos, funcion Details " + er.Message.ToString(), "Error " + DateTime.Now);
                return View(); }
        }
        public JsonResult Ah_vista(int id)
        {
            int tipo = tempid;

            dynamic cuentas = extBll.GetAlaVista(id, tipo);
            return Json(cuentas, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Ah_permanente(string id)
        {
            int tipo = tempid;

            dynamic perm;
            perm = extBll.GetPermanentes(id,tipo);
            //}
            
            ViewBag.seguimiento = perm;

            var Ahp = perm.Ahperm;

            ICollection<dynamic> ahpermmov = new List<dynamic>();

            decimal consignaciones = 0, nuevosaldo = 0, retiros = 0, cont = 0;

            try
            {
                foreach (var item in Ahp)
                {
                    dynamic d = new ExpandoObject();

                    if (cont == 0)
                    {
                        ViewBag.saldoini = item.saldoanterior;
                        cont = 1;
                    }


                    if (item.natura == "CNGI" || item.natura == "RFTE" || item.natura == "RETI")
                    { }
                    else {
                        d.fechatrabajo = item.fechatrabajo.ToString("yyyy-MM-dd");
                        d.detalle = item.detalle;

                        if (item.natura == "RETT")
                        {
                            d.retiro = item.valorefectivo;
                            d.valorefectivo = 0;
                            d.saldoanterior = item.saldoanterior - item.valorefectivo;
                            retiros = retiros + item.valorefectivo;

                        }
                        else
                        {
                            d.retiro = 0;
                            d.valorefectivo = item.valorefectivo;
                            d.saldoanterior = item.saldoanterior + item.valorefectivo;
                            consignaciones = consignaciones + item.valorefectivo;

                        }
                        ahpermmov.Add(d);
                    }

                }

                
                ViewBag.nuevosaldo = ViewBag.saldoini - nuevosaldo + consignaciones - retiros;
                ViewBag.consignaciones = consignaciones;
                ViewBag.retiros = retiros;

                dynamic lista = (from ll in ahpermmov
                                 select new { ll.fechatrabajo, ll.detalle, ll.retiro, ll.valorefectivo, ll.saldoanterior,nuevos= ViewBag.nuevosaldo,nuevacons= ViewBag.consignaciones,retiross= ViewBag.retiros,saldoin= ViewBag.saldoini }).ToList();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception er)
            {
                Trace.WriteLine("Controlador AhExtractos, funcion Details ln.85 " + er.Message.ToString(), "Error " + DateTime.Now);
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
