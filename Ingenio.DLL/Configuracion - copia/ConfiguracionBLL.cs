using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.DAL.Configuracion;
using Ingenio.BO.Ingenio;
using System.Text.RegularExpressions;

namespace Ingenio.BLL.Configuracion
{
    public class ConfiguracionBLL
    {
        ConfiguracionDAL configDAL = new ConfiguracionDAL();

        public ICollection<Estados> getEstados(int id, bool NoticiaCofiFunda)
        {

            return configDAL.getEstados(id, NoticiaCofiFunda);
        }

        public bool CreateNoticia(Estados estados)
        {
            try
            {
                string expresionUrl = @"[^-A-Za-z0-9]+";//exprecion para url
                if (estados.Imagen == null || estados.Imagen == "")
                {
                    throw new Excepciones("Error PEC-IMG-001", "Suba una imagen previa");
                }
                if (estados.Titulo == null || estados.Titulo == "")
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese un titulo");
                }
                if (estados.Html == null || estados.Html == "")
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese una noticia o evento");
                }
                if(Regex.IsMatch(estados.Url, expresionUrl)){
                    throw new Excepciones("Error PEC-NOM-001", "Algo salio mal con el enlace permanente");
                }

                estados.Titulo = estados.Titulo.Trim();// Estados.Titulo.Trim()
                return configDAL.CreateNoticia(estados);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICollection<Sliders> getSlidersHomeFundacion()
        {
            return configDAL.getSlidersHomeFundacion();
        }

        public ICollection<Estados> getEstadoHomeFudacion()
        {
            return configDAL.getEstadoHomeFudacion();
        }

        public Estados getEstadoIndexCofinal(string Id)
        {
            return configDAL.getEstadoIndexCofinal(Id);
        }

        public ICollection<Sliders> getSlidersHome()
        {
            return configDAL.getSlidersHome();
        }

        public ICollection<Estados> getEstadoHome()
        {
            return configDAL.getEstadoHome();
        }

        public bool DeleteEstado(int id)
        {
            return configDAL.DeleteEstado(id);
        }

        public Estados getEstado(int id, int id_user, bool NoticiaCofiFunda)
        {
            return configDAL.getEstado(id, id_user, NoticiaCofiFunda);
        }

        public bool EditNoticia(Estados estados)
        {
            try
            {
                if (estados.Titulo == null || estados.Titulo == "")
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese un titulo");
                }
                if (estados.Html == null || estados.Html == "")
                {
                    throw new Excepciones("Error PEC-NOM-001", "Ingrese una noticia o evento");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return configDAL.EditNoticia(estados);
        }

        public bool UpdateSlider(Sliders d, bool eliminar)
        {
            return configDAL.UpdateSlider(d, eliminar);
        }      

        public ICollection<Sliders> getSliders(bool TipoSlider)
        {
            return configDAL.getSliders(TipoSlider);
        }

        public Sliders GetItem(int id)
        {
            return configDAL.GetItem(id);
        }

        public bool CreateRutaGaleria(Galeria galeria)
        {
            return configDAL.CreateRutaGaleria(galeria);
        }

        public ICollection<Galeria> GetImganes(int id_Usuario)
        {
            return configDAL.GetImganes(id_Usuario);
        }

        public bool DeleteGaleria(int id_Usuario, string src)
        {
            return configDAL.DeleteGaleria(id_Usuario, src);
        }
    }
}
