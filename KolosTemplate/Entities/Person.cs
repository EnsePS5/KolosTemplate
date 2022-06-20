using System.Collections.Generic;

namespace KolosTemplate.Entities
{
    public class Person
    {

        public int IdPerson { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DrivingLicense { get; set; }


        public virtual ICollection<CarPerson> CarPersons { get; set; }

        public Person() {

            CarPersons = new HashSet<CarPerson>();
        }
    }
}