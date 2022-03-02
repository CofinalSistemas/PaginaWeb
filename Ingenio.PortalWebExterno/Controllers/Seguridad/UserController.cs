using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ingenio.BO.Ingenio;
using Ingenio.BLL;
using Ingenio.BLL.Seguridad;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Ingenio.Filters;
using System.Diagnostics;

namespace Ingenio.Controllers.Seguridad
{
    [Allow(action = "USER_ROLES")]
    public class UserController : Controller
    {
        UsuarioBll usuariobll = new UsuarioBll();
        RolesBll rolesbll = new RolesBll();

        public ActionResult Index()
        {
            ICollection<Usuarios> user = usuariobll.GetUsuarios();
            ViewBag.cargo = usuariobll.GetCargos();
            return View("", user);
        }
        [HttpPost]
        public JsonResult Create(Usuarios usuarios, string Cargo = "" /*string Identificacion, string Nombre, string Telefono, string UserName, string password, string Cargo = "", int Id_Cargo = -1*/)
        {
            try
            {
                Cifrado cifrado = new Cifrado();
                int id_carg = 0;
                if (Cargo != "")
                {
                    Cargos sl = usuariobll.CrearCargo(Cargo);
                    id_carg = sl.Id;
                }

                Usuarios usuario = new Usuarios();
                usuario.Identificacion = usuarios.Identificacion;
                usuario.Nombre = usuarios.Nombre;
                usuario.Telefono = usuarios.Telefono;
                usuario.FechaCreacion = DateTime.Now;
                usuario.UserName = usuarios.UserName.Trim().ToUpper();
                usuario.Password = cifrado.EncodeSHA1(usuarios.Password);
                usuario.Id_Cargo = id_carg != 0 ? id_carg : usuarios.Id_Cargo;

                bool user = usuariobll.Create(usuario);

                return Json(new
                {
                    estado = true,
                    Id = usuario.Id,
                    telefono = usuario.Telefono,
                    Id_Cargo = usuario.Id_Cargo,
                    fecha_acceso = usuario.FechaUltimoAcceso,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Excepciones e)
            {
                Trace.WriteLine("Controlador User, funcion create  " + e.Message.ToString(), "Error " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Controlador User, funcion create  " + e.Message.ToString(), "Error2 " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = "Algo salio mal.",
                }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public JsonResult Edit(Usuarios usuarios, string Cargo = "")
        {
            try
            {
                Cifrado cifrado = new Cifrado();
                int id_carg = 0;
                bool disabledcheck = false;
                
                if (Cargo != "")
                {
                    Cargos sl = usuariobll.CrearCargo(Cargo);
                    id_carg = sl.Id;
                }
                //Usuarios usuario = new Usuarios(); 
                if (usuarios.Password != null)
                {
                    usuarios.Password = cifrado.EncodeSHA1(usuarios.Password);
                }
                if (usuarios.Id == 1)
                {
                    disabledcheck = true;
                }
                usuarios.Id_Cargo = id_carg != 0 ? id_carg : usuarios.Id_Cargo;

                bool user = usuariobll.Edit(usuarios);

                return Json(new
                {
                    estado = true,
                    Id = usuarios.Id,
                    telefono = usuarios.Telefono,
                    Id_Cargo = usuarios.Id_Cargo,
                    fecha_acceso = usuarios.FechaUltimoAcceso,
                    disabledcheck =disabledcheck,
                    activo =usuarios.Activo
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Excepciones e)
            {
                Trace.WriteLine("Controlador User, funcion edit  " + e.Message.ToString(), "Error " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Controlador User, funcion edit  " + e.Message.ToString(), "Error2 " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = "Algo salio mal.",
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult ActivarUsuario(int id, bool Activo)
        {
            dynamic res = new
            {
                estado = usuariobll.ActivarUsuario(id, Activo)
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            ICollection<Roles> roles = rolesbll.GetRoles();
            ICollection<Roles> marcados = usuariobll.GetUsuariosRoles(id);
            ICollection<object> lista = new List<object>();

            foreach (var item in roles)
            {
                bool ban = false;
                foreach (var item2 in marcados)
                {
                    if (item.Id == item2.Id)
                    {
                        ban = true;
                        break;
                    }
                }
                lista.Add(new
                {
                    nombre = item.Nombre,
                    id = item.Id,
                    activo = ban,
                    disabled = id == 1 && (item.Id == 1) ? true : false
                });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
            //return Content(Newtonsoft.Json.JsonConvert.SerializeObject(modulos));
        }

        public ActionResult CambiarRoles(string data, int id)
        {
            try
            {
                dynamic json = JsonConvert.DeserializeObject(data);
                ICollection<UsuariosRoles> userRol = new List<UsuariosRoles>();
                if (id == 1)
                {
                    userRol.Add(new UsuariosRoles
                    {
                        Id_Usuario = 1,
                        Id_Rol = 1
                    });
                }
                foreach (var item in json)
                {
                    if (id == 1 && item == 1)
                    {
                        continue;
                    }
                    userRol.Add(new UsuariosRoles
                    {
                        Id_Usuario = id,
                        Id_Rol = item
                    });

                }
                return Json(usuariobll.CambiarRoles(id, userRol), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Controlador User, funcion cambiarroles  " + e.Message.ToString(), "Error " + DateTime.Now);
                return View();
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                bool res = usuariobll.Delete(id);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                Trace.WriteLine("Controlador User, funcion delete  ", "Error " + DateTime.Now);

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


    }
}