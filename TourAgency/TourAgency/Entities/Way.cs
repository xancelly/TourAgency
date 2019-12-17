namespace TourAgency.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Way")]
    public partial class Way
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Way()
        {
            Rout = new HashSet<Rout>();
        }

        public int Id { get; set; }

        [StringLength(2)]
        public string StartPoint { get; set; }

        [StringLength(2)]
        public string FinalPoint { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public virtual Country Country { get; set; }

        public virtual Country Country1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rout> Rout { get; set; }
    }
}
