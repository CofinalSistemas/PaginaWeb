using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Web.Script.Serialization;
using System.Reflection.Emit;
using Ingenio.PortalWebExterno.Models;
using System.Net.Mail;
using System.Configuration;
using Ingenio.BO.Ingenio;
using Ingenio.BLL.Configuracion;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace Ingenio.PortalWebExterno.Controllers
{
    public class FundacionController : Controller
    {
        // GET: Fudancion
        ConfiguracionBLL configBLL = new ConfiguracionBLL();

        public ActionResult Index()
        {
            ICollection<Estados> NoticiasEve = configBLL.getEstadoHomeFudacion();
            ICollection<Sliders> NotiSlider = configBLL.getSlidersHomeFundacion();
            ViewBag.NotSliderFudancion = NotiSlider;
            return View("", NoticiasEve.OrderByDescending( x => x.Fecha));
        }
        public ActionResult NoticiasFundacion(string Id)
        {
            ViewBag.TitleNoti = null;
            ViewBag.fechainicio = new DateTime();
            ViewBag.fechafin = new DateTime();
            Estados estado = configBLL.getEstadoIndexFundacion(Id);
            if (estado.FechaInicio != null)
            {
                ViewBag.fechainicio = estado.FechaInicio;
            }
            if (estado.FechaFin != null)
            {
                ViewBag.fechafin = estado.FechaInicio;
            }
            ViewBag.fechapublicada = new DateTime();
            if (estado.Fecha != null)
            {
                ViewBag.fechapublicada = estado.Fecha;
            }

            ViewBag.html = System.Web.HttpUtility.HtmlDecode(estado.Html);//decodificar el html de la noticia
            return View("../Configuracion/preview", estado);
        }
        public ActionResult informacion()
        {
            return View();
        }
        public ActionResult solidaridad()
        {
            string[] files = Directory.GetFiles(Server.MapPath("~/img/Cursos"));
            ICollection<string> response = new List<string>();
            foreach (string s in files)
            {
                try
                {
                    string Nombre = Path.GetFileName(s);
                    string extencion = Path.GetExtension(Nombre);
                    if (extencion == ".JPG" || extencion == ".PNG")
                    {
                        response.Add("/img/Cursos/" + Nombre);
                    }

                }
                catch (FileNotFoundException e)
                {
                    Trace.WriteLine("Controlador Fundacion, funcion solidaridad " + e.Message.ToString(), "Error " + DateTime.Now);

                    response.Add("No hay imagenes");
                }

            }
            ViewBag.activo = "NuestrosProgramas";
            // return Json(response, JsonRequestBehavior.AllowGet);

            ViewBag.imagenesSolidaridad = response;
            return View();
        }
        public ActionResult turismo()
        {

            string[] files = Directory.GetFiles(Server.MapPath("~/img/fundacion/turismo"));
            ICollection<string> response = new List<string>();
            foreach (string s in files)
            {
                try
                {
                    string Nombre = Path.GetFileName(s);
                    string extencion = Path.GetExtension(Nombre);
                    if (extencion == ".JPG" || extencion == ".PNG"|| extencion == ".jpg" || extencion == ".png")
                    {
                        response.Add("/img/fundacion/turismo/" + Nombre);
                    }

                }
                catch (FileNotFoundException e)
                {
                    Trace.WriteLine("Controlador Fundacion, funcion turismo " + e.Message.ToString(), "Error " + DateTime.Now);

                    response.Add("No hay imagenes");
                }

            }
            ViewBag.activo = "NuestrosProgramas";
            // return Json(response, JsonRequestBehavior.AllowGet);

            ViewBag.imagenesTurismo = response;
            return View();
        }
        public ActionResult proyectos()
        {
            string[] files = Directory.GetFiles(Server.MapPath("~/img/fundacion/viviendas"));
            ICollection<string> response = new List<string>();
            foreach (string s in files)
            {
                try
                {
                    string Nombre = Path.GetFileName(s);
                    string extencion = Path.GetExtension(Nombre);
                    if (extencion == ".JPG" || extencion == ".PNG")
                    {
                        response.Add("/img/fundacion/viviendas/" + Nombre);
                    }


                }
                catch (FileNotFoundException e)
                {
                    Trace.WriteLine("Controlador Fundacion, funcion proyectos " + e.Message.ToString(), "Error " + DateTime.Now);

                    response.Add("No hay imagenes");
                }

            }
            ViewBag.activo = "NuestrosProgramas";
            // return Json(response, JsonRequestBehavior.AllowGet);

            ViewBag.imagenesProyectos = response;
            return View();

        }
        public ActionResult convenios()
        {
            return View();
        }
        public ActionResult servicios()
        {
            string[] files = Directory.GetFiles(Server.MapPath("~/img/fundacion/recreativo"));
            ICollection<string> response = new List<string>();
            foreach (string s in files)
            {
                try
                {
                    string Nombre = Path.GetFileName(s);
                    string extencion = Path.GetExtension(Nombre);
                    if (extencion == ".JPG" || extencion == ".jpg" || extencion == ".PNG" || extencion == ".png")
                    {
                        response.Add("/img/fundacion/recreativo/" + Nombre);
                    }

                }
                catch (FileNotFoundException e)
                {
                    Trace.WriteLine("Controlador Fundacion, funcion servicios " + e.Message.ToString(), "Error " + DateTime.Now);

                    response.Add("No hay imagenes");
                }

            }
            ViewBag.activo = "NuestrosProgramas";
            // return Json(response, JsonRequestBehavior.AllowGet);

            ViewBag.imagenesServi = response;
            return View();


        }
        public ActionResult asesoriasP()
        {
            return View();
        }
        public ActionResult seminarios()
        {
            return View();
        }
        public ActionResult añosFelices()
        {
            string[] files = Directory.GetFiles(Server.MapPath("~/img/fundacion/añosFelices"));
            ICollection<string> response = new List<string>();
            foreach (string s in files)
            {
                try
                {
                    string Nombre = Path.GetFileName(s);
                    string extencion = Path.GetExtension(Nombre);
                    if (extencion == ".JPG" || extencion == ".PNG" || extencion == ".jpg" || extencion == ".png")
                    {
                        response.Add("/img/fundacion/añosFelices/" + Nombre);
                    }

                }
                catch (FileNotFoundException e)
                {
                    Trace.WriteLine("Controlador Fundacion, funcion añosfelices " + e.Message.ToString(), "Error " + DateTime.Now);

                    response.Add("No hay imagenes");
                }

            }
            ViewBag.activo = "NuestrosProgramas";
            // return Json(response, JsonRequestBehavior.AllowGet);

            ViewBag.imagenesAnios = response;
            return View();

        }
        public ActionResult contactenos()
        {
            string param1 = this.Request.QueryString["msj"];
            ViewBag.Message = "Your contact page.";
            if (param1 == "1")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Gracias, por su comentario.')</script>");
            }

            return View();
        }
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
        [HttpPost]
        public ActionResult ValidateCaptcha(SubscribeModel model)
        {

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
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
            {
                ModelState.AddModelError("Captcha", "Resultado incorrecto, Intentelo nuevamente.");
                //dispay error and generate a new captcha 
                return View("contactenos", model);
            }
            else {
                ViewBag.Message = "Valid";

                SmtpClient WebMail = new SmtpClient(ConfigurationManager.AppSettings["smtp"], Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                WebMail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["pss"]);
                WebMail.EnableSsl = true;


                MailMessage mnsj = new MailMessage();
                mnsj.Subject = "Contactenos";
                mnsj.To.Add(new MailAddress(ConfigurationManager.AppSettings["emailcontactanosFundacion"]));
                mnsj.From = new MailAddress(ConfigurationManager.AppSettings["email"], "FUNDACION COFINAL");

                mnsj.Body = ( "Enviado por:" + Request["firstname"].ToString().ToUpper() + " " + Request["lastname"].ToString().ToUpper() + "\n con " + Request["tipo"] + " numero: " + Request["documento"] + "\n Número Celular o Teléfono fijo: " + Request["Telefono"] + "\n Ciudad o Municipio: " + Request["ciudad"] + "\n El Correo Electrónico es: " + Request["email"] + "\n El Tipo de Información es: " + Request["tipo_informacion"] + "\n El mensaje enviado es: " + Request["message"] );
                /* Enviar */

                WebMail.Send(mnsj);

                //}
                if (ViewBag.Message == "Valid")
                {

                    return RedirectToAction("contactenos", "Fundacion", new { msj = "1" });

                }

                return View();
            }
        }
    }
}