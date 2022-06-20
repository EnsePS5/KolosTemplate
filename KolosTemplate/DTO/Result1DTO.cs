using System.Collections.Generic;
using System.Net;

namespace KolosTemplate.DTO {


    public class Result1DTO {

        public int IdCar { get; set; }
        public string Make { get; set; }
        public int ProductionYear { get; set; }

        public virtual ICollection<PersonDTO> PersonsDTO { get; set; }

        public string MainOwner { get; set; } //By pokazywalo imie mainownera a nie id

        public int Insurence { get; set; }

        public HttpStatusCode httpStatusCode { get; set; }
    }
}