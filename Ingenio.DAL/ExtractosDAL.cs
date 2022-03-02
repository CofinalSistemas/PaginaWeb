using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO;
using System.Dynamic;
using System.Data;
using Ingenio.BO.Ingenio;
using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Ingenio.DAL
{
    /// <summary>
    /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
    /// Consultas de movimientos ahorro y creditos por pagare y nro. cuenta
    /// </summary>
    public class ExtractosDAL
    {
        ModelContainer db = new ModelContainer();
        IngenioEntities dbing = new IngenioEntities();
        
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene un seguimiento de credito
        /// </summary>
        /// <param name="txtpagare">tipo entero</param>
        /// <param name="txtcedula">tipo entero</param>
        /// <returns></returns>
        public seguimientocredito GetSeguimiento(int txtpagare, int txtcedula)
        {
            try
            {
                seguimientocredito _a = (from a in db.seguimientocredito
                                where
                                a.cedulasociado == txtcedula && a.pagare==txtpagare
                                         orderby a.fechaTrabajo ascending
                                         select a).FirstOrDefault();
                if (_a == null)
                {
                    // throw new Excepciones("POR-EDI-00901", "No existe la Persona", "No se encontro la cedula solicitada o esta inactiva");
                }

                return _a;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Param_dian SaveInteres(Param_dian dat)
        {

            //var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)dbing).ObjectContext;
            //objCtx.ExecuteStoreCommand("TRUNCATE TABLE [Param_dian]");
            //var all = from c in dbing.Param_dian select c;
            //dbing.Param_dian.RemoveRange(all);
            //dbing.SaveChanges();

            //dbing.Param_dian.Add(dat);
            //dbing.SaveChanges();
            //return null;

            Param_dian f = dbing.Param_dian.FirstOrDefault(x => x.ID == dat.ID);
            f.INTERES_DEDUCIBLES =dat.INTERES_DEDUCIBLES;
            f.INTERES_PAGADOS = dat.INTERES_PAGADOS;
            dbing.SaveChanges();
            //dbing.Entry(dat).State = EntityState.Modified;
            //db.SaveChanges();
            return null;
        }

        public dynamic GETpagares(int id)
        {
            dynamic perman = (from a in db.creditos
                              where a.cedulasociado == id && a.saldocapital > 0
                              orderby a.fechasistema ascending
                              select new { a.pagare, a.capitalinicial }).ToList();
            return perman;
        }

        public ICollection<Param_dian> listinteres()
        {
            ICollection<Param_dian> aso = (from a in dbing.Param_dian select a).ToList();
            return aso;
        }

        public Param_dian actuainteres(int num)
        {
            Param_dian aso = dbing.Param_dian.Find(num);
            return aso;
        }

        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene las cuentas con saldo total>0
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns></returns>
        public dynamic GetCuentas(int cedula)
        {
            dynamic perman = (from a in db.ahorrospermanentes
                                where a.Cedulasociado == cedula && a.saldoTotal>0
                                orderby a.fechainicio ascending
                                select new { a.numerocuenta, a.fechainicio,a.valor,a.saldoTotal, a.estado }).ToList();

            ICollection<dynamic> cuenta_perm = new List<dynamic>();

            dynamic d = new ExpandoObject();
            foreach (var item in perman)
            {
                d = new ExpandoObject();
                d.numerocuenta = item.numerocuenta;
                d.fechainicio = item.fechainicio;
                d.valor = item.valor;
                d.saldoTotal = item.saldoTotal;
                d.estado = item.estado;
                cuenta_perm.Add(d);
            }

            //
            dynamic alavista = (from a in db.ahorrosalavista
                              where a.Cedulasociado == cedula && a.saldoTotal > 0
                                orderby a.fechainicio ascending
                              select new { a.numerocuenta, a.fechainicio,valor= a.valorcuota, a.saldoTotal, a.estado }).ToList();

            ICollection<dynamic> cuenta_alavista = new List<dynamic>();

            d = new ExpandoObject();
            foreach (var item in alavista)
            {
                d = new ExpandoObject();
                d.numerocuenta = item.numerocuenta;
                d.fechainicio = item.fechainicio;
                d.valor = item.valor;
                d.saldoTotal = item.saldoTotal;
                d.estado = item.estado;
                cuenta_alavista.Add(d);
            }

            //
            dynamic atermino = (from a in db.ahorrosAtermino
                                where a.Cedulasociado == cedula && a.saldototal > 0
                                orderby a.fechainicio ascending
                                select new { a.numerocuenta, a.fechainicio,valor= a.valortitulo, saldoTotal= a.saldototal,estado=a.modalidad }).ToList();

            ICollection<dynamic> cuenta_atermino = new List<dynamic>();

            d = new ExpandoObject();
            foreach (var item in atermino)
            {
                d = new ExpandoObject();
                d.numerocuenta = item.numerocuenta;
                d.fechainicio = item.fechainicio;
                d.valor = item.valor;
                d.saldoTotal = item.saldoTotal;
                d.estado = item.estado;
                cuenta_atermino.Add(d);
            }

            //
            dynamic contractual = (from a in db.ahorrosContractual
                                where a.Cedulasociado == cedula && a.saldoTotal > 0
                                   orderby a.fechainicio ascending
                                select new { a.numerocuenta, a.fechainicio,valor= a.valorcuota, a.saldoTotal, a.estado }).ToList();

            ICollection<dynamic> cuenta_contractual= new List<dynamic>();

            d = new ExpandoObject();
            foreach (var item in contractual)
            {
                d = new ExpandoObject();
                d.numerocuenta = item.numerocuenta;
                d.fechainicio = item.fechainicio;
                d.valor = item.valor;
                d.saldoTotal = item.saldoTotal;
                d.estado = item.estado;
                cuenta_contractual.Add(d);
            }

            //
            dynamic creditos = (from a in db.creditos
                                   join des in db.destinos on a.coddestino equals des.Coddestino
                                   where a.cedulasociado == cedula && a.saldocapital>0
                                   orderby a.F_iniciofinanciacion ascending
                                   select new { a.pagare, des.nombredestino, a.F_iniciofinanciacion, a.cuotasmora,a.anualidad,a.saldocapital,a.intcorriente, a.estado }).ToList();

            ICollection<dynamic> cuenta_creditos = new List<dynamic>();

            d = new ExpandoObject();
            foreach (var item in creditos)
            {
                d = new ExpandoObject();
                d.pagare = item.pagare;
                d.coddestino = item.nombredestino;
                d.F_iniciofinanciacion = item.F_iniciofinanciacion;
                d.cuotasmora = item.cuotasmora;
                d.anualidad = item.anualidad;
                d.saldocapital = item.saldocapital;
                d.intcorriente = item.intcorriente;
                d.estado = item.estado;
                cuenta_creditos.Add(d);
            }
            //
            dynamic aportes = (from a in db.aportessociales
                                where a.cedulasociado == cedula && a.saldoTotal > 0
                               orderby a.fechaapertura ascending
                                select new { a.numerocuenta, a.fechaapertura, a.valorcuota, a.saldoTotal, a.estado }).ToList();

            ICollection<dynamic> cuenta_aportes = new List<dynamic>();

            d = new ExpandoObject();
            foreach (var item in aportes)
            {
                d = new ExpandoObject();
                d.numerocuenta = item.numerocuenta;
                d.fechaapertura = item.fechaapertura;
                d.valorcuota = item.valorcuota;
                d.saldoTotal = item.saldoTotal;
                d.estado = item.estado;
               
                cuenta_aportes.Add(d);
            }



            d = new ExpandoObject();
            d.Perm = cuenta_perm;
            d.Alavista = cuenta_alavista;
            d.Termino = cuenta_atermino;
            d.Contractual = cuenta_contractual;
            d.Creditos = cuenta_creditos;
            d.Aportes = cuenta_aportes;
            return d;
        }

        public dynamic transaccion(DateTime? inicial, DateTime? final, int chk)
        {
            dbing.P_Transaccciones_Uiaf(inicial, final, chk);

            dynamic query = dbing.P_Operador_UiAf4(inicial,final).ToList();

            ICollection<dynamic> cuenta_perm = new List<dynamic>();

            dynamic d = new ExpandoObject();
            foreach (P_Operador_UiAf4_Result item in query)
            {
                d = new ExpandoObject();
                d.fechaseguimiento = item.fechaseguimiento;
                d.Valor_Transaccion = item.Valor_Transaccion;
                d.codciudad = item.codciudad;
                d.nombreagencia = item.nombreagencia;
                d.tipoproducto = item.tipoproducto;
                d.numerocuenta = item.numerocuenta;
                d.codlinea = item.codlinea;
                d.tipoidentificacion = item.tipoidentificacion == "C" ? 13 :
                                                  item.tipoidentificacion == "N" ? 31 :
                                                  item.tipoidentificacion == "H" ? 13 :
                                                  item.tipoidentificacion == "E" ? 21 :
                                                  item.tipoidentificacion == "R" ? 11 :
                                                  item.tipoidentificacion == "T" ? 12 :
                                                  item.tipoidentificacion == "O" ? 00 :
                                                  item.tipoidentificacion == "U" ? 13 :
                                                  item.tipoidentificacion == "P" ? 41 : 00;
                d.cedulasociado = item.cedulasociado;
                d.primerapellido = item.primerapellido;
                d.segundoapellido = item.segundoapellido;
                d.nombres = item.nombres;
                d.segundonombre = item.segundonombre;
                d.nombreasociado = item.nombreasociado;
                d.nombre = item.nombre;
                d.salario = item.salario;
                d.tipo_transaccion = item.TIPO_TRANSACCION;
                d.codoperador = item.operador;
                cuenta_perm.Add(d);
            }

            d = new ExpandoObject();
            d.Perm = cuenta_perm;
            return d;

        }

        public aportessociales GetInfoAhorro3(int txtcedula)
        {
            try
            {
                aportessociales _a = (from a in db.aportessociales
                                      where
                                      a.cedulasociado == txtcedula

                                      select a).FirstOrDefault();
                if (_a == null)
                {
                    // throw new Excepciones("POR-EDI-001", "No existe la Persona", "No se encontro la cedula solicitada o esta inactiva");
                }

                return _a;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public dynamic GetEspecificoContractual(int nroc, int id)
        {
            string nrocuenta = nroc.ToString();
            var atermino = (from a in db.ahorrosContractual
                            where a.Cedulasociado == id && a.saldoTotal > 0 && a.numerocuenta == nrocuenta
                            orderby a.fechainicio ascending
                            select new { a.numerocuenta, fechainicio= a.fechainicio.ToString().Substring(0, 11), valor = a.valorcuota, a.saldoTotal, a.estado}).ToList();

            return atermino;
        }

        public dynamic GetEspecificoAsociales(string nroc, int id)
        {
            
            var atermino = (from a in db.aportessociales
                            where a.cedulasociado == id && a.saldoTotal > 0 && a.numerocuenta == nroc
                            orderby a.fechaapertura ascending
                            select new { a.numerocuenta, fechaapertura= a.fechaapertura.ToString().Substring(0, 11), valor = a.valorcuota, a.saldoTotal, a.estado }).ToList();

            return atermino;
        }

        public dynamic GetEspecificoPermanente(int nroc, int id)
        {
            string nrocuenta = nroc.ToString();
            var atermino = (from a in db.ahorrospermanentes
                            where a.Cedulasociado == id && a.saldoTotal > 0 && a.numerocuenta == nrocuenta
                            orderby a.fechainicio ascending
                            select new { a.numerocuenta, fechainicio= a.fechainicio.ToString().Substring(0, 11), valor = a.valor, a.saldoTotal,a.estado}).ToList();

            return atermino;
        }

        public dynamic Getasociales(int id)
        {
            var sociales = (from a in db.aportessociales
                               where a.cedulasociado == id && a.saldoTotal > 0
                               orderby a.fechaapertura ascending
                               select new { a.numerocuenta, a.fechaapertura, valor = a.valorcuota, a.saldoTotal, a.estado }).ToList();

            return sociales;
        }

        public dynamic Getacontractual(int id)
        {
            var apermanente = (from a in db.ahorrosContractual
                               where a.Cedulasociado == id && a.saldoTotal > 0
                               orderby a.fechainicio ascending
                               select new { a.numerocuenta, a.fechainicio, valor = a.valorcuota, a.saldoTotal, a.estado }).ToList();

            return apermanente;
        }

        public dynamic Getapermanente(int id)
        {
            var apermanente = (from a in db.ahorrospermanentes
                            where a.Cedulasociado == id && a.saldoTotal > 0
                            orderby a.fechainicio ascending
                            select new { a.numerocuenta, a.fechainicio, valor = a.valor, a.saldoTotal, a.estado }).ToList();

            return apermanente;
        }

        public dynamic GetAtermino(int id)
        {
            var alavista = (from a in db.ahorrosAtermino
                            where a.Cedulasociado == id && a.saldototal > 0
                            orderby a.fechainicio ascending
                            select new { a.numerocuenta, a.fechainicio, valor = a.valortitulo, a.saldototal, estado="" }).ToList();

            return alavista;
        }

        public dynamic GetEspecificoAtermino(int nroc, int id)
        {
            string nrocuenta = nroc.ToString();
            var atermino = (from a in db.ahorrosAtermino
                            where a.Cedulasociado == id && a.saldototal > 0 && a.numerocuenta == nrocuenta
                            orderby a.fechainicio ascending
                            select new { a.numerocuenta, fechainicio= a.fechainicio.ToString().Substring(0, 11), valor = a.valortitulo, a.saldototal, estado="" }).ToList();

            return atermino;
        }

        public dynamic GetEspecifico(int nroc, int id)
        {
            string nrocuenta = nroc.ToString();
            var alavista = (from a in db.ahorrosalavista
                            where a.Cedulasociado == id && a.saldoTotal > 0 && a.numerocuenta==nrocuenta
                            orderby a.fechainicio ascending
                            select new { a.numerocuenta, fechainicio= a.fechainicio.ToString().Substring(0,11), valor = a.valorcuota, a.saldoTotal, a.estado }).ToList();

            return alavista;
        }

        public dynamic GetPagos_cuota(int txtcedula, int txtpagare, DateTime f_ultimopago2, DateTime f_ultimopago1)
        {
            var query = (from a in db.seguimientocredito
                         where a.cedulasociado == txtcedula && a.pagare == txtpagare
                         && a.fechaTrabajo <= f_ultimopago2 && a.fechaTrabajo>= f_ultimopago1
                         orderby a.fechaTrabajo ascending
                         select new { a.fechaTrabajo, a.natura, a.totalefectivo, a.recibopago, a.agencia }).ToList();

            ICollection<dynamic> creditosm = new List<dynamic>();

            dynamic d = new ExpandoObject();
            foreach (var item in query)
            {
                d = new ExpandoObject();
                d.fechaTrabajo = item.fechaTrabajo;
                d.natura = item.natura;
                d.totalefectivo = item.totalefectivo;
                d.recibopago = item.recibopago;
                d.agencia = item.agencia;
                creditosm.Add(d);
            }

            return creditosm;
        }

        public dynamic Historicodepagoscreditos(int txtpagare, int txtcedula)
        {
            var query = (from a in db.hcreditos
                                     where
                                     a.cedulasociado == txtcedula && a.pagare == txtpagare
                                     orderby a.f_ultimopago ascending
                                     select new {a.f_ultimopago, a.saldocapital }).ToList();

            ICollection<dynamic> saldocap = new List<dynamic>();
            dynamic d = new ExpandoObject();
            foreach (var item in query)
            {
                d = new ExpandoObject();
                d.saldocapital = item.saldocapital;
                d.f_ultimopago = item.f_ultimopago;
                saldocap.Add(d);
            }

            d = new ExpandoObject();
            d.Datos = saldocap;

            return d;
        }
        public dynamic GetAlaVista(int cedula,int tipo)
        {
            var query = "";
            if (tipo == 1) { 
            var alavista = (from a in db.ahorrosalavista
                                where a.Cedulasociado == cedula && a.saldoTotal > 0
                                orderby a.fechainicio ascending
                                select new { a.numerocuenta, a.fechainicio, valor = a.valorcuota, a.saldoTotal, a.estado }).ToList();

                return alavista;
                
            }
            if (tipo == 2)
            {
                var atermino = (from a in db.ahorrosAtermino
                                where a.Cedulasociado == cedula && a.saldototal > 0
                                orderby a.fechainicio ascending
                                select new { a.numerocuenta, a.fechainicio, valor = a.valortitulo, a.saldototal, estado="" }).ToList();

                return atermino;
            }
            if (tipo == 3)
            {
                var acontratual = (from a in db.ahorrosContractual
                                where a.Cedulasociado == cedula && a.saldoTotal > 0
                                orderby a.fechainicio ascending
                                select new { a.numerocuenta, a.fechainicio, valor = a.valorcuota, a.saldoTotal, a.estado }).ToList();

                return acontratual;
            }
            if (tipo == 4)
            {
                var aportes = (from a in db.aportessociales
                                where a.cedulasociado == cedula && a.saldoTotal > 0
                                orderby a.fechaapertura ascending
                                select new { a.numerocuenta, a.fechaapertura, valor = a.valorcuota, a.saldoTotal, a.estado }).ToList();

                return aportes;
            }
            return query;
        }

        public dynamic GetMov(int id)
        {
            var query = (from t in db.seguimientocredito
                         where t.pagare == id &&( t.natura =="CNGC" || t.natura== "RETT")
                         orderby t.fechaTrabajo descending
                         select new { fechaTrabajo= t.fechaTrabajo.ToString().Substring(0,11), t.totalefectivo, natura = t.natura == "CNGC" ? "VALOR DEL ABONO" : t.natura == "RETT" ? "RETIRO" : "", t.recibopago }).Take(5).ToList();
            

            return query;
        }

        public dynamic GetMovimientos(string v)
        {
            
            var query = (from t in db.sahorrosalavista
                         where t.nrocuenta == v && (t.natura == "CNGT" || t.natura == "RETT")
                         orderby t.fechatrabajo descending
                         select new { fechatrabajo=t.fechatrabajo.ToString().Substring(0, 11), t.valorefectivo,natura= t.natura=="CNGT"?"CONSIGNACION":t.natura=="RETT"?"RETIRO":"",t.detalle}).Take(5).ToList();


            if (query.Count == 0)
            {
                var query1 = (from t in db.sahorrosAtermino
                              where t.nrocuenta == v && (t.natura == "CNGT" || t.natura == "RETT")
                              orderby t.fechatrabajo descending
                              select new { fechatrabajo = t.fechatrabajo.ToString().Substring(0, 11), t.valorefectivo, natura = t.natura == "CNGT" ? "CONSIGNACION" : t.natura == "RETT" ? "RETIRO" : "", t.detalle }).Take(5).ToList();
                
                if (query1.Count == 0)
                {
                    var query2 = (from t in db.sahorrosContractual
                                  where t.nrocuenta == v && (t.natura == "CNGT" || t.natura == "RETT")
                                  orderby t.fechatrabajo descending
                                  select new { fechatrabajo = t.fechatrabajo.ToString().Substring(0, 11), t.valorefectivo, natura = t.natura == "CNGT" ? "CONSIGNACION" : t.natura == "RETT" ? "RETIRO" : "", t.detalle }).Take(5).ToList();
                    

                    if (query2.Count == 0)
                    {
                        var query3 = (from t in db.sahorrospermanentes
                                      where t.nrocuenta == v && (t.natura == "CNGT" || t.natura == "RETT")
                                      orderby t.fechatrabajo descending
                                      select new { fechatrabajo = t.fechatrabajo.ToString().Substring(0, 11), t.valorefectivo, natura = t.natura == "CNGT" ? "CONSIGNACION" : t.natura == "RETT" ? "RETIRO" : "", t.detalle }).Take(5).ToList();
                       

                        if (query3.Count == 0)
                        {
                            var query4 = (from t in db.sAportesSociales
                                          where t.nrocuenta == v && (t.natura == "CNGT" || t.natura == "RETT")
                                          orderby t.fechatrabajo descending
                                          select new { fechatrabajo = t.fechatrabajo.ToString().Substring(0, 11), t.valorefectivo, natura = t.natura == "CNGT" ? "CONSIGNACION" : t.natura == "RETT" ? "RETIRO" : "", t.detalle }).Take(5).ToList();
                            return query4;


                        }
                        else { return query3; }

                    }
                    else { return query2; }

                }
                else { return query1; }

            }else
            { return query; }

            
           
        }

        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene el numero de simulaciones realizadas en un periodo de tiempo 
        /// </summary>
        /// <param name="inicial"> tipo fecha</param>
        /// <param name="final">tipo fecha</param>
        /// <param name="n">tipo cadena </param>
        /// <returns></returns>
        public dynamic GetDatosCDATyCre(DateTime inicial, DateTime final, string n)
        {
            var query = (from t in dbing.SEGUIMIENTO_SIMULADORES
                         where t.TIPO==n && t.FECHA>=inicial && t.FECHA<=final
                         select new { FECHA= t.FECHA.ToString(), t.NOMBRE,t.TELEFONO,t.EMAIL }).ToList();

            return query;

        }
       

        public dynamic GEtTasa2(int plazo)
        {
            var query = (from t in db.tasasplazoahorrotermino
                         where t.plazo == plazo
                         select new { t.PeriodoLiquida,t.tasa }).ToList();

            return query;
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene las operaciones multiples de UIAF en un periodo
        /// </summary>
        /// <param name="inicial"> tipo fecha</param>
        /// <param name="final">tipo fecha</param>
        /// <param name="mostrar">tipo entero</param>
        /// <param name="tipo">tipo cadena</param>
        /// <returns></returns>
        public dynamic GetReporte(DateTime? inicial, DateTime? final,int mostrar,string tipo)
        {
            dynamic reporte="";
           
            ICollection<dynamic> repor = new List<dynamic>();
            dynamic d = new ExpandoObject();

            if (tipo == "C")
            {

                ICollection<dynamic> costomov = new List<dynamic>();
                dynamic multiples = dbing.UIAF_TRANSMULTIPLES2(inicial, final);//


                //dynamic multi_vista = (from s in db.sahorrosalavista
                //                   join a in db.ahorrosalavista on s.nrocuenta equals a.numerocuenta
                //                   join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                //                   where a.codlinea == s.codlinea && s.fechatrabajo >= inicial && s.fechatrabajo <= final
                //                   && s.valorefectivo < 10000000 && s.valorefectivo> 0 && (s.natura == "CNGC" || s.natura == "CNGT" || s.natura == "RETT") 
                //                   group s by new
                //                   {
                //                       s.nrocuenta,
                //                       a.Cedulasociado
                //                   }
                //                   into g
                //                   where g.Sum(s => s.valorefectivo) >= 50000000
                //                   select new
                //                   {
                //                       g.Key.Cedulasociado,
                //                       g.Key.nrocuenta,
                //                       total = g.Sum(s => s.valorefectivo)
                //                   }).ToList();

                //realizado con PROCEDIMIENTO ALMACENADO
                //ICollection<dynamic> costomov = new List<dynamic>();
                //dynamic multiples = dbing.UIAF_MULTIPLES_TRANSAC(ini2, fin2);//crear tabla en DB, y llamarla despues como from t in db.nuevatabla
                var tt = multiples;

                foreach (UIAF_TRANSMULTIPLES2_Result item1 in tt)
                {
                    string tipos = item1.tipo;

                    long CEDULA = item1.cedula;
                    Decimal TOTALEFECTIVO = item1.totalefectivo;
                    string cuenta = item1.nrocuenta;

                    if (tipos == "1")
                    {

                        reporte = (from s in db.sahorrosalavista
                                   join a in db.ahorrosalavista on s.nrocuenta equals a.numerocuenta
                                   join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                                   join ni in db.nits on aso.cedulasociado equals ni.nit
                                   join ag in db.agencias on s.agencia equals ag.codigoagencia
                                   join dd in db.ciiu on ni.codciiu equals dd.codciiu

                                   where a.Cedulasociado == CEDULA && a.numerocuenta == cuenta
                                   && s.fechatrabajo > inicial && s.fechatrabajo <= final
                                   select new
                                   {
                                       fechatransaccion = s.fechatrabajo,
                                       valortransaccion = TOTALEFECTIVO,
                                       moneda = 1,
                                       codigooficina = ag.nombreagencia,
                                       codigodepartamento = ni.codciudad,
                                       tipo_producto = 90,
                                       tipo_transaccion = 2,
                                       a.numerocuenta,
                                       tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                                                          ni.tipoidentificacion == "N" ? 31 :
                                                          ni.tipoidentificacion == "H" ? 13 :
                                                          ni.tipoidentificacion == "E" ? 21 :
                                                          ni.tipoidentificacion == "R" ? 11 :
                                                          ni.tipoidentificacion == "T" ? 12 :
                                                          ni.tipoidentificacion == "O" ? 00 :
                                                          ni.tipoidentificacion == "U" ? 13 :
                                                          ni.tipoidentificacion == "P" ? 41 : 00,
                                       documento = ni.nit,
                                       ni.primerapellido,
                                       ni.segundoapellido,
                                       ni.nombres,
                                       otronombre = ni.segundonombre,
                                       razonsocial = ni.nombreintegrado,
                                       actividadeconomica = dd.nombre,
                                       aso.salario,
                                       ni.digito,
                                       tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                                                          ni.tipoidentificacion == "N" ? 31 :
                                                          ni.tipoidentificacion == "H" ? 13 :
                                                          ni.tipoidentificacion == "E" ? 21 :
                                                          ni.tipoidentificacion == "R" ? 11 :
                                                          ni.tipoidentificacion == "T" ? 12 :
                                                          ni.tipoidentificacion == "O" ? 00 :
                                                          ni.tipoidentificacion == "U" ? 13 :
                                                          ni.tipoidentificacion == "P" ? 41 : 00,
                                       tipodocconsignante = ni.nit,
                                       primerapellidoconsignante = ni.primerapellido,
                                       segundoapellidoconsignante = ni.segundoapellido,
                                       nombreconsignante = ni.nombres,
                                       otronombreconsignante = ni.segundonombre,
                                       operador = s.operador
                                   }).Take(1).ToList();





                        foreach (var item in reporte)
                        {
                            d = new ExpandoObject();
                            d.fechatransaccion = item.fechatransaccion;
                            d.valortransaccion = item.valortransaccion;
                            d.moneda = item.moneda;
                            d.codigooficina = item.codigooficina;
                            d.codigodepartamento = item.codigodepartamento;
                            d.tipo_producto = item.tipo_producto;
                            d.tipo_transaccion = item.tipo_transaccion;
                            d.numerocuenta = item.numerocuenta;
                            d.tipoidentificacion = item.tipoidentificacion;
                            d.cedulasociado = item.documento;
                            d.primerapellido = item.primerapellido;
                            d.segundoapellido = item.segundoapellido;
                            d.nombres = item.nombres;
                            d.segundonombre = item.otronombre;
                            d.razonsocial = item.razonsocial;
                            d.actividadeconomica = item.actividadeconomica;
                            d.salario = item.salario;
                            d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                            d.tipodocconsignante = item.tipodocconsignante;
                            d.primerapellidoconsignante = item.primerapellidoconsignante;
                            d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                            d.nombreconsignante = item.nombreconsignante;
                            d.otronombreconsignante = item.otronombreconsignante;
                            d.digito = item.digito;
                            d.operador = item.operador;
                            repor.Add(d);
                        }
                    }

                    ///
                    //dynamic multi_termino = (from s in db.sahorrosAtermino
                    //                       join a in db.ahorrosAtermino on s.nrocuenta equals a.numerocuenta
                    //                       join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                    //                       where a.codlinea == s.codlinea && s.fechatrabajo >= inicial && s.fechatrabajo <= final
                    //                       && s.valorefectivo < 10000000 && s.valorefectivo > 0 && (s.natura == "CNGC" || s.natura == "CNGT" || s.natura == "RETT") && aso.estado == "A"
                    //                       group s by new
                    //                       {
                    //                           s.nrocuenta,
                    //                           a.Cedulasociado
                    //                       }
                    //                   into g
                    //                       where g.Sum(s => s.valorefectivo) > 50000000
                    //                       select new
                    //                       {
                    //                           g.Key.Cedulasociado,
                    //                           g.Key.nrocuenta,
                    //                           total = g.Sum(s => s.valorefectivo)
                    //                       }).ToList();

                    //realizado con PROCEDIMIENTO ALMACENADO
                    //ICollection<dynamic> costomov = new List<dynamic>();
                    //dynamic multiples = dbing.UIAF_MULTIPLES_TRANSAC(ini2, fin2);//crear tabla en DB, y llamarla despues como from t in db.nuevatabla
                    //var tt1 = multi_termino;

                    //foreach (var item1 in tt1)
                    //{
                    //    long CEDULA = item1.Cedulasociado;
                    //    Decimal TOTALEFECTIVO = item1.total;
                    //    string cuenta = item1.nrocuenta;
                    if (tipos == "2")
                    {

                        reporte = (from a in db.ahorrosAtermino
                                   join s in db.sahorrosAtermino on a.numerocuenta equals s.nrocuenta
                                   join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                                   join ni in db.nits on aso.cedulasociado equals ni.nit
                                   join ag in db.agencias on s.agencia equals ag.codigoagencia
                                   join dd in db.ciiu on ni.codciiu equals dd.codciiu

                                   where (s.fechatrabajo >= inicial && s.fechatrabajo <= final)
                                   && a.Cedulasociado == CEDULA && a.numerocuenta == cuenta
                                   orderby s.fechatrabajo ascending
                                   select new
                                   {
                                       fechatransaccion = s.fechatrabajo,
                                       valortransaccion = TOTALEFECTIVO,
                                       moneda = 1,
                                       codigooficina = ag.nombreagencia,
                                       codigodepartamento = ni.codciudad,
                                       tipo_producto = 91,
                                       tipo_transaccion = 2,
                                       a.numerocuenta,
                                       tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                                                          ni.tipoidentificacion == "N" ? 31 :
                                                          ni.tipoidentificacion == "H" ? 13 :
                                                          ni.tipoidentificacion == "E" ? 21 :
                                                          ni.tipoidentificacion == "R" ? 11 :
                                                          ni.tipoidentificacion == "T" ? 12 :
                                                          ni.tipoidentificacion == "O" ? 00 :
                                                          ni.tipoidentificacion == "U" ? 13 :
                                                          ni.tipoidentificacion == "P" ? 41 : 00,
                                       documento = ni.nit,
                                       ni.primerapellido,
                                       ni.segundoapellido,
                                       ni.nombres,
                                       otronombre = ni.segundonombre,
                                       razonsocial = ni.nombreintegrado,
                                       actividadeconomica = dd.nombre,
                                       aso.salario,
                                       ni.digito,
                                       tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                                                          ni.tipoidentificacion == "N" ? 31 :
                                                          ni.tipoidentificacion == "H" ? 13 :
                                                          ni.tipoidentificacion == "E" ? 21 :
                                                          ni.tipoidentificacion == "R" ? 11 :
                                                          ni.tipoidentificacion == "T" ? 12 :
                                                          ni.tipoidentificacion == "O" ? 00 :
                                                          ni.tipoidentificacion == "U" ? 13 :
                                                          ni.tipoidentificacion == "P" ? 41 : 00,
                                       tipodocconsignante = ni.nit,
                                       primerapellidoconsignante = ni.primerapellido,
                                       segundoapellidoconsignante = ni.segundoapellido,
                                       nombreconsignante = ni.nombres,
                                       otronombreconsignante = ni.segundonombre,
                                       operador = s.operador

                                   }).Take(1).ToList();





                        foreach (var item in reporte)
                        {
                            d = new ExpandoObject();
                            d.fechatransaccion = item.fechatransaccion;
                            d.valortransaccion = item.valortransaccion;
                            d.moneda = item.moneda;
                            d.codigooficina = item.codigooficina;
                            d.codigodepartamento = item.codigodepartamento;
                            d.tipo_producto = item.tipo_producto;
                            d.tipo_transaccion = item.tipo_transaccion;
                            d.numerocuenta = item.numerocuenta;
                            d.tipoidentificacion = item.tipoidentificacion;
                            d.documento = item.documento;
                            d.primerapellido = item.primerapellido;
                            d.segundoapellido = item.segundoapellido;
                            d.nombres = item.nombres;
                            d.otronombre = item.otronombre;
                            d.razonsocial = item.razonsocial;
                            d.actividadeconomica = item.actividadeconomica;
                            d.salario = item.salario;
                            d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                            d.tipodocconsignante = item.tipodocconsignante;
                            d.primerapellidoconsignante = item.primerapellidoconsignante;
                            d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                            d.nombreconsignante = item.nombreconsignante;
                            d.otronombreconsignante = item.otronombreconsignante;
                            d.digito = item.digito;
                            d.operador = item.operador;
                            repor.Add(d);
                        }
                      }
                        /////
                        //dynamic multi_contractual = (from s in db.sahorrosContractual
                        //                             join a in db.ahorrosalavista on s.nrocuenta equals a.numerocuenta
                        //                             join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                        //                             where a.codlinea == s.codlinea && s.fechatrabajo >= inicial && s.fechatrabajo <= final
                        //                             && s.valorefectivo < 10000000 && s.valorefectivo > 0 && (s.natura == "CNGC" || s.natura == "CNGT" || s.natura == "RETT") && aso.estado == "A"
                        //                             group s by new
                        //                             {
                        //                                 s.nrocuenta,
                        //                                 a.Cedulasociado
                        //                             }
                        //                   into g
                        //                             where g.Sum(s => s.valorefectivo) > 50000000
                        //                             select new
                        //                             {
                        //                                 g.Key.Cedulasociado,
                        //                                 g.Key.nrocuenta,
                        //                                 total = g.Sum(s => s.valorefectivo)
                        //                             }).ToList();

                        ////realizado con PROCEDIMIENTO ALMACENADO
                        ////ICollection<dynamic> costomov = new List<dynamic>();
                        ////dynamic multiples = dbing.UIAF_MULTIPLES_TRANSAC(ini2, fin2);//crear tabla en DB, y llamarla despues como from t in db.nuevatabla
                        //var tt2 = multi_contractual;

                        //foreach (var item1 in tt2)
                        //{
                        //    long CEDULA = item1.Cedulasociado;
                        //    Decimal TOTALEFECTIVO = item1.total;
                        //    string cuenta = item1.nrocuenta;

                    if (tipos=="3")
                     {
                        reporte = (from a in db.ahorrosContractual
                                   join s in db.sahorrosContractual on a.numerocuenta equals s.nrocuenta
                                   join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                                   join ni in db.nits on aso.cedulasociado equals ni.nit
                                   join ag in db.agencias on s.agencia equals ag.codigoagencia
                                   join dd in db.ciiu on ni.codciiu equals dd.codciiu

                                   where (s.fechatrabajo >= inicial && s.fechatrabajo <= final)
                                   && a.Cedulasociado == CEDULA && a.numerocuenta == cuenta
                                   orderby s.fechatrabajo ascending
                                   select new
                                   {
                                       fechatransaccion = s.fechatrabajo,
                                       valortransaccion = TOTALEFECTIVO,
                                       moneda = 1,
                                       codigooficina = ag.nombreagencia,
                                       codigodepartamento = ni.codciudad,
                                       tipo_producto = s.codlinea == "CONT" ? 92 :
                                                       s.codlinea == "PROG" ? 93 : 00,
                                       tipo_transaccion = 2,
                                       a.numerocuenta,
                                       tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                                                          ni.tipoidentificacion == "N" ? 31 :
                                                          ni.tipoidentificacion == "H" ? 13 :
                                                          ni.tipoidentificacion == "E" ? 21 :
                                                          ni.tipoidentificacion == "R" ? 11 :
                                                          ni.tipoidentificacion == "T" ? 12 :
                                                          ni.tipoidentificacion == "O" ? 00 :
                                                          ni.tipoidentificacion == "U" ? 13 :
                                                          ni.tipoidentificacion == "P" ? 41 : 00,
                                       documento = ni.nit,
                                       ni.primerapellido,
                                       ni.segundoapellido,
                                       ni.nombres,
                                       otronombre = ni.segundonombre,
                                       razonsocial = ni.nombreintegrado,
                                       actividadeconomica = dd.nombre,
                                       aso.salario,
                                       ni.digito,
                                       tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                                                          ni.tipoidentificacion == "N" ? 31 :
                                                          ni.tipoidentificacion == "H" ? 13 :
                                                          ni.tipoidentificacion == "E" ? 21 :
                                                          ni.tipoidentificacion == "R" ? 11 :
                                                          ni.tipoidentificacion == "T" ? 12 :
                                                          ni.tipoidentificacion == "O" ? 00 :
                                                          ni.tipoidentificacion == "U" ? 13 :
                                                          ni.tipoidentificacion == "P" ? 41 : 00,
                                       tipodocconsignante = ni.nit,
                                       primerapellidoconsignante = ni.primerapellido,
                                       segundoapellidoconsignante = ni.segundoapellido,
                                       nombreconsignante = ni.nombres,
                                       otronombreconsignante = ni.segundonombre,
                                       operador = s.operador
                                       }).Take(1).ToList();





                            foreach (var item in reporte)
                            {
                                d = new ExpandoObject();
                                d.fechatransaccion = item.fechatransaccion;
                                d.valortransaccion = item.valortransaccion;
                                d.moneda = item.moneda;
                                d.codigooficina = item.codigooficina;
                                d.codigodepartamento = item.codigodepartamento;
                                d.tipo_producto = item.tipo_producto;
                                d.tipo_transaccion = item.tipo_transaccion;
                                d.numerocuenta = item.numerocuenta;
                                d.tipoidentificacion = item.tipoidentificacion;
                                d.documento = item.documento;
                                d.primerapellido = item.primerapellido;
                                d.segundoapellido = item.segundoapellido;
                                d.nombres = item.nombres;
                                d.otronombre = item.otronombre;
                                d.razonsocial = item.razonsocial;
                                d.actividadeconomica = item.actividadeconomica;
                                d.salario = item.salario;
                                d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                                d.tipodocconsignante = item.tipodocconsignante;
                                d.primerapellidoconsignante = item.primerapellidoconsignante;
                                d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                                d.nombreconsignante = item.nombreconsignante;
                                d.otronombreconsignante = item.otronombreconsignante;
                                d.digito = item.digito;
                                d.operador = item.operador;
                                repor.Add(d);
                            }

                        }
                        ///////
                        //dynamic multi_aportes = (from s in db.sAportesSociales
                        //                         join a in db.aportessociales on s.nrocuenta equals a.numerocuenta
                        //                         join aso in db.asociados on a.cedulasociado equals aso.cedulasociado
                        //                         where a.codlinea == s.codlinea && s.fechatrabajo >= inicial && s.fechatrabajo <= final
                        //                         && s.valorefectivo < 10000000 && s.valorefectivo > 0 && (s.natura == "CNGC" || s.natura == "CNGT" || s.natura == "RETT") && aso.estado == "A"
                        //                         group s by new
                        //                         {
                        //                             s.nrocuenta,
                        //                             a.cedulasociado
                        //                         }
                        //                   into g
                        //                         where g.Sum(s => s.valorefectivo) > 50000000
                        //                         select new
                        //                         {
                        //                             g.Key.cedulasociado,
                        //                             g.Key.nrocuenta,
                        //                             total = g.Sum(s => s.valorefectivo)
                        //                         }).ToList();

                        //realizado con PROCEDIMIENTO ALMACENADO
                        //ICollection<dynamic> costomov = new List<dynamic>();
                        //dynamic multiples = dbing.UIAF_MULTIPLES_TRANSAC(ini2, fin2);//crear tabla en DB, y llamarla despues como from t in db.nuevatabla
                        //var tt3 = multi_aportes;

                        //foreach (var item1 in tt3)
                        //{
                        //    long CEDULA = item1.cedulasociado;
                        //    Decimal TOTALEFECTIVO = item1.total;
                        //    string cuenta = item1.nrocuenta;
                        if (tipos=="4")
                        { 
                            reporte = (from a in db.aportessociales
                                       join s in db.sAportesSociales on a.numerocuenta equals s.nrocuenta
                                       join aso in db.asociados on a.cedulasociado equals aso.cedulasociado
                                       join ni in db.nits on aso.cedulasociado equals ni.nit
                                       join ag in db.agencias on s.agencia equals ag.codigoagencia
                                       join dd in db.ciiu on ni.codciiu equals dd.codciiu

                                       where (s.fechatrabajo >= inicial && s.fechatrabajo <= final)
                                       && a.cedulasociado == CEDULA && a.numerocuenta == cuenta
                                       orderby s.fechatrabajo ascending
                                       select new
                                       {
                                           fechatransaccion = s.fechatrabajo,
                                           valortransaccion = TOTALEFECTIVO,
                                           moneda = 1,
                                           codigooficina = ag.nombreagencia,
                                           codigodepartamento = ni.codciudad,
                                           tipo_producto = 94,
                                           tipo_transaccion = 2,
                                           a.numerocuenta,
                                           tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                                                              ni.tipoidentificacion == "N" ? 31 :
                                                              ni.tipoidentificacion == "H" ? 13 :
                                                              ni.tipoidentificacion == "E" ? 21 :
                                                              ni.tipoidentificacion == "R" ? 11 :
                                                              ni.tipoidentificacion == "T" ? 12 :
                                                              ni.tipoidentificacion == "O" ? 00 :
                                                              ni.tipoidentificacion == "U" ? 13 :
                                                              ni.tipoidentificacion == "P" ? 41 : 00,
                                           documento = ni.nit,
                                           ni.primerapellido,
                                           ni.segundoapellido,
                                           ni.nombres,
                                           otronombre = ni.segundonombre,
                                           razonsocial = ni.nombreintegrado,
                                           actividadeconomica = dd.nombre,
                                           aso.salario,
                                           ni.digito,
                                           tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                                                              ni.tipoidentificacion == "N" ? 31 :
                                                              ni.tipoidentificacion == "H" ? 13 :
                                                              ni.tipoidentificacion == "E" ? 21 :
                                                              ni.tipoidentificacion == "R" ? 11 :
                                                              ni.tipoidentificacion == "T" ? 12 :
                                                              ni.tipoidentificacion == "O" ? 00 :
                                                              ni.tipoidentificacion == "U" ? 13 :
                                                              ni.tipoidentificacion == "P" ? 41 : 00,
                                           tipodocconsignante = ni.nit,
                                           primerapellidoconsignante = ni.primerapellido,
                                           segundoapellidoconsignante = ni.segundoapellido,
                                           nombreconsignante = ni.nombres,
                                           otronombreconsignante = ni.segundonombre,
                                           operador=s.operador
                                       }).Take(1).ToList();





                            foreach (var item in reporte)
                            {
                                d = new ExpandoObject();
                                d.fechatransaccion = item.fechatransaccion;
                                d.valortransaccion = item.valortransaccion;
                                d.moneda = item.moneda;
                                d.codigooficina = item.codigooficina;
                                d.codigodepartamento = item.codigodepartamento;
                                d.tipo_producto = item.tipo_producto;
                                d.tipo_transaccion = item.tipo_transaccion;
                                d.numerocuenta = item.numerocuenta;
                                d.tipoidentificacion = item.tipoidentificacion;
                                d.documento = item.documento;
                                d.primerapellido = item.primerapellido;
                                d.segundoapellido = item.segundoapellido;
                                d.nombres = item.nombres;
                                d.otronombre = item.otronombre;
                                d.razonsocial = item.razonsocial;
                                d.actividadeconomica = item.actividadeconomica;
                                d.salario = item.salario;
                                d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                                d.tipodocconsignante = item.tipodocconsignante;
                                d.primerapellidoconsignante = item.primerapellidoconsignante;
                                d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                                d.nombreconsignante = item.nombreconsignante;
                                d.otronombreconsignante = item.otronombreconsignante;
                                d.digito = item.digito;
                                d.operador = item.operador;
                                repor.Add(d);
                            }

                        }
                    
                    else {
                        //reporte = (from a in db.ahorrospermanentes
                        //           join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                        //           join ni in db.nits on aso.cedulasociado equals ni.nit

                        //           where (a.fechainicio >= inicial && a.fechainicio <= final) && a.saldoTotal > 0
                        //           && aso.estado == "A"

                        //           orderby a.fechainicio ascending
                        //           select new
                        //           {
                        //               fechatransaccion = a.fechainicio,

                        //               codigodepartamento = ni.codciudad,
                        //               tipo_producto = 93,

                        //               a.numerocuenta,
                        //               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                        //                                  ni.tipoidentificacion == "N" ? 31 :
                        //                                  ni.tipoidentificacion == "H" ? 13 :
                        //                                  ni.tipoidentificacion == "E" ? 21 :
                        //                                  ni.tipoidentificacion == "R" ? 11 :
                        //                                  ni.tipoidentificacion == "T" ? 12 :
                        //                                  ni.tipoidentificacion == "O" ? 00 :
                        //                                  ni.tipoidentificacion == "U" ? 13 :
                        //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                        //               documento = ni.nit,
                        //               ni.primerapellido,
                        //               ni.segundoapellido,
                        //               ni.nombres,
                        //               otronombre = ni.segundonombre,
                        //               razonsocial = ni.nombreintegrado,

                        //               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                        //                                  ni.tipoidentificacion == "N" ? 31 :
                        //                                  ni.tipoidentificacion == "H" ? 13 :
                        //                                  ni.tipoidentificacion == "E" ? 21 :
                        //                                  ni.tipoidentificacion == "R" ? 11 :
                        //                                  ni.tipoidentificacion == "T" ? 12 :
                        //                                  ni.tipoidentificacion == "O" ? 00 :
                        //                                  ni.tipoidentificacion == "U" ? 13 :
                        //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                        //               tipodocconsignante = ni.nit,
                        //               primerapellidoconsignante = ni.primerapellido,
                        //               segundoapellidoconsignante = ni.segundoapellido,
                        //               nombreconsignante = ni.nombres,
                        //               otronombreconsignante = ni.segundonombre

                        //           }).ToList();





                        //foreach (var item in reporte)
                        //{
                        //    d = new ExpandoObject();
                        //    d.fechatransaccion = item.fechatransaccion;

                        //    d.codigodepartamento = item.codigodepartamento;
                        //    d.tipo_producto = item.tipo_producto;

                        //    d.numerocuenta = item.numerocuenta;
                        //    d.tipoidentificacion = item.tipoidentificacion;
                        //    d.documento = item.documento;
                        //    d.primerapellido = item.primerapellido;
                        //    d.segundoapellido = item.segundoapellido;
                        //    d.nombres = item.nombres;
                        //    d.otronombre = item.otronombre;
                        //    d.razonsocial = item.razonsocial;

                        //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                        //    d.tipodocconsignante = item.tipodocconsignante;
                        //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                        //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                        //    d.nombreconsignante = item.nombreconsignante;
                        //    d.otronombreconsignante = item.otronombreconsignante;
                        //    repor.Add(d);
                        //}
                    }
                }
            }
            d = new ExpandoObject();
            d.Reporte = repor;
            
            return d;

        }


        public dynamic GetLineas4(string linea)
        {
            var query = (from t in db.tasasporplazos
                         join d1 in db.destinos on t.coddestino equals d1.Coddestino
                         where t.codlinea == linea && d1.Estado=="A"
                         select new { d1.nombredestino, t.plazoinicial, t.plazofinal,coddestino=t.coddestino, tasainteres = Math.Round(((Math.Pow(1 + ((double)t.tasainteres / 100), 30.0 / 360)) - 1) * 100, 2) }).ToList();

            ICollection<dynamic> perman = new List<dynamic>();
            dynamic d = new ExpandoObject();
            //por aparte... destinos
            if (query.Count == 0)
            {
                var query1 = (from d3 in db.destinos
                              where d3.codlinea == linea && d3.Estado == "A"
                              select new { nombredestino = d3.nombredestino, plazoinicial = d3.peridominimodias, plazofinal = d3.periodomaximodias,coddestino=d3.Coddestino, tasainteres = Math.Round(((Math.Pow(1 + ((double)d3.tasapordefecto / 100), 30.0 / 360)) - 1) * 100, 2) }).ToList();

                foreach (var item in query1)
                {
                    d = new ExpandoObject();
                    d.nombredestino = item.nombredestino;
                    d.plazoinicial = item.plazoinicial;
                    d.plazofinal = item.plazofinal;
                    d.coddestino = item.coddestino;
                    d.tasainteres = item.tasainteres;
                    perman.Add(d);

                }


                d = new ExpandoObject();
                d.Datos = perman;

                return d;
            }
            else
            {

                foreach (var item in query)
                {
                    d = new ExpandoObject();
                    d.nombredestino = item.nombredestino;
                    d.plazoinicial = item.plazoinicial;
                    d.plazofinal = item.plazofinal;
                    d.coddestino = item.coddestino;
                    d.tasainteres = item.tasainteres;
                    perman.Add(d);

                }


                d = new ExpandoObject();
                d.Datos = perman;

                return d;
            }
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Guarda la simulacion realizada
        /// </summary>
        /// <param name="aa"> tipo TABLA</param>
        /// <returns></returns>
        public SEGUIMIENTO_SIMULADORES GetSeguimiento_email(SEGUIMIENTO_SIMULADORES aa)
        {
                        
            dbing.SEGUIMIENTO_SIMULADORES.Add(aa);
            dbing.SaveChanges();
            return null;
        }
        
        public dynamic GetTasas3(int txtCapital, int v)
        {
            ICollection<dynamic> perman = new List<dynamic>();
            dynamic d = new ExpandoObject();

            var query = (from t in db.tasaspormontosyplazos
                         select new { t.montodesde,t.montohasta,t.plazodesde,t.plazohasta,t.tasa }).ToList();

            int montominimo = 0;
            int montomaximo = 0;

            foreach (var item in query)
            {
                d = new ExpandoObject();

                d.nombredestino = item.montodesde;
                montominimo = Convert.ToInt32(item.montodesde);
                montomaximo = Convert.ToInt32(item.montohasta);
               
                    if ((txtCapital >= montominimo && montomaximo >= txtCapital) && ( Convert.ToInt32(item.plazodesde)<= v && Convert.ToInt32(item.plazohasta)>= v))
                    {
                        d.tasacolocacion = item.tasa;
                        d.aplica = 1;
                        perman.Add(d);
                        break;
                    }
                    else
                    {
                        d.aplica = 0;
                        d.tasacolocacion = 0;
                    }
                
                perman.Add(d);
            }
            return perman;
        }
        public dynamic GetTasas4(int v)
        {

            //var query = (from t in db.tasaspormontosyplazos
            //             where t.plazodesde<=v && t.plazohasta>=v
            //             select new { t.montodesde, t.montohasta, t.plazodesde, t.plazohasta, tasa = Math.Round( ((Math.Pow(1 + ((double)t.tasa / 100), 30.0 / 360)) - 1) * 100,2)
            //                         }).ToList();
            var query = (from t in db.tasaspormontosyplazos
                         where t.plazodesde <= v && t.plazohasta >= v
                         select new
                         {
                             t.montodesde,
                             t.montohasta,
                             t.plazodesde,
                             t.plazohasta,
                             tasa = t.tasa
                         }).ToList();

            return query;
        }


        public dynamic GetLineas2(string linea)
        {
            var query = (from t in db.tasasporplazos
                         join d in db.destinos on t.coddestino equals d.Coddestino
                         where (d.codlinea == t.codlinea) && t.codlinea == linea && d.Estado=="A"
                         select new { d.Coddestino, d.nombredestino }).GroupBy(X => new { X.Coddestino, X.nombredestino }).Select(X => X.FirstOrDefault()).ToList();
            //por aparte... destinos
            if (query.Count==0)
            {
                query = (from d in db.destinos 
                             where d.codlinea == linea && d.Estado == "A"
                             select new { d.Coddestino, d.nombredestino }).ToList();

            }

            return query;
        }
        public dynamic GetLineas3(string linea, string destino)
        {
            var query = (from t in db.tasasporplazos
                         join d1 in db.destinos on t.coddestino equals d1.Coddestino
                         where t.coddestino==destino && t.codlinea==linea
                         select new { d1.nombredestino, t.plazoinicial, t.plazofinal, tasainteres = Math.Round(((Math.Pow(1 + ((double)t.tasainteres / 100), 30.0 / 360)) - 1) * 100, 2) }).ToList();

            ICollection<dynamic> perman = new List<dynamic>();
            dynamic d = new ExpandoObject();
            //por aparte... destinos
            if (query.Count == 0)
            {
                var query1 = (from d3 in db.destinos
                              where d3.codlinea == linea && d3.Estado == "A"
                              select new { nombredestino = d3.nombredestino, plazoinicial = d3.peridominimodias, plazofinal = d3.periodomaximodias, tasainteres = Math.Round(((Math.Pow(1 + ((double)d3.tasapordefecto / 100), 30.0 / 360)) - 1) * 100, 2) }).ToList();

                foreach (var item in query1)
                {
                    d = new ExpandoObject();
                    d.nombredestino = item.nombredestino;
                    d.plazoinicial = item.plazoinicial;
                    d.plazofinal = item.plazofinal;
                    d.tasainteres = item.tasainteres;
                    perman.Add(d);

                }


                d = new ExpandoObject();
                d.Datos = perman;

                return d;
            }
            else
            {
                
                foreach (var item in query)
                {
                    d = new ExpandoObject();
                    d.nombredestino = item.nombredestino;
                    d.plazoinicial = item.plazoinicial;
                    d.plazofinal = item.plazofinal;
                    d.tasainteres = item.tasainteres;
                    perman.Add(d);

                }


                d = new ExpandoObject();
                d.Datos = perman;

                return d;
            }

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene el plazo inicial y final de cada nombre destino 
        /// </summary>
        /// <param name="linea"></param>
        /// <param name="destino"></param>
        /// <returns></returns>
        public dynamic GetLineas33(string linea, string destino)
        {
            var query = (from t in db.tasasporplazos
                         join d1 in db.destinos on t.coddestino equals d1.Coddestino
                         where t.coddestino == destino && t.codlinea == linea
                         select new { d1.nombredestino, t.plazoinicial, t.plazofinal, t.tasainteres }).ToList();

            ICollection<dynamic> perman = new List<dynamic>();
            dynamic d = new ExpandoObject();
            //por aparte... destinos
            if (query.Count == 0)
            {
                var query1 = (from d3 in db.destinos
                              where d3.codlinea == linea && d3.Estado == "A"
                              select new { nombredestino = d3.nombredestino, plazoinicial = d3.peridominimodias, plazofinal = d3.periodomaximodias, d3.tasapordefecto  }).ToList();

                foreach (var item in query1)
                {
                    d = new ExpandoObject();
                    d.nombredestino = item.nombredestino;
                    d.plazoinicial = item.plazoinicial;
                    d.plazofinal = item.plazofinal;
                    d.tasainteres = item.tasapordefecto;
                    perman.Add(d);

                }


                d = new ExpandoObject();
                d.Datos = perman;

                return d;
            }
            else
            {

                foreach (var item in query)
                {
                    d = new ExpandoObject();
                    d.nombredestino = item.nombredestino;
                    d.plazoinicial = item.plazoinicial;
                    d.plazofinal = item.plazofinal;
                    d.tasainteres = item.tasainteres;
                    perman.Add(d);

                }


                d = new ExpandoObject();
                d.Datos = perman;

                return d;
            }

        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene operaciones de transacciones y productos de aportes sociales de reporte UIAF 
        /// </summary>
        /// <param name="inicial">tipo fecha</param>
        /// <param name="final">tipo fecha</param>
        /// <param name="mostrar">tipo entero</param>
        /// <param name="tipo">tipo cadena</param>
        /// <returns></returns>
        public dynamic GetReporteSocial(DateTime? inicial, DateTime? final, int mostrar,string tipo)
        {
            dynamic reporte = "";
            ICollection<dynamic> repor = new List<dynamic>();
            dynamic d = new ExpandoObject();

            if (tipo == "C")
            {
                //reporte = (from a in db.aportessociales
                //           join aso in db.asociados on a.cedulasociado equals aso.cedulasociado
                //           join ni in db.nits on aso.cedulasociado equals ni.nit
                //           join s in db.sAportesSociales on a.numerocuenta equals s.nrocuenta
                //           join ag in db.agencias on s.agenciatransaccion equals ag.codigoagencia
                //           join dd in db.divisionciiu on new { ni.coddivision, ni.codciiu } equals new { dd.coddivision, dd.codciiu }

                //           where (s.fechatrabajo >= inicial && s.fechatrabajo <= final)
                //          && aso.estado == "A"
                //          && (s.natura == "CNGC" || s.natura == "CNGT" || s.natura == "RETT")
                //          && (tipo == "C" ? s.valorefectivo >= mostrar : 1 == 1)
                //           orderby s.fechatrabajo ascending
                //           select new
                //           {
                //               fechatransaccion = s.fechatrabajo,
                //               valortransaccion = s.valorefectivo,
                //               moneda = 1,
                //               codigooficina = ag.nombreagencia,
                //               codigodepartamento = ni.codciudad,
                //               tipo_producto = 94,
                //               tipo_transaccion = 2,
                //               a.numerocuenta,
                //               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                //                                  ni.tipoidentificacion == "N" ? 31 :
                //                                  ni.tipoidentificacion == "H" ? 13 :
                //                                  ni.tipoidentificacion == "E" ? 21 :
                //                                  ni.tipoidentificacion == "R" ? 11 :
                //                                  ni.tipoidentificacion == "T" ? 12 :
                //                                  ni.tipoidentificacion == "O" ? 00 :
                //                                  ni.tipoidentificacion == "U" ? 13 :
                //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                //               documento = ni.nit,
                //               ni.primerapellido,
                //               ni.segundoapellido,
                //               ni.nombres,
                //               otronombre = ni.segundonombre,
                //               razonsocial = ni.nombreintegrado,
                //               actividadeconomica = dd.nombre,
                //               aso.salario,
                //               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                //                                  ni.tipoidentificacion == "N" ? 31 :
                //                                  ni.tipoidentificacion == "H" ? 13 :
                //                                  ni.tipoidentificacion == "E" ? 21 :
                //                                  ni.tipoidentificacion == "R" ? 11 :
                //                                  ni.tipoidentificacion == "T" ? 12 :
                //                                  ni.tipoidentificacion == "O" ? 00 :
                //                                  ni.tipoidentificacion == "U" ? 13 :
                //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                //               tipodocconsignante = ni.nit,
                //               primerapellidoconsignante = ni.primerapellido,
                //               segundoapellidoconsignante = ni.segundoapellido,
                //               nombreconsignante = ni.nombres,
                //               otronombreconsignante = ni.segundonombre

                //           }).ToList();





                //foreach (var item in reporte)
                //{
                //    d = new ExpandoObject();
                //    d.fechatransaccion = item.fechatransaccion;
                //    d.valortransaccion = item.valortransaccion;
                //    d.moneda = item.moneda;
                //    d.codigooficina = item.codigooficina;
                //    d.codigodepartamento = item.codigodepartamento;
                //    d.tipo_producto = item.tipo_producto;
                //    d.tipo_transaccion = item.tipo_transaccion;
                //    d.numerocuenta = item.numerocuenta;
                //    d.tipoidentificacion = item.tipoidentificacion;
                //    d.documento = item.documento;
                //    d.primerapellido = item.primerapellido;
                //    d.segundoapellido = item.segundoapellido;
                //    d.nombres = item.nombres;
                //    d.otronombre = item.otronombre;
                //    d.razonsocial = item.razonsocial;
                //    d.actividadeconomica = item.actividadeconomica;
                //    d.salario = item.salario;
                //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                //    d.tipodocconsignante = item.tipodocconsignante;
                //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                //    d.nombreconsignante = item.nombreconsignante;
                //    d.otronombreconsignante = item.otronombreconsignante;
                //    repor.Add(d);
                //}
            }
            else {
                reporte = (from a in db.aportessociales
                           //join aso in db.asociados on a.cedulasociado equals aso.cedulasociado
                           join ni in db.nits on a.cedulasociado equals ni.nit
                           //join pro in db.profesiones on aso.codprofesion equals pro.codprofesion
                           join ag in db.agencias on a.agencia equals ag.codigoagencia
                           where ( a.fechaapertura <= final) && a.saldoTotal>0
                           //&& aso.estado == "A"
                           
                           orderby a.fechaapertura ascending
                           select new
                           {
                               fechatransaccion = a.fechaapertura,

                               codigodepartamento = ag.codciudad,
                               tipo_producto = 94,

                               a.numerocuenta,
                               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                                                  ni.tipoidentificacion == "N" ? 31 :
                                                  ni.tipoidentificacion == "H" ? 13 :
                                                  ni.tipoidentificacion == "E" ? 21 :
                                                  ni.tipoidentificacion == "R" ? 11 :
                                                  ni.tipoidentificacion == "T" ? 12 :
                                                  ni.tipoidentificacion == "O" ? 00 :
                                                  ni.tipoidentificacion == "U" ? 13 :
                                                  ni.tipoidentificacion == "P" ? 41 : 00,
                               documento = ni.nit,
                               ni.primerapellido,
                               ni.segundoapellido,
                               ni.nombres,
                               otronombre = ni.segundonombre,
                               razonsocial = ni.nombreintegrado,

                               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                                                  ni.tipoidentificacion == "N" ? 31 :
                                                  ni.tipoidentificacion == "H" ? 13 :
                                                  ni.tipoidentificacion == "E" ? 21 :
                                                  ni.tipoidentificacion == "R" ? 11 :
                                                  ni.tipoidentificacion == "T" ? 12 :
                                                  ni.tipoidentificacion == "O" ? 00 :
                                                  ni.tipoidentificacion == "U" ? 13 :
                                                  ni.tipoidentificacion == "P" ? 41 : 00,
                               tipodocconsignante = ni.nit,
                               primerapellidoconsignante = ni.primerapellido,
                               segundoapellidoconsignante = ni.segundoapellido,
                               nombreconsignante = ni.nombres,
                               otronombreconsignante = ni.segundonombre

                           }).ToList();





                foreach (var item in reporte)
                {
                    d = new ExpandoObject();
                    d.fechatransaccion = item.fechatransaccion;

                    d.codigodepartamento = item.codigodepartamento;
                    d.tipo_producto = item.tipo_producto;

                    d.numerocuenta = item.numerocuenta;
                    d.tipoidentificacion = item.tipoidentificacion;
                    d.documento = item.documento;
                    d.primerapellido = item.primerapellido;
                    d.segundoapellido = item.segundoapellido;
                    d.nombres = item.nombres;
                    d.otronombre = item.otronombre;
                    d.razonsocial = item.razonsocial;

                    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                    d.tipodocconsignante = item.tipodocconsignante;
                    d.primerapellidoconsignante = item.primerapellidoconsignante;
                    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                    d.nombreconsignante = item.nombreconsignante;
                    d.otronombreconsignante = item.otronombreconsignante;
                    repor.Add(d);
                }
            }

                d = new ExpandoObject();
            d.Reporte = repor;

            return d;
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene operaciones de transacciones y productos de ahorro contractual de reporte UIAF 
        /// </summary>
        /// <param name="inicial"> tipo fecha</param>
        /// <param name="final">tipo fecha</param>
        /// <param name="mostrar">tipo entero</param>
        /// <param name="tipo">tipo cadena</param>
        /// <returns></returns>
        public dynamic GetReporteContractual(DateTime? inicial, DateTime? final, int mostrar,string tipo)
        {
            dynamic reporte = "";
            ICollection<dynamic> repor = new List<dynamic>();
            dynamic d = new ExpandoObject();

            if (tipo == "C")
            {
                //reporte = (from a in db.ahorrosContractual
                //           join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                //           join ni in db.nits on aso.cedulasociado equals ni.nit
                //           join s in db.sahorrosContractual on a.numerocuenta equals s.nrocuenta
                //           join ag in db.agencias on s.agenciatransaccion equals ag.codigoagencia
                //           join dd in db.divisionciiu on new { ni.coddivision, ni.codciiu } equals new { dd.coddivision, dd.codciiu }

                //           where (s.fechatrabajo >= inicial && s.fechatrabajo <= final)
                //          && aso.estado == "A"
                //          && (s.natura == "CNGC" || s.natura == "CNGT" || s.natura == "RETT")
                //          && (tipo == "C" ? s.valorefectivo >= mostrar : 1 == 1)
                //           orderby s.fechatrabajo ascending
                //           select new
                //           {
                //               fechatransaccion = s.fechatrabajo,
                //               valortransaccion = s.valorefectivo,
                //               moneda = 1,
                //               codigooficina = ag.nombreagencia,
                //               codigodepartamento = ni.codciudad,
                //               tipo_producto = 92,
                //               tipo_transaccion = 2,
                //               a.numerocuenta,
                //               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                //                                  ni.tipoidentificacion == "N" ? 31 :
                //                                  ni.tipoidentificacion == "H" ? 13 :
                //                                  ni.tipoidentificacion == "E" ? 21 :
                //                                  ni.tipoidentificacion == "R" ? 11 :
                //                                  ni.tipoidentificacion == "T" ? 12 :
                //                                  ni.tipoidentificacion == "O" ? 00 :
                //                                  ni.tipoidentificacion == "U" ? 13 :
                //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                //               documento = ni.nit,
                //               ni.primerapellido,
                //               ni.segundoapellido,
                //               ni.nombres,
                //               otronombre = ni.segundonombre,
                //               razonsocial = ni.nombreintegrado,
                //               actividadeconomica = dd.nombre,
                //               aso.salario,
                //               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                //                                  ni.tipoidentificacion == "N" ? 31 :
                //                                  ni.tipoidentificacion == "H" ? 13 :
                //                                  ni.tipoidentificacion == "E" ? 21 :
                //                                  ni.tipoidentificacion == "R" ? 11 :
                //                                  ni.tipoidentificacion == "T" ? 12 :
                //                                  ni.tipoidentificacion == "O" ? 00 :
                //                                  ni.tipoidentificacion == "U" ? 13 :
                //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                //               tipodocconsignante = ni.nit,
                //               primerapellidoconsignante = ni.primerapellido,
                //               segundoapellidoconsignante = ni.segundoapellido,
                //               nombreconsignante = ni.nombres,
                //               otronombreconsignante = ni.segundonombre

                //           }).ToList();





                //foreach (var item in reporte)
                //{
                //    d = new ExpandoObject();
                //    d.fechatransaccion = item.fechatransaccion;
                //    d.valortransaccion = item.valortransaccion;
                //    d.moneda = item.moneda;
                //    d.codigooficina = item.codigooficina;
                //    d.codigodepartamento = item.codigodepartamento;
                //    d.tipo_producto = item.tipo_producto;
                //    d.tipo_transaccion = item.tipo_transaccion;
                //    d.numerocuenta = item.numerocuenta;
                //    d.tipoidentificacion = item.tipoidentificacion;
                //    d.documento = item.documento;
                //    d.primerapellido = item.primerapellido;
                //    d.segundoapellido = item.segundoapellido;
                //    d.nombres = item.nombres;
                //    d.otronombre = item.otronombre;
                //    d.razonsocial = item.razonsocial;
                //    d.actividadeconomica = item.actividadeconomica;
                //    d.salario = item.salario;
                //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                //    d.tipodocconsignante = item.tipodocconsignante;
                //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                //    d.nombreconsignante = item.nombreconsignante;
                //    d.otronombreconsignante = item.otronombreconsignante;
                //    repor.Add(d);
                //}
            }
            else {
                reporte = (from a in db.ahorrosContractual
                           join ni in db.nits on a.Cedulasociado equals ni.nit
                           join ag in db.agencias on a.agencia equals ag.codigoagencia
                           where (a.fechainicio <= final) && a.saldoTotal > 0 
                           //&& a.estado == "A"
                          
                           orderby a.fechainicio ascending
                           select new
                           {
                               fechatransaccion = a.fechainicio,
                               
                               codigodepartamento = ag.codciudad,
                               tipo_producto = 92,
                               a.codlinea,
                               a.numerocuenta,
                               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                                                  ni.tipoidentificacion == "N" ? 31 :
                                                  ni.tipoidentificacion == "H" ? 13 :
                                                  ni.tipoidentificacion == "E" ? 21 :
                                                  ni.tipoidentificacion == "R" ? 11 :
                                                  ni.tipoidentificacion == "T" ? 12 :
                                                  ni.tipoidentificacion == "O" ? 00 :
                                                  ni.tipoidentificacion == "U" ? 13 :
                                                  ni.tipoidentificacion == "P" ? 41 : 00,
                               documento = ni.nit,
                               ni.primerapellido,
                               ni.segundoapellido,
                               ni.nombres,
                               otronombre = ni.segundonombre,
                               razonsocial = ni.nombreintegrado,

                               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                                                  ni.tipoidentificacion == "N" ? 31 :
                                                  ni.tipoidentificacion == "H" ? 13 :
                                                  ni.tipoidentificacion == "E" ? 21 :
                                                  ni.tipoidentificacion == "R" ? 11 :
                                                  ni.tipoidentificacion == "T" ? 12 :
                                                  ni.tipoidentificacion == "O" ? 00 :
                                                  ni.tipoidentificacion == "U" ? 13 :
                                                  ni.tipoidentificacion == "P" ? 41 : 00,
                               tipodocconsignante = ni.nit,
                               primerapellidoconsignante = ni.primerapellido,
                               segundoapellidoconsignante = ni.segundoapellido,
                               nombreconsignante = ni.nombres,
                               otronombreconsignante = ni.segundonombre

                           }).ToList();





                foreach (var item in reporte)
                {
                    d = new ExpandoObject();
                    d.fechatransaccion = item.fechatransaccion;

                    d.codigodepartamento = item.codigodepartamento;
                    d.tipo_producto = item.tipo_producto;
                    d.codlinea = item.codlinea;
                    d.numerocuenta = item.numerocuenta;
                    d.tipoidentificacion = item.tipoidentificacion;
                    d.documento = item.documento;
                    d.primerapellido = item.primerapellido;
                    d.segundoapellido = item.segundoapellido;
                    d.nombres = item.nombres;
                    d.otronombre = item.otronombre;
                    d.razonsocial = item.razonsocial;

                    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                    d.tipodocconsignante = item.tipodocconsignante;
                    d.primerapellidoconsignante = item.primerapellidoconsignante;
                    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                    d.nombreconsignante = item.nombreconsignante;
                    d.otronombreconsignante = item.otronombreconsignante;
                    repor.Add(d);
                }
            }

                d = new ExpandoObject();
            d.Reporte = repor;

            return d;
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene operaciones de transacciones y productos de ahorro a termino de reporte UIAF 
        /// </summary>
        /// <param name="inicial"> tipo fecha</param>
        /// <param name="final">tipo fecha</param>
        /// <param name="mostrar">tipo entero</param>
        /// <param name="tipo">tipo cadena</param>
        public dynamic GetReporteAtermino(DateTime? inicial, DateTime? final, int mostrar,string tipo)
        {
            dynamic reporte = "";
            ICollection<dynamic> repor = new List<dynamic>();
            dynamic d = new ExpandoObject();

            if (tipo == "C") { 
                //reporte = (from a in db.ahorrosAtermino
                //           join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                //           join ni in db.nits on aso.cedulasociado equals ni.nit
                //           join s in db.sahorrosAtermino on a.numerocuenta equals s.nrocuenta
                //           join ag in db.agencias on s.agenciatransaccion equals ag.codigoagencia
                //           join dd in db.divisionciiu on new { ni.coddivision, ni.codciiu } equals new { dd.coddivision, dd.codciiu }

                //           where (s.fechatrabajo >= inicial && s.fechatrabajo <= final)
                //          && aso.estado == "A"
                //          && (s.natura == "CNGC" || s.natura == "CNGT" || s.natura == "RETT")
                //          && (tipo == "C" ? s.valorefectivo >= mostrar : 1 == 1)
                //           orderby s.fechatrabajo ascending
                //           select new
                //           {
                //               fechatransaccion = s.fechatrabajo,
                //               valortransaccion = s.valorefectivo,
                //               moneda = 1,
                //               codigooficina = ag.nombreagencia,
                //               codigodepartamento = ni.codciudad,
                //               tipo_producto = 91,
                //               tipo_transaccion = 2,
                //               a.numerocuenta,
                //               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                //                                  ni.tipoidentificacion == "N" ? 31 :
                //                                  ni.tipoidentificacion == "H" ? 13 :
                //                                  ni.tipoidentificacion == "E" ? 21 :
                //                                  ni.tipoidentificacion == "R" ? 11 :
                //                                  ni.tipoidentificacion == "T" ? 12 :
                //                                  ni.tipoidentificacion == "O" ? 00 :
                //                                  ni.tipoidentificacion == "U" ? 13 :
                //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                //               documento = ni.nit,
                //               ni.primerapellido,
                //               ni.segundoapellido,
                //               ni.nombres,
                //               otronombre = ni.segundonombre,
                //               razonsocial = ni.nombreintegrado,
                //               actividadeconomica = dd.nombre,
                //               aso.salario,
                //               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                //                                  ni.tipoidentificacion == "N" ? 31 :
                //                                  ni.tipoidentificacion == "H" ? 13 :
                //                                  ni.tipoidentificacion == "E" ? 21 :
                //                                  ni.tipoidentificacion == "R" ? 11 :
                //                                  ni.tipoidentificacion == "T" ? 12 :
                //                                  ni.tipoidentificacion == "O" ? 00 :
                //                                  ni.tipoidentificacion == "U" ? 13 :
                //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                //               tipodocconsignante = ni.nit,
                //               primerapellidoconsignante = ni.primerapellido,
                //               segundoapellidoconsignante = ni.segundoapellido,
                //               nombreconsignante = ni.nombres,
                //               otronombreconsignante = ni.segundonombre

                //           }).ToList();





                //foreach (var item in reporte)
                //{
                //    d = new ExpandoObject();
                //    d.fechatransaccion = item.fechatransaccion;
                //    d.valortransaccion = item.valortransaccion;
                //    d.moneda = item.moneda;
                //    d.codigooficina = item.codigooficina;
                //    d.codigodepartamento = item.codigodepartamento;
                //    d.tipo_producto = item.tipo_producto;
                //    d.tipo_transaccion = item.tipo_transaccion;
                //    d.numerocuenta = item.numerocuenta;
                //    d.tipoidentificacion = item.tipoidentificacion;
                //    d.documento = item.documento;
                //    d.primerapellido = item.primerapellido;
                //    d.segundoapellido = item.segundoapellido;
                //    d.nombres = item.nombres;
                //    d.otronombre = item.otronombre;
                //    d.razonsocial = item.razonsocial;
                //    d.actividadeconomica = item.actividadeconomica;
                //    d.salario = item.salario;
                //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                //    d.tipodocconsignante = item.tipodocconsignante;
                //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                //    d.nombreconsignante = item.nombreconsignante;
                //    d.otronombreconsignante = item.otronombreconsignante;
                //    repor.Add(d);
                //}
            }
            else {
                reporte = (from a in db.ahorrosAtermino
                           //join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                           join ni in db.nits on a.Cedulasociado equals ni.nit
                           join s in db.agencias on a.agencia equals s.codigoagencia
                           where (a.fechainicio <= final) && a.saldototal > 0
                                                      
                           orderby a.fechainicio ascending
                           select new
                           {
                               fechatransaccion = a.fechainicio,

                               codigodepartamento =s.codciudad,
                               tipo_producto = 91,

                               a.numerocuenta,
                               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                                                  ni.tipoidentificacion == "N" ? 31 :
                                                  ni.tipoidentificacion == "H" ? 13 :
                                                  ni.tipoidentificacion == "E" ? 21 :
                                                  ni.tipoidentificacion == "R" ? 11 :
                                                  ni.tipoidentificacion == "T" ? 12 :
                                                  ni.tipoidentificacion == "O" ? 00 :
                                                  ni.tipoidentificacion == "U" ? 13 :
                                                  ni.tipoidentificacion == "P" ? 41 : 00,
                               documento = ni.nit,
                               ni.primerapellido,
                               ni.segundoapellido,
                               ni.nombres,
                               otronombre = ni.segundonombre,
                               razonsocial = ni.nombreintegrado,

                               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                                                  ni.tipoidentificacion == "N" ? 31 :
                                                  ni.tipoidentificacion == "H" ? 13 :
                                                  ni.tipoidentificacion == "E" ? 21 :
                                                  ni.tipoidentificacion == "R" ? 11 :
                                                  ni.tipoidentificacion == "T" ? 12 :
                                                  ni.tipoidentificacion == "O" ? 00 :
                                                  ni.tipoidentificacion == "U" ? 13 :
                                                  ni.tipoidentificacion == "P" ? 41 : 00,
                               tipodocconsignante = ni.nit,
                               primerapellidoconsignante = ni.primerapellido,
                               segundoapellidoconsignante = ni.segundoapellido,
                               nombreconsignante = ni.nombres,
                               otronombreconsignante = ni.segundonombre

                           }).ToList();





                foreach (var item in reporte)
                {
                    d = new ExpandoObject();
                    d.fechatransaccion = item.fechatransaccion;

                    d.codigodepartamento = item.codigodepartamento;
                    d.tipo_producto = item.tipo_producto;

                    d.numerocuenta = item.numerocuenta;
                    d.tipoidentificacion = item.tipoidentificacion;
                    d.documento = item.documento;
                    d.primerapellido = item.primerapellido;
                    d.segundoapellido = item.segundoapellido;
                    d.nombres = item.nombres;
                    d.otronombre = item.otronombre;
                    d.razonsocial = item.razonsocial;

                    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                    d.tipodocconsignante = item.tipodocconsignante;
                    d.primerapellidoconsignante = item.primerapellidoconsignante;
                    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                    d.nombreconsignante = item.nombreconsignante;
                    d.otronombreconsignante = item.otronombreconsignante;
                    repor.Add(d);
                }
            }

            d = new ExpandoObject();
            d.Reporte = repor;

            return d;
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene operaciones de transacciones y productos de ahorro a la vista de reporte UIAF 
        /// </summary>
        /// <param name="inicial"> tipo fecha</param>
        /// <param name="final">tipo fecha</param>
        /// <param name="mostrar">tipo entero</param>
        /// <param name="tipo">tipo cadena</param>
        public dynamic GetReporteAlavista(DateTime? inicial, DateTime? final, int mostrar,string tipo)
        {
            dynamic reporte = "";
            ICollection<dynamic> repor = new List<dynamic>();
            dynamic d = new ExpandoObject();

           if (tipo == "C") { 
                //reporte = (from a in db.ahorrosalavista
                //           join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                //           join ni in db.nits on aso.cedulasociado equals ni.nit
                //           join s in db.sahorrosalavista on a.numerocuenta equals s.nrocuenta
                //           join ag in db.agencias on s.agenciatransaccion equals ag.codigoagencia
                //           join dd in db.divisionciiu on new { ni.coddivision, ni.codciiu } equals new { dd.coddivision, dd.codciiu }

                //           where (s.fechatrabajo >= inicial && s.fechatrabajo <= final)
                //          && aso.estado == "A"
                //          && (s.natura == "CNGC" || s.natura == "CNGT" || s.natura == "RETT")
                //          && (tipo == "C" ? s.valorefectivo >= mostrar : 1 == 1)
                //           orderby s.fechatrabajo ascending
                //           select new
                //           {
                //               fechatransaccion = s.fechatrabajo,
                //               valortransaccion = s.valorefectivo,
                //               moneda = 1,
                //               codigooficina = ag.nombreagencia,
                //               codigodepartamento = ni.codciudad,
                //               tipo_producto = 90,
                //               tipo_transaccion = 2,
                //               a.numerocuenta,
                //               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                //                                  ni.tipoidentificacion == "N" ? 31 :
                //                                  ni.tipoidentificacion == "H" ? 13 :
                //                                  ni.tipoidentificacion == "E" ? 21 :
                //                                  ni.tipoidentificacion == "R" ? 11 :
                //                                  ni.tipoidentificacion == "T" ? 12 :
                //                                  ni.tipoidentificacion == "O" ? 00 :
                //                                  ni.tipoidentificacion == "U" ? 13 :
                //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                //               documento = ni.nit,
                //               ni.primerapellido,
                //               ni.segundoapellido,
                //               ni.nombres,
                //               otronombre = ni.segundonombre,
                //               razonsocial = ni.nombreintegrado,
                //               actividadeconomica = dd.nombre,
                //               aso.salario,
                //               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                //                                  ni.tipoidentificacion == "N" ? 31 :
                //                                  ni.tipoidentificacion == "H" ? 13 :
                //                                  ni.tipoidentificacion == "E" ? 21 :
                //                                  ni.tipoidentificacion == "R" ? 11 :
                //                                  ni.tipoidentificacion == "T" ? 12 :
                //                                  ni.tipoidentificacion == "O" ? 00 :
                //                                  ni.tipoidentificacion == "U" ? 13 :
                //                                  ni.tipoidentificacion == "P" ? 41 : 00,
                //               tipodocconsignante = ni.nit,
                //               primerapellidoconsignante = ni.primerapellido,
                //               segundoapellidoconsignante = ni.segundoapellido,
                //               nombreconsignante = ni.nombres,
                //               otronombreconsignante = ni.segundonombre

                //           }).ToList();





                //foreach (var item in reporte)
                //{
                //    d = new ExpandoObject();
                //    d.fechatransaccion = item.fechatransaccion;
                //    d.valortransaccion = item.valortransaccion;
                //    d.moneda = item.moneda;
                //    d.codigooficina = item.codigooficina;
                //    d.codigodepartamento = item.codigodepartamento;
                //    d.tipo_producto = item.tipo_producto;
                //    d.tipo_transaccion = item.tipo_transaccion;
                //    d.numerocuenta = item.numerocuenta;
                //    d.tipoidentificacion = item.tipoidentificacion;
                //    d.documento = item.documento;
                //    d.primerapellido = item.primerapellido;
                //    d.segundoapellido = item.segundoapellido;
                //    d.nombres = item.nombres;
                //    d.otronombre = item.otronombre;
                //    d.razonsocial = item.razonsocial;
                //    d.actividadeconomica = item.actividadeconomica;
                //    d.salario = item.salario;
                //    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                //    d.tipodocconsignante = item.tipodocconsignante;
                //    d.primerapellidoconsignante = item.primerapellidoconsignante;
                //    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                //    d.nombreconsignante = item.nombreconsignante;
                //    d.otronombreconsignante = item.otronombreconsignante;
                //    repor.Add(d);
                //}

            }
            else {
                reporte = (from a in db.ahorrosalavista
                           //join aso in db.asociados on a.Cedulasociado equals aso.cedulasociado
                           join ni in db.nits on a.Cedulasociado equals ni.nit
                           join ag in db.agencias on a.agencia equals ag.codigoagencia
                           where (a.fechainicio <= final) && a.saldoTotal > 0
                           //&& a.estado == "A"
                          
                           orderby a.fechainicio ascending
                           select new
                           {
                               fechatransaccion = a.fechainicio,

                               codigodepartamento = ag.codciudad,
                               tipo_producto = 90,

                               a.numerocuenta,
                               tipoidentificacion = ni.tipoidentificacion == "C" ? 13 :
                                                  ni.tipoidentificacion == "N" ? 31 :
                                                  ni.tipoidentificacion == "H" ? 13 :
                                                  ni.tipoidentificacion == "E" ? 21 :
                                                  ni.tipoidentificacion == "R" ? 11 :
                                                  ni.tipoidentificacion == "T" ? 12 :
                                                  ni.tipoidentificacion == "O" ? 00 :
                                                  ni.tipoidentificacion == "U" ? 13 :
                                                  ni.tipoidentificacion == "P" ? 41 : 00,
                               documento = ni.nit,
                               ni.primerapellido,
                               ni.segundoapellido,
                               ni.nombres,
                               otronombre = ni.segundonombre,
                               razonsocial = ni.nombreintegrado,

                               tipoidentificaciondeconsignante = ni.tipoidentificacion == "C" ? 13 :
                                                  ni.tipoidentificacion == "N" ? 31 :
                                                  ni.tipoidentificacion == "H" ? 13 :
                                                  ni.tipoidentificacion == "E" ? 21 :
                                                  ni.tipoidentificacion == "R" ? 11 :
                                                  ni.tipoidentificacion == "T" ? 12 :
                                                  ni.tipoidentificacion == "O" ? 00 :
                                                  ni.tipoidentificacion == "U" ? 13 :
                                                  ni.tipoidentificacion == "P" ? 41 : 00,
                               tipodocconsignante = ni.nit,
                               primerapellidoconsignante = ni.primerapellido,
                               segundoapellidoconsignante = ni.segundoapellido,
                               nombreconsignante = ni.nombres,
                               otronombreconsignante = ni.segundonombre

                           }).ToList();





                foreach (var item in reporte)
                {
                    d = new ExpandoObject();
                    d.fechatransaccion = item.fechatransaccion;

                    d.codigodepartamento = item.codigodepartamento;
                    d.tipo_producto = item.tipo_producto;

                    d.numerocuenta = item.numerocuenta;
                    d.tipoidentificacion = item.tipoidentificacion;
                    d.documento = item.documento;
                    d.primerapellido = item.primerapellido;
                    d.segundoapellido = item.segundoapellido;
                    d.nombres = item.nombres;
                    d.otronombre = item.otronombre;
                    d.razonsocial = item.razonsocial;

                    d.tipoidentificaciondeconsignante = item.tipoidentificaciondeconsignante;
                    d.tipodocconsignante = item.tipodocconsignante;
                    d.primerapellidoconsignante = item.primerapellidoconsignante;
                    d.segundoapellidoconsignante = item.segundoapellidoconsignante;
                    d.nombreconsignante = item.nombreconsignante;
                    d.otronombreconsignante = item.otronombreconsignante;
                    repor.Add(d);
                }
            }
            d = new ExpandoObject();
            d.Reporte = repor;

            return d;
        }

        public dynamic GEtTasa()
        {
            try
            {
                dynamic _a = (from d in db.tasasplazoahorrotermino
                          select new { d.plazo }).GroupBy(X => X.plazo).Select(X => X.FirstOrDefault()).ToList();
                
                if (_a == null)
                {
                    // throw new Excepciones("POR-EDI-001", "No existe la Persona", "No se encontro la cedula solicitada o esta inactiva");
                }

                return _a;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public dynamic GETPermanentes(string txtcedula,int tipo)
        {
            DateTime fechas = DateTime.Now.AddMonths(-3).AddDays(-(DateTime.Now.Day-1 ));
            dynamic d = new ExpandoObject();
            ICollection<dynamic> perm = new List<dynamic>();

            if (tipo == 1) { 
            dynamic ahperm = (from a in db.sahorrosalavista
                              where a.nrocuenta == txtcedula && a.fechatrabajo > fechas
                              orderby a.fechatrabajo ascending
                                select new {a.fechatrabajo, a.detalle, a.valorefectivo,a.saldoanterior,a.tasa,a.natura }).ToList();
             
            foreach (var item in ahperm)
            {
                d = new ExpandoObject();
                d.fechatrabajo = item.fechatrabajo;
                d.detalle = item.detalle;
                
                d.valorefectivo = item.valorefectivo;
                d.saldoanterior = item.saldoanterior;
                d.tasa = item.tasa;
                d.natura = item.natura;
                perm.Add(d);
            }
            }
            if (tipo ==2)
            {
                dynamic ahperm = (from a in db.sahorrosAtermino
                                  where a.nrocuenta == txtcedula && a.fechatrabajo > fechas
                                  orderby a.fechatrabajo ascending
                                  select new { a.fechatrabajo, a.detalle, a.valorefectivo, saldoanterior=0, a.tasa, a.natura }).ToList();

                foreach (var item in ahperm)
                {
                    d = new ExpandoObject();
                    d.fechatrabajo = item.fechatrabajo;
                    d.detalle = item.detalle;

                    d.valorefectivo = item.valorefectivo;
                    d.saldoanterior = item.saldoanterior;
                    d.tasa = item.tasa;
                    d.natura = item.natura;
                    perm.Add(d);
                }
            }
            if (tipo == 3)
            {
                dynamic ahperm = (from a in db.sahorrosContractual
                                  where a.nrocuenta == txtcedula && a.fechatrabajo > fechas
                                  orderby a.fechatrabajo ascending
                                  select new { a.fechatrabajo, a.detalle, a.valorefectivo, a.saldoanterior, a.tasa, a.natura }).ToList();

                foreach (var item in ahperm)
                {
                    d = new ExpandoObject();
                    d.fechatrabajo = item.fechatrabajo;
                    d.detalle = item.detalle;

                    d.valorefectivo = item.valorefectivo;
                    d.saldoanterior = item.saldoanterior;
                    d.tasa = item.tasa;
                    d.natura = item.natura;
                    perm.Add(d);
                }
            }
            if (tipo == 4)
            {
                dynamic ahperm = (from a in db.sAportesSociales
                                  where a.nrocuenta == txtcedula && a.fechatrabajo > fechas
                                  orderby a.fechatrabajo ascending
                                  select new { a.fechatrabajo, a.detalle, a.valorefectivo, a.saldoanterior, a.tasa, a.natura }).ToList();

                foreach (var item in ahperm)
                {
                    d = new ExpandoObject();
                    d.fechatrabajo = item.fechatrabajo;
                    d.detalle = item.detalle;

                    d.valorefectivo = item.valorefectivo;
                    d.saldoanterior = item.saldoanterior;
                    d.tasa = item.tasa;
                    d.natura = item.natura;
                    perm.Add(d);
                }
            }


            d = new ExpandoObject();
            d.Ahperm = perm;
           
            return d;
        }
        
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// obtiene cantidad de pagos realizados
        /// </summary>
        /// <param name="txtpagare">tipo entero</param>
        /// <param name="txtcedula">tipo entero</param>
        /// <returns></returns>
        public dynamic CuotasPagadas(int txtpagare, int txtcedula)
        {
            var query1 = (from p in db.hcreditos
                          where p.pagare == txtpagare && p.cedulasociado == txtcedula
                          select new { p.plazo, p.saldocapital }).Count();     
            return query1;
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene nombre de linea de destino 
        /// </summary>
        /// <param name="linea"></param>
        /// <returns></returns>
        public dynamic DestinosReporte(string linea)
        {
            var query1 = (from d3 in db.destinos
                          where d3.Coddestino == linea && d3.Estado == "A"
                          select new { nombredestino = d3.nombredestino }).ToList();

            ICollection<dynamic> perm = new List<dynamic>();
            dynamic d = new ExpandoObject();
            foreach (var item in query1)
            {
                d = new ExpandoObject();
                d.nombredestino = item.nombredestino;
                
                perm.Add(d);

            }


            d = new ExpandoObject();
            d.Des_reportes = perm;

            return d;
        }

        public dynamic GetLineasReporte(string linea = "")
        {
            var query = (from l in db.lineas
                         join c in db.plancuentas
                         on l.codcuenta equals c.codcuenta
                         where l.codlinea == linea
                         select new { l.codlinea, c.nombre }).ToList();

            ICollection<dynamic> perm = new List<dynamic>();
            dynamic d = new ExpandoObject();
            foreach (var item in query)
            {
                d = new ExpandoObject();
                d.codlinea = item.codlinea;
                d.nombre = item.nombre;

                perm.Add(d);
            }

            d = new ExpandoObject();
            d.datos = perm;

            return d;

        }

        public dynamic GetLineas()
        {
            var query = (from l in db.lineas 
                        join c in db.plancuentas
                        on l.codcuenta equals c.codcuenta
                        select new { l.codlinea,c.nombre }).ToList();
            
           return query;
            
        }

        public ahorrosalavista GetInfoAhorro(int txtcedula)
        {
            try
            {
                ahorrosalavista _a = (from a in db.ahorrosalavista 
                                         where
                                         a.Cedulasociado == txtcedula
                                         
                                         select a).FirstOrDefault();
                if (_a == null)
                {
                    // throw new Excepciones("POR-EDI-001", "No existe la Persona", "No se encontro la cedula solicitada o esta inactiva");
                }

                return _a;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public ahorrosAtermino GetInfoAhorro1(int txtcedula)
        {
            try
            {
                ahorrosAtermino _a = (from a in db.ahorrosAtermino
                                      where
                                      a.Cedulasociado == txtcedula

                                      select a).FirstOrDefault();
                if (_a == null)
                {
                    // throw new Excepciones("POR-EDI-001", "No existe la Persona", "No se encontro la cedula solicitada o esta inactiva");
                }

                return _a;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public ahorrosContractual GetInfoAhorro2(int txtcedula)
        {
            try
            {
                ahorrosContractual _a = (from a in db.ahorrosContractual
                                         where
                                      a.Cedulasociado == txtcedula

                                      select a).FirstOrDefault();
                if (_a == null)
                {
                    // throw new Excepciones("POR-EDI-001", "No existe la Persona", "No se encontro la cedula solicitada o esta inactiva");
                }

                return _a;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene la info inicial del credito 
        /// </summary>
        /// <param name="txtpagare"></param>
        /// <param name="txtcedula"></param>
        /// <returns></returns>
        public creditos GetCreadito(int txtpagare, int txtcedula)
        {
            try
            {
                creditos _a = (from a in db.creditos
                                         where
                                         a.cedulasociado == txtcedula && a.pagare == txtpagare
                                         select a).FirstOrDefault();
                if (_a == null)
                {
                    // throw new Excepciones("POR-EDI-001", "No existe la Persona", "No se encontro la cedula solicitada o esta inactiva");
                }

                return _a;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// creado por Ing. Carlos Jojoa C, fec. 2016-05-06
        /// Obtiene el detalle del seguimiento de credito 
        /// </summary>
        /// <param name="cedula"></param>
        /// <param name="pagare"></param>
        /// <returns></returns>
        public dynamic GETCreditos(int cedula, int pagare)
        {


            dynamic creditos = (from a in db.seguimientocredito
                                join g1 in db.agencias on a.agencia equals g1.codigoagencia
                                where a.cedulasociado == cedula && a.pagare == pagare
                              orderby a.fechaTrabajo ascending
                              select new {a.fechaTrabajo,a.natura,a.totalefectivo,a.recibopago,agencia=g1.nombreagencia }).ToList();

            ICollection<dynamic> creditosm = new List<dynamic>();

            dynamic d = new ExpandoObject();
            foreach (var item in creditos)
            {
                d = new ExpandoObject();
                d.fechaTrabajo = item.fechaTrabajo;
                d.natura = item.natura;               
                d.totalefectivo = item.totalefectivo;
                d.recibopago = item.recibopago;
                d.agencia = item.agencia;
                creditosm.Add(d);
            }


            dynamic costos = (from a in db.costosliquidacion
                              where a.pagare == pagare
                              orderby a.fechapago ascending
                              select new { a.valorcosto, a.valorcancelado }).Skip(1).ToList();

            ICollection<dynamic> costosm = new List<dynamic>();

            foreach (var item in costos)
            {
                d = new ExpandoObject();
                d.valorcosto = item.valorcosto;
                d.valorcancelado = item.valorcancelado;

                costosm.Add(d);
            }

            d = new ExpandoObject();
            d.Creditos = creditosm;
            d.Costos = costosm;
            return d;
        }
    }
}
