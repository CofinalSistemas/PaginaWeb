//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ingenio.BO.Ingenio
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class IngenioEntities : DbContext
    {
        public IngenioEntities()
            : base("name=IngenioEntities")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 0;
            
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<UsuariosRoles> UsuariosRoles { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesModulos> RolesModulos { get; set; }
        public virtual DbSet<SEGUIMIENTO_SIMULADORES> SEGUIMIENTO_SIMULADORES { get; set; }
        public virtual DbSet<Sliders> Sliders { get; set; }
        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Galeria> Galeria { get; set; }
        public virtual DbSet<Log_Usuarios> Log_Usuarios { get; set; }
        public virtual DbSet<Asociados_Ingenio> Asociados_Ingenio { get; set; }
        public virtual DbSet<Param_dian> Param_dian { get; set; }
    
        public virtual ObjectResult<Nullable<long>> UIAF_MULTIPLES(Nullable<System.DateTime> fECHAINI, Nullable<System.DateTime> fECHAFIN)
        {
            var fECHAINIParameter = fECHAINI.HasValue ?
                new ObjectParameter("FECHAINI", fECHAINI) :
                new ObjectParameter("FECHAINI", typeof(System.DateTime));
    
            var fECHAFINParameter = fECHAFIN.HasValue ?
                new ObjectParameter("FECHAFIN", fECHAFIN) :
                new ObjectParameter("FECHAFIN", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("UIAF_MULTIPLES", fECHAINIParameter, fECHAFINParameter);
        }
    
        public virtual int UIAF_MULTIPLES_TRANSAC(Nullable<System.DateTime> fECHAINI, Nullable<System.DateTime> fECHAFIN)
        {
            var fECHAINIParameter = fECHAINI.HasValue ?
                new ObjectParameter("FECHAINI", fECHAINI) :
                new ObjectParameter("FECHAINI", typeof(System.DateTime));
    
            var fECHAFINParameter = fECHAFIN.HasValue ?
                new ObjectParameter("FECHAFIN", fECHAFIN) :
                new ObjectParameter("FECHAFIN", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UIAF_MULTIPLES_TRANSAC", fECHAINIParameter, fECHAFINParameter);
        }
    
        public virtual int P_Transaccciones_Uiaf(Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta, Nullable<decimal> valorMinimoInclusion)
        {
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("FechaDesde", fechaDesde) :
                new ObjectParameter("FechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("FechaHasta", fechaHasta) :
                new ObjectParameter("FechaHasta", typeof(System.DateTime));
    
            var valorMinimoInclusionParameter = valorMinimoInclusion.HasValue ?
                new ObjectParameter("ValorMinimoInclusion", valorMinimoInclusion) :
                new ObjectParameter("ValorMinimoInclusion", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("P_Transaccciones_Uiaf", fechaDesdeParameter, fechaHastaParameter, valorMinimoInclusionParameter);
        }
    
        public virtual ObjectResult<P_Operador_UiAf_Result> P_Operador_UiAf()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_Operador_UiAf_Result>("P_Operador_UiAf");
        }
    
        public virtual ObjectResult<P_Operador_UiAf2_Result> P_Operador_UiAf2()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_Operador_UiAf2_Result>("P_Operador_UiAf2");
        }
    
        public virtual ObjectResult<P_Operador_UiAf3_Result> P_Operador_UiAf3()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_Operador_UiAf3_Result>("P_Operador_UiAf3");
        }
    
        public virtual ObjectResult<UIAF_TRANSMULTIPLES_Result> UIAF_TRANSMULTIPLES(Nullable<System.DateTime> fECHAINI, Nullable<System.DateTime> fECHAFIN)
        {
            var fECHAINIParameter = fECHAINI.HasValue ?
                new ObjectParameter("FECHAINI", fECHAINI) :
                new ObjectParameter("FECHAINI", typeof(System.DateTime));
    
            var fECHAFINParameter = fECHAFIN.HasValue ?
                new ObjectParameter("FECHAFIN", fECHAFIN) :
                new ObjectParameter("FECHAFIN", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UIAF_TRANSMULTIPLES_Result>("UIAF_TRANSMULTIPLES", fECHAINIParameter, fECHAFINParameter);
        }
    
        public virtual ObjectResult<UIAF_TRANSMULTIPLES2_Result> UIAF_TRANSMULTIPLES2(Nullable<System.DateTime> fECHAINI, Nullable<System.DateTime> fECHAFIN)
        {
            var fECHAINIParameter = fECHAINI.HasValue ?
                new ObjectParameter("FECHAINI", fECHAINI) :
                new ObjectParameter("FECHAINI", typeof(System.DateTime));
    
            var fECHAFINParameter = fECHAFIN.HasValue ?
                new ObjectParameter("FECHAFIN", fECHAFIN) :
                new ObjectParameter("FECHAFIN", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UIAF_TRANSMULTIPLES2_Result>("UIAF_TRANSMULTIPLES2", fECHAINIParameter, fECHAFINParameter);
        }
    
        public virtual ObjectResult<P_Operador_UiAf4_Result> P_Operador_UiAf4(Nullable<System.DateTime> fechai, Nullable<System.DateTime> fechaf)
        {
            var fechaiParameter = fechai.HasValue ?
                new ObjectParameter("fechai", fechai) :
                new ObjectParameter("fechai", typeof(System.DateTime));
    
            var fechafParameter = fechaf.HasValue ?
                new ObjectParameter("fechaf", fechaf) :
                new ObjectParameter("fechaf", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_Operador_UiAf4_Result>("P_Operador_UiAf4", fechaiParameter, fechafParameter);
        }
    }
}
