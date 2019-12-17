namespace TourAgency.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        public string Email { get; set; }

        [StringLength(10)]
        public string Passport { get; set; }

        [StringLength(10)]
        public string Address { get; set; }

        public int? IdUser { get; set; }

        public int? IdTaxation { get; set; }

        public virtual Taxation Taxation { get; set; }

        public virtual Users Users { get; set; }
    }
}
