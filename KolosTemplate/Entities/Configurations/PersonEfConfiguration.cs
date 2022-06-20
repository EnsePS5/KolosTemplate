using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KolosTemplate.Entities
{

    public class PersonEfConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            builder.HasKey(e => e.IdPerson).HasName("Person_pk");
            builder.Property(e => e.IdPerson).UseIdentityColumn();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
            builder.Property(e => e.Surname).IsRequired().HasMaxLength(40);
            builder.Property(e => e.DrivingLicense).HasMaxLength(5);

            builder.ToTable("Person");


            //Seedowanie przykładowymi danymi 

            builder.HasData(new Person
            {
                IdPerson = 1,
                Name = "Oskar",
                Surname = "Dudzik",
                DrivingLicense = "B"

            });
            builder.HasData(new Person
            {
                IdPerson = 2,
                Name = "Dominik",
                Surname = "Kozluk",
                DrivingLicense = "B"

            });
            builder.HasData(new Person
            {
                IdPerson = 3,
                Name = "Kacper",
                Surname = "Godlewski",
                DrivingLicense = null

            });
        }
    }
}