using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ingenio.BO;
using Ingenio.BLL.Seguridad;
using Ingenio.BO.Ingenio;

namespace Ingenio.Models
{
    public class AccountModels
    {
        private Usuarios User { get; set; }
        private Asociados_Ingenio asociado { get; set; }
        public ICollection<Modulos> _Modulos_Usu = new List<Modulos>();
        private bool isNull = true;
        public AccountModels()

        {
            UsuarioBll usuarioBll = new UsuarioBll();

            if (HttpContext.Current.Session["userInfo"] != null)
            {
                isNull = false;
                User = (Usuarios)HttpContext.Current.Session["userInfo"];
                Modulos_Usu = (ICollection<string>)HttpContext.Current.Session["userModulos"];
            }
            if (HttpContext.Current.Session["asociadoInfo"] != null)
            {
                _Modulos_Usu.Clear();

                _Modulos_Usu.Add((Modulos)HttpContext.Current.Session["modulosAso"]);
                isNull = false;
                Asociados_Ingenio asoingenio = (Asociados_Ingenio)HttpContext.Current.Session["asociadoInfo"];
                Usuarios asociados = new Usuarios();
                asociados.UserName = asoingenio.PrimerNombre + " " + asoingenio.PrimerApellido;
                asociados.Id = asoingenio.Id;
                asociados.Identificacion = asoingenio.Identificacion;
                asociados.Id_Cargo = asoingenio.tipo;
                HttpContext.Current.Session["userInfo"] = asociados;
            }

        }
        public string UserName
        {
            get
            {
                return User.UserName;
            }
        }
        public int Id
        {
            get
            {
                return User.Id;
            }
        }
        public int Identificacion
        {
            get
            {
                return Convert.ToInt32(User.Identificacion);
            }
        }
        public int Id_Cargo
        {
            get
            {
                return User.Id_Cargo;
            }
        }

        public bool IsAsociado
        {
            get
            {
                return HttpContext.Current.Session["asociadoInfo"] != null;
            }
        }
        public bool IsUsuario
        {
            get
            {
                return HttpContext.Current.Session["userInfo"] != null;
            }
        }

        public ICollection<string> Modulos_Usu
        {
            get;
            set;
        }

        public bool AccesoAModulo(string modulo)
        {
            AccountBll accountbll = new AccountBll();
            bool modulos = accountbll.AccesoAModulo(User.Id, modulo);
            return modulos;
        }

        public bool IsNull()
        {
            return isNull;
        }
        public Usuarios GerUser()
        {
            return User;
        }


    }
}