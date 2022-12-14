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
    
    public partial class Viaje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Viaje()
        {
            this.BL = new HashSet<BL>();
        }
    
        public int ID { get; set; }
        public string cod_adua { get; set; }
        public string num_viaj { get; set; }
        public Nullable<System.DateTime> fec_sal { get; set; }
        public Nullable<System.DateTime> fec_arr { get; set; }
        public string loc_cod { get; set; }
        public Nullable<int> uso { get; set; }
        public Nullable<int> total_bls { get; set; }
        public Nullable<int> total_paq { get; set; }
        public Nullable<int> total_cont { get; set; }
        public Nullable<decimal> total_gm { get; set; }
        public string cod_carr { get; set; }
        public string nom_carr { get; set; }
        public string dir_carr { get; set; }
        public Nullable<int> cod_mod_trans { get; set; }
        public string id_trans { get; set; }
        public string cod_nac_trans { get; set; }
        public string cod_pto_sal { get; set; }
        public string cod_pto_des { get; set; }
        public string cod_lin { get; set; }
        public string alm_dest { get; set; }
        public string cod_buq { get; set; }
        public string nom_buq { get; set; }
        public string file_path { get; set; }
        public string co_us_in { get; set; }
        public Nullable<System.DateTime> fe_us_in { get; set; }
        public string co_us_mo { get; set; }
        public Nullable<System.DateTime> fe_us_mo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BL> BL { get; set; }
    }
}
