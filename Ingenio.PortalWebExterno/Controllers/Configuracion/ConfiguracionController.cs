using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO.Ingenio;
using Ingenio.BLL.Configuracion;
using System.IO;
using System.Dynamic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ingenio.Filters;
using Ingenio.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections;
using System.Web.Caching;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Hosting;

namespace Ingenio.PortalWebExterno.Controllers.Configuracion
{
    public  class ModalConfigurationModel
    {
        [DisplayName("Activo?")]
        public bool IsActive { get; set; }
        [DisplayName("Mostrar despues de Cerrado?")]
        public bool ShowAgainAfterClosed { get; set; }
        public Uri ImageUrl { get; set; }
        [DisplayName("Mensaje")]
        public string Message { get; set; }
    }

    public  class ConfiguracionController : Controller
    {
        AccountModels cuentaUsuario = new AccountModels();
        ConfiguracionBLL configBLL = new ConfiguracionBLL();

        #region MODAL DNTO
        /**/
        [Allow(action = "CONFIGURACION_NOTICIAS")]
        public  ActionResult Modal()
        {
            //var path = Server.MapPath(@"/Documentos/modal.json");
            var path =  @"/Documentos/modal.json" ;
            var model = GetModalConfiguration(path);
            return View(model);
        }
        [HttpPost]
        [Allow(action = "CONFIGURACION_NOTICIAS")]
        public ActionResult Modal( HttpPostedFileBase Imagen )
        {
            //elimino
            var file = Path.Combine(HttpContext.Server.MapPath("~/Imagenes"),"modal.jpg");
            if ( System.IO.File.Exists(file) )
                System.IO.File.Delete(file);
            //creo
            try
            {
                var path = "";
                if ( Imagen != null && Imagen.ContentLength > 0 )
                {
                    var nombreArchivo = Path.GetFileName(Imagen.FileName);
                    path = Path.Combine(Server.MapPath("~/Imagenes"),"modal.jpg");
                    Imagen.SaveAs(path);
                }
            }
            catch ( Exception a )
            {
                Trace.WriteLine("MODAL" + a.Message.ToString(),"Error " + DateTime.Now);
            }
            return View("Modal");
        }

       // [OutputCache(Duration = 500)]
        public ActionResult ImagenPrevia()
        {
            var path = Server.MapPath("~/Imagenes");
            var file = string.Format("modal.jpg");
            var fullPath = Path.Combine(path,file);
            return File(fullPath,"image/jpg",file);
        }

        [HttpPost]
        [Allow(action = "CONFIGURACION_NOTICIAS")]
        public JsonResult Configure( ModalConfigurationModel model,FormCollection col )
        {
            var path = Server.MapPath("~/Documentos/modal.json");
            //open o create json file
            try
            {
                if ( new FileInfo(path).Exists )
                {
                    System.IO.File.WriteAllText(path,JsonConvert.SerializeObject(model));
                }
                else
                {
                    using ( StreamWriter file = System.IO.File.CreateText(path) )
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file,model);
                    }
                }
            }
            catch ( Exception e )
            {
                Response.StatusCode = 500;
                return Json(e);
            }

            //save/update things
            //serialize json
            //save json

