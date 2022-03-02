using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenio.BO.Ingenio;
using Ingenio.BLL;
using Ingenio.BLL.Seguridad;
using Newtonsoft.Json;
using Ingenio.Filters;
using Ingenio.Models;
using System.Diagnostics;

namespace Ingenio.Controllers.Seguridad
{
    [Allow(action = "ROLES_MODULOS")]
    public class RolController : Controller
    {
        RolesBll rolesbll = new RolesBll();
        AccountModels cuentaUsuario = new AccountModels();
        public ActionResult Index()
        {

            ICollection<Roles> roles = rolesbll.GetRoles();
            return View("", roles);
        }

        public JsonResult Create()
        {
            ViewBag.Modulos = rolesbll.getModulos();
            ICollection<Modulos> modulos = rolesbll.getModulos();
            ICollection<dynamic> response = new List<dynamic>();
            foreach (var item in modulos)
            {
                if (item.Id > 2)
                {
                    response.Add(new
                    {
                        item.Nombre,
                        item.Id
                    });
                }

            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CambiarPermisos(string data, string nombre, /*bool activo,*/ int rol)
        {
            try
            {
                dynamic json = JsonConvert.DeserializeObject(data);
                ICollection<RolesModulos> modulos = new List<RolesModulos>();
                foreach (var item in json)
                {
                    if (rol==1)
                    {
                        modulos.Add(new RolesModulos
                        {
                            Id_Modulo = item,
                            Id_Rol = rol
                        });
                    }else  if (item!=2 && item!=1)
                    {
                        modulos.Add(new RolesModulos
                        {
                            Id_Modulo = item,
                            Id_Rol = rol
                        });
                    }
                    
                }
                bool disabledcheck = false;
                if (rol == 1)
                {
                    disabledcheck = true;
                }

                Roles role = new Roles
                {
                    Id = rol,
                    Nombre = nombre,
                    //  Activo = activo
                };

                // rolesbll.SetNombre(role);           
                bool res = rolesbll.CambiarPermisos(role, modulos);
                return Json(new { estado = true, activo = role.Activo, disabledcheck = disabledcheck }, JsonRequestBehavior.AllowGet);
            }
            catch (Excepciones e)
            {
                Trace.WriteLine("Controlador Rol, funcion cambiarpermisos  " + e.Message.ToString(), "Error " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion,
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Trace.WriteLine("Controlador Rol, funcion cambiarpermisos  " + e.Message.ToString(), "Error2 " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = "Algo salio mal.",
                }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public JsonResult create2(string data, string nombre/*, bool activo*/)
        {

            try
            {
                dynamic json = JsonConvert.DeserializeObject(data);
                ICollection<int> modulos = new List<int>();
                foreach (var item in json)
                {
                  
                    modulos.Add(Convert.ToInt32(item));
                }
                Roles rol = new Roles
                {
                    Nombre = nombre,
                    // Activo = activo
                };

                bool res = rolesbll.Create2(modulos, rol);
                return Json(new { estado = true, id_rol = rol.Id, activo = rol.Activo }, JsonRequestBehavior.AllowGet);
            }
            catch (Excepciones e)
            {
                Trace.WriteLine("Controlador Rol, funcion create2  " + e.Message.ToString(), "Error " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = e.Descripcion,
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Trace.WriteLine("Controlador Rol, funcion create2  " + e.Message.ToString(), "Error2 " + DateTime.Now);

                return Json(new
                {
                    estado = false,
                    mensaje = "Algo salio mal.",
                }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Details(int id)
        {

            ICollection<Modulos> modulos = rolesbll.getModulos();
            ICollection<Modulos> marcados = rolesbll.GetModulosRol(id);
            //ICollection<Modulos> ModulosAdmin = rolesbll.GetModulosRolUser(cuentaUsuario.Id);
            //int idu = cuenta;
            ICollection<object> lista = new List<object>();
            foreach (var item in modulos)
            {
                //bool a = marcados.Contains(item);
                if (item.Id > 2 && id != 1)
                {
                    lista.Add(new
                    {
                        nombre = item.Nombre,
                        id = item.Id,
                        activo = marcados.Contains(item),
                    });
                }
                else if (id == 1)
                {
                    lista.Add(new
                    {
                        nombre = item.Nombre,
                        id = item.Id,
                        activo = marcados.Contains(item),
                        disabled = id == 1 && (item.Id == 1 || item.Id == 2) ? true : false
                    });
                }

            }

            var response = new
            {
                modulos = lista,
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActivarRol(int Id, bool Activo)
        {
            //string u = Request.Cookies["user"]["user"];

            try
            {
                dynamic res = new
                {
                    estado = rolesbll.ActivarRol(Id, Activo)
                };
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                Trace.WriteLine("Controlador Rol, funcion activarrol  ", "Error " + DateTime.Now);

                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Delete(int id)
        {

            try
            {
                // TODO: Add delete logic here

                bool res = rolesbll.Delete(id);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                Trace.WriteLine("Controlador Rol, funcion delete  " , "Error " + DateTime.Now);

                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }



}
