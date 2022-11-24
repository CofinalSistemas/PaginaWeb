using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Models
{
    public class SimuladorCredito
    {
        readonly double Tasa_E = 12.2;
        private string _strNombre;
        private int _intEdad;

        // Constructor


        public double Saldo
        {
            get { return Tasa_E; }
        }
        public string Id 
        {
            get
            {
                return _strNombre;
            }
            set
            {
                _strNombre = value;
            }
        }

        [Required]
        [DisplayName("Capital: ")]
        [Range(500000, 100000000)]
        //[DisplayFormat(DataFormatString = "{0:C}")]
        public float? Capital { get; set; }
        public string Tasa_Efectiva { get; set; }
        public string Periodo_Ahorro { get; set; }
        public string Tasa_Nominal { get; set; }
        public string Interes_Periodo { get; set; }
        public string Periodo { get; set; }
        public string Rte_Fuente { get; set; }
        public string Interes_Neto { get; set; }
    }
}