            // return View("Modal");
            return Json("OK");
        }

        public static ModalConfigurationModel GetModalConfiguration(string serverPath)
        {
            
            var path = @"~/Documentos/modal.json";
            var model = new ModalConfigurationModel();
            using (StreamReader file = System.IO.File.OpenText(HostingEnvironment.MapPath(path)))
            {
                JsonSerializer serializer = new JsonSerializer();
                model = (ModalConfigurationModel)serializer.Deserialize(file, typeof(ModalConfigurationModel));
            };

            return model;
        }

        #endregion



        // GET: 

        // Cargar noticias cofinal       
        [Allow(action = "CONFIGURACION_NOTICIAS")]
        public ActionResult IndexNoticia()
        {
            ViewBag.TitleNoti = "Noticias cofinal";
            int id = cuentaUsuario.Id;
            bool NoticiaCofiFunda = false;// define que la noticia es para la pagina de cofinal = false
            ViewBag.NoticiaCofiFunda = NoticiaCofiFunda;
            ICollection<Estados> NoticiasEve = configBLL.getEstados(id,NoticiaCofiFunda);
            return View("",NoticiasEve);
        }
        //cargar noticias fundacion
        [Allow(action = "CONFIGURACION_NOTICIAS_FUNDACION")]
        public ActionResult IndexNoticiaFundacion()
        {
            ViewBag.TitleNoti = "Noticias fundación";
            int id = cuentaUsuario.Id;
            bool NoticiaCofiFunda = true;///definir si la noticia es para la pagina de fundación = true
            ViewBag.NoticiaCofiFunda = NoticiaCofiFunda;
            ICollection<Estados> NoticiasEve = configBLL.getEstados(id,NoticiaCofiFunda);
            return View("IndexNoticia",NoticiasEve);
        }
        [Allow(action = "CONFIGURACION_NOTICIAS")]
        public ActionResult CreateNoticia()
        {
            ViewBag.TitleNoti = "Crear noticia cofinal";
            bool NoticiaCofiFunda = false;
            ViewBag.NoticiaCofiFunda = NoticiaCofiFunda;// vista false si es noticia cofinal
            return View();
        }
        //crear noticia
        [Allow(action = "CONFIGURACION_NOTICIAS",actionDos = "CONFIGURACION_NOTICIAS_FUNDACION")]
        [HttpPost]
        public JsonResult CreateNoticia( string Titulo,string Html,string Url,bool Activo,bool NoticiaCofiFunda,HttpPostedFileBase Imagen,DateTime? FechaInicio = null,DateTime? FechaFin = null )
        {
            try
            {
                string urlimg = "";

                if ( Imagen.ContentType != "image/png" && Imagen.ContentType != "image/jpg" && Imagen.ContentType != "image/jpeg" )
                {
                    return Json(
                   new
                   {
                       estado = false,
                       mensaje = "No se aceptan ese formato en imagen miniatura.",

                   },JsonRequestBehavior.AllowGet);
                }

                if ( Imagen != null && Imagen.ContentLength > 0 )
                {
                    if ( Imagen.ContentLength >= 510000 )
                    {
                        return Json(
                       new
                       {
                           estado = false,
                           mensaje = "La imagen es demasiado grande, solo se permite menor o igual a 500KB.",

                       },JsonRequestBehavior.AllowGet);
                    }

                    //Obtiene la url de la imangen
                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Imagen.FileName).ToLower();
                    Imagen.SaveAs(Server.MapPath("~/Imagenes/" + archivo));
                    urlimg = "/Imagenes/" + archivo;
                }
                Estados estados = new Estados
                {
                    Imagen = urlimg,
                    Activo = Activo,
                    NoticiaCofiFunda = NoticiaCofiFunda,
                    Titulo = Titulo,
                    Html = Html,
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    Fecha = DateTime.Now,
                    Id_Usuario = cuentaUsuario.Id,
                    Url = Url
                };
                bool Resultado = configBLL.CreateNoticia(estados);
                return Json(
                    new
                    {
                        estado = true,
                        id = estados.Id,
                        activo = estados.Activo,
                        urlEditNot = NoticiaCofiFunda ? "/configuracion/EditNoticiaFundacion" : "/configuracion/Edit",
                        urlPrewNot = NoticiaCofiFunda ? "/configuracion/PreviewNotiFundacion" : "/configuracion/preview",
                    },JsonRequestBehavior.AllowGet);
            }
            catch ( Excepciones e )
            {
                Trace.WriteLine("Controlador Configuracion, funcion Createnoticia  " + e.Message.ToString(),"Error1 " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion,
                },JsonRequestBehavior.AllowGet);
            }
            catch ( Exception e )
            {
                Trace.WriteLine("Controlador Configuracion, funcion Createnoticia  " + e.Message.ToString(),"Error2 " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = "Algo salio mal.",
                },JsonRequestBehavior.AllowGet);
            }
        }

        //visualizar creacion de noticia de fundación
        [Allow(action = "CONFIGURACION_NOTICIAS_FUNDACION")]
        public ActionResult CreateNoticiaFundacion()
        {
            ViewBag.TitleNoti = "Crear noticia fundación";
            bool NoticiaCofiFunda = true;
            ViewBag.NoticiaCofiFunda = NoticiaCofiFunda; // vista true si es noticia cofinal fundación
            return View("CreateNoticia");
        }

        //listar los sliders cofinal
        [Allow(action = "CONFIGURACION_SLIDER_COFINAL")]
        public ActionResult Index()
        {
            ViewBag.title = "Slider cofinal";
            int id = cuentaUsuario.Id;
            bool TipoSlider = false;//identificar que la noticia es cofinal
            ICollection<Sliders> sliders = configBLL.getSliders(TipoSlider);
            return View("",sliders);
        }

        //Guardar el slider cofinal
        [Allow(action = "CONFIGURACION_SLIDER_COFINAL",actionDos = "CONFIGURACION_SLIDER_FUNDACION")]
        [HttpPost]
        public ActionResult uploadSlider( HttpPostedFileBase file,string titulo,string descripcion,string url,int Id,bool activo,byte posicion,bool eliminar = false )
        {
            try
            {
                Sliders sl = configBLL.GetItem(Id);// metodo para obtener la url de la antigua imagen y usarla para elimarla
                string urlimg = sl.Imagen;
                if ( (Directory.Exists(Path.GetDirectoryName(Server.MapPath(urlimg))) && urlimg != null && file != null) || (eliminar == true && urlimg != null) )
                {
                    // remover imagen de la carpeta del servidor
                    var a = Server.MapPath(urlimg);
                    System.IO.File.SetAttributes(Server.MapPath(urlimg),FileAttributes.Normal);
                    System.IO.File.Delete(Server.MapPath(urlimg));
                }
                string slImg = null;
                if ( file != null && file.ContentLength > 0 )
                {// generar url para guardar la ruta en base de datos  y el archivo en la carpeta del servidor
                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                    file.SaveAs(Server.MapPath("~/igmSliders/" + archivo));
                    slImg = "/igmSliders/" + archivo;
                }
                Sliders d = new Sliders();
                d.Id = Id;
                d.Titulo = titulo;
                d.Descripcion = descripcion;
                d.Url = url;
                d.Imagen = slImg;
                d.Posicion = posicion;
                if ( eliminar == true )
                {
                    d.Activo = null;
                }
                else
                {
                    d.Activo = activo;
                }
                bool response = configBLL.UpdateSlider(d,eliminar);
                return Json(new { estado = true },JsonRequestBehavior.AllowGet);
            }
            catch ( Excepciones e )
            {
                Trace.WriteLine("Controlador Configuracion, funcion uploadSlider  " + e.Message.ToString(),"Error " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion,
                },JsonRequestBehavior.AllowGet);
            }
        }
        //listar slider fundacion
        [Allow(action = "CONFIGURACION_SLIDER_FUNDACION")]
        public ActionResult IndexFundacion()
        {
            ViewBag.title = "Slider fundación";
            int id = cuentaUsuario.Id;
            bool TipoSlider = true; //identifica que la noticia pertenese a fundacion
            ICollection<Sliders> sliders = configBLL.getSliders(TipoSlider);
            return View("index",sliders);
        }

        // para subir una imagen en la galeria del editor de texto Froala
        [Allow(action = "CONFIGURACION_NOTICIAS",actionDos = "CONFIGURACION_SLIDER_FUNDACION")]
        [HttpPost]
        public JsonResult Upload( HttpPostedFileBase file )
        {
            string url = "";
            if ( file != null && file.ContentLength > 0 )
            {
                string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                file.SaveAs(Server.MapPath("~/Imagenes/" + archivo));
                url = "/Imagenes/" + archivo;
                Galeria galeria = new Galeria
                {
                    RutaImagen = url,
                    Id_Usuario = cuentaUsuario.Id
                };
                bool ruta = configBLL.CreateRutaGaleria(galeria);
            }
            //guardar ruta en base de datos

            return Json(new { link = url },JsonRequestBehavior.AllowGet);
        }
        //para subir un documento en el editor de texto Froala
        [Allow(action = "CONFIGURACION_NOTICIAS",actionDos = "CONFIGURACION_NOTICIAS_FUNDACION")]
        [HttpPost]
        public JsonResult uploadDocumentos( HttpPostedFileBase file )
        {
            string url = "";
            if ( file != null && file.ContentLength > 0 )
            {
                string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName).ToLower();
                file.SaveAs(Server.MapPath("~/Documentos/" + archivo));
                url = "/Documentos/" + archivo;
            }
            return Json(new { link = url },JsonRequestBehavior.AllowGet);
        }
        //para cargar todas las imagenes que estan en el servidor 
        [Allow(action = "CONFIGURACION_NOTICIAS",actionDos = "CONFIGURACION_NOTICIAS_FUNDACION")]
        public ActionResult cargarGaleria()
        {
            // string[] files = Directory.GetFiles(Server.MapPath("~/Imagenes/"));// obtener el directorio de las imagenes
            int Id_Usuario = cuentaUsuario.Id;
            ICollection<Galeria> galeria = configBLL.GetImganes(Id_Usuario);
            ICollection<dynamic> response = new List<dynamic>();
            foreach ( var s in galeria )
            {
                try
                {
                    response.Add(new
                    {
                        url = s.RutaImagen
                    });
                }
                catch ( Exception e )
                {
                    Trace.WriteLine("Controlador Configuracion, funcion cargarGaleria  " + e.Message.ToString(),"Error " + DateTime.Now);

                    response.Add(new
                    {
                        error = "no se encontro imagenes"
                    });
                }

            }
            return Json(response,JsonRequestBehavior.AllowGet);
        }
        //eliminar una imagen de la galerida edito froala
        [Allow(action = "CONFIGURACION_NOTICIAS",actionDos = "CONFIGURACION_NOTICIAS_FUNDACION")]
        [HttpPost]
        public ActionResult EliminarGaleria( string src )
        {
            int Id_Usuario = cuentaUsuario.Id;

            if ( Directory.Exists(Path.GetDirectoryName(Server.MapPath(src))) )
            {
                bool elimEstado = configBLL.DeleteGaleria(Id_Usuario,src);
                System.IO.File.SetAttributes(Server.MapPath(src),FileAttributes.Normal);
                System.IO.File.Delete(Server.MapPath(src));
            }
            return Json(new { error = "" },JsonRequestBehavior.AllowGet);
        }

        //subir la imagen del input tooltip
        [Allow(action = "CONFIGURACION_NOTICIAS",actionDos = "CONFIGURACION_NOTICIAS_FUNDACION")]
        [HttpPost]
        public void Subir( HttpPostedFileBase[] file )
        {
            if ( file == null ) return;

            foreach ( var f in file )
            {
                if ( f != null )
                {
                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + f.FileName).ToLower();
                    f.SaveAs(Server.MapPath("~/Imagenes/" + archivo));
                }
            }
        }
        //eliminar una noticia
        [Allow(action = "CONFIGURACION_NOTICIAS",actionDos = "CONFIGURACION_NOTICIAS_FUNDACION")]
        [HttpPost]
        public JsonResult Delete( int id )
        {
            try
            {

                bool elimEstado = configBLL.DeleteEstado(id);
                return Json(elimEstado,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                Trace.WriteLine("Controlador Configuracion, funcion Delete  ","Error " + DateTime.Now);

                return Json(false,JsonRequestBehavior.AllowGet);
            }
        }
        //Cargar edicion de la noticia
        [Allow(action = "CONFIGURACION_NOTICIAS")]
        public ActionResult Edit( int id )
        {
            ViewBag.TitleNoti = "Editar noticia cofinal";
            bool NoticiaCofiFunda = false;//definir si la noticia es para la pagina de cofinal = false
            ViewBag.NoticiaCofiFunda = NoticiaCofiFunda;

            ViewBag.fechainicio = new DateTime();
            ViewBag.fechafin = new DateTime();
            int id_user = cuentaUsuario.Id;
            Estados estado = configBLL.getEstado(id,id_user,NoticiaCofiFunda);
            if ( estado.FechaInicio != null )
            {
                ViewBag.fechainicio = estado.FechaInicio;
            }
            if ( estado.FechaFin != null )
            {
                ViewBag.fechafin = estado.FechaInicio;
            }
            ViewBag.fechapublicada = new DateTime();
            if ( estado.Fecha != null )
            {
                ViewBag.fechapublicada = estado.Fecha;
            }
            ViewBag.html = System.Web.HttpUtility.HtmlDecode(estado.Html);
            @ViewBag.id = id;
            // @ViewBag.id = estado.Url;
            return View("",estado);
        }
        //Editar la noticia
        [Allow(action = "CONFIGURACION_NOTICIAS",actionDos = "CONFIGURACION_NOTICIAS_FUNDACION")/*, action="CONFIGURACION_NOTICIAS")*/]
        [HttpPost]
        public JsonResult Edit( int Id,bool Activo,string Url,bool NoticiaCofiFunda,string Titulo,string Html,HttpPostedFileBase Imagen,DateTime? FechaInicio = null,DateTime? FechaFin = null )
        {
            try
            {
                string url = null;
                if ( Imagen != null && Imagen.ContentLength > 0 )
                {

                    if ( Imagen.ContentLength >= 510000 )
                    {
                        return Json(
                       new
                       {
                           estado = false,
                           mensaje = "La imagen es demasiado grande, solo se permite menor o igual a 500KB.",

                       },JsonRequestBehavior.AllowGet);
                    }

                    //validar formatos correctos de imagen
                    if ( Imagen.ContentType != "image/png" && Imagen.ContentType != "image/jpg" && Imagen.ContentType != "image/jpeg" )
                    {
                        return Json(
                       new
                       {
                           estado = false,
                           mensaje = "No se aceptan ese formato en imagen miniatura.",

                       },JsonRequestBehavior.AllowGet);
                    }

                    //guardar imagen en la carpeta imagenes del servidor             
                    string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Imagen.FileName).ToLower();
                    Imagen.SaveAs(Server.MapPath("~/Imagenes/" + archivo));
                    url = "/Imagenes/" + archivo;
                }
                Estados estados = new Estados
                {

                    Id = Id,
                    Titulo = Titulo.Trim(),
                    Html = Html.Trim(),
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    Fecha = DateTime.Now,
                    Id_Usuario = cuentaUsuario.Id,
                    Activo = Activo,
                    Imagen = url,
                    NoticiaCofiFunda = NoticiaCofiFunda,
                    Url = Url
                };
                bool Resultado = configBLL.EditNoticia(estados);
                return Json(
                    new
                    {
                        estado = true,
                        activo = estados.Activo,
                        fecha = estados.Fecha,
                        urlEditNot = NoticiaCofiFunda ? "/configuracion/EditNoticiaFundacion" : "/configuracion/Edit",
                        urlPrewNot = NoticiaCofiFunda ? "/configuracion/PreviewNotiFundacion" : "/configuracion/preview",
                    },JsonRequestBehavior.AllowGet);

            }
            catch ( Excepciones e )
            {
                Trace.WriteLine("Controlador Configuracion, funcion Edit  " + e.Message.ToString(),"Error " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion,
                },JsonRequestBehavior.AllowGet);
            }
        }

        [Allow(action = "CONFIGURACION_NOTICIAS_FUNDACION")]
        public ActionResult EditNoticiaFundacion( int id )
        {

            ViewBag.TitleNoti = "Editar noticia fundación";
            bool NoticiaCofiFunda = true;///definir si la noticia es para la pagina de fundación = true
            ViewBag.NoticiaCofiFunda = NoticiaCofiFunda;

            ViewBag.fechainicio = new DateTime();
            ViewBag.fechafin = new DateTime();
            int id_user = cuentaUsuario.Id;
            Estados estado = configBLL.getEstado(id,id_user,NoticiaCofiFunda);
            if ( estado.FechaInicio != null )
            {
                ViewBag.fechainicio = estado.FechaInicio;
            }
            if ( estado.FechaFin != null )
            {
                ViewBag.fechafin = estado.FechaInicio;
            }
            ViewBag.fechapublicada = new DateTime();
            if ( estado.Fecha != null )
            {
                ViewBag.fechapublicada = estado.Fecha;
            }
            ViewBag.html = System.Web.HttpUtility.HtmlDecode(estado.Html);
            @ViewBag.id = id;
            return View("Edit",estado);

        }

        //previsualisar la noticia cofinal
        [Allow(action = "CONFIGURACION_NOTICIAS")]
        public ActionResult Preview( int id )
        {
            ViewBag.TitleNoti = "Vista previa noticia cofinal";
            bool NoticiaCofiFunda = false;///definir si la noticia es para la pagina de fundación = true
            ViewBag.NoticiaCofiFunda = NoticiaCofiFunda;

            ViewBag.fechainicio = new DateTime();
            ViewBag.fechafin = new DateTime();
            Estados estado = configBLL.getEstado(id,cuentaUsuario.Id,NoticiaCofiFunda);
            if ( estado.FechaInicio != null )
            {
                ViewBag.fechainicio = estado.FechaInicio;
            }
            if ( estado.FechaFin != null )
            {
                ViewBag.fechafin = estado.FechaInicio;
            }
            ViewBag.fechapublicada = new DateTime();
            if ( estado.Fecha != null )
            {
                ViewBag.fechapublicada = estado.Fecha;
            }

            ViewBag.html = System.Web.HttpUtility.HtmlDecode(estado.Html);//decodificar el html de la noticia
            return View("Preview",estado);
        }

        [Allow(action = "CONFIGURACION_NOTICIAS_FUNDACION")]
        public ActionResult PreviewNotiFundacion( int id )
        {
            ViewBag.TitleNoti = "Vista previa noticia fundación";
            bool NoticiaCofiFunda = true;///definir si la noticia es para la pagina de fundación = true
            ViewBag.NoticiaCofiFunda = NoticiaCofiFunda;

            ViewBag.fechainicio = new DateTime();
            ViewBag.fechafin = new DateTime();
            Estados estado = configBLL.getEstado(id,cuentaUsuario.Id,NoticiaCofiFunda);
            if ( estado.FechaInicio != null )
            {
                ViewBag.fechainicio = estado.FechaInicio;
            }
            if ( estado.FechaFin != null )
            {
                ViewBag.fechafin = estado.FechaInicio;
            }
            ViewBag.fechapublicada = new DateTime();
            if ( estado.Fecha != null )
            {
                ViewBag.fechapublicada = estado.Fecha;
            }
            ViewBag.html = System.Web.HttpUtility.HtmlDecode(estado.Html);//decodificar el html de la noticia
            return View("Preview",estado);
        }


    }
}