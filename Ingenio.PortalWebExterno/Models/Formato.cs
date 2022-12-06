using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Models
{
    public class Formato
    {
        [Required(ErrorMessage = "La cedula es obligatoria")]
        [Display(Name = "Cedula*")]
        public decimal Cedula { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorio")]
        [Display(Name = "Nombre*")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Los apellidos son obligatorio")]
        [Display(Name = "Apellido*")]
        public string Apellido { get; set; }
        public Asociados Asociado { get; set; }
        public Agencias Agencia { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [Display(Name = "Correo*")]
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime Fecha { get; set; }
        
        [StringLength(300, ErrorMessage = "Escribe maximo 300 palabras")]
        public string Descripcion { get; set; }

        [Display(Name = "Autorizacion*")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Tiene que autorizar")]
        public bool Autorizacion { get; set; }
    }

    public enum Asociados
    {
        Si,
        No
    }
    public enum Agencias
    {
        Pasto,
        Sandona,
        Ipiales
    }
}