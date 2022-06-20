using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KolosTemplate.Entities
{

    public class CarPersonEfConfiguration : IEntityTypeConfiguration<CarPerson>
    {
        public void Configure(EntityTypeBuilder<CarPerson> builder)
        {
            //Jesli jest klucz do tabeli wieledowiele (IdCarPerson) to robimy tak V
            //builder.HasKey(e => e.IdCarPerson).HasName("CarPerson_pk");
            //
            //builder.Property(e. => e.IdPerson).IsRequired();
            //builder.Property(e. => e.IdCar).IsRequired();

            builder.HasKey(e => new { e.IdPerson, e.IdCar}).HasName("CarPerson_pk");

            builder.Property(e => e.MainOwner).IsRequired();

            builder.HasOne(e => e.IdCarNavigation)
                .WithMany(e => e.CarPersons)
                .HasForeignKey(e => e.IdCar)
                .HasConstraintName("CarPerson_Car")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.IdPersonNavigation)
                .WithMany(e => e.CarPersons)
                .HasForeignKey(e => e.IdPerson)
                .HasConstraintName("CarPerson_Person")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.ToTable("Car_Person");

            //Seedowanie przykładowymi danymi 

            builder.HasData(new CarPerson 
            { 
                IdCar = 1,
                IdPerson = 1,
                MainOwner = 1
            });
            builder.HasData(new CarPerson
            {
                IdCar = 1,
                IdPerson = 3,
                MainOwner = 0
            });
            builder.HasData(new CarPerson
            {
                IdCar = 2,
                IdPerson = 3,
                MainOwner = 1
            });
            builder.HasData(new CarPerson
            {
                IdCar = 3,
                IdPerson = 2,
                MainOwner = 1
            });
            builder.HasData(new CarPerson
            {
                IdCar = 3,
                IdPerson = 1,
                MainOwner = 0
            });
        }
    }
}