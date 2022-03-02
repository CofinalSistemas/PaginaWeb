using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO;

namespace Ingenio.DAL
{
    public class ToolDal
    {
        ModelContainer db = new ModelContainer();
        public ICollection<pais> GetPaises(string rpais)
        {
            ICollection<pais> paises = (from p in db.pais
                                        where p.nombre.Contains(rpais)
                                        select p).ToList();
            return paises;
        }

        public ICollection<departamentos> GetDepartamentos(string term, string id)
        {
            ICollection<departamentos> departamento = (from p in db.departamentos
                                                       where p.nombredepartamento.Contains(term) && p.codpais == id
                                                       select p).ToList(); //para retornar lista de los departamentos
            return departamento;
        }

        public departamentos GetDepartamento(string codDepartamento)
        {
            try
            {
                departamentos depart = (from codDepar in db.departamentos
                                        where codDepar.coddepartamento.Trim() == codDepartamento
                                        select codDepar
                                           ).FirstOrDefault(); // valor unico
                return depart;
            }
            catch ( Exception e )
            {

                throw;
            }
        }

        public ICollection<ciudades> GetCiudades(string term, string id)
        {
            ICollection<ciudades> ciudades = (from p in db.ciudades
                                              where p.nombreciudad.Contains(term) && p.coddepartamento == id
                                              select p).ToList(); //para retornar lista de las ciudades
            return ciudades;
        }

        public ciudades GetCiudad(string codCiudad)
        {
            ciudades ciudad = (from codCiud in db.ciudades
                               where codCiud.codciudad.Trim() == codCiudad
                               select codCiud
                                    ).FirstOrDefault(); // valor unico
            return ciudad;
        }

        public ICollection<zonasgeograficas> GetZonas(string term, string id)
        {
            ICollection<zonasgeograficas> zonas = (from p in db.zonasgeograficas
                                                   where p.nombrezona.Contains(term) && p.codciudad == id
                                                   select p).ToList(); //para retornar lista de las ciudades
            return zonas;
        }

        public zonasgeograficas GetZona(string codZona)
        {
            zonasgeograficas zona = (from codZon in db.zonasgeograficas
                                     where codZon.codzona.Trim() == codZona
                                     select codZon
                                   ).FirstOrDefault(); // valor unico
            return zona;
        }

        public ICollection<comunas> GetComunas(string term, string id)
        {
            ICollection<comunas> comunas = (from p in db.comunas
                                            where p.codcomuna.Contains(term) && p.codzona == id
                                            select p).ToList(); //para retornar lista de las ciudades
            return comunas;
        }

        public comunas GetComuna(string codComuna)
        {
            comunas comuna = (from codCom in db.comunas
                              where codCom.codcomuna.Trim() == codComuna
                              select codCom
                                   ).FirstOrDefault(); // valor unico
            return comuna;
        }

        public ICollection<barrios> GetBarrios(string term, string id)
        {
            ICollection<barrios> barrios = (from p in db.barrios
                                            where p.codbarrio.Contains(term) && p.codzona == id
                                            select p).ToList(); //para retornar lista de las ciudades
            return barrios;
        }

        public barrios GetBarrio(string codBarrio)
        {
            barrios barrio = (from codBarr in db.barrios
                              where codBarr.codbarrio.Trim() == codBarrio
                              select codBarr
                                   ).FirstOrDefault(); // valor unico
            return barrio;
        }
    }
}
