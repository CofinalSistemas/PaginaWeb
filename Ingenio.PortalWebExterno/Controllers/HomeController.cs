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
using Ingenio.Filters;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;


namespace Ingenio.PortalWebExterno.Controllers
{
    public class HomeController : Controller
    {
        ConfiguracionBLL configBLL;

        public HomeController()
        {
            configBLL = new ConfiguracionBLL();
        }
        [OutputCache(Duration =300,Location =System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            ICollection<Sliders> NotiSlider = new List<Sliders>();
            ICollection<Estados> NoticiasEve = new List<Estados>();
            #region MODAL
            var t3 = Task.Run( () => {
                Trace.WriteLine(Server.MapPath("/Documentos/modal.json"));
                return Controllers.Configuracion.ConfiguracionController.GetModalConfiguration( $"{Server.MapPath( "/Documentos/modal.json" )}" );
               
                }
            );

            try
            {
                NoticiasEve = configBLL.getEstadoHome();
                NotiSlider = configBLL.getSlidersHome();
            }
            catch (Exception e)
            {
                Trace.WriteLine( $"home/index - {e.Message }" );
                //throw;
            }


            Task.WaitAll( t3 );
            var modalConfiguration = t3.Result; //TODO refactor this in view
            ViewBag.ShowAgain = modalConfiguration.ShowAgainAfterClosed;
            ViewBag.Active = modalConfiguration.IsActive;
            ViewBag.Message = modalConfiguration.Message;
            ViewBag.ModalConf = modalConfiguration;

            #endregion

            ViewBag.NotSlider = NotiSlider;
            return View( "", NoticiasEve.OrderByDescending( x => x.Fecha ) );
        }


        public ActionResult NoticiasCofinal(string Id)
        {
            ViewBag.TitleNoti = null;
            ViewBag.fechainicio = new DateTime();
            ViewBag.fechafin = new DateTime();
            Estados estado = configBLL.getEstadoIndexCofinal( Id );
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

            ViewBag.html = System.Web.HttpUtility.HtmlDecode( estado.Html );//decodificar el html de la noticia
            return View( "../Configuracion/preview", estado );
        }

        [Allow( action = "AsociadosModulo" )]
        public ActionResult HomeAso()
        {
            return View();
        }

        public ActionResult HomeAdmin()
        {
            return View();
        }
        public ActionResult Ahorros()
        {
            return View();
        }
        public ActionResult Alivios2020()
        {
            return View();
        }
        public ActionResult Creditos()
        {
            return View();
        }

        public ActionResult Seguros()
        {
            return View();
        }
        public ActionResult QuienesSomos()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult NuestrasAgencias()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult ConceptosDeinteres()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Fundacion()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Directorio()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult RteDian()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Multiportal()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Productos()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult ProductosCreditos()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult AlianzasC()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult ConveniosC()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Alianzas()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult PoliticasTIC()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // Crear Pagina Nueva (Prueba)
        public ActionResult CanalesPago()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Simulador()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ActualizacionDatos()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult ActualizacionDatos(user model)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:\PaginaWeb_NUEVA\cofi_web-master\Ingenio.PortalWebExterno\Pruebas_SitioWeb.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            xlWorksheet= xlWorkbook.ActiveSheet;
            
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;


            string[,] miArray = new string[rowCount-1, 2];
            
            for (int i = 2; i <= rowCount; i++)
            {
                    string Nombre = xlRange.Cells[i, 1].Value2.ToString();
                    string Cedula = xlRange.Cells[i, 2].Value.ToString();
                    miArray[i-2, 0] = Nombre;
                    miArray[i-2, 1] = Cedula;
            }
            bool isBase = false;

            for (int i = 0; i <= rowCount-2; i++)
            {
                if (miArray[i, 0] == model.Cedula)
                {
                    isBase = true;
                    if (miArray[i, 1] == model.fechaExpedicion.ToString())
                    {
                        xlWorksheet.Cells[i+2, 3] = model.Nombre;
                        xlWorksheet.Cells[i+2, 4] = model.Apellido;
                        xlWorksheet.Cells[i+2, 5] = model.Direccion;
                        xlWorksheet.Cells[i+2, 6] = model.Telefono;
                        xlApp.DisplayAlerts = false;
                        xlWorkbook.Save();
                        TempData["SuccessMessage"] = "Tu informacion a sido Actualizada";
                        break;
                    }
                    else
                    {
                        ModelState.AddModelError("fechaExpedicion", "Su Fecha de expedicion no coincide con su Cedula");
                    }
                }
            }

            if (!isBase)
            {
                ModelState.AddModelError("Cedula", "Su Cedula no esta en la Base de datos");
            }

            // Salir de Excel y Eliminar Todos los procesos del mismo
            xlWorkbook.Close(true, Type.Missing, Type.Missing);
            xlApp.Quit();
            Process[] pro = Process.GetProcessesByName("excel");
            pro[0].Kill();
            pro[0].WaitForExit();


            return View();
        }


