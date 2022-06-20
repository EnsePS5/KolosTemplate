using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KolosTemplate.Entities
{

    public class CarEfConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {

            builder.HasKey(e => e.IdCar).HasName("Car_pk");
            builder.Property(e => e.IdCar).UseIdentityColumn();

            builder.Property(e => e.Make).IsRequired().HasMaxLength(15);
            builder.Property(e => e.ProductionYear).IsRequired();

            builder.ToTable("Car");


            //Seedowanie przykładowymi danymi 

            builder.HasData(new Car
            {
                IdCar = 1,
                Make = "Audi",
                ProductionYear = 2021

            });
            builder.HasData(new Car
            {
                IdCar = 2,
                Make = "Audi",
                ProductionYear = 2019

            });
            builder.HasData(new Car
            {
                IdCar = 3,
                Make = "Toyota",
                ProductionYear = 2021

            });
        }
    }
}