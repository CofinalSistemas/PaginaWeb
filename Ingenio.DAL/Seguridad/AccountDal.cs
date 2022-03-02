using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO.Ingenio;
using System.Dynamic;
using Ingenio.BO;

namespace Ingenio.DAL.Seguridad
{
    public class AccountDal
    {
        IngenioEntities db = new IngenioEntities();
        ModelContainer dbi = new ModelContainer();
        public dynamic Login(string user, string password, DateTime fechaAcceso,string ip)
        {
            try
            {
             
                int Numero;
                bool res = int.TryParse(user, out Numero);
                if (res)
                {
                    int AsoIdentificacion = Convert.ToInt32(user);
                    asociados aso = (from asoc in dbi.asociados
                                     join nt in dbi.nits on asoc.cedulasociado equals nt.nit
                                     where asoc.cedulasociado == AsoIdentificacion && asoc.estado == "A"
                                     select asoc).FirstOrDefault();

                    if (aso != null)
                    {
                        Asociados_Ingenio asoIngenio = (from asoin in db.Asociados_Ingenio where asoin.Identificacion == user && asoin.Password == password select asoin).FirstOrDefault();
                        if (asoIngenio != null)
                        {
                            asoIngenio.FechaAnteriroAcceso = fechaAcceso;
                            db.SaveChanges();

                            Log_Usuarios log_usu = new Log_Usuarios();
                            log_usu.Usuario = asoIngenio.Identificacion;
                            log_usu.Fecha_acesso = fechaAcceso;
                            log_usu.Ip = ip;
                            db.Log_Usuarios.Add(log_usu);
                            db.SaveChanges();

                            return asoIngenio;//mirar que esta mal
                        }
                    }
                    else {
                        nits ason = (from asoc in dbi.nits
                                         
                                         where asoc.nit == AsoIdentificacion && asoc.estado == "A"
                                         select asoc).FirstOrDefault();
                        if (ason != null)
                        {
                            Asociados_Ingenio asoIngenio = (from asoin in db.Asociados_Ingenio where asoin.Identificacion == user && asoin.Password == password select asoin).FirstOrDefault();
                            if (asoIngenio != null)
                            {
                                asoIngenio.FechaAnteriroAcceso = fechaAcceso;
                                db.SaveChanges();

                                Log_Usuarios log_usu = new Log_Usuarios();
                                log_usu.Usuario = asoIngenio.Identificacion;
                                log_usu.Fecha_acesso = fechaAcceso;
                                log_usu.Ip = ip;
                                db.Log_Usuarios.Add(log_usu);
                                db.SaveChanges();

                                return asoIngenio;//mirar que esta mal
                            }
                        }

                    }
                }               

                Usuarios usuario = (from u in db.Usuarios where u.UserName == user && u.Password == password && u.Activo select u).FirstOrDefault();
                if (usuario != null)
                {
                    usuario.FechaUltimoAcceso = fechaAcceso;
                    db.SaveChanges();

                    Log_Usuarios log_usu = new Log_Usuarios();
                    log_usu.Usuario = usuario.Nombre;
                    log_usu.Fecha_acesso = fechaAcceso;
                    log_usu.Ip = ip;
                    db.Log_Usuarios.Add(log_usu);
                    db.SaveChanges();

                    return usuario;
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;

            }


        }

        public bool AccesoAModulo(int userId, string modulo)
        {
            int a = (from u in db.Usuarios
                     join
                         ur in db.UsuariosRoles on u.Id equals ur.Id_Usuario
                     join
                         r in db.Roles on ur.Id_Rol equals r.Id
                     join
                         rm in db.RolesModulos on r.Id equals rm.Id_Rol
                     join
                         m in db.Modulos on rm.Id_Modulo equals m.Id
                     where u.Id == userId && m.Nombre == modulo
                     select m
                ).ToList().Count();
            return a >= 1 ? true : false;
        }
        public ICollection<Modulos> GetModulos(int p)
        {
            var a = (from u in db.Usuarios
                     join
                         ur in db.UsuariosRoles on u.Id equals ur.Id_Usuario
                     join
                         r in db.Roles on ur.Id_Rol equals r.Id
                     join
                         rm in db.RolesModulos on r.Id equals rm.Id_Rol
                     join
                         m in db.Modulos on rm.Id_Modulo equals m.Id
                     where u.Id == p && r.Activo==true
                     select m
                ).ToList();
            return a;
        }
    }
}
