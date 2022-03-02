using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ingenio.DAL;
using Ingenio.DAL.Seguridad;
using Ingenio.BO.Ingenio;
using Ingenio.BO;
using System.Text.RegularExpressions;

namespace Ingenio.BLL.Seguridad
{
    public class UsuarioBll
    {
        UsuarioDal usuariodal = new UsuarioDal();
        public ICollection<Usuarios> GetUsuarios()
        {
            return usuariodal.GetUsuarios();
        }

        public bool Create(Usuarios usuario)
        {
            return usuariodal.Create(usuario);
        }

        public ICollection<Cargos> GetCargos()
        {
            return usuariodal.GetCargos();
        }

        public ICollection<Roles> GetUsuariosRoles(int id)
        {
            return usuariodal.GetUsuariosRoles(id);
        }

        public Cargos CrearCargo(string Cargo)
        {
            return usuariodal.CrearCargo(Cargo);
        }

        public bool CambiarRoles(int id, ICollection<UsuariosRoles> userRol)
        {
            return usuariodal.CambiarRoles(id, userRol);
        }

        public bool Delete(int id)
        {
            return usuariodal.Delete(id);
        }

        public Usuarios GetUserByName(string user)
        {
            return usuariodal.GetUserByName(user);
        }
        public ICollection<string> GetNombreModulos(int id)
        {
            return usuariodal.GetNombreModulos(id);
        }
        public bool cambiarHash(Usuarios u, string cad)
        {
            return usuariodal.cambiarHash(u, cad);
        }

        public bool ActivarUsuario(int id, bool Activo)
        {
            return usuariodal.ActivarUsuario(id, Activo);
        }

        public bool Edit(Usuarios usuarios)
        {
            try
            {

                if (usuarios.Nombre == null)
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese un nombre");
                }
                if (usuarios.UserName == null)
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese un Usuario");
                }
                usuarios.UserName = usuarios.UserName.Trim().ToUpper();
                return usuariodal.Edit(usuarios);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Registro(Asociados_Ingenio asoc)
        {
            try
            {
                string expresion = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

                if (asoc.PrimerNombre == null)
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese su primer nombre");
                }else if (asoc.PrimerApellido == null)
                {
                    throw new Excepciones("Error PEC-APE-001", "Ingrese su  primer apellido");
                }
                else if (asoc.Correo == null || !Regex.IsMatch(asoc.Correo,expresion))
                {
                    throw new Excepciones("Error PEC-APE-001", "Ingrese un correo");
                }
                return usuariodal.Registro(asoc);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool paswordRegistro(string key, string password)
        {
           
            return  usuariodal.paswordRegistro(key, password);
        }

        public Usuarios RestaurarUsu(int identificacion)
        {
            try
            {
                if (identificacion != 0)
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese una numero de indentificaión");
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            return usuariodal.RestaurarUsu(identificacion);
        }
        public Asociados_Ingenio RestaurarAso(int identificacion)
        {
            try
            {
                if (identificacion==0)
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese una numero de indentificaión");
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            return usuariodal.RestaurarAso(identificacion);
        }

        public bool paswordRegistro(string id)
        {
            return usuariodal.paswordRegistro(id);
        }

        //public dynamic getPreguntas(int numero, int identificacion)
        //{
        //    return  usuariodal.getPreguntas(numero, identificacion);
        //}
    }
}
