using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO;
using Ingenio.BLL;
using Ingenio.BLL.Seguridad;
using Newtonsoft.Json;
using Ingenio.Filters;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Ingenio.BO.Ingenio;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using Ingenio.Models;
using System.Net;
using System.Diagnostics;
using Ingenio.PortalWebExterno.Models;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace Ingenio.Controllers.Seguridad
{

    public class AccountController : Controller
    {
        UsuarioBll usuariobll = new UsuarioBll();
       // RolesBll rolesbll = new RolesBll();
        AccountBll cuenta = new AccountBll();
        AsociadoBLL asociadobll = new AsociadoBLL();

        public static Exception loginException { get; set; }

        public Exception GetInnerException(Exception e)
        {
            if (e.InnerException != null)
            {
                GetInnerException(e.InnerException);
            }
            return e;
        }
        public JsonResult VerificaError()
        {
            Exception e = GetInnerException(loginException);
            return Json(new
            {
                e.Message,
                e.Source,
                e.Data
            }, JsonRequestBehavior.AllowGet);
        }
        //cargar login
        public ActionResult Login()
        {
            if (new AccountModels().IsNull() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ban = false;
            ViewBag.show = false;

            return View("Login");

        }

        public JsonResult Cifrar(string cadena)
        {
            Cifrado cifrado = new Cifrado();
            DateTime inicio = DateTime.Now;
        
            string serial = cifrado.EncodeSHA1(inicio.ToString() + cadena); 
            serial = cifrado.EncodeMD5(serial);

            serial = serial.Substring(0, 8) + "-" + serial.Substring(7, 8) + "-" + serial.Substring(15, 8) + "-" + serial.Substring(23, 8);

            return Json(new
            {
                Empresa = cadena,
                fechaInicio = inicio.ToString(),              
                serial = serial
            }, JsonRequestBehavior.AllowGet);
        }

        //Ingreso de usuarios 
        [HttpPost]
        public ActionResult Login(string usuario, string password, string urlReturn, FormCollection col, SubscribeModel model)
        {
            
            Cifrado cifrado = new Cifrado();

            usuario = usuario.Trim().ToUpper();
            password = cifrado.EncodeSHA1(password);
            DateTime fechaAcceso = DateTime.Now;

            string str = "";
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(str);
            IPAddress[] addr = ipEntry.AddressList;
            string IP = addr[addr.Length - 1].MapToIPv4().ToString();

            try
            {

                //validate captcha 
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
                {
                    ModelState.AddModelError("Captcha", "Resultado incorrecto, Intentelo nuevamente.");
                    //dispay error and generate a new captcha 
                    return View(model);
                }

                dynamic res = cuenta.Login(usuario, password, fechaAcceso, IP);             
                
                if (res != null)
                {
                    int UserNumero;
                    bool num = int.TryParse(usuario, out UserNumero);
                    if (num)   // creacion de sesiones para asociados                   
                    {                        
                        Session["modulosAso"] = new Modulos
                        {
                            Nombre = "AsociadosModulo"
                        };
                        Session["asociadoInfo"] = res;
                        Session["isAsociado"] = true;
                        AccountModels asocidos = new AccountModels();
                        return Redirect("/Home/homeaso");
                    }
                    else
                    {//crear sesion para usuarios
                        ICollection<string> modulos = usuariobll.GetNombreModulos(res.Id);
                       
                        Session["userInfo"] = res;
                        if (modulos.Count==0)
                        {
                            Session.Clear();                          
                            return RedirectToAction("login", "account");
                        }
                        return Redirect("/Home/homeAdmin");                     

                    }

                }
                else
                {
                    ViewBag.ban = false;
                    ViewBag.show = true;
                    return View();
                }
                if (Request.Cookies["urlReturn"] != null)
                {
                    return Redirect(Request.Cookies["urlReturn"].Value);
                }
                return Redirect("/Home/Index");
            }
                catch (Exception e)
            {
                Trace.WriteLine("Controlador Account, funcion Login  " + e.Message.ToString(), "Error " + DateTime.Now);
                return Redirect("/Home/Index");
            }

            
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
       
        //Registro de asociados en la tabla de ingenio
        [HttpPost]
        public ActionResult Registro(Asociados_Ingenio asoc, SubscribeModel model)
        {
            try
            {
                if ( asoc.tipo == 2 ) asoc.PrimerApellido = " ";
                
               
                int identificacion = Convert.ToInt32(asoc.Identificacion);
                string correo = asoc.Correo.Trim();
                asociados asocofinal = asociadobll.GetAsociadoRegistro(identificacion, correo);
               

                if (asocofinal != null)
                {
                    asoc.FechaCreacion = DateTime.Now;
                    string KeyUrlConfirmacion = Convert.ToString(asoc.FechaCreacion) + asoc.Identificacion;
                    //asoc.Password = "106";
                    //convercion MD5
                    MD5 md5 = MD5CryptoServiceProvider.Create();
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] stream = null;
                    StringBuilder sb = new StringBuilder();
                    stream = md5.ComputeHash(encoding.GetBytes(KeyUrlConfirmacion));
                    for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                    string pasencript = sb.ToString();
                    Cifrado cifrado = new Cifrado();
                    string password = cifrado.EncodeSHA1(pasencript);

                    //guardar usuario y verificar si esta registrado               
                    asoc.Password = password;
                    bool u = usuariobll.Registro(asoc);

                    //enviar correo
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/EmailRegistroAsociado.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    string NomApeasociado = asocofinal.nits.nombreintegrado;
                    string ContraseñaRegistro = ConfigurationManager.AppSettings["httpurl"] + "/Account/passwordRegistro/" + password;

                    body = body.Replace("{NomApeasociado}", NomApeasociado);
                    body = body.Replace("{ContraseñaRegistro}", ContraseñaRegistro);
                 
                    SmtpClient WebMail = new SmtpClient(ConfigurationManager.AppSettings["smtp"], Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                    WebMail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["pss"]);
                    WebMail.EnableSsl = true;
                    string emailAsociadoEnvio = asocofinal.nits.email.Trim();
                    MailMessage mnsj = new MailMessage();
                    mnsj.Subject = "Confirmar registro COFINAL.";
                    mnsj.To.Add(emailAsociadoEnvio);
                    mnsj.From = new MailAddress(ConfigurationManager.AppSettings["email"], "COFINAL LTDA");

                    mnsj.Body = body;
                    mnsj.IsBodyHtml = true;
                    /* Enviar */

                    WebMail.Send(mnsj);

                }
                else{
                    nits nitscofinal = asociadobll.GetNitsoRegistro(identificacion, correo);
                    if (nitscofinal != null)
                    {
                        asoc.FechaCreacion = DateTime.Now;
                        string KeyUrlConfirmacion = Convert.ToString(asoc.FechaCreacion) + asoc.Identificacion;
                        //asoc.Password = "106";
                        //convercion MD5
                        MD5 md5 = MD5CryptoServiceProvider.Create();
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        byte[] stream = null;
                        StringBuilder sb = new StringBuilder();
                        stream = md5.ComputeHash(encoding.GetBytes(KeyUrlConfirmacion));
                        for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                        string pasencript = sb.ToString();
                        Cifrado cifrado = new Cifrado();
                        string password = cifrado.EncodeSHA1(pasencript);

                        //guardar usuario y verificar si esta registrado               
                        asoc.Password = password;
                        bool u = usuariobll.Registro(asoc);

                        //enviar correo
                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/EmailRegistroAsociado.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        string NomApeasociado = nitscofinal.nombreintegrado;
                        string ContraseñaRegistro = ConfigurationManager.AppSettings["httpurl"] + "/Account/passwordRegistro/" + password;

                        body = body.Replace("{NomApeasociado}", NomApeasociado);
                        body = body.Replace("{ContraseñaRegistro}", ContraseñaRegistro);

                        SmtpClient WebMail = new SmtpClient(ConfigurationManager.AppSettings["smtp"], Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                        WebMail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["pss"]);
                        WebMail.EnableSsl = true;
                        string emailAsociadoEnvio = nitscofinal.email.Trim();
                        MailMessage mnsj = new MailMessage();
                        mnsj.Subject = "Confirmar registro COFINAL.";
                        mnsj.To.Add(emailAsociadoEnvio);
                        mnsj.From = new MailAddress(ConfigurationManager.AppSettings["email"], "COFINAL LTDA");

                        mnsj.Body = body;
                        mnsj.IsBodyHtml = true;
                        /* Enviar */

                        WebMail.Send(mnsj);

                    }
                }
                return Json(new
                {
                    estado = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Excepciones e)
            {
                Trace.WriteLine("Controlador Account, funcion registro  " + e.Message.ToString(), "Error1 " + DateTime.Now);
                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion,
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Trace.WriteLine("Controlador Account, funcion registro  " + e.Message.ToString(), "Error2 " + DateTime.Now);
                return Json(new
                {
                    estado = false,
                    mensaje = "Algo salio mal.",
                }, JsonRequestBehavior.AllowGet);

            }
        }

        
        public ActionResult passwordRegistro(string id)
        {
            bool pass = usuariobll.paswordRegistro(id);
            ViewBag.linkValido = pass;
            return View();
        }

        [HttpPost]
        public ActionResult passwordRegistro(string id, string password)
        {
            try
            {                          
                Cifrado cifrado = new Cifrado();
                password = cifrado.EncodeSHA1(password);
                bool pass = usuariobll.paswordRegistro(id, password);

                return Json(new { estado = true, }, JsonRequestBehavior.AllowGet);
            }
            catch (Excepciones e)
            {
                Trace.WriteLine("Controlador Account, funcion passwordregistro  " + e.Message.ToString(), "Error " + DateTime.Now);
                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Controlador Account, funcion passwordregistro  " + e.Message.ToString(), "Error2 " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = "Algo salio mal.",
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult RestaurarPass()
        {
            return View();
        }

        //Restaurar contraseña
        [HttpPost]
        public ActionResult RestaurarPass(int identificacion,SubscribeModel model)
        {
            try
            {
                //validate captcha 
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
                {
                    ModelState.AddModelError("Captcha", "Resultado incorrecto, Intentelo nuevamente.");
                    //dispay error and generate a new captcha 
                    return View(model);
                }

                Asociados_Ingenio asoc = usuariobll.RestaurarAso(identificacion);

                DateTime FechaCambioPass = DateTime.Now;
                string KeyUrlConfirmacion = Convert.ToString(FechaCambioPass) + identificacion;

                //enviar correo
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/EmailResetUsuarios.html")))
                {
                    body = reader.ReadToEnd();
                }
                string NomApeasociado = asoc.PrimerNombre + asoc.PrimerApellido;
                string ContraseñaReset = ConfigurationManager.AppSettings["httpurl"]+"/Account/passwordRegistro/" + asoc.Password;

                body = body.Replace("{NomApeasociado}", NomApeasociado);
                body = body.Replace("{ContraseñaReset}", ContraseñaReset);            
                SmtpClient WebMail = new SmtpClient(ConfigurationManager.AppSettings["smtp"], Convert.ToInt32(ConfigurationManager.AppSettings["port"]));
                WebMail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["pss"]);
                WebMail.EnableSsl = true;
                string emailAsociadoEnvio = asoc.Correo;
                MailMessage mnsj = new MailMessage();
                mnsj.Subject = "Restablecer contraseña COFINAL.";
                mnsj.To.Add(emailAsociadoEnvio);
                mnsj.From = new MailAddress(ConfigurationManager.AppSettings["email"], "COFINAL LTDA");

                mnsj.Body = body;
                mnsj.IsBodyHtml = true;
                /* Enviar */

                WebMail.Send(mnsj);

                return Json(new { estado = true, }, JsonRequestBehavior.AllowGet);
            }
            catch (Excepciones e)
            {
                Trace.WriteLine("Controlador Account, funcion restaurarpass  " + e.Message.ToString(), "Error " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Controlador Account, funcion restaurarpass  " + e.Message.ToString(), "Error2 " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = "Algo salio mal.",
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        //cerrar sesion
        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cookies["user"].Expires = DateTime.Now.AddDays(-365);
            return RedirectToAction("Index", "Home");
        }

    }

}