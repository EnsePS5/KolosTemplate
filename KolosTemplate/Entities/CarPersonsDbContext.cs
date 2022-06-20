using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace KolosTemplate.Entities
{

    public partial class CarPersonsDbContext : DbContext
    {
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<CarPerson> CarPersons { get; set; }


        public CarPersonsDbContext() { }
        public CarPersonsDbContext(DbContextOptions<CarPersonsDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarEfConfiguration).GetTypeInfo().Assembly);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}