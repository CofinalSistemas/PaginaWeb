using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO.Ingenio;
using Ingenio.DAL;
using Ingenio.DAL.Seguridad;

namespace Ingenio.BLL.Seguridad
{
    public class RolesBll
    {
        RolesDal rolesdal = new RolesDal();
        public ICollection<Roles> GetRoles()
        {
            return rolesdal.GetRoles();
        }

        public ICollection<Modulos> getModulos()
        {
            return rolesdal.GetModulos();
        }

        public bool Create(string nombre, IList<int> modulos)
        {
            nombre = nombre.ToUpper().Trim();
            return rolesdal.Create(nombre, modulos);
        }

        public bool CreateRo(Roles roles)
        {
            try
            {

                if (roles.Nombre == null)
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese un nombre");
                }
                roles.Nombre = roles.Nombre.Trim().ToUpper();
                return true;//rolesdal.CreateRo(roles);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICollection<Modulos> GetModulosRol(int id)
        {
            return rolesdal.GetModulosRol(id);
        }
       
        public bool CambiarPermisos(Roles role, ICollection<RolesModulos> modulos)
        {
            role.Nombre = role.Nombre.ToUpper().Trim();
            return rolesdal.CambiarPermisos(role, modulos);
        }

        public bool Delete(int id)
        {
            return rolesdal.Delete(id);
        }

     
        public bool Create2(ICollection<int> modulos, Roles rol)
        {
            return rolesdal.Create2(modulos, rol);
        }

        public object ActivarRol(int Id, bool Activo)
        {
            try
            {

                if (Id == 1)
                {
                    throw new Excepciones("Error AROL-001", "No puedes desactivar este rol");
                }
                return rolesdal.ActivarRol(Id, Activo);
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

      
    }
}
