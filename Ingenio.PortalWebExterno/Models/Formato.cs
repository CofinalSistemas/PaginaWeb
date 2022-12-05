using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Models
{
    public class Formato
    {
        [Required(ErrorMessage = "Title is required")]
        public decimal Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Asociados Asociado { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        [Display(Name = "Autorizacion*")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Tiene que autorizar")]
        public bool Autorizacion { get; set; }

        //public formfiles Archivo { get; set; }
    }

    public enum Asociados
    {
        si,
        no
    }
    public enum Agencias
    {
        Pasto,
        Sandona,
        Ipiales
    }
}