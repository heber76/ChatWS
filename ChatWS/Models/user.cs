//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatWS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.message = new HashSet<message>();
        }
    
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int idState { get; set; }
        public System.DateTime date_created { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string access_Token { get; set; }
    
        public virtual cState cState { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<message> message { get; set; }
    }
}
