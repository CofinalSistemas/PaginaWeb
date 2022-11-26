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
        decimal tasa_efectiva;

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
        [DisplayName("Monto de Capital: ")]
        [DisplayFormat(DataFormatString = "{0:0,0.0}")]
        [Range(100000, 100000000000)]
        //[DisplayFormat(DataFormatString = "{0:C}")]
        public double Capital { get; set; }

        [Required]
        [DisplayName("Periodo pago de interes: ")]
        public string Periodo_Ahorro { get; set; }
        
        [Required]
        [DisplayName("Plazo en días: ")]
        public string Periodo { get; set; }

        public decimal Tasa_Efectiva(string tasa)
        {
            switch (tasa)
            {
                case "90":
                    tasa_efectiva = 14.00m;
                    break;

                case "120":
                    tasa_efectiva = 14.25m;
                    break;

                case "150":
                    tasa_efectiva = 14.50m;
                    break;

                case "180":
                    tasa_efectiva = 14.75m;
                    break;

                case "210":
                    tasa_efectiva = 15.00m;
                    break;

                case "240":
                    tasa_efectiva = 15.25m;
                    break;

                case "270":
                    tasa_efectiva = 15.50m;
                    break;

                case "300":
                    tasa_efectiva = 15.75m;
                    break;

                case "330":
                    tasa_efectiva = 16.00m;
                    break;

                case "360":
                    tasa_efectiva = 16.25m;
                    break;

            }

            return tasa_efectiva;
        }

        public string Tasa_Nominal { get; set; }

        public string Interes_Periodo { get; set; }

        public string Rte_Fuente { get; set; }

        public string Interes_Neto { get; set; }

    }
}