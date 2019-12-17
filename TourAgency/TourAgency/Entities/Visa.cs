namespace TourAgency.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visa")]
    public partial class Visa
    {
        public int Id { get; set; }

        public int? NumberRout { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public virtual Rout Rout { get; set; }
    }
}
