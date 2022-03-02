using ClosedXML.Excel;
using Ingenio.DLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ingenio.PortalWebExterno.ActionResults;
using Ingenio.PortalWebExterno.Models;
using System.Web.Helpers;
using Ingenio.Filters;
using System.Diagnostics;

namespace Ingenio.PortalWebExterno.Controllers
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// valida el re captcha , recibe el archivo y envia email a tesoreria el  adjunto de consignacion
    /// </summary>
    [Allow(action= "REPORTES_UIAF")]
    public class ReportesUIAFController : Controller
    {
       
        ExtractosBLL extBll = new ExtractosBLL();
        UIAF cdm = new UIAF();
        public static List<UIAF> laPeople;
        public static ICollection<dynamic> uiafmov { get; set; }
        public static ICollection<dynamic> cuenta_perm { get; set; }
        public static string tipo1;
        public static string tipo2;
        // GET: ReportesUIAF
        
        public ActionResult Index()
        {
            return View();
        }
      
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// muestra el formulario para ingresar datos iniciales y generar el informe
        /// </summary>
        // GET: ReportesUIAF/Details/5
        public ActionResult Details()
        {
             return View();
        }

        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// recibe los datos de formulario details para procesar la informacion en base de datos y visualizar
        /// </summary>
        /// <param name="page">argumento tipo entero es enviado desde index formulario</param>
        /// <param name="inicial">argumento tipo fecha es enviado desde index formulario</param>
        /// <param name="final">argumento tipo fecha es enviado desde index formulario</param>
        /// <param name="chk">argumento tipo entero=1 es enviado desde index formulario</param>
        /// <param name="tipo">argumento tipo cadena es enviado desde index formulario</param>
        /// <returns></returns>
        public JsonResult Calcular(int page = 1, DateTime? inicial=null, DateTime? final=null, int chk=1,string tipo="H")
        {
            if (tipo == "H")
            {
                cdm.PageSize = 25;
                if (tipo2 == "C")
                {
                    ViewBag.Reportes = 1;
                }
                else {
                    ViewBag.Reportes1 = 1;
                }
                cdm.TotalRecord = laPeople.Count();
                cdm.NoOfPages = (cdm.TotalRecord / cdm.PageSize) + ((cdm.TotalRecord % cdm.PageSize) > 0 ? 1 : 0);
                cdm.Customer = laPeople.OrderBy(o => o.nro).Skip((page - 1) * cdm.PageSize).Take(cdm.PageSize).ToList();
                //return View("Details", cdm);
                return Json(new
                {
                    estado = false,
                }, JsonRequestBehavior.AllowGet);
            }
            laPeople = new List<UIAF>();

            DateTime fechainicial = DateTime.Today;
            final = Convert.ToDateTime(final.ToString().Substring(0, 10) + " 23:59:00.000");
            try
            {
                
                //dynamic reporte = extBll.GetReporte(inicial, final, chk, tipo);
                //var Reportito = reporte.Reporte;
                tipo1 = tipo;
                int i = 1;
                uiafmov = new List<dynamic>();
                cuenta_perm = new List<dynamic>();
                if (tipo == "C")
                {
                    dynamic perm =extBll.transaccion(inicial, final, chk);

                    var query = perm.Perm;
                    
                    dynamic d = new ExpandoObject();
                    foreach (var item in query)
                    {
                        d = new ExpandoObject();
                        d.nro = i;
                        d.fechatransaccion = item.fechaseguimiento.ToString("yyyy-MM-dd");

                        d.moneda = 1;
                        d.codigooficina = item.nombreagencia;
                        d.codigodepartamento = item.codciudad;
                        //d.tipo_producto = item.tipoproducto;
                        if (item.tipoproducto.ToString().Trim() == "AV") d.tipo_producto = 90;
                        if ((item.tipoproducto.ToString().Trim() == "AC") && item.codlinea.ToString().Trim() =="CONT") d.tipo_producto = 92;
                        if ((item.tipoproducto.ToString().Trim() == "AC") && item.codlinea.ToString().Trim()=="PROG") d.tipo_producto = 93;
                        if (item.tipoproducto.ToString().Trim() == "AT") d.tipo_producto = 91;
                        if (item.tipoproducto.ToString().Trim() == "PO") d.tipo_producto = 94;
                        if (item.tipoproducto.ToString().Trim() == "CR") d.tipo_producto = 09;

                        d.tipo_transaccion = item.tipo_transaccion;
                        d.numerocuenta = item.numerocuenta;
                        d.tipoidentificacion = item.tipoidentificacion.ToString();
                        d.documento = item.cedulasociado.ToString();
                        d.primerapellido = item.primerapellido;
                        d.segundoapellido = item.segundoapellido;
                        d.nombres = item.nombres;
                        d.otronombre = item.segundonombre;

                        if (item.tipoidentificacion == 31)
                        { d.razonsocial = item.nombreasociado; }
                        else { d.razonsocial = ""; }

                        d.actividadeconomica = item.nombre;
                        d.salario = item.salario;
                        decimal valort = item.Valor_Transaccion;
                        if (valort >= 10000000)
                        {
                            d.valortransaccion = Math.Round(item.Valor_Transaccion);
                            d.tipoidentificaciondeconsignante = item.tipoidentificacion.ToString();
                            d.tipodocconsignante = item.cedulasociado.ToString();
                            d.primerapellidoconsignante = item.primerapellido;
                            d.segundoapellidoconsignante = item.segundoapellido;
                            d.nombreconsignante = item.nombres;
                            d.otronombreconsignante = item.segundonombre;
                        }
                        else
                        {
                            d.valortransaccion = 0;
                            d.tipoidentificaciondeconsignante = "";
                            d.tipodocconsignante = "";
                            d.primerapellidoconsignante = "";
                            d.segundoapellidoconsignante = "";
                            d.nombreconsignante = "";
                            d.otronombreconsignante = "";
                        }
                        if (item.tipoidentificacion == 31)
                        {
                            d.primerapellido = "";
                            d.segundoapellido = "";
                            d.nombres = "";
                            d.otronombre = "";
                            d.primerapellidoconsignante = "";
                            d.segundoapellidoconsignante = "";
                            d.nombreconsignante = "";
                            d.otronombreconsignante = "";
                        }
                        d.codoperador = item.codoperador;

                        cuenta_perm.Add(d);
                        //laPeople.Add(d);
                        i++;
                    }

                    //obtengo las transacciones multiples
                    dynamic reporte = extBll.GetReporte(inicial, final, chk, tipo);
                    var Reportito = reporte.Reporte;

                    foreach (var item in Reportito)
                    {
                        d = new ExpandoObject();
                        d.nro = i;
                        d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");

                        d.moneda = 1;
                        d.codigooficina = item.codigooficina;
                        d.codigodepartamento = item.codigodepartamento;
                        d.tipo_producto = item.tipo_producto;
                        //if (item.tipoproducto.ToString().Trim() == "AV") d.tipo_producto = 90;
                        //if ((item.tipoproducto.ToString().Trim() == "AC") && item.codlinea.ToString().Trim() == "CONT") d.tipo_producto = 92;
                        //if ((item.tipoproducto.ToString().Trim() == "AC") && item.codlinea.ToString().Trim() == "PROG") d.tipo_producto = 93;
                        //if (item.tipoproducto.ToString().Trim() == "AT") d.tipo_producto = 91;
                        //if (item.tipoproducto.ToString().Trim() == "PO") d.tipo_producto = 94;
                        //if (item.tipoproducto.ToString().Trim() == "CR") d.tipo_producto = 09;

                        d.tipo_transaccion = item.tipo_transaccion;
                        d.numerocuenta = item.numerocuenta;
                        d.tipoidentificacion = item.tipoidentificacion.ToString();
                        d.documento = item.cedulasociado.ToString();
                        d.primerapellido = item.primerapellido;
                        d.segundoapellido = item.segundoapellido;
                        d.nombres = item.nombres;
                        d.otronombre = item.segundonombre;

                        if (item.tipoidentificacion == 31)
                        { d.razonsocial = item.razonsocial; }
                        else { d.razonsocial = ""; }

                        d.actividadeconomica = item.actividadeconomica;
                        if ((item.digito == 0) && (item.salario == 0)) { d.salario = 10000; }
                        else
                        {
                            d.salario = item.salario;
                        }
                        if (item.digito != 0)  { d.salario = 0; }
                        
                        decimal valort = item.valortransaccion;
                        if (valort >= 10000000)
                        {
                            d.valortransaccion = Math.Round(item.valortransaccion);
                            d.tipoidentificaciondeconsignante = item.tipoidentificacion.ToString();
                            d.tipodocconsignante = item.cedulasociado.ToString();
                            d.primerapellidoconsignante = item.primerapellido;
                            d.segundoapellidoconsignante = item.segundoapellido;
                            d.nombreconsignante = item.nombres;
                            d.otronombreconsignante = item.segundonombre;
                        }
                        else
                        {
                            d.valortransaccion = 0;
                            d.tipoidentificaciondeconsignante = "";
                            d.tipodocconsignante = "";
                            d.primerapellidoconsignante = "";
                            d.segundoapellidoconsignante = "";
                            d.nombreconsignante = "";
                            d.otronombreconsignante = "";
                        }
                        if (item.tipoidentificacion == 31)
                        {
                            d.primerapellido = "";
                            d.segundoapellido = "";
                            d.nombres = "";
                            d.otronombre = "";
                            d.primerapellidoconsignante = "";
                            d.segundoapellidoconsignante = "";
                            d.nombreconsignante = "";
                            d.otronombreconsignante = "";
                        }
                        d.codoperador = item.operador;

                        cuenta_perm.Add(d);
                        //laPeople.Add(d);
                        i++;
                    }

                }



                //foreach (var item in Reportito)
                //{
                //    if (tipo == "C")
                //    {

                //        //dynamic d = new ExpandoObject();
                //        UIAF d = new UIAF();
                //        d.nro = i;
                //        d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");

                //        d.moneda = item.moneda;
                //        d.codigooficina = item.codigooficina;
                //        d.codigodepartamento = item.codigodepartamento;
                //        d.tipo_producto = item.tipo_producto;
                //        d.tipo_transaccion = item.tipo_transaccion;
                //        d.numerocuenta = item.numerocuenta;
                //        d.tipoidentificacion = item.tipoidentificacion.ToString();
                //        d.documento = item.documento.ToString();
                //        d.primerapellido = item.primerapellido;
                //        d.segundoapellido = item.segundoapellido;
                //        d.nombres = item.nombres;
                //        d.otronombre = item.otronombre;

                //        if (item.tipoidentificacion == 31)
                //        { d.razonsocial = item.razonsocial; }
                //        else { d.razonsocial = ""; }

                //        d.actividadeconomica = item.actividadeconomica;
                //        d.salario = item.salario;
                //        decimal valort = item.valortransaccion;
                //        if (valort >= 10000000)
                //        {
                //            d.valortransaccion = Math.Round(item.valortransaccion);
                //            d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante.ToString();
                //            d.tipodocconsignante = item.tipodocconsignante.ToString();
                //            d.primerapellidoconsignante = item.primerapellidoconsignante;
                //            d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                //            d.nombreconsignante = item.nombreconsignante;
                //            d.otronombreconsignante = item.otronombreconsignante;
                //        }
                //        else
                //        {
                //            d.valortransaccion = 0;
                //            d.tipoidentificaciondeconsignante = "";
                //            d.tipodocconsignante = "";
                //            d.primerapellidoconsignante = "";
                //            d.segundoapellidoconsignante = "";
                //            d.nombreconsignante = "";
                //            d.otronombreconsignante = "";
                //        }
                //        if (item.tipoidentificacion == 31)
                //        {
                //            d.primerapellido = "";
                //            d.segundoapellido = "";
                //            d.nombres = "";
                //            d.otronombre = "";
                //            d.primerapellidoconsignante = "";
                //            d.segundoapellidoconsignante = "";
                //            d.nombreconsignante = "";
                //            d.otronombreconsignante = "";
                //        }

                //        i++;
                //        uiafmov.Add(d);
                //        laPeople.Add(d);
                //    }
                //    else
                //    {
                //        //dynamic d = new ExpandoObject();
                //        UIAF d = new UIAF();
                //        d.nro = i;
                //        d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");//fecha vinculacion

                //        d.codigodepartamento = item.codigodepartamento;//
                //        d.tipo_producto = item.tipo_producto;//

                //        d.numerocuenta = item.numerocuenta;//numero del producto
                //        d.tipoidentificacion = item.tipoidentificacion.ToString();//
                //        d.documento = item.documento.ToString();//
                //        d.primerapellido = item.primerapellido;//
                //        d.segundoapellido = item.segundoapellido;//
                //        d.nombres = item.nombres;//
                //        d.otronombre = item.otronombre;//

                //        if (item.tipoidentificacion == 31)//
                //        { d.razonsocial = item.razonsocial; }
                //        else { d.razonsocial = ""; }

                //        d.tipoidentificaciondeconsignante ="";
                //        d.tipodocconsignante = "";
                //        d.primerapellidoconsignante = "";
                //        d.segundoapellidoconsignante = "";
                //        d.nombreconsignante = "";
                //        d.otronombreconsignante = "";
                //        if (item.tipoidentificacion == 31)
                //        {
                //            d.primerapellido = "";
                //            d.segundoapellido = "";
                //            d.nombres = "";
                //            d.otronombre = "";
                //            d.primerapellidoconsignante = "";
                //            d.segundoapellidoconsignante = "";
                //            d.nombreconsignante = "";
                //            d.otronombreconsignante = "";
                //        }
                //        i++;
                //        uiafmov.Add(d);
                //        laPeople.Add(d);
                //    }
                //}



                dynamic reporte2 = extBll.GetReporteSocial(inicial, final, chk, tipo);
                var Reportito2 = reporte2.Reporte;


                foreach (var item in Reportito2)
                {
                    if (tipo == "C")
                    {
                        ////dynamic d = new ExpandoObject();
                        //UIAF d = new UIAF();
                        //d.nro = i;
                        //d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");

                        //d.moneda = item.moneda;
                        //d.codigooficina = item.codigooficina;
                        //d.codigodepartamento = item.codigodepartamento;
                        //d.tipo_producto = item.tipo_producto;
                        //d.tipo_transaccion = item.tipo_transaccion;
                        //d.numerocuenta = item.documento.ToString();
                        //d.tipoidentificacion = item.tipoidentificacion.ToString(); 
                        //d.documento = item.documento.ToString();
                        //d.primerapellido = item.primerapellido;
                        //d.segundoapellido = item.segundoapellido;
                        //d.nombres = item.nombres;
                        //d.otronombre = item.otronombre;
                        //if (item.tipoidentificacion == 31)
                        //{ d.razonsocial = item.razonsocial; }
                        //else { d.razonsocial = ""; }
                        //d.actividadeconomica = item.actividadeconomica;
                        //d.salario = item.salario;
                        //decimal valort = item.valortransaccion;
                        //if (valort >= 10000000)
                        //{
                        //    d.valortransaccion = Math.Round(item.valortransaccion);
                        //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante.ToString();
                        //    d.tipodocconsignante = item.tipodocconsignante.ToString();
                        //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                        //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                        //    d.nombreconsignante = item.nombreconsignante;
                        //    d.otronombreconsignante = item.otronombreconsignante;
                        //}
                        //else
                        //{
                        //    d.valortransaccion = 0;
                        //    d.tipoidentificaciondeconsignante = "";
                        //    d.tipodocconsignante = "";
                        //    d.primerapellidoconsignante = "";
                        //    d.segundoapellidoconsignante = "";
                        //    d.nombreconsignante = "";
                        //    d.otronombreconsignante = "";
                        //}
                        //if (item.tipoidentificacion == 31)
                        //{
                        //    d.primerapellido = "";
                        //    d.segundoapellido = "";
                        //    d.nombres = "";
                        //    d.otronombre = "";
                        //    d.primerapellidoconsignante = "";
                        //    d.segundoapellidoconsignante = "";
                        //    d.nombreconsignante = "";
                        //    d.otronombreconsignante = "";
                        //}
                        //i++;
                        //uiafmov.Add(d);
                        //laPeople.Add(d);
                    }
                    else
                    {
                        //dynamic d = new ExpandoObject();
                        UIAF d = new UIAF();
                        d.nro = i;
                        d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");//fecha vinculacion

                        d.codigodepartamento = item.codigodepartamento;//
                        d.tipo_producto = item.tipo_producto;//

                        d.numerocuenta = item.documento.ToString();//numero del producto
                        d.tipoidentificacion = item.tipoidentificacion.ToString();//
                        d.documento = item.documento.ToString();//
                        d.primerapellido = item.primerapellido;//
                        d.segundoapellido = item.segundoapellido;//
                        d.nombres = item.nombres;//
                        d.otronombre = item.otronombre;//

                        if (item.tipoidentificacion == 31)//
                        { d.razonsocial = item.razonsocial; }
                        else { d.razonsocial = ""; }
                        d.tipoidentificaciondeconsignante = "";
                        d.tipodocconsignante = "";
                        d.primerapellidoconsignante = "";
                        d.segundoapellidoconsignante = "";
                        d.nombreconsignante = "";
                        d.otronombreconsignante = "";
                        if (item.tipoidentificacion == 31)
                        {
                            d.primerapellido = "";
                            d.segundoapellido = "";
                            d.nombres = "";
                            d.otronombre = "";
                            d.primerapellidoconsignante = "";
                            d.segundoapellidoconsignante = "";
                            d.nombreconsignante = "";
                            d.otronombreconsignante = "";
                        }
                        i++;
                        uiafmov.Add(d);
                        laPeople.Add(d);
                    }

                }

                dynamic reporte3 = extBll.GetReporteContractual(inicial, final, chk, tipo);
                var Reportito3 = reporte3.Reporte;


                foreach (var item in Reportito3)
                {
                    if (tipo == "C")
                    {
                        ////dynamic d = new ExpandoObject();
                        //UIAF d = new UIAF();
                        //d.nro = i;
                        //d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");

                        //d.moneda = item.moneda;
                        //d.codigooficina = item.codigooficina;
                        //d.codigodepartamento = item.codigodepartamento;
                        //d.tipo_producto = item.tipo_producto;
                        //d.tipo_transaccion = item.tipo_transaccion;
                        //d.numerocuenta = item.numerocuenta;
                        //d.tipoidentificacion = item.tipoidentificacion.ToString();
                        //d.documento = item.documento.ToString();
                        //d.primerapellido = item.primerapellido;
                        //d.segundoapellido = item.segundoapellido;
                        //d.nombres = item.nombres;
                        //d.otronombre = item.otronombre;
                        //if (item.tipoidentificacion == 31)
                        //{ d.razonsocial = item.razonsocial; }
                        //else { d.razonsocial = ""; }
                        //d.actividadeconomica = item.actividadeconomica;
                        //d.salario = item.salario;
                        //decimal valort = item.valortransaccion;
                        //if (valort >= 10000000)
                        //{
                        //    d.valortransaccion = Math.Round(item.valortransaccion);
                        //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante.ToString();
                        //    d.tipodocconsignante = item.tipodocconsignante.ToString();
                        //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                        //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                        //    d.nombreconsignante = item.nombreconsignante;
                        //    d.otronombreconsignante = item.otronombreconsignante;
                        //}
                        //else
                        //{
                        //    d.valortransaccion = 0;
                        //    d.tipoidentificaciondeconsignante = "";
                        //    d.tipodocconsignante = "";
                        //    d.primerapellidoconsignante = "";
                        //    d.segundoapellidoconsignante = "";
                        //    d.nombreconsignante = "";
                        //    d.otronombreconsignante = "";
                        //}
                        //if (item.tipoidentificacion == 31)
                        //{
                        //    d.primerapellido = "";
                        //    d.segundoapellido = "";
                        //    d.nombres = "";
                        //    d.otronombre = "";
                        //    d.primerapellidoconsignante = "";
                        //    d.segundoapellidoconsignante = "";
                        //    d.nombreconsignante = "";
                        //    d.otronombreconsignante = "";
                        //}
                        //i++;
                        //uiafmov.Add(d);
                        //laPeople.Add(d);
                    }
                    else
                    {
                        //dynamic d = new ExpandoObject();
                        UIAF d = new UIAF();
                        d.nro = i;
                        d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");//fecha vinculacion

                        d.codigodepartamento = item.codigodepartamento;//
                        if (item.codlinea == "PROG") { d.tipo_producto = 93; }
                        if (item.codlinea == "CONT") { d.tipo_producto = 92; }

                        d.numerocuenta = item.numerocuenta;//numero del producto
                        d.tipoidentificacion = item.tipoidentificacion.ToString();//
                        d.documento = item.documento.ToString();//
                        d.primerapellido = item.primerapellido;//
                        d.segundoapellido = item.segundoapellido;//
                        d.nombres = item.nombres;//
                        d.otronombre = item.otronombre;//

                        if (item.tipoidentificacion == 31)//
                        { d.razonsocial = item.razonsocial; }
                        else { d.razonsocial = ""; }

                        d.tipoidentificaciondeconsignante = "";
                        d.tipodocconsignante = "";
                        d.primerapellidoconsignante = "";
                        d.segundoapellidoconsignante = "";
                        d.nombreconsignante = "";
                        d.otronombreconsignante = "";
                        if (item.tipoidentificacion == 31)
                        {
                            d.primerapellido = "";
                            d.segundoapellido = "";
                            d.nombres = "";
                            d.otronombre = "";
                            d.primerapellidoconsignante = "";
                            d.segundoapellidoconsignante = "";
                            d.nombreconsignante = "";
                            d.otronombreconsignante = "";
                        }
                        i++;
                        uiafmov.Add(d);
                        laPeople.Add(d);
                    }
                }

                dynamic reporte4 = extBll.GetReporteAtermino(inicial, final, chk, tipo);
                var Reportito4 = reporte4.Reporte;


                foreach (var item in Reportito4)
                {
                    if (tipo == "C")
                    {
                        ////dynamic d = new ExpandoObject();
                        //UIAF d = new UIAF();
                        //d.nro = i;
                        //d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");

                        //d.moneda = item.moneda;
                        //d.codigooficina = item.codigooficina;
                        //d.codigodepartamento = item.codigodepartamento;
                        //d.tipo_producto = item.tipo_producto;
                        //d.tipo_transaccion = item.tipo_transaccion;
                        //d.numerocuenta = item.numerocuenta;
                        //d.tipoidentificacion = item.tipoidentificacion.ToString();
                        //d.documento = item.documento.ToString();
                        //d.primerapellido = item.primerapellido;
                        //d.segundoapellido = item.segundoapellido;
                        //d.nombres = item.nombres;
                        //d.otronombre = item.otronombre;
                        //if (item.tipoidentificacion == 31)
                        //{ d.razonsocial = item.razonsocial; }
                        //else { d.razonsocial = ""; }
                        //d.actividadeconomica = item.actividadeconomica;
                        //d.salario = item.salario;
                        //decimal valort = item.valortransaccion;
                        //if (valort >= 10000000)
                        //{
                        //    d.valortransaccion = Math.Round(item.valortransaccion);
                        //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante.ToString();
                        //    d.tipodocconsignante = item.tipodocconsignante.ToString();
                        //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                        //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                        //    d.nombreconsignante = item.nombreconsignante;
                        //    d.otronombreconsignante = item.otronombreconsignante;
                        //}
                        //else
                        //{
                        //    d.valortransaccion = 0;
                        //    d.tipoidentificaciondeconsignante = "";
                        //    d.tipodocconsignante = "";
                        //    d.primerapellidoconsignante = "";
                        //    d.segundoapellidoconsignante = "";
                        //    d.nombreconsignante = "";
                        //    d.otronombreconsignante = "";
                        //}
                        //if (item.tipoidentificacion == 31)
                        //{
                        //    d.primerapellido = "";
                        //    d.segundoapellido = "";
                        //    d.nombres = "";
                        //    d.otronombre = "";
                        //    d.primerapellidoconsignante = "";
                        //    d.segundoapellidoconsignante = "";
                        //    d.nombreconsignante = "";
                        //    d.otronombreconsignante = "";
                        //}
                        //i++;
                        //uiafmov.Add(d);
                        //laPeople.Add(d);
                    }
                    else
                    {
                        //dynamic d = new ExpandoObject();
                        UIAF d = new UIAF();
                        d.nro = i;
                        d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");//fecha vinculacion

                        d.codigodepartamento = item.codigodepartamento;//
                        d.tipo_producto = item.tipo_producto;//

                        d.numerocuenta = item.numerocuenta;//numero del producto
                        d.tipoidentificacion = item.tipoidentificacion.ToString();//
                        d.documento = item.documento.ToString();//
                        d.primerapellido = item.primerapellido;//
                        d.segundoapellido = item.segundoapellido;//
                        d.nombres = item.nombres;//
                        d.otronombre = item.otronombre;//

                        if (item.tipoidentificacion == 31)//
                        { d.razonsocial = item.razonsocial; }
                        else { d.razonsocial = ""; }

                        d.tipoidentificaciondeconsignante = "";
                        d.tipodocconsignante = "";
                        d.primerapellidoconsignante = "";
                        d.segundoapellidoconsignante = "";
                        d.nombreconsignante = "";
                        d.otronombreconsignante = "";
                        if (item.tipoidentificacion == 31)
                        {
                            d.primerapellido = "";
                            d.segundoapellido = "";
                            d.nombres = "";
                            d.otronombre = "";
                            d.primerapellidoconsignante = "";
                            d.segundoapellidoconsignante = "";
                            d.nombreconsignante = "";
                            d.otronombreconsignante = "";
                        }
                        i++;
                        uiafmov.Add(d);
                        laPeople.Add(d);
                    }
                }

                dynamic reporte5 = extBll.GetReporteAlavista(inicial, final, chk, tipo);
                var Reportito5 = reporte5.Reporte;


                foreach (var item in Reportito5)
                {
                    if (tipo == "C")
                    {
                        ////dynamic d = new ExpandoObject();
                        //UIAF d = new UIAF();
                        //d.nro = i;
                        //d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");

                        //d.moneda = item.moneda;
                        //d.codigooficina = item.codigooficina;
                        //d.codigodepartamento = item.codigodepartamento;
                        //d.tipo_producto = item.tipo_producto;
                        //d.tipo_transaccion = item.tipo_transaccion;
                        //d.numerocuenta = item.numerocuenta;
                        //d.tipoidentificacion = item.tipoidentificacion.ToString();
                        //d.documento = item.documento.ToString();
                        //d.primerapellido = item.primerapellido;
                        //d.segundoapellido = item.segundoapellido;
                        //d.nombres = item.nombres;
                        //d.otronombre = item.otronombre;
                        //if (item.tipoidentificacion == 31)
                        //{ d.razonsocial = item.razonsocial; }
                        //else { d.razonsocial = ""; }
                        //d.actividadeconomica = item.actividadeconomica;
                        //d.salario = item.salario;
                        //decimal valort = item.valortransaccion;
                        //if (valort >= 10000000)
                        //{
                        //    d.valortransaccion = Math.Round(item.valortransaccion);
                        //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante.ToString();
                        //    d.tipodocconsignante = item.tipodocconsignante.ToString();
                        //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                        //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                        //    d.nombreconsignante = item.nombreconsignante;
                        //    d.otronombreconsignante = item.otronombreconsignante;
                        //}
                        //else
                        //{
                        //    d.valortransaccion = 0;
                        //    d.tipoidentificaciondeconsignante = "";
                        //    d.tipodocconsignante = "";
                        //    d.primerapellidoconsignante = "";
                        //    d.segundoapellidoconsignante = "";
                        //    d.nombreconsignante = "";
                        //    d.otronombreconsignante = "";
                        //}
                        //if (item.tipoidentificacion == 31)
                        //{
                        //    d.primerapellido = "";
                        //    d.segundoapellido = "";
                        //    d.nombres = "";
                        //    d.otronombre = "";
                        //    d.primerapellidoconsignante = "";
                        //    d.segundoapellidoconsignante = "";
                        //    d.nombreconsignante = "";
                        //    d.otronombreconsignante = "";
                        //}
                        //i++;
                        //uiafmov.Add(d);
                        //laPeople.Add(d);
                    }
                    else
                    {
                        //dynamic d = new ExpandoObject();
                        UIAF d = new UIAF();
                        d.nro = i;
                        d.fechatransaccion = item.fechatransaccion.ToString("yyyy-MM-dd");//fecha vinculacion

                        d.codigodepartamento = item.codigodepartamento;//
                        d.tipo_producto = item.tipo_producto;//

                        d.numerocuenta = item.numerocuenta;//numero del producto
                        d.tipoidentificacion = item.tipoidentificacion.ToString();//
                        d.documento = item.documento.ToString();//
                        d.primerapellido = item.primerapellido;//
                        d.segundoapellido = item.segundoapellido;//
                        d.nombres = item.nombres;//
                        d.otronombre = item.otronombre;//

                        if (item.tipoidentificacion == 31)//
                        { d.razonsocial = item.razonsocial; }
                        else { d.razonsocial = ""; }

                        d.tipoidentificaciondeconsignante = "";
                        d.tipodocconsignante = "";
                        d.primerapellidoconsignante = "";
                        d.segundoapellidoconsignante = "";
                        d.nombreconsignante = "";
                        d.otronombreconsignante = "";
                        if (item.tipoidentificacion == 31)
                        {
                            d.primerapellido = "";
                            d.segundoapellido = "";
                            d.nombres = "";
                            d.otronombre = "";
                            d.primerapellidoconsignante = "";
                            d.segundoapellidoconsignante = "";
                            d.nombreconsignante = "";
                            d.otronombreconsignante = "";
                        }
                        i++;
                        uiafmov.Add(d);
                        laPeople.Add(d);
                    }
                }


                if (tipo == "C")
                {
                    ViewBag.Reportes = cuenta_perm;
                    ViewBag.Reportes1 = null;
                }
                else {
                    ViewBag.Reportes1 = uiafmov;
                    ViewBag.Reportes = null;
                }
                //ViewBag.inicial = inicial.ToString("yyyy-MM-dd");
                //ViewBag.final = final.ToString("yyyy-MM-dd"); 
                tipo2 = tipo;
                cdm.PageSize = 25;

                cdm.TotalRecord = laPeople.Count();
                cdm.NoOfPages = (cdm.TotalRecord / cdm.PageSize) + ((cdm.TotalRecord % cdm.PageSize) > 0 ? 1 : 0);
                cdm.Customer = laPeople.OrderBy(o => o.nro).Skip((page - 1) * cdm.PageSize).Take(cdm.PageSize).ToList();

                //return View("Details", cdm);
                return Json(new
                {
                    estado = true,
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception a) {
                Trace.WriteLine("Controlador ReportesUIAF, funcion calcular " + a.Message.ToString(), "Error " + DateTime.Now);

                //return View("Details");
                return Json(new
                {
                    estado = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// crea el archivo excel con la consulta generada en el metodo consulta
        /// </summary>
        public ExcelResult EstadoAseguradoras()
        {

            //CREAR LIBRO Y HOJA DE CALCULO
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet 1");
            string vaa = tipo1;
            if (vaa.Trim() == "C")
            {
                ViewBag.repor = cuenta_perm;
            }
            else{ ViewBag.repor = uiafmov; }
               
            try
            {

                
                if (vaa.Trim() == "C")
                {
                    worksheet.Cell(1, 1).SetValue("No.").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 2).SetValue("Fecha transaccion").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 3).SetValue("Valor Transaccion").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 4).SetValue("Moneda").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 5).SetValue("Codigo oficina").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 6).SetValue("Codigo departamento").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 7).SetValue("Tipo producto").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 8).SetValue("Tipo transaccion").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 9).SetValue("Numero cuenta").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 10).SetValue("Identificacion").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 11).SetValue("Documento").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 12).SetValue("Primer apellido").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 13).SetValue("Segundo apellido").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 14).SetValue("Nombres").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 15).SetValue("Otro Nombre").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 16).SetValue("Razon social").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 17).SetValue("Actividad economica").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 18).SetValue("Salario").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 19).SetValue("Identificacion de consignante").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 20).SetValue("Documento consignante").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 21).SetValue("Primer apellido consignante").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 22).SetValue("Segundo apellido consignante").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 23).SetValue("Nombre consignante").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 24).SetValue("Otro nombre consignante").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 25).SetValue("Operador").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    

                    int i = 2;
                    foreach (var item in ViewBag.repor)
                    {
                        int nro = item.nro;
                        worksheet.Cell(i, 1).SetValue(nro).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        string ffecha = item.fechatransaccion;
                        worksheet.Cell(i, 2)
                         .SetValue(ffecha)
                         .Style.DateFormat.SetFormat("yyyy-MM-dd")
                         .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        //worksheet.Cell(i, 2).Value = ffecha;

                        if (Convert.ToDecimal(item.valortransaccion) == 0)
                        {
                            string valor = item.valortransaccion.ToString();
                            worksheet.Cell(i, 3).SetValue(valor).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        }
                        else
                        {
                            decimal valor = Math.Round(item.valortransaccion);
                            worksheet.Cell(i, 3).SetValue(valor).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        }

                        worksheet.Cell(i, 4).Value = item.moneda;
                        string oficina = item.codigooficina;
                        worksheet.Cell(i, 5).SetValue(oficina).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        string depar = item.codigodepartamento;
                        worksheet.Cell(i, 6)
                         .SetValue(depar)
                         .Style.NumberFormat.SetFormat("#######").Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        //worksheet.Cell(i, 6).SetValue(depar).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        string tipopro = item.tipo_producto.ToString();
                        if (tipopro.Trim() == "9") tipopro = "09";
                        worksheet.Cell(i, 7).Value = tipopro;
                        worksheet.Cell(i, 8).Value = item.tipo_transaccion;
                        string nrocuenta = item.numerocuenta;
                        worksheet.Cell(i, 9).SetValue(nrocuenta).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(i, 10).Value = item.tipoidentificacion;
                        string doc = item.documento;
                        worksheet.Cell(i, 11).SetValue(doc).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string pa = item.primerapellido;
                        worksheet.Cell(i, 12).SetValue(pa).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string sa = item.segundoapellido;
                        worksheet.Cell(i, 13).SetValue(sa).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string nom = item.nombres;
                        worksheet.Cell(i, 14).SetValue(nom).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string onom = item.otronombre;
                        worksheet.Cell(i, 15).SetValue(onom).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string rs = item.razonsocial;
                        worksheet.Cell(i, 16).SetValue(rs).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(i, 17).Value = item.actividadeconomica;
                        decimal salario = item.salario;
                        worksheet.Cell(i, 18).SetValue(salario).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet.Cell(i, 19).Value = item.tipoidentificaciondeconsignante;
                        string td = item.tipodocconsignante.ToString();
                        worksheet.Cell(i, 20).SetValue(td).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string pac = item.primerapellidoconsignante;
                        worksheet.Cell(i, 21).SetValue(pac).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string sac = item.segundoapellidoconsignante;
                        worksheet.Cell(i, 22).SetValue(sac).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string nomc = item.nombreconsignante;
                        worksheet.Cell(i, 23).SetValue(nomc).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string onomc = item.otronombreconsignante;
                        worksheet.Cell(i, 24).SetValue(onomc).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string codopera = item.codoperador;
                        worksheet.Cell(i, 25).SetValue(codopera).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        
                        i++;
                        GC.SuppressFinalize(this);
                    }
                }
                else
                {
                    worksheet.Cell(1, 1).SetValue("No.").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 2).SetValue("Número del producto").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 3).SetValue("Fecha de vinculación").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 4).SetValue("Tipo de producto").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    worksheet.Cell(1, 5).SetValue("Codigo departamento").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);


                    worksheet.Cell(1, 6).SetValue("Identificacion titular 1").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 7).SetValue("Documento titular 1").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 8).SetValue("Primer apellido titular 1").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 9).SetValue("Segundo apellido titular 1").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 10).SetValue("Primer nombre titular 1").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 11).SetValue("Otro Nombre titular 1").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 12).SetValue("Razon social titular 1").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    worksheet.Cell(1, 13).SetValue("Identificacion titular 2").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 14).SetValue("Documento titular 2").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 15).SetValue("Primer apellido titular 2").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 16).SetValue("Segundo apellido titular 2").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 17).SetValue("Primer nombre titular 2").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 18).SetValue("Otro Nombre titular 2").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 19).SetValue("Razon social titular 2").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);


                    int i = 2;
                    foreach (var item in ViewBag.repor)
                    {
                        if (i > 100000) GC.SuppressFinalize(this);
                        int nro = item.nro;
                        worksheet.Cell(i, 1).SetValue(nro).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        string nrocuenta = item.numerocuenta;
                        worksheet.Cell(i, 2).SetValue(nrocuenta).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string ffecha = item.fechatransaccion;
                        worksheet.Cell(i, 3)
                        .SetValue(ffecha)
                        .Style.DateFormat.SetFormat("yyyy-MM-dd")
                        .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        //worksheet.Cell(i, 3).Value = ffecha;
                        worksheet.Cell(i, 4).Value = item.tipo_producto;

                        string depar = item.codigodepartamento;
                        worksheet.Cell(i, 5).SetValue(depar).Style.NumberFormat.SetFormat("#######").Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                        worksheet.Cell(i, 6).Value = item.tipoidentificacion;
                        string doc = item.documento;
                        worksheet.Cell(i, 7).SetValue(doc).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string pa = item.primerapellido;
                        worksheet.Cell(i, 8).SetValue(pa).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string sa = item.segundoapellido;
                        worksheet.Cell(i, 9).SetValue(sa).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string nom = item.nombres;
                        worksheet.Cell(i, 10).SetValue(nom).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string onom = item.otronombre;
                        worksheet.Cell(i, 11).SetValue(onom).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string rs = item.razonsocial;
                        worksheet.Cell(i, 12).SetValue(rs).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        string tdi = item.tipoidentificaciondeconsignante.ToString();
                        if (tdi == "0") tdi = "";
                        worksheet.Cell(i, 13).SetValue(tdi).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        string td = item.tipodocconsignante.ToString();
                        if (td == "0") td = "";
                        worksheet.Cell(i, 14).SetValue(td).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string pac = item.primerapellidoconsignante;
                        worksheet.Cell(i, 15).SetValue(pac).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string sac = item.segundoapellidoconsignante;
                        worksheet.Cell(i, 16).SetValue(sac).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string nomc = item.nombreconsignante;
                        worksheet.Cell(i, 17).SetValue(nomc).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        string onomc = item.otronombreconsignante;
                        worksheet.Cell(i, 18).SetValue(onomc).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        rs = "";
                        worksheet.Cell(i, 19).SetValue(rs).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        i++;
                        GC.SuppressFinalize(this);
                    }
                }

                worksheet.Columns().AdjustToContents();
                
                return new ExcelResult(workbook, "REPORTES UIAF");

            }
            catch(Exception a)
            {
                Trace.WriteLine("Controlador ReportesUIAF, funcion EstadoAseguradoras, GenerarExcel " + a.Message.ToString(), "Error " + DateTime.Now);
                return new ExcelResult(workbook, "REPORTES UIAF");
            }
        

        }
       
    }
}
