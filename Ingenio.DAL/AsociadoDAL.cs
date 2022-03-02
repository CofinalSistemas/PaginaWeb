using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO;
using System.Dynamic;
using Ingenio.BO.Ingenio;

namespace Ingenio.DAL
{

    public class AsociadoDAL
    {
        ModelContainer db = new ModelContainer();
        IngenioEntities dbi = new IngenioEntities();
        public asociados GetAsociado(int id)
        {
            try
            {
                asociados aso = (from a in db.asociados
                                 where
                                 a.cedulasociado == id && a.estado == "A"
                                 select a).FirstOrDefault();
                if (aso == null)
                {
                    //throw new Excepciones("POR-EDI-001", "No existe el asociado", "La identificación no corresponde a ningun asociado o se encuentra inactivo.");
                    return null;
                }
                return aso;
            }
            catch (Exception e)
            {
                return null;
                //throw e;
            }
        }

        public bool EliminarAso(int id)
        {
            string identi = Convert.ToString(id);
            Asociados_Ingenio estado = (from a in dbi.Asociados_Ingenio where a.Identificacion == identi select a).FirstOrDefault();
            if (estado == null) return false;
            dbi.Asociados_Ingenio.Remove(estado);
            return dbi.SaveChanges() == 1 ? true : false; 
        }

        public ICollection<Asociados_Ingenio> asociados()
        {
            ICollection<Asociados_Ingenio> aso = (from a in dbi.Asociados_Ingenio select a).ToList();
            return aso;
        }

        public ICollection<profesiones> GetProfesiones(string term)
        {
            ICollection<profesiones> profesion = (from p in db.profesiones
                                                  where p.nombreprofesion.Contains(term)
                                                  select p).ToList();
            return profesion;
        }

        public dynamic GetEmpresaLabora(string codempresalabora)
        {
            empresas empresalabora = (from codEmpreLabora in db.empresas
                                      where codEmpreLabora.codempresa.Trim() == codempresalabora
                                      select codEmpreLabora
                                   ).FirstOrDefault(); // valor unico
            return empresalabora;
        }        

        public ICollection<divisionciiu> GetDivisionciiu(string term, string id)
        {
            ICollection<divisionciiu> divisciiu = (from p in db.divisionciiu
                                                   where p.nombre.Contains(term) && p.codciiu == id
                                                   select p).ToList(); //para retornar lista de los departamentos
            return divisciiu;
        }

