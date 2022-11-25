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
        double tasa_efectiva;

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
        [Range(100000, 100000000000)]
        //[DisplayFormat(DataFormatString = "{0:C}")]
        public double? Capital { get; set; }

        [Required]
        [DisplayName("Periodo pago de interes: ")]
        public string Periodo_Ahorro { get; set; }
        
        [Required]
        [DisplayName("Plazo en días: ")]
        public string Periodo { get; set; }

        public double Tasa_Efectiva(string tasa)
        {
            switch (tasa)
            {
                case "90":
                    tasa_efectiva = 14;
                    break;

                case "120":
                    tasa_efectiva = 14.25;
                    break;

                case "150":
                    tasa_efectiva = 14.5;
                    break;

                case "180":
                    tasa_efectiva = 14.75;
                    break;

                case "210":
                    tasa_efectiva = 15;
                    break;

                case "240":
                    tasa_efectiva = 15.25;
                    break;

                case "270":
                    tasa_efectiva = 15.5;
                    break;

                case "300":
                    tasa_efectiva = 15.75;
                    break;

                case "330":
                    tasa_efectiva = 16;
                    break;

                case "360":
                    tasa_efectiva = 16.25;
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