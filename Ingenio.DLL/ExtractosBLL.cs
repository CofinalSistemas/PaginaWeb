using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenio.BO;
using Ingenio.DAL;
using System.Data;
using Ingenio.BO.Ingenio;

namespace Ingenio.DLL
{
    public class ExtractosBLL
    {
        ExtractosDAL segDAL = new ExtractosDAL();
        public seguimientocredito GetSeguimiento(int txtpagare, int txtcedula)
        {
            return segDAL.GetSeguimiento(txtpagare, txtcedula);
        }

        public creditos GetCreaditos(int txtpagare, int txtcedula)
        {
            return segDAL.GetCreadito(txtpagare, txtcedula);
        }

        public dynamic GetCreditos(int txtcedula, int txtpagare)
        {
            dynamic cred = segDAL.GETCreditos(txtcedula, txtpagare);
            return cred;
        }

        public dynamic GetDatosCDATyCre(DateTime inicial, DateTime final,string n)
        {
            dynamic perm = segDAL.GetDatosCDATyCre(inicial,final,n);
            return perm;
        }

        public dynamic GetTasas()
        {
            dynamic perm = segDAL.GEtTasa();
            return perm;
        }

        public Param_dian SaveInteres(Param_dian dat)
        {
            segDAL.SaveInteres(dat);
            return null;
        }

        public dynamic GetTasas2(int plazo)
        {
            dynamic perm = segDAL.GEtTasa2(plazo);
            return perm;
        }

        public dynamic GetReporte(DateTime? inicial, DateTime? final, int mostrar,string tipo)
        {
            dynamic perm = segDAL.GetReporte(inicial, final, mostrar,tipo);
            return perm;
        }

        public dynamic Getpagares(int id)
        {
            dynamic perm = segDAL.GETpagares(id);
            return perm;
        }

        public dynamic GetPermanentes(string txtcedula,int tipo)
        {
            dynamic perm = segDAL.GETPermanentes(txtcedula,tipo);
            return perm;
        }

        public dynamic Historicodepagoscreditos(int txtpagare, int txtcedula)
        {
            dynamic perm = segDAL.Historicodepagoscreditos(txtpagare,txtcedula);
            return perm;
        }

        public ICollection<Param_dian> listinteres()
        {
            return segDAL.listinteres();
        }

        public dynamic GetCuentas(int id)
        {
            dynamic perm = segDAL.GetCuentas(id);
            return perm;
        }

        public object CuotasPagadas(int txtpagare, int txtcedula)
        {
            dynamic perm = segDAL.CuotasPagadas(txtpagare,txtcedula);
            return perm;
        }

        public Param_dian actuainteres(int id)
        {
            return segDAL.actuainteres(id);
        }

        public dynamic DestinosReporte(string coddestino)
        {
            dynamic perm = segDAL.DestinosReporte(coddestino);
            return perm;
        }

        public dynamic GetPagos_cuota(int txtcedula, int txtpagare, DateTime f_ultimopago2, DateTime f_ultimopago1)
        {
            dynamic perm = segDAL.GetPagos_cuota(txtcedula,txtpagare,f_ultimopago2, f_ultimopago1);
            return perm;
        }

        public dynamic GetTasas4(int v)
        {
            dynamic perm = segDAL.GetTasas4(v);
            return perm;
        }

        public aportessociales GetInfoAhorro3(int txtcedula)
        {
            return segDAL.GetInfoAhorro3(txtcedula);
        }

        public dynamic GetTasas3(int txtCapital, int v)
        {
            dynamic perm = segDAL.GetTasas3(txtCapital,v);
            return perm;
        }

        public ahorrosalavista GetInfoAhorro(int txtcedula)
        {
            return segDAL.GetInfoAhorro(txtcedula);
        }
        public ahorrosAtermino GetInfoAhorro1(int txtcedula)
        {
            return segDAL.GetInfoAhorro1(txtcedula);
        }
        public ahorrosContractual GetInfoAhorro2(int txtcedula)
        {
            return segDAL.GetInfoAhorro2(txtcedula);
        }