        public bool Edit(int id, nits nit, asociados aso)
        {
            try
            {
                nits nitt = (from n in db.nits where n.nit == id select n).FirstOrDefault(); //FirstOrDefault() : obtine el primero y si no lo encuetra retorna null
                asociados asoc = (from a in db.asociados where a.cedulasociado == id select a).FirstOrDefault();
                asoc.estadocivil = aso.estadocivil;
                asoc.fechanacimiento = aso.fechanacimiento;
                nitt.direccion = nit.direccion;
                nitt.codpais = nit.codpais;
                nitt.coddepartamento = nit.coddepartamento;
                nitt.codciudad = nit.codciudad;
                nitt.codzona = nit.codzona;
                nitt.codcomuna = nit.codcomuna;
                nitt.codbarrio = nit.codbarrio;
                nitt.telefono1 = nit.telefono1;
                nitt.extension1 = nit.extension1;
                nitt.telefono2 = nit.telefono2;
                nitt.extencion2 = nit.extencion2;
                nitt.celular = nit.celular;
                nitt.email = nit.email;
                asoc.estudios = aso.estudios;
                asoc.codprofesion = aso.codprofesion;
                asoc.codempresa = aso.codempresa;
                asoc.codempresalabora = aso.codempresalabora;
                asoc.coddependencia = aso.coddependencia;
                nitt.codciiu = nit.codciiu;
                nitt.coddivision = nit.coddivision;

                int res = db.SaveChanges();
                return res == 2 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string GetAgenciasxNit(int agen)
        {
            var agencia = (from ag in db.agencias
                           where ag.codigoagencia == agen
                           select ag.nombreagencia).FirstOrDefault().ToString().Trim();
            return agencia;
        }

        public nits GetNitsAsociado(int id)
        {
            try
            {
                nits aso = (from asoc in db.nits
                            where asoc.nit == id && asoc.estado == "A"
                            select asoc).FirstOrDefault();
                if (aso == null)
                {
                    throw new Excepciones("POR-EDI-001", "No existe el asociado", "El usuario no es asociado de cofinal o esta inactivo");
                }
                return aso;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public nits GetNitsoRegistro(int identificacion, string correo)
        {
            try
            {
                nits aso = (from asoc in db.nits
                                 where asoc.nit == identificacion && asoc.estado == "A" && asoc.email == correo
                                 select asoc).FirstOrDefault();
                if (aso == null)
                {
                    throw new Excepciones("POR-EDI-001", "No existe el asociado", "El usuario no es asociado de cofinal o los datos son incorrectos");
                }
                return aso;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public asociados GetAsociadoRegistro(int identificacion, string correo)
        {
            try
            {
                asociados aso = (from asoc in db.asociados
                                 join nt in db.nits on asoc.cedulasociado equals nt.nit
                                 where asoc.cedulasociado == identificacion && asoc.estado == "A" && nt.email==correo
                                 select asoc).FirstOrDefault();
                if (aso == null)
                {
                    //throw new Excepciones("POR-EDI-001", "No existe el asociado", "El usuario no es asociado de cofinal o los datos son incorrectos");
                }
                return aso;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ICollection<ciiu> GetCiiu(string term)
        {
            ICollection<ciiu> cii = (from p in db.ciiu
                                     where p.nombre.Contains(term)
                                     select p).ToList();
            return cii;
        }

        public ICollection<empresas> GetEmpresas(string term)
        {

            ICollection<empresas> empresa = (from p in db.empresas
                                             where p.nombreempresa.Contains(term)
                                             select p).ToList();
            return empresa;
        }

        public ICollection<dependenciasempresas> GetDependencias(string term, string id)
        {
            ICollection<dependenciasempresas> dependencia = (from p in db.dependenciasempresas
                                                             where p.nombredependencia.Contains(term) && p.codempresa == id
                                                             select p).ToList(); //para retornar lista de los departamentos
            return dependencia;
        }


        public object GetAgencias(asociados aso)
        {
            var agencia = (from ag in db.agencias
                           join n in db.nits on ag.codigoagencia equals n.agencia
                           where ag.codigoagencia == aso.nits.agencia
                           select ag.nombreagencia).FirstOrDefault().ToString().Trim();
            return agencia;
        }

        public nits Getnit(int id)
        {
            nits _a = (from a in db.nits
                       where
                     a.nit == id
                     && a.estado == "A"
                       select a).FirstOrDefault();
            return _a;

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene los movimientos historicos
        /// </summary>
        /// <param name="anio">tipo entero</param>
        /// <param name="mes">tipo entero</param>
        /// <param name="nit">tipo entero</param>
        /// <returns></returns>
        public dynamic GETAhorros(int anio, int mes, int nit)
        {


            dynamic ahorro = (from a in db.hahorrospermanentes
                              where (anio == 1900 ? a.año >= 1900 : a.año == anio) && (mes == 0 ? a.mes > 0 : a.mes == mes) && a.Cedulasociado == nit && a.saldoTotal>0
                              && (anio == 1900 ? a.f_ultimatransaccion == (from b in db.hahorrospermanentes where b.Cedulasociado == nit select b.f_ultimatransaccion).Max() : 1 == 1)
                              && (anio == 1900 ? a.año == (from b in db.hahorrospermanentes where b.Cedulasociado == nit select b.año).Max() : 1 == 1)
                              select new { a.año, a.mes, a.codlinea, a.numerocuenta, nombre = "DEPOSITO DE AHORRO PERMANENTE", a.fechainicio, fechaVencimiento = "", cuota = 0, a.saldoTotal, Interes = 0 }).ToList();

            ICollection<dynamic> ahorroP = new List<dynamic>();

            dynamic d = new ExpandoObject();
            foreach (var item in ahorro)
            {
                d = new ExpandoObject();
                d.año = item.año;
                d.mes = item.mes;
                d.codlinea = item.codlinea;
                d.numerocuenta = item.numerocuenta;
                d.nombre = item.nombre;
                d.fechainicio = item.fechainicio;
                d.fechavencimiento = item.fechaVencimiento;
                d.cuota = item.cuota;
                d.saldototal = item.saldoTotal;
                d.interes = item.Interes;

                ahorroP.Add(d);
                if (anio == 1900) break;
            }

            var sociales = (
                             from a in db.hAportesSociales
                             where (anio == 1900 ? a.año >= 1900 : a.año == anio) && (mes == 0 ? a.mes > 0 : a.mes == mes) && a.cedulasociado == nit && a.saldoTotal > 0
                             && (anio == 1900 ? a.f_ultimatransaccion == (from b in db.hAportesSociales where b.cedulasociado == nit select b.f_ultimatransaccion).Max() : 1 == 1)
                             && (anio == 1900 ? a.año == (from b in db.hAportesSociales where b.cedulasociado == nit select b.año).Max() : 1 == 1)
                             select new { a.año, a.mes, a.codlinea, a.numerocuenta, nombre = "APORTES SOCIALES", a.fechaapertura, fechaVencimiento = "", a.valorcuota, a.saldoTotal, Interes = 0 }
                             ).ToList();

            ICollection<dynamic> ahorroS = new List<dynamic>();

            foreach (var item in sociales)
            {
                d = new ExpandoObject();
                d.año = item.año;
                d.mes = item.mes;
                d.codlinea = item.codlinea;
                d.numerocuenta = item.numerocuenta;
                d.nombre = item.nombre;
                d.fechainicio = item.fechaapertura;
                d.fechavencimiento = item.fechaVencimiento;
                d.cuota = item.valorcuota;
                d.saldototal = item.saldoTotal;
                d.interes = item.Interes;
                ahorroS.Add(d);
                if (anio == 1900) break;
            }

            var atermino = (from a in db.hahorrosAtermino
                            where (anio == 1900 ? a.año >= 1900 : a.año == anio) && (mes == 0 ? a.mes > 0 : a.mes == mes) && a.Cedulasociado == nit && a.saldototal > 0
                            && (anio == 1900 ? a.f_ultimatransaccion == (from b in db.hahorrosAtermino where b.Cedulasociado == nit select b.f_ultimatransaccion).Max() : 1 == 1)
                            && (anio == 1900 ? a.año == (from b in db.hahorrosAtermino where b.Cedulasociado == nit select b.año).Max() : 1 == 1)
                            select new { a.año, a.mes, a.codlinea, a.numerocuenta, nombre = "AHORROS A TERMINO", a.fechainicio, a.fechavencimientocapital, a.valortitulo, a.saldototal, a.interescausado })
            .ToList();

            ICollection<dynamic> ahorroAT = new List<dynamic>();

            foreach (var item in atermino)
            {
                d = new ExpandoObject();
                d.año = item.año;
                d.mes = item.mes;
                d.codlinea = item.codlinea;
                d.numerocuenta = item.numerocuenta;
                d.nombre = item.nombre;
                d.fechainicio = item.fechainicio;
                d.fechavencimiento = item.fechavencimientocapital;
                d.cuota = item.valortitulo;
                d.saldototal = item.saldototal;
                d.interes = item.interescausado;
                ahorroAT.Add(d);
                if (anio == 1900) break;
            }

            var alavista = (from a in db.hahorrosalavista
                            where (anio == 1900 ? a.año >= 1900 : a.año == anio) && (mes == 0 ? a.mes > 0 : a.mes == mes) && a.Cedulasociado == nit && a.saldoTotal > 0
                            && (anio == 1900 ? a.f_ultimatransaccion == (from b in db.hahorrosAtermino where b.Cedulasociado == nit select b.f_ultimatransaccion).Max() : 1 == 1)
                            && (anio == 1900 ? a.año == (from b in db.hahorrosAtermino where b.Cedulasociado == nit select b.año).Max() : 1 == 1)
                            select new { a.año, a.mes, a.codlinea, a.numerocuenta, nombre = "AHORROS A TERMINO", a.fechainicio, a.f_ultimatransaccion, a.valorcuota, a.saldoTotal, a.interescausado })
            .ToList();

            ICollection<dynamic> ahorroAlaV = new List<dynamic>();

            foreach (var item in alavista)
            {
                d = new ExpandoObject();
                d.año = item.año;
                d.mes = item.mes;
                d.codlinea = item.codlinea;
                d.numerocuenta = item.numerocuenta;
                d.nombre = item.nombre;
                d.fechainicio = item.fechainicio;
                d.fechavencimiento = item.f_ultimatransaccion;
                d.cuota = item.valorcuota;
                d.saldototal = item.saldoTotal;
                d.interes = item.interescausado;
                ahorroAlaV.Add(d);
                if (anio == 1900) break;
            }

            var acontractual = (from a in db.hahorrosContractual
                            where (anio == 1900 ? a.año >= 1900 : a.año == anio) && (mes == 0 ? a.mes > 0 : a.mes == mes) && a.Cedulasociado == nit && a.saldoTotal > 0
                            && (anio == 1900 ? a.f_ultimatransaccion == (from b in db.hahorrosAtermino where b.Cedulasociado == nit select b.f_ultimatransaccion).Max() : 1 == 1)
                            && (anio == 1900 ? a.año == (from b in db.hahorrosAtermino where b.Cedulasociado == nit select b.año).Max() : 1 == 1)
                            select new { a.año, a.mes, a.codlinea, a.numerocuenta, nombre = "AHORROS A TERMINO", a.fechainicio, a.fechaultvencimiento, a.valorcuota, a.saldoTotal, a.interescausado })
            .ToList();

            ICollection<dynamic> ahorroAC = new List<dynamic>();

            foreach (var item in acontractual)
            {
                d = new ExpandoObject();
                d.año = item.año;
                d.mes = item.mes;
                d.codlinea = item.codlinea;
                d.numerocuenta = item.numerocuenta;
                d.nombre = item.nombre;
                d.fechainicio = item.fechainicio;
                d.fechavencimiento = item.fechaultvencimiento;
                d.cuota = item.valorcuota;
                d.saldototal = item.saldoTotal;
                d.interes = item.interescausado;
                ahorroAC.Add(d);
                if (anio == 1900) break;
            }

            // CREDITOS

            dynamic credito = (from a in db.hcreditos
                               join b in db.destinos on a.coddestino equals b.Coddestino
                               where (anio == 1900 ? a.año >= 1900 : a.año == anio) && (mes == 0 ? a.mes > 0 : a.mes == mes) && a.cedulasociado == nit && a.saldocapital > 0
                                      && (anio == 1900 ? a.f_ultimopago == (from b in db.hcreditos where b.cedulasociado == nit select b.f_ultimopago).Max() : 1 == 1)
                                      && (anio == 1900 ? a.año == (from b in db.hcreditos where b.cedulasociado == nit select b.año).Max() : 1 == 1)
                               select new { a.saldocapital, a.cuotasmora, a.año, a.mes, a.cedulasociado, a.codlinea, b.nombredestino, a.F_iniciofinanciacion, a.fechavence, a.anualidad, a.saldoponersedia, a.pagare, a.estado })
                        .ToList();

            ICollection<dynamic> C_Creditos = new List<dynamic>();

            foreach (var item in credito)
            {
                d = new ExpandoObject();
                d.año = item.año;
                d.mes = item.mes;
                d.cedulasociado = item.cedulasociado;
                d.cuotasmora = item.cuotasmora;
                d.codlinea = item.codlinea;
                d.nombre = item.nombredestino;
                d.fechainicio = item.F_iniciofinanciacion;
                d.fechavencimiento = item.fechavence;
                d.anulidad = item.anualidad;
                d.saldoponersedia = item.saldoponersedia;
                d.pagare = item.pagare;
                d.estado = item.estado;
                d.saldocapital = item.saldocapital;
                C_Creditos.Add(d);
                if (anio == 1900) break;
            }

            //////
            d = new ExpandoObject();
            d.ahorroPermanente = ahorroP;
            d.ahorroSocial = ahorroS;
            d.ahorroAtermino = ahorroAT;
            d.ahorroContractual = ahorroAC;
            d.ahorroVista = ahorroAlaV;
            d.Creditos = C_Creditos;
            return d;
        }
    }
}

