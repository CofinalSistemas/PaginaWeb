//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ingenio.BO
{
    using System;
    using System.Collections.Generic;
    
    public partial class personacargo
    {
        public int registro { get; set; }
        public long cedulaAsociado { get; set; }
        public string nombre { get; set; }
        public Nullable<long> nit { get; set; }
        public string tipoidentificacion { get; set; }
        public string sexo { get; set; }
        public Nullable<System.DateTime> fechanacimiento { get; set; }
        public string parentesco { get; set; }
        public Nullable<System.DateTime> fechaingreso { get; set; }
        public Nullable<byte> szin { get; set; }
        public string estudiante { get; set; }
        public string discapacitado { get; set; }
        public string terceraedad { get; set; }
        public string administracion { get; set; }
        public string tratamiento { get; set; }
        public string codigoniveleducativo { get; set; }
        public string email { get; set; }
        public string celular { get; set; }
    
        public virtual asociados asociados { get; set; }
    }
}
