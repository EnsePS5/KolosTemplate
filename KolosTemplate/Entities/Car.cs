using System.Collections.Generic;

namespace KolosTemplate.Entities {

    public class Car {

        public int IdCar { get; set; }
        public string Make { get; set; }
        public int ProductionYear {get; set;}


        public virtual ICollection<CarPerson> CarPersons { get; set; }

        public Car() {

            CarPersons = new HashSet<CarPerson>(); 
        }
    }
}