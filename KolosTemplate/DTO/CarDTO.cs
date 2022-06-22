using System.Collections.Generic;

namespace KolosTemplate.DTO
{
    public class CarDTO
    {
        public string Make { get; set; }
        public int ProductionYear { get; set; }
        public int MainOwnerId { get; set; }

        public IEnumerable<int> OwnersId { get; set; }


        public CarDTO() {

            OwnersId = new HashSet<int>();
        }
    }
}