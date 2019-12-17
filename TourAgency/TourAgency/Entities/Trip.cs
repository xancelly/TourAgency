namespace TourAgency.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trip")]
    public partial class Trip
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? DayCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? TypeTrip { get; set; }

        public int? UserId { get; set; }

        public int? NumberRout { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual Rout Rout { get; set; }

        public virtual TypeTrip TypeTrip1 { get; set; }

        public virtual Users Users { get; set; }
    }
}
