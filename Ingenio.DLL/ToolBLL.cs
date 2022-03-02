using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO;
using Ingenio.DAL;

namespace Ingenio.BLL
{
    public class ToolBLL
    {
        ToolDal toolDAL = new ToolDal();
        public ICollection<pais> GetPais(string rpais)
        {
            return toolDAL.GetPaises(rpais);
            
        }

        public ICollection<departamentos> GetDepartamentos(string term, string id)
        {
            return toolDAL.GetDepartamentos(term, id);
            
        }

        public departamentos GetDepartamento(string codDepartamento)
        {
            return toolDAL.GetDepartamento(codDepartamento);

        }

        public ICollection<ciudades> GetCiudades(string term, string id)
        {
            return toolDAL.GetCiudades(term, id);
        }

        public ciudades GetCiudad(string codCiudad)
        {
            return toolDAL.GetCiudad(codCiudad);
        }

        public ICollection<zonasgeograficas> GetZonas(string term, string id)
        {
            return toolDAL.GetZonas(term, id);
        }
        public zonasgeograficas GetZona(string codZona)
        {
            return toolDAL.GetZona(codZona);
        }

        public ICollection<comunas> GetComunas(string term, string id)
        {
            return toolDAL.GetComunas(term, id);
        }

        public comunas GetComuna(string codComuna)
        {
            return toolDAL.GetComuna(codComuna);
        }

        public ICollection<barrios> GetBarrios(string term, string id)
        {
            return toolDAL.GetBarrios(term, id);
        }

        public barrios GetBarrio(string codBarrio)
        {
            return toolDAL.GetBarrio(codBarrio);
        }
    }
}
