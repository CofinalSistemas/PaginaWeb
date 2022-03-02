using Ingenio.DLL;
using Ingenio.PortalWebExterno.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO.Ingenio;
using Ingenio.Models;
using Ingenio.BO;
using Ingenio.BLL;
using Ingenio.Filters;
using System.Diagnostics;

namespace Ingenio.PortalWebExterno.Controllers
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// Simulador de creditos
    /// </summary>
    public class SimuladorCreController : Controller
    {
        ExtractosBLL extBll = new ExtractosBLL();
        AsociadoBLL asocBll = new AsociadoBLL();
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra un formulario para ingresar los datos iniciales para generar consulta
        /// </summary>
        // GET: SimuladorCre
        [Allow(action = "REPORTE_SIMULADOR_CREDITO")]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Genera el reporte consulta de base de datos 
        /// </summary>
        /// <param name="inicial">argumento tipo fecha es enviado desde index formulario</param>
        /// <param name="final">argumento tipo fecha es enviado desde index formulario</param>
        [Allow(action = "REPORTE_SIMULADOR_CREDITO")]
        public JsonResult Reporte(DateTime inicial, DateTime final)
        {
            //OBTIENE DATOS DE SIMULACIONES REALIZADAS
            dynamic query = extBll.GetDatosCDATyCre(inicial, final = Convert.ToDateTime(final.ToString().Substring(0, 10) + " 23:59:00.000"), "1");
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra el formulario para ingresar los datos y calcular la simulacion
        /// </summary>
        // GET: SimuladorCre/Details/5
        public ActionResult Details()
        {
            AccountModels cuentaUsuario = new AccountModels();
            int txtcedula = 0,bandera=0;
            ViewBag.boton = null;
            //ViewBag.Aso.nits.nombreingtegrado = "";
            if (!cuentaUsuario.IsNull() && cuentaUsuario.IsAsociado)
            {
                txtcedula = cuentaUsuario.Identificacion;
                asociados aso = asocBll.GetAsociado(txtcedula);
                if (aso != null)
                {
                    ViewBag.nombreintegrado = aso.nits.nombreintegrado;
                    ViewBag.Email = aso.nits.email;
                    ViewBag.Telefono = aso.nits.telefono1;
                }
                else
                {
                    nits nitaso = asocBll.GetNitsAsociado(txtcedula);
                    ViewBag.nombreintegrado = nitaso.nombreintegrado;
                    ViewBag.Email = nitaso.email;
                    ViewBag.Telefono = nitaso.telefono1;
                }
                bandera = 1;
            }

            string param1 = Request["msj"];
            string des = Request["destino"];
            string lin = Request["linea"];
            //RECIBO LOS PARAMETROS DESDE FORM
           
            if (bandera == 0)
            {

                ViewBag.Monto1 = Request["capi"];
                ViewBag.Plazo = Request["pla"];
                ViewBag.Periodo = Request["peri"];
                //ViewBag.Periodo = peri;
                //liqui = liquida
                ViewBag.nombreintegrado = Request["nomb"];
                ViewBag.Email = Request["txtema"];
                ViewBag.Telefono = Request["tele"];
            }
            if (param1 == "1")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('El Plazo no corresponde con la linea seleccionada, ver Tasas')</script>");
            }
            ViewBag.Destino = des;
            ViewBag.Linea = lin;
            return View();
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Recibe los datos y procede a consultar las tasas en BD. y realizar la simulacion segun parametros
        /// </summary>
        /// <param name="txtPlazo">argumento tipo cadena es enviado desde details formulario</param>
        /// <param name="txtMonto">argumento tipo cadena es enviado desde details formulario</param>
        /// <param name="txtTabla">argumento tipo cadena es enviado desde details formulario</param>
        /// <param name="txtPeriodo">argumento tipo cadena es enviado desde details formulario</param>
        /// <param name="txtNombre">argumento tipo cadena es enviado desde details formulario</param>
        /// <param name="txtEmail">argumento tipo cadena es enviado desde details formulario</param>
        /// <param name="Destino">argumento tipo cadena es enviado desde details formulario</param>
        /// <param name="Telefono">argumento tipo cadena es enviado desde details formulario</param>
        /// <returns></returns>
        public ActionResult Calcular(int txtPlazo, string txtMonto, string txtTabla, string txtPeriodo, string txtNombre, string txtEmail, string Destino, string Telefono)
        {
            DateTime fechainicial = DateTime.Today;
            /////////////convierto a dias////////////////////////
            Double periodo = Convert.ToDouble(txtPeriodo);
            int dias = 0;
            if (periodo == 30) dias = 30;
            if (periodo == 15) dias = 15;
            if (periodo == 2) dias = 180;
            if (periodo == 4) dias = 90;

            dias = dias * txtPlazo;
            dynamic query = extBll.GetLineas33(txtTabla, Destino);

            var datos = query.Datos;
            int aplica = 0;
            int plazoinicial = 0;
            int plazofinal = 0;
            decimal tasainteres = 0;
            foreach (var item in datos)
            {
                plazoinicial = item.plazoinicial;
                plazofinal = item.plazofinal;
                if (plazoinicial <= dias && plazofinal >= dias)
                {
                    aplica = 1;
                    TempData["tasacolocacion"] = item.tasainteres;
                    TempData["nombredestino"] = item.nombredestino;
                    break;
                }
                else { aplica = 0; }

            }
            if (aplica == 0)
            {
                TempData["vv"] = "1";
                return RedirectToAction("Details", "SimuladorCre", new { msj = "1", capi = txtMonto, pla = txtPlazo, peri = txtPeriodo, liqui = txtTabla, nomb = txtNombre, txtema = txtEmail, tele = Telefono });
            
            }

            dynamic querynom = extBll.GetLineasReporte(txtTabla);
            var inf = querynom.datos;
            foreach (var item in inf)
            {
                TempData["nombrelinea"] = item.nombre;
            }
            //////////////CALCULO NUMERICOS////////////////////////

            try
            {
                Double ts = Convert.ToDouble(Convert.ToString(TempData["tasacolocacion"]).Replace(".", ",")); //14.40924138666792; 
                Double saldoinicial = Convert.ToInt32(txtMonto.Substring(0, txtMonto.IndexOf(",")).Replace(".", ""));
                ViewBag.Monto = saldoinicial;
                ViewBag.Monto1 = ViewBag.Monto + "00";
                ViewBag.Nombredestino = TempData["nombredestino"];
                ViewBag.Nombrelinea = TempData["nombrelinea"];
                ViewBag.TasaEfectiva = ts;
                Double plazo = txtPlazo;
                Double sgvida = saldoinicial / 1000;

                Double tasanominal = 0;
                if (periodo == 30) tasanominal = 30;
                if (periodo == 15) tasanominal = 15;
                if (periodo == 2) tasanominal = 180;
                if (periodo == 4) tasanominal = 90;

                double tasa_nominal = ((Math.Pow(1 + ((double)ts / 100), tasanominal / 360)) - 1) * 100;


                Double cuota = (((tasa_nominal / 100) * saldoinicial) / (1 - Math.Pow((1 + (tasa_nominal / 100)), -txtPlazo)));


                ICollection<dynamic> simulamov = new List<dynamic>();
                for (int i = 1; i <= plazo; i++)
                {
                    dynamic b = new ExpandoObject();
                    b.nro = i;

                    b.fecha = fechainicial.ToShortDateString();
                    if (periodo == 30) fechainicial = fechainicial.AddMonths(1);
                    if (periodo == 15) fechainicial = fechainicial.AddDays(15);
                    if (periodo == 2) fechainicial = fechainicial.AddMonths(6);
                    if (periodo == 4) fechainicial = fechainicial.AddMonths(3);


                    Double AbInteres = saldoinicial * (tasa_nominal / 100) / 30 * 30;
                    b.abinteres = AbInteres;

                    Double abcapital = cuota - AbInteres;
                    b.abcapital = abcapital;

                    b.sgvida = (saldoinicial) / 1000;
                    b.cuota = cuota + ((saldoinicial) / 1000);

                    saldoinicial = saldoinicial - abcapital;
                    b.saldocapital = saldoinicial;


                    simulamov.Add(b);

                }

                ViewBag.Simulador = simulamov;
                ViewBag.Tasa = tasa_nominal;
                ViewBag.Cuota = cuota;
                if (periodo == 30) ViewBag.tiempo = "MENSUAL";
                if (periodo == 15) ViewBag.tiempo = "QUINCENAL";
                if (periodo == 2) ViewBag.tiempo = "SEMESTRAL";
                if (periodo == 4) ViewBag.tiempo = "TRIMESTRAL";

                ViewBag.Plazo = txtPlazo;

                ViewBag.Linea = txtTabla;
                ViewBag.Periodo = txtPeriodo;
                ViewBag.nombreintegrado = txtNombre;
                ViewBag.Email = txtEmail;
                ViewBag.Telefono = Telefono;
                //ValidateCaptcha();
                /////////////VALIDACION CAPTCHA/////////////////////
                //try
                //{
                //    var response = Request["g-recaptcha-response"];
                //    //secret that was generated in key value pair
                //    const string secret = "6LfNcBMTAAAAAINMYzYeHJSyUqnmA0wrkp4rFnoq";

                //    var client = new WebClient();
                //    var reply =
                //        client.DownloadString(
                //            string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                //    var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                //    //when response is false check for the error message
                //    if (!captchaResponse.Success)
                //    {
                //        if (captchaResponse.ErrorCodes.Count <= 0) return View();

                //        var error = captchaResponse.ErrorCodes[0].ToLower();
                //        switch (error)
                //        {
                //            case ("missing-input-secret"):
                //                ViewBag.Message = "The secret parameter is missing.";
                //                break;
                //            case ("invalid-input-secret"):
                //                ViewBag.Message = "The secret parameter is invalid or malformed.";
                //                break;

                //            case ("missing-input-response"):
                //                ViewBag.Message = "The response parameter is missing.";
                //                break;
                //            case ("invalid-input-response"):
                //                ViewBag.Message = "The response parameter is invalid or malformed.";
                //                break;

                //            default:
                //                ViewBag.Message = "Error occured. Please try again";
                //                break;
                //        }
                //    }
                //    else
                //    {
                        ViewBag.Message = "Valid";
                        //////ENVIO EMAIL /////////////
                        SmtpClient WebMail = new SmtpClient(ConfigurationManager.AppSettings["smtp"], Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                        WebMail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["pss"]);
                        WebMail.EnableSsl = true;


                        MailMessage mnsj = new MailMessage();
                        mnsj.Subject = "Usuario realizo simulacion credito";
                        mnsj.To.Add(new MailAddress(ConfigurationManager.AppSettings["emailmercadeo"]));
                        mnsj.From = new MailAddress(ConfigurationManager.AppSettings["email"], "Gestion Comercial - Simulacion credito COFINAL");

                        mnsj.Body = ("El usuario " + txtNombre.ToUpper() + " con Email " + txtEmail + " y Telefono " + Telefono + ",\n realizo una simulacion de credito con periodo: " + ViewBag.tiempo + ", Destino: " + TempData["nombredestino"] + ", Plazo: " + txtPlazo + ",Monto: " + txtMonto);
                        /* Enviar */

                        WebMail.Send(mnsj);
                        //////ENVIO DATOS A BD /////////////
                        SEGUIMIENTO_SIMULADORES dat = new SEGUIMIENTO_SIMULADORES();
                        dat.FECHA = DateTime.Now;
                        dat.NOMBRE = txtNombre;
                        dat.TELEFONO = Telefono.ToString();
                        dat.EMAIL = txtEmail;
                        dat.TIPO = "1";
                        extBll.GetSeguimiento_email(dat);

                //    }

                //}
                //catch (Exception e)
                //{
                //    Trace.WriteLine("Controlador SimuladorCre, funcion Calcular ln.146 " + e.Message.ToString(), "Error " + DateTime.Now);

                //}
                ///////////////////////////////////
                AccountModels cuentaUsuario = new AccountModels();

                if (!cuentaUsuario.IsNull() && cuentaUsuario.IsAsociado)
                {
                    ViewBag.boton = 2;
                }
                else
                {
                    ViewBag.boton = 3;
                }
                return View("Details");
            }
            catch (Exception a) {
                Trace.WriteLine("Controlador SimuladorCre, funcion Calcular " + a.Message.ToString(), "Error " + DateTime.Now);

                return View("Details"); }
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// obtiene las lineas
        /// </summary>
        public JsonResult GetLinea()
        {
            //OBTIENE LAS LINEAS 
            dynamic query = extBll.GetLineas();

            return Json(query, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// obtiene los destinos
        /// </summary>
        public JsonResult GetLinea2(string linea)
        {
            
            TempData["linea"] = linea;
            dynamic query = extBll.GetLineas2(linea);

            return Json(query, JsonRequestBehavior.AllowGet);

        }
        public class tasas
        {
            public string nombredestino { get; set; }
            public int plazoinicial { get; set; }
            public int plazofinal { get; set; }
            public string coddestino { get; set; }
            public string tasainteres { get; set; }

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// obtiene las tasas por cada destino
        /// </summary>
        public JsonResult GetLinea3(string line, string destino)
        {
           
            dynamic query = extBll.GetLineas3(line, destino);
            var Datos = query.Datos;

            List<tasas> parts = new List<tasas>();

            foreach (var item in Datos)
            {
                dynamic d = new ExpandoObject();
                parts.Add(new tasas()
                {
                    nombredestino = item.nombredestino,
                    plazoinicial = item.plazoinicial,
                    plazofinal = item.plazofinal,
                    tasainteres = item.tasainteres.ToString()
                }
                );

            }
            return Json(parts, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// obtiene todas las tasas por destino
        /// </summary>
        public JsonResult GetLinea4(string line)
        {
            
            dynamic query = extBll.GetLineas4(line);
            var Datos = query.Datos;

            List<tasas> parts = new List<tasas>();

            foreach (var item in Datos)
            {
                dynamic d = new ExpandoObject();
                parts.Add(new tasas()
                {
                    nombredestino = item.nombredestino,
                    plazoinicial = item.plazoinicial,
                    plazofinal = item.plazofinal,
                    coddestino = item.coddestino,
                    tasainteres = item.tasainteres.ToString()
                }
                );

            }
            return Json(parts, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra todas las lineas de credito con su respectivos destinos y ser direccionado a details
        /// </summary>
        // GET: SimuladorCre/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: SimuladorCre/Edit/5
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
    }
}
