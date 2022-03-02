using Ingenio.BLL;
using Ingenio.BO;
using Ingenio.BO.Ingenio;
using Ingenio.DLL;
using Ingenio.Filters;
using Ingenio.Models;
using Ingenio.PortalWebExterno.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Ingenio.PortalWebExterno.Controllers
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// Simulador de ahorro CDAT    
    /// </summary>
    public class SimuladorCDATController : Controller
    {
        ExtractosBLL extBll = new ExtractosBLL();
        AsociadoBLL asocBll = new AsociadoBLL();
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra un formulario para ingresar los datos iniciales para generar consulta
        /// </summary>
        // GET: SimuladorCDAT
        [Allow(action = "REPORTE_SIMULADOR_AHORRO")]
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
        [Allow(action = "REPORTE_SIMULADOR_AHORRO")]
        public JsonResult Reporte(DateTime inicial, DateTime final)
        {
            //OBTIENE DATOS DE SIMULACIONES REALIZADAS
            dynamic query = extBll.GetDatosCDATyCre(inicial, final=Convert.ToDateTime( final.ToString().Substring(0,10) + " 23:59:00.000"), "2");
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra el formulario para ingresar los datos y calcular la simulacion
        /// </summary>
        // GET: SimuladorCDAT/Details/5
        public ActionResult Details()
        {
            AccountModels cuentaUsuario = new AccountModels();
            int txtcedula = 0,bandera=0;
            ViewBag.boton = null;
            ViewBag.estilo = @"style=""display:none; width: 124px;""";
            if (!cuentaUsuario.IsNull() && cuentaUsuario.IsAsociado )
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
            //RECIBO LOS PARAMETROS DESDE FORM
            string param1 = Request["msj"];
            if (bandera == 0)
            {
                
                ViewBag.capital = Request["capi"];
                ViewBag.Plazo = Request["pla"];
                //ViewBag.Periodo = peri;
                //liqui = liquida
                ViewBag.nombreintegrado = Request["nomb"];
                ViewBag.Email = Request["txtema"];
                ViewBag.Telefono = Request["tele"];
            }
            if (param1 == "1")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('El Monto no corresponde con el plazo seleccionado, ver Tasas')</script>");
            }
            if (param1 == "2")
            {
                ViewBag.boton = 2;
            }
            return View();
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// obtiene los plazos
        /// </summary>
        public JsonResult GetPlazo()
        {
            //OBTIENE LOS PLAZOS
            dynamic query = extBll.GetTasas();

            return Json(query, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// obtiene los periodos
        /// </summary>
        public JsonResult GetPlazo2(string plazo)
        {
            //OBTIENE LOS PERIODOS
            dynamic query = extBll.GetTasas2(Convert.ToInt32(plazo));

            return Json(query, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// obtiene tasas por cada plazo
        /// </summary>
        public JsonResult GetPlazo3(string plazo)
        {
            //OBTIENE LAS TASAS PARA CADA PLAZO
            dynamic query = extBll.GetTasas4(Convert.ToInt32(plazo));

            return Json(query, JsonRequestBehavior.AllowGet);

        }
        public JsonResult validaciones(string txtCapital, string Plazo)
        {
            int aplica = 0;
            dynamic query = extBll.GetTasas3(Convert.ToInt32(txtCapital.Substring(0, txtCapital.IndexOf(",")).Replace(".", "")), Convert.ToInt32(Plazo));
            foreach (var item in query)
            {
                aplica = item.aplica;
                if (aplica == 1)
                {
                    aplica = 1;
                    //Periodo = Convert.ToString(item.tasacolocacion);
                }
            }
            if (aplica == 0)
            {
                //TempData["vv"] = "1";
                //return Json(false, JsonRequestBehavior.AllowGet);
                return Json(new { estado = false }, JsonRequestBehavior.AllowGet);
            }
            else {
                //return Json(true, JsonRequestBehavior.AllowGet);
                return Json(new { estado = true }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Recibe los datos y procede a consultar las tasas en BD. y realizar la simulacion segun parametros
        /// </summary>
        /// <param name="txtCapital">argumento tipo cadena es enviado desde details formulario</param>
        /// <param name="Plazo">argumento tipo cadena es enviado desde details formulario</param>
        ///  <param name="Periodo">argumento tipo cadena es enviado desde details formulario</param>
        ///  <param name="Tarifa">argumento tipo cadena es enviado desde details formulario</param>
        ///  <param name="liquida">argumento tipo cadena es enviado desde details formulario</param>
        ///   <param name="txtNombre">argumento tipo cadena es enviado desde details formulario</param>
        ///  <param name="txtEmail">argumento tipo cadena es enviado desde details formulario</param>
        ///  <param name="Telefono">argumento tipo cadena es enviado desde details formulario</param>
        ///     <param name="nuevo_periodo">argumento entero opcional cadena es enviado details index formulario</param>
        ///  <param name="nuevo_Plazo">argumento tipo entero opcional es enviado desde details formulario</param>
        public ActionResult Calcular(string txtCapital, string Plazo, string Periodo,string Tarifa,string liquida,string txtNombre,string txtEmail,string Telefono, int nuevo_periodo=0, int nuevo_Plazo=0)
        {
            DateTime fechainicial = DateTime.Today;

            try
            {
                if (nuevo_Plazo != 0)
                {
                    liquida = nuevo_periodo.ToString();
                    Plazo = nuevo_Plazo.ToString();
                }

                int aplica = 0;
                dynamic query = extBll.GetTasas3(Convert.ToInt32(txtCapital.Substring(0, txtCapital.IndexOf(",")).Replace(".", "")), Convert.ToInt32(Plazo));
                foreach (var item in query)
                {
                    aplica = item.aplica;
                    if (aplica == 1)
                    {
                        aplica = 1;
                        Periodo = Convert.ToString(item.tasacolocacion);
                    }
                }
                if (aplica == 0)
                {
                    TempData["vv"] = "1";
                    return RedirectToAction("Details", "SimuladorCDAT", new { msj = "1", capi=txtCapital,pla=Plazo, peri=Periodo,liqui= liquida,nomb= txtNombre, txtema=txtEmail, tele=Telefono });
                    //return Json(new { estado = false, msj = "El Monto no corresponde con el plazo seleccionado"}, JsonRequestBehavior.AllowGet);
                }
                //////CALCULOS NUMERICOS/////

                decimal tasa = Convert.ToDecimal(Periodo);
                double expo = (Convert.ToDouble(liquida) / (double)360);

                double tasa_nominal = ((Math.Pow(1 + ((double)tasa / 100), expo)) - 1) * 100;
                tasa = (decimal)tasa_nominal;
                Double x = Convert.ToInt32(Plazo) / Convert.ToDouble(liquida);
                Decimal ts = (Math.Ceiling((decimal)tasa * (decimal)Math.Pow(10, 6)) / (decimal)Math.Pow(10, 6));


                Double saldoinicial = Convert.ToInt32(txtCapital.Substring(0, txtCapital.IndexOf(",")).Replace(".", ""));

                Double interesganado = saldoinicial * (Convert.ToDouble(ts) / (double)100);
                Double retefuente = interesganado * (Convert.ToDouble(Tarifa) / (double)100);
                double intereses = 0, rtf = 0, pagado = 0;
                ICollection<dynamic> simulamov = new List<dynamic>();
                for (int i = 1; i <= x; i++)
                {
                    dynamic b = new ExpandoObject();
                    b.capital = saldoinicial;
                    b.interesganado = interesganado;
                    intereses += interesganado;
                    b.retefuente = retefuente;
                    rtf += retefuente;
                    b.interespagado = interesganado - retefuente;
                    pagado += interesganado - retefuente;
                    simulamov.Add(b);

                }

                ViewBag.intereses = intereses;
                ViewBag.rtf = rtf;
                ViewBag.pagado = pagado;

                ViewBag.Simulador = simulamov;
                ViewBag.Tasa = (ts);
                ViewBag.capital = saldoinicial;
                ViewBag.capital1 = saldoinicial;
                ViewBag.capital = ViewBag.capital + "00";
                ViewBag.tasaefectiva = 7;

                ViewBag.plazo = Plazo;
                ViewBag.nombreintegrado = txtNombre;
                ViewBag.Email = txtEmail;
                ViewBag.Telefono = Telefono;
                ViewBag.periodo = liquida;
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
                //////ENVIO EMAIL /////////////
                SmtpClient WebMail = new SmtpClient(ConfigurationManager.AppSettings["smtp"], Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                WebMail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["pss"]);
                WebMail.EnableSsl = true;


                MailMessage mnsj = new MailMessage();
                mnsj.Subject = "Usuario realizo simulacion CDAT";
                mnsj.To.Add(new MailAddress(ConfigurationManager.AppSettings["emailmercadeo"]));
                mnsj.From = new MailAddress(ConfigurationManager.AppSettings["email"], "Gestion Comercial - Simulacion CDAT COFINAL");

                mnsj.Body = ("El usuario " + txtNombre.ToUpper() + " con Email " + txtEmail + " y telefono " + Telefono + " ,\n realizo una simulacion de CDAT con Plazo: " + Plazo + " y periodo de liquidacion:" + liquida + ", Monto: " + txtCapital);
                /* Enviar */

                WebMail.Send(mnsj);
                //////ENVIO DATOS A BD /////////////
                SEGUIMIENTO_SIMULADORES dat = new SEGUIMIENTO_SIMULADORES();
                dat.FECHA = DateTime.Now;
                dat.NOMBRE = txtNombre;
                dat.TELEFONO = Telefono.ToString();
                dat.EMAIL = txtEmail;
                dat.TIPO = "2";
                extBll.GetSeguimiento_email(dat);

                //}

                //}
                //catch (Exception e)
                //{
                //    Trace.WriteLine("Controlador SimuladorCDAT, funcion Calcular ln.205 " + e.Message.ToString(), "Error " + DateTime.Now);

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
                //return Json(new { estado = true, msj = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception a)
            {
                Trace.WriteLine("Controlador SimuladorCDAT, funcion Calcular " + a.Message.ToString(), "Error " + DateTime.Now);

                return View("Details"); 
                //return Json(new { estado = false,msj="" } , JsonRequestBehavior.AllowGet);
            }
        }
        // GET: SimuladorCDAT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimuladorCDAT/Create
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

        // GET: SimuladorCDAT/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SimuladorCDAT/Edit/5
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

        // GET: SimuladorCDAT/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SimuladorCDAT/Delete/5
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
