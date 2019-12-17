namespace TourAgency.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Rout> Rout { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Taxation> Taxation { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }
        public virtual DbSet<TypeTrip> TypeTrip { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Visa> Visa { get; set; }
        public virtual DbSet<Way> Way { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.Way)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.FinalPoint);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Way1)
                .WithOptional(e => e.Country1)
                .HasForeignKey(e => e.StartPoint);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.IdRole);

            modelBuilder.Entity<Rout>()
                .HasMany(e => e.Trip)
                .WithOptional(e => e.Rout)
                .HasForeignKey(e => e.NumberRout);

            modelBuilder.Entity<Rout>()
                .HasMany(e => e.Visa)
                .WithOptional(e => e.Rout)
                .HasForeignKey(e => e.NumberRout);

            modelBuilder.Entity<Taxation>()
                .HasMany(e => e.Client)
                .WithOptional(e => e.Taxation)
                .HasForeignKey(e => e.IdTaxation);

            modelBuilder.Entity<Trip>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TypeTrip>()
                .HasMany(e => e.Trip)
                .WithOptional(e => e.TypeTrip)
                .HasForeignKey(e => e.IdTypeTrip);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Agent)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Client)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Trip)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Visa>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Way>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Way>()
                .HasMany(e => e.Rout)
                .WithRequired(e => e.Way)
                .HasForeignKey(e => e.IdWay)
                .WillCascadeOnDelete(false);
        }
    }
}