        public dynamic transaccion(DateTime? inicial, DateTime? final, int chk)
        {
            dynamic perm = segDAL.transaccion(inicial, final,chk);
            return perm;
        }

        public dynamic GetLineas()
        {
            dynamic perm = segDAL.GetLineas();
            return perm;
        }

        public dynamic GetReporteSocial(DateTime? inicial, DateTime? final, int txtcedula,string tipo)
        {
            dynamic perm = segDAL.GetReporteSocial(inicial, final, txtcedula,tipo);
            return perm;
        }

        public dynamic GetReporteContractual(DateTime? inicial, DateTime? final, int txtcedula,string tipo)
        {
            dynamic perm = segDAL.GetReporteContractual(inicial, final, txtcedula,tipo);
            return perm;
        }

        public dynamic GetReporteAtermino(DateTime? inicial, DateTime? final, int txtcedula,string tipo)
        {
            dynamic perm = segDAL.GetReporteAtermino(inicial, final, txtcedula,tipo);
            return perm;
        }

        public dynamic GetLineasReporte(string txtTabla)
        {
            dynamic perm = segDAL.GetLineasReporte(txtTabla);
            return perm;
        }

        public dynamic GetReporteAlavista(DateTime? inicial, DateTime? final, int txtcedula,string tipo)
        {
            dynamic perm = segDAL.GetReporteAlavista(inicial, final, txtcedula,tipo);
            return perm;
        }

        public dynamic GetLineas2(string linea)
        {
            dynamic perm = segDAL.GetLineas2(linea);
            return perm;
        }

        public dynamic GetLineas3(string v, string destino)
        {
            dynamic perm = segDAL.GetLineas3(v,destino);
            return perm;
        }
        public dynamic GetLineas33(string v, string destino)
        {
            dynamic perm = segDAL.GetLineas33(v, destino);
            return perm;
        }


        public SEGUIMIENTO_SIMULADORES GetSeguimiento_email(SEGUIMIENTO_SIMULADORES aa)
        {
            segDAL.GetSeguimiento_email(aa);
            return null;
        }

        public dynamic GetLineas4(string line)
        {
            dynamic perm = segDAL.GetLineas4(line);
            return perm;
        }

        public dynamic GetMovimientos(string v)
        {
            dynamic perm = segDAL.GetMovimientos(v);
            return perm;
        }

        public dynamic GetMov(int id)
        {
            dynamic perm = segDAL.GetMov(id);
            return perm;
        }

        public dynamic GetAlaVista(int id,int tipo)
        {
            dynamic perm = segDAL.GetAlaVista(id,tipo);
            return perm;
        }

        public dynamic GetEspecifico(int nroc, int id)
        {
            dynamic perm = segDAL.GetEspecifico(nroc,id);
            return perm;
        }

        public dynamic GetEspecificoAtermino(int nroc, int id)
        {
            dynamic perm = segDAL.GetEspecificoAtermino(nroc, id);
            return perm;
        }

        public dynamic GetAtermino(int id)
        {
            dynamic perm = segDAL.GetAtermino(id);
            return perm;
        }

        public dynamic Getapermanente(int id)
        {
            dynamic perm = segDAL.Getapermanente(id);
            return perm;
        }

        public dynamic Getacontractual(int id)
        {
            dynamic perm = segDAL.Getacontractual(id);
            return perm;
        }

        public dynamic Getasociales(int id)
        {
            dynamic perm = segDAL.Getasociales(id);
            return perm;
        }

        public dynamic GetEspecificoContractual(int nroc, int id)
        {
            dynamic perm = segDAL.GetEspecificoContractual(nroc, id);
            return perm;
        }

        public dynamic GetEspecificoPermanente(int nroc, int id)
        {
            dynamic perm = segDAL.GetEspecificoPermanente(nroc, id);
            return perm;
        }

        public dynamic GetEspecificoAsociales(string nroc, int id)
        {
            dynamic perm = segDAL.GetEspecificoAsociales(nroc, id);
            return perm;
        }
    }
}
