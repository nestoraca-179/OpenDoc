//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenDoc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BL()
        {
            this.Contenedor = new HashSet<Contenedor>();
        }
    
        public int ID { get; set; }
        public int id_viaje { get; set; }
        public string num_bl { get; set; }
        public Nullable<int> num_naturaleza { get; set; }
        public string tipo { get; set; }
        public string pto_carga { get; set; }
        public string pto_descarga { get; set; }
        public string destino { get; set; }
        public string booking { get; set; }
        public string condicion { get; set; }
        public Nullable<int> tipo_mercancia { get; set; }
        public string nom_consign { get; set; }
        public string dir_consign { get; set; }
        public string nom_notify { get; set; }
        public string dir_notify { get; set; }
        public string nom_export { get; set; }
        public string dir_export { get; set; }
        public Nullable<decimal> gross_mass { get; set; }
        public string shipping_marks { get; set; }
        public Nullable<int> num_conts { get; set; }
        public Nullable<decimal> volumen { get; set; }
        public string descripcion { get; set; }
        public string tipo_paq { get; set; }
        public Nullable<int> cant_paq { get; set; }
        public string precinto_bl { get; set; }
        public string sobre_dimens { get; set; }
        public string observaciones { get; set; }
        public Nullable<bool> gobierno { get; set; }
        public Nullable<int> fletes { get; set; }
        public string mone_flet { get; set; }
        public string co_us_in { get; set; }
        public Nullable<System.DateTime> fe_us_in { get; set; }
        public string co_us_mo { get; set; }
        public Nullable<System.DateTime> fe_us_mo { get; set; }
    
        public virtual Viaje Viaje { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contenedor> Contenedor { get; set; }
    }
}
