using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Models
{
    public class CustomerDataModel
    {
       
    }
    public class UIAF
    {
        public int nro { get; set; }
        public string fechatransaccion { get; set; }
        
        public int moneda { get; set; }
        public string codigooficina { get; set; }
        public string codigodepartamento { get; set; }
        public int tipo_producto { get; set; }
        public int tipo_transaccion { get; set; }
        public string numerocuenta { get; set; }
        public string tipoidentificacion { get; set; }
        public string documento { get; set; }
        public string primerapellido { get; set; }
        public string segundoapellido { get; set; }
        public string nombres { get; set; }
        public string otronombre { get; set; }
        public string razonsocial { get; set; }
        public string actividadeconomica { get; set; }
        public Decimal salario { get; set; }
        public Decimal valortransaccion { get; set; }
        public string tipoidentificaciondeconsignante { get; set; }
        public string tipodocconsignante { get; set; }
        public string primerapellidoconsignante { get; set; }
        public string segundoapellidoconsignante { get; set; }
        public string nombreconsignante { get; set; }
        public string otronombreconsignante { get; set; }

        //// 
        public List<UIAF> Customer { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int NoOfPages { get; set; }
    }
}