//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TourAgency.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Way
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Way()
        {
            this.Trip = new HashSet<Trip>();
        }
    
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string FinalPoint { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Country Country1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trip> Trip { get; set; }
    }
}
