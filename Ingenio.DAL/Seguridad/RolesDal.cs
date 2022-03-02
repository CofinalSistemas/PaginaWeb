using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO.Ingenio;

namespace Ingenio.DAL.Seguridad
{
    public class RolesDal
    {
        IngenioEntities db = new IngenioEntities();
        public ICollection<Roles> GetRoles()
        {
            return db.Roles.ToList();
        }

        public ICollection<Modulos> GetModulos()
        {
            return db.Modulos.ToList();
        }

        public bool Create(string nombre, IList<int> modulos)
        {
            Roles rol = new Roles();
            rol.Nombre = nombre;
            db.Roles.Add(rol);

            foreach (var item in modulos)
            {
                RolesModulos rm = new RolesModulos();
                rm.Id_Rol = rol.Id;
                rm.Id_Modulo = item;
                db.RolesModulos.Add(rm);
            }

            int res = db.SaveChanges();
            return res > 2 ? true : false;

        }

        public ICollection<Modulos> GetModulosRol(int id)
        {
            try
            {
                ICollection<Modulos> modulos = (from md in db.Modulos
                                                join
                                 rm in db.RolesModulos on md.Id equals rm.Id_Modulo
                                                where rm.Id_Rol == id
                                                select md).ToList();
                return modulos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CambiarPermisos(Roles role, ICollection<RolesModulos> modulos)
        {
            try
            {
                Roles rol = db.Roles.Find(role.Id);                
                if (role.Nombre == "")
                {
                    throw new Excepciones("Error ROL-NOM-01", "El nombre no pude ser vacio.");
                }
                if (rol.Nombre != role.Nombre)
                {
                    Roles NomRoles = (from n in db.Roles where n.Nombre == role.Nombre select n).FirstOrDefault(); //FirstOrDefault() : obtine el primero y si no lo encuetra retorna null

                    if (NomRoles != null)
                    {
                        throw new Excepciones("Error ROL-NOM-01", "El nombre de rol ya existe.");
                    }
                }              
               
                rol.Nombre = role.Nombre;
                rol.Activo = role.Activo;

                if (modulos.Count <= 0) //se cuenta si la lista se encuentra algun campo chequeado para remover
                {
                    db.RolesModulos.RemoveRange(db.RolesModulos.Where(m => m.Id_Rol == role.Id));
                    return db.SaveChanges() > 1 ? true : false;
                }
                bool ban = false;
                ICollection<RolesModulos> rolM = (from rm in db.RolesModulos where rm.Id_Rol == role.Id select rm).ToList();
                foreach (var item in rolM)
                {
                    ban = false;
                    foreach (var mod in modulos)
                    {
                        if (mod.Id_Modulo == item.Id_Modulo)
                        {
                            ban = true;
                            modulos.Remove(mod);
                            break;
                        }
                    }
                    if (ban == false)
                    {
                        db.RolesModulos.Remove(item);
                    }
                }
                if (modulos.Count > 0)
                {
                    db.RolesModulos.AddRange(modulos);
                }
                return db.SaveChanges() > 2 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public object ActivarRol(int Id, bool Activo)
        {
            try
            {
                Roles rol = db.Roles.Find(Id);
                if (rol == null)
                {
                    throw new Excepciones("Codigo", "rol invalido", "El Id no corresponde a ningun rol");
                }
                rol.Activo = Activo;
                int res = db.SaveChanges();
                return res == 1 ? true : false;
            }
            catch (Excepciones e)
            {
                throw;
            }
        }

        public bool Create2(ICollection<int> modulos, Roles rol)
        {
            try
            {
                Roles roles = (from n in db.Roles where n.Nombre == rol.Nombre select n).FirstOrDefault(); //FirstOrDefault() : obtine el primero y si no lo encuetra retorna null
                if (roles != null)
                {
                    throw new Excepciones("Error ROL-NOM-01", "El nombre de rol ya existe.");
                }

                db.Roles.Add(rol);

                foreach (var item in modulos)
                {
                    RolesModulos rm = new RolesModulos();
                    rm.Id_Rol = rol.Id;
                    rm.Id_Modulo = item;
                    db.RolesModulos.Add(rm);
                }

                return db.SaveChanges() <= 2 ? true : false;

            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                Roles rol = db.Roles.Find(id);
                db.Roles.Remove(rol);
                return db.SaveChanges() == 1 ? true : false;               
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
