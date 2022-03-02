using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO;
using Ingenio.BO.Ingenio;
using Ingenio.DAL;

namespace Ingenio.BLL
{
    public class AsociadoBLL
    {
        AsociadoDAL AsoDAL = new AsociadoDAL();

        public void Edit(int v, int id)
        {
            throw new NotImplementedException();
        }

        //public bool Editar(int id)
        //{
        //    var asoc = AsoDAL.Editar(id);
        //    return asoc;
        //}

        public asociados GetAsociado(int id)
        {
            return AsoDAL.GetAsociado(id);
        }

        public object GetAgencias(asociados aso)
        {
            return AsoDAL.GetAgencias(aso);
        }

        public ICollection<Asociados_Ingenio> asociados()
        {
            return AsoDAL.asociados();
        }

        public bool EliminarAso(int id)
        {
             return AsoDAL.EliminarAso(id);
        }

        public nits Getnit(int id)
        {
            return AsoDAL.Getnit(id);
        }

        public dynamic GetAhorros(int anio, int mes, int nit)
        {
            dynamic ahorro = AsoDAL.GETAhorros(anio, mes, nit);
            return ahorro;
        }
              
        public ICollection<profesiones> GetProfesiones(string term)
        {
            return AsoDAL.GetProfesiones(term);
        }

        public ICollection<empresas> GetEmpresas(string term)
        {
            return AsoDAL.GetEmpresas(term);
        }

        public dynamic GetEmpresaLabora(string codempresalabora)
        {
            return AsoDAL.GetEmpresaLabora(codempresalabora);
        }
        

        public ICollection<dependenciasempresas> GetDependencias(string term, string id)
        {
            return AsoDAL.GetDependencias(term, id);
        }

        public ICollection<ciiu> GetCiiu(string term)
        {
            return AsoDAL.GetCiiu(term);
        }

        public ICollection<divisionciiu> GetDivisionciiu(string term, string id)
        {
            return AsoDAL.GetDivisionciiu(term, id);
        }

        public bool Edit(int id, nits nit, asociados aso)
        {
            try
            {
                if (nit.telefono1 == null)
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese un teléfono");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return AsoDAL.Edit(id,nit,aso);
        }

        public nits GetNitsAsociado(int id)
        {
            return AsoDAL.GetNitsAsociado(id);
        }

        public asociados GetAsociadoRegistro(int identificacion, string correo)
        {
            try
            {
                if (correo == null || correo=="")
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese un correo");
                }             

            }
            catch (Exception e)
            {

                throw e;
            }
          

            return AsoDAL.GetAsociadoRegistro(identificacion, correo);
        }

        public string GetAgenciasxNit(int nitaso)
        {
            return AsoDAL.GetAgenciasxNit(nitaso);
        }

        public nits GetNitsoRegistro(int identificacion, string correo)
        {
            try
            {
                if (correo == null || correo == "")
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese un correo");
                }

            }
            catch (Exception e)
            {

                throw e;
            }


            return AsoDAL.GetNitsoRegistro(identificacion, correo);
        }
    }
}
