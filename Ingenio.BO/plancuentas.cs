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
    
    public partial class plancuentas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public plancuentas()
        {
            this.costosliquidacion = new HashSet<costosliquidacion>();
            this.cuentasnovedadesvarias = new HashSet<cuentasnovedadesvarias>();
            this.lineas = new HashSet<lineas>();
            this.lineas1 = new HashSet<lineas>();
            this.lineas2 = new HashSet<lineas>();
            this.parameahorroAlaVista = new HashSet<parameahorroAlaVista>();
            this.parameahorroAlaVista1 = new HashSet<parameahorroAlaVista>();
            this.parameahorroAlaVista2 = new HashSet<parameahorroAlaVista>();
        }
    
        public string codcuenta { get; set; }
        public string naturaleza { get; set; }
        public string nombre { get; set; }
        public Nullable<int> nivel { get; set; }
        public string documentoreferencia { get; set; }
        public string cuentamovimiento { get; set; }
        public string tercero { get; set; }
        public string requierecentrocostos { get; set; }
        public string centrocostos { get; set; }
        public Nullable<int> año { get; set; }
        public string triibutaria { get; set; }
        public string cuentatributaria { get; set; }
        public Nullable<decimal> porcentaje { get; set; }
        public Nullable<decimal> baseretencion { get; set; }
        public string estado { get; set; }
        public string estadodecuentas { get; set; }
        public string anexos { get; set; }
        public Nullable<decimal> debito1 { get; set; }
        public Nullable<decimal> debito2 { get; set; }
        public Nullable<decimal> debito3 { get; set; }
        public Nullable<decimal> debito4 { get; set; }
        public Nullable<decimal> debito5 { get; set; }
        public Nullable<decimal> debito6 { get; set; }
        public Nullable<decimal> debito7 { get; set; }
        public Nullable<decimal> debito8 { get; set; }
        public Nullable<decimal> debito9 { get; set; }
        public Nullable<decimal> debito10 { get; set; }
        public Nullable<decimal> debito11 { get; set; }
        public Nullable<decimal> debito12 { get; set; }
        public Nullable<decimal> debito13 { get; set; }
        public Nullable<decimal> credito1 { get; set; }
        public Nullable<decimal> credito2 { get; set; }
        public Nullable<decimal> credito3 { get; set; }
        public Nullable<decimal> credito4 { get; set; }
        public Nullable<decimal> credito5 { get; set; }
        public Nullable<decimal> credito6 { get; set; }
        public Nullable<decimal> credito7 { get; set; }
        public Nullable<decimal> credito8 { get; set; }
        public Nullable<decimal> credito9 { get; set; }
        public Nullable<decimal> credito10 { get; set; }
        public Nullable<decimal> credito11 { get; set; }
        public Nullable<decimal> credito12 { get; set; }
        public Nullable<decimal> credito13 { get; set; }
        public Nullable<decimal> debitoapertura { get; set; }
        public Nullable<decimal> creditoApertura { get; set; }
        public Nullable<long> nitgenerico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<costosliquidacion> costosliquidacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cuentasnovedadesvarias> cuentasnovedadesvarias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lineas> lineas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lineas> lineas1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lineas> lineas2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parameahorroAlaVista> parameahorroAlaVista { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parameahorroAlaVista> parameahorroAlaVista1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parameahorroAlaVista> parameahorroAlaVista2 { get; set; }
    }
}
