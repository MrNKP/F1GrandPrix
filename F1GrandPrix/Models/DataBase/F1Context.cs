namespace F1GrandPrix.Models.DataBase
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class F1Context : DbContext
    {
        public F1Context()
            : base("name=F1Context")
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Drivers)
                .WithRequired(e => e.Car)
                .HasForeignKey(e => e.Car_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Driver)
                .HasForeignKey(e => e.Driver_1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Driver>()
                .HasMany(e => e.Teams1)
                .WithRequired(e => e.Driver1)
                .HasForeignKey(e => e.Driver_2)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
