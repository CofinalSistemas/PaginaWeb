using Ingenio.PortalWebExterno.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Ingenio.PortalWebExterno.Controllers
{
    public class PQRSController : Controller
    {
        // GET: PQRS
        //private readonly IWebHostEnvironment _webHostEnvironment;
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(Formato formato, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", formato);
            }

            string path_archivo="null";

            if (file != null && file.ContentLength > 0)
                try
                {
                    file.GetType();
                    string nombre = DateTime.Now.ToString() + formato.Cedula + ".jpg" ;

                    string path = Path.Combine(Server.MapPath("~/PQRS-Archivos"),
                                               Path.GetFileName(nombre));
                    file.SaveAs(path);
                    path_archivo = path;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }

            //if (formato.Archivo != null)
            //{
            //    string carpeta = "pqrs_archivos/enviados";

            //}


            string body =
                "<body>" +
                    "<h1>Formulario de PQRS </h1>" +
                    "<br>" +
                    "Nombre: " + formato.Nombre + "<br>" +
                    "Apellido: " + formato.Apellido + "<br>" +
                    "Correo: " + formato.Correo + "<br>" +
                    "Telefono: " + formato.Telefono + "<br>" +
                    "Fecha: " + formato.Fecha + "<br>" +
                    "Descripcion: " + formato.Descripcion + "<br>" +
                "</body>";
            


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sistemascofinal@gmail.com", "aadxnyctwwnchyef");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
                
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sistemascofinal@gmail.com", "PQRS Cofinal");
            mail.To.Add(new MailAddress("juanjo.figue97@gmail.com"));
            mail.Subject = "PQRS";
            mail.IsBodyHtml = true;
            mail.Body = body;

            if (path_archivo != "null")
            {
                Attachment attachment;
                attachment = new Attachment(path_archivo);
                mail.Attachments.Add(attachment);
            }


            smtp.Send(mail);



            TempData["Message"] = "Petición Guardada";
            return View("Index");
        }
    }
}