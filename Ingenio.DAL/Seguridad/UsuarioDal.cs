using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO.Ingenio;
using Ingenio.BO;


namespace Ingenio.DAL.Seguridad
{
    public class UsuarioDal
    {
        IngenioEntities db = new IngenioEntities();
       // ModelContainer dbi = new ModelContainer();
        public ICollection<Usuarios> GetUsuarios()
        {
            return db.Usuarios.ToList();
        }

        public bool Create(Usuarios usuario)
        {
            try
            {
                int username = (from us in db.Usuarios where us.UserName == usuario.UserName select us).ToList().Count();
                int identificacion = (from us in db.Usuarios where us.Identificacion == usuario.Identificacion select us).ToList().Count();
                if (username >= 1)
                {
                    throw new Excepciones("Codigo", "Usuario ya existe", "El nombre de usuario ya se encuentra registrado en la Base de datos");
                }
                if (identificacion >= 1)
                {
                    throw new Excepciones("Codigo", "Identificaicon ya existe", "El numero de identificación ya se encuentra registrado en la Base de datos");
                }
                db.Usuarios.Add(usuario);
                int res = db.SaveChanges();
                return res == 1 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ICollection<Cargos> GetCargos()
        {
            return db.Cargos.ToList();
        }


        public Cargos CrearCargo(string cargo)
        {
            try
            {
                int nombre = (from us in db.Cargos where us.Nombre == cargo select us).ToList().Count();
                if (nombre >= 1)
                {
                    throw new Excepciones("Codigo", "Cargo ya existe", "El cargo ya se encuentra registrado en la Base de datos");
                }
                Cargos Cargo = new Cargos();
                Cargo.Nombre = cargo;
                db.Cargos.Add(Cargo);
                db.SaveChanges();
                return Cargo;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool Edit(Usuarios usuarios)
        {
            try
            {

                int username = (from us in db.Usuarios where us.UserName == usuarios.UserName select us).ToList().Count();
                Usuarios usuario = (from us in db.Usuarios where us.Id == usuarios.Id select us).FirstOrDefault();
                if (usuario.UserName != usuarios.UserName && username > 0)
                {
                    throw new Excepciones("Codigo", "Usuario ya existe", "El nombre de usuario ya se encuentra registrado en la Base de datos");
                }
                if (usuario.Identificacion != usuarios.Identificacion)
                {
                    int identificacion = (from us in db.Usuarios where us.Identificacion == usuarios.Identificacion select us).ToList().Count();
                    if (identificacion >= 1)
                    {
                        throw new Excepciones("Codigo", "Identificaicon ya existe", "El numero de identificación ya se encuentra registrado en la Base de datos");
                    }
                }

                usuario.Identificacion = usuarios.Identificacion;
                usuario.Nombre = usuarios.Nombre;
                usuario.UserName = usuarios.UserName;
                usuario.Telefono = usuarios.Telefono;
                usuario.Id_Cargo = usuarios.Id_Cargo;
                usuario.Password = usuarios.Password == null ? usuario.Password : usuarios.Password;

                int res = db.SaveChanges();
                return res == 1 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Usuarios RestaurarUsu(int identificacion)
        {
            try
            {
                string identificacionString = Convert.ToString(identificacion);
                Usuarios usu = (from usuario in db.Usuarios where usuario.Identificacion == identificacionString && usuario.Activo == true select usuario).FirstOrDefault();
                if (usu == null)
                {
                    throw new Excepciones("Codigo", "Usuario icorrecto", "La identificación ingresada no es valida o se encuentra inactiva");
                }
                return usu;
            }
            catch (Excepciones c)
            {
                throw;
            }
        }

        public bool paswordRegistro(string id)
        {
            try
            {
                bool passAsociado = (from pass in db.Asociados_Ingenio where pass.Password == id select pass).FirstOrDefault() != null;
                return passAsociado;
            }
            catch (Excepciones c)
            {
                throw;
            }
        }

        public Asociados_Ingenio RestaurarAso(int identificacion)
        {
            try
            {
                string identificacionString = Convert.ToString(identificacion);
                Asociados_Ingenio asoc = (from asociado in db.Asociados_Ingenio where asociado.Identificacion == identificacionString select asociado).FirstOrDefault();
                if (asoc == null)
                {
                    throw new Excepciones("Codigo", "Usuario icorrecot", "La identificación ingresada no es valida o se encuentra inactiva");
                }
                return asoc;
            }
            catch (Excepciones c)
            {
                throw;
            }
        }


        public bool paswordRegistro(string key, string password)
        {
            try
            {

                Asociados_Ingenio passAsociado = (from pass in db.Asociados_Ingenio where pass.Password == key select pass).FirstOrDefault();
                if (passAsociado == null)
                {
                    throw new Excepciones("Codigo", "Clave enviada no coincide", "EL codigo de acceso no es valido");
                }
                passAsociado.Password = password;
                int res = db.SaveChanges();
                return res == 1 ? true : false;
            }
            catch (Excepciones c)
            {
                throw;
            }
        }

        public bool Registro(Asociados_Ingenio asoc)
        {
            try
            {
                int asociado = (from aso in db.Asociados_Ingenio where aso.Identificacion == asoc.Identificacion select aso).ToList().Count();

                if (asociado != 0)
                {
                    throw new Excepciones("Codigo", "Asociado ya existe", "El usuario ya se encutra registrado");
                }
                //asoc.PrimerNombre.ToUpper();
                //asoc.SegundoNombre.ToUpper();
                //asoc.SegundoNombre.ToUpper();
                //asoc.SegundoAPellido.ToUpper();

                db.Asociados_Ingenio.Add(asoc);
                return db.SaveChanges() == 1 ? true : false;

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ICollection<Roles> GetUsuariosRoles(int id)
        {
            try
            {
                ICollection<Roles> roles = (from rol in db.Roles
                                            join
                                                usuRol in db.UsuariosRoles on rol.Id equals usuRol.Id_Rol
                                            where usuRol.Id_Usuario == id
                                            select rol).ToList();
                return roles;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ActivarUsuario(int id, bool Activo)
        {
            try
            {
                Usuarios user = db.Usuarios.Find(id);
                if (user == null)
                {
                    throw new Excepciones("Codigo", "Usuario invalido", "El Id no corresponde a ningun usuario");
                }
                user.Activo = Activo;
                int res = db.SaveChanges();
                return res == 1 ? true : false;
            }
            catch (Excepciones e)
            {
                throw;
            }
        }

        public bool CambiarRoles(int id, ICollection<UsuariosRoles> userRol)
        {
            if (userRol.Count <= 0)
            {
                db.UsuariosRoles.RemoveRange(db.UsuariosRoles.Where(m => m.Id_Usuario == id));
                db.SaveChanges();
                return true;
            }

            bool ban = false;
            ICollection<UsuariosRoles> userR = (from rm in db.UsuariosRoles where rm.Id_Usuario == id select rm).ToList();
            foreach (var item in userR)
            {
                ban = false;
                foreach (var mod in userRol)
                {
                    if (mod.Id_Rol == item.Id_Rol)
                    {
                        ban = true;
                        userRol.Remove(mod);
                        break;
                    }
                }
                if (ban == false)
                {
                    db.UsuariosRoles.Remove(item);
                }
            }
            if (userRol.Count > 0)
            {
                db.UsuariosRoles.AddRange(userRol);
            }
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                Usuarios u = db.Usuarios.Find(id);
                if (u == null)
                {
                    throw new Excepciones("Codigo", "Id invalido", "El usuario no existe");
                }
                db.Usuarios.Remove(u);
                return db.SaveChanges() == 1 ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuarios GetUserByName(string user)
        {
            Usuarios us = (from u in db.Usuarios where u.UserName == user && u.Activo==true select u).FirstOrDefault();
            return us;
        }

        public ICollection<string> GetNombreModulos(int id)
        {
            Usuarios usuario = db.Usuarios.Find(id);
            ICollection<string> modulos = (from us in db.Usuarios
                                           join
                                               rol in db.UsuariosRoles on us.Id equals rol.Id_Usuario
                                           join
                                               r in db.Roles on rol.Id_Rol equals r.Id
                                           join
                                               mr in db.RolesModulos on rol.Id_Rol equals mr.Id_Rol
                                           join
                                               mod in db.Modulos on mr.Id_Modulo equals mod.Id
                                           where us.Id == usuario.Id && r.Activo==true
                                           select mod.Nombre).ToList();


            if (usuario != null)
            {
                return modulos;
            }
            return null;
        }

        public bool cambiarHash(Usuarios u, string cad)
        {
            int res = db.SaveChanges();
            return res == 2 ? true : false;
        }
    }
}
