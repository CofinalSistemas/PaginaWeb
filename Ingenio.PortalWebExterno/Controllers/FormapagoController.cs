using Ingenio.BLL;
using Ingenio.BLL.Seguridad;
using Ingenio.BO;
using Ingenio.Filters;
using Ingenio.Models;
using Ingenio.PortalWebExterno.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Ingenio.PortalWebExterno.Controllers
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// Formulario para diligenciar campos y enviar consignacion realizada a tesoreria
    /// </summary>
    [Allow(action = "AsociadosModulo")]
    public class FormapagoController : Controller
    {
        AsociadoBLL asocBll = new AsociadoBLL();
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra formulario
        /// </summary>
        // GET: Formapago
        public ActionResult Index()
        {
            AccountModels cuentaUsuario = new AccountModels();
            int txtcedula = 0;
            //ViewBag.Aso.nits.nombreingtegrado = "";
            if (!cuentaUsuario.IsNull())
            {
                txtcedula = cuentaUsuario.Identificacion;
                asociados aso = asocBll.GetAsociado(txtcedula);
                if (aso != null)
                {
                    ViewBag.nombreintegrado = aso.nits.nombres;
                    ViewBag.apellido = aso.nits.primerapellido;
                    ViewBag.cedula = aso.nits.nit;
                    ViewBag.Telefono = aso.nits.telefono1;
                }
                else
                {
                    nits nitaso = asocBll.GetNitsAsociado(txtcedula);
                    ViewBag.nombreintegrado = nitaso.nombres;
                    ViewBag.apellido = nitaso.primerapellido;
                    ViewBag.cedula = nitaso.nit;
                    ViewBag.Telefono = nitaso.telefono1;
                }
            }

            string param1 = this.Request.QueryString["msj"];
            if (param1 == "1")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Gracias, Consignacion Enviada.')</script>");
            }
            return View();
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// valida el re captcha , recibe el archivo y envia email a tesoreria el  adjunto de consignacion
        /// </summary>
        ///<param name="archivo">argumento tipo FILE es enviado desde index formulario</param>
        [HttpPost]
        public ActionResult ValidateCaptcha(HttpPostedFileBase archivo)
        {
            var path = "";
            if (archivo != null && archivo.ContentLength > 0)
            {
                var nombreArchivo = Path.GetFileName(archivo.FileName);
                path = Path.Combine(Server.MapPath("~/consignaciones"), nombreArchivo);
                archivo.SaveAs(path);
            }

            //var response = Request["g-recaptcha-response"];
            ////secret that was generated in key value pair
            //const string secret = "6LfNcBMTAAAAAINMYzYeHJSyUqnmA0wrkp4rFnoq";

            //var client = new WebClient();
            //var reply =
            //    client.DownloadString(
            //        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            //var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            ////when response is false check for the error message
            //if (!captchaResponse.Success)
            //{
            //    if (captchaResponse.ErrorCodes.Count <= 0) return View();

            //    var error = captchaResponse.ErrorCodes[0].ToLower();
            //    switch (error)
            //    {
            //        case ("missing-input-secret"):
            //            ViewBag.Message = "The secret parameter is missing.";
            //            break;
            //        case ("invalid-input-secret"):
            //            ViewBag.Message = "The secret parameter is invalid or malformed.";
            //            break;

            //        case ("missing-input-response"):
            //            ViewBag.Message = "The response parameter is missing.";
            //            break;
            //        case ("invalid-input-response"):
            //            ViewBag.Message = "The response parameter is invalid or malformed.";
            //            break;

            //        default:
            //            ViewBag.Message = "Error occured. Please try again";
            //            break;
            //    }
            //}
            //else
            //{
                ViewBag.Message = "Valid";

                SmtpClient WebMail = new SmtpClient(ConfigurationManager.AppSettings["smtp"],Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                WebMail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["pss"]);
                WebMail.EnableSsl = true;


                MailMessage mnsj = new MailMessage();
                mnsj.Subject = "Urgente consignacion cliente";
                mnsj.To.Add(new MailAddress(ConfigurationManager.AppSettings["emailtesoreria"]));
                mnsj.From = new MailAddress(ConfigurationManager.AppSettings["email"], "CONSIGNACIONES COFINAL");

                mnsj.Body = HttpUtility.HtmlEncode("Enviado por " + Request["firstname"] + " " + Request["lastname"] + " con cedula " + Request["Cedula"] +", " + Request["message"]);
                /* Enviar */
                mnsj.Attachments.Add(new Attachment(path));
                WebMail.Send(mnsj);
            
            //}
            if (ViewBag.Message == "Valid")
            {
                
                return RedirectToAction("Index", "Formapago", new { msj = "1" });

            }

            return View();

        }
    }
}