        //[HttpPost]
        //public ActionResult Form4(StudentModel sm)
        //{
        //    string value = "ID: " + Convert.ToString(sm.Id)
        //        + "<br />Name: " + sm.Name
        //        + "<br />Addon: " + Convert.ToString(sm.Addon);

        //    string s = "$('#output').html('" + value + "');";
        //    return JavaScript(s);
        //}




        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Muestra el formulario de peticiones, quejas y reclamos , y se envia por email hacia cofinal
        /// </summary>
        public ActionResult Contact()
        {
            string param1 = this.Request.QueryString["msj"];
            ViewBag.Message = "Your contact page.";
            if (param1 == "1")
            {
                Response.Write( "<script LANGUAGE='JavaScript' >alert('Gracias, por su comentario.')</script>" );
            }

            return View();
        }
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random( ( int )DateTime.Now.Ticks );
            //generate new question 
            int a = rand.Next( 10, 99 );
            int b = rand.Next( 0, 9 );
            var captcha = string.Format( "{0} + {1} = ?", a, b );

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap( 130, 30 ))
            using (var gfx = Graphics.FromImage( ( Image )bmp ))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle( Brushes.White, new Rectangle( 0, 0, bmp.Width, bmp.Height ) );

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen( Color.Yellow );
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        ( rand.Next( 0, 255 ) ),
                        ( rand.Next( 0, 255 ) ),
                        ( rand.Next( 0, 255 ) ) );

                        r = rand.Next( 0, ( 130 / 3 ) );
                        x = rand.Next( 0, 130 );
                        y = rand.Next( 0, 30 );

                        gfx.DrawEllipse( pen, x - r, y - r, r, r );
                    }
                }

                //add question 
                gfx.DrawString( captcha, new Font( "Tahoma", 15 ), Brushes.Gray, 2, 3 );

                //render as Jpeg 
                bmp.Save( mem, System.Drawing.Imaging.ImageFormat.Jpeg );
                img = this.File( mem.GetBuffer(), "image/Jpeg" );
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
            //{}
            //validate captcha 
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
            {
                ModelState.AddModelError( "Captcha", "Resultado incorrecto, Intentelo nuevamente." );
                //dispay error and generate a new captcha 
                return View( "Contact", model );
            }
            else
            {


                ViewBag.Message = "valid";

                SmtpClient WebMail = new SmtpClient( ConfigurationManager.AppSettings["smtp"], Convert.ToInt32( ConfigurationManager.AppSettings["port"] ) );
                WebMail.Credentials = new System.Net.NetworkCredential( ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["pss"] );
                WebMail.EnableSsl = true;


                MailMessage mnsj = new MailMessage();
                mnsj.Subject = Request["asunto"];
                mnsj.To.Add( new MailAddress( ConfigurationManager.AppSettings["emailcontactanos"] ) );
                mnsj.From = new MailAddress( ConfigurationManager.AppSettings["email"], "COFINAL LTDA" );

                mnsj.Body = ( "Enviado por:" + Request["firstname"].ToString().ToUpper() + " " + Request["lastname"].ToString().ToUpper() + "\n con " + Request["tipo"] + " numero: " + Request["documento"] + "\n Número Celular o Teléfono fijo: " + Request["Telefono"] + "\n Ciudad o Municipio: " + Request["ciudad"] + "\n El Correo Electrónico es: " + Request["email"] + "\n El Tipo de Información es: " + Request["tipo_informacion"] + "\n El mensaje enviado es: " + Request["message"] );
                /* Enviar */

                WebMail.Send( mnsj );

                //}
                if (ViewBag.Message == "Valid")
                {

                    return RedirectToAction( "Contact", "Home", new { msj = "1" } );

                }

                return View();
            }
        }
    }
}