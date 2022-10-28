using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;



namespace Ingenio.PortalWebExterno.Models
{

    public class user
    {
        [Required]
        [DisplayName("Cedula: ")]
        public string Cedula { get; set; }
        
        [Required]
        [DisplayName("Fecha de Expedición: ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime fechaExpedicion { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        [Required]
        [DisplayName("Terminos y Condiciones: ")]
        public bool Terminos { get; set; }
    }
}