namespace KolosTemplate.Entities
{
    public class CarPerson
    {

        public int IdCar { get; set; }
        public int IdPerson { get; set; }
        public byte MainOwner { get; set; }


        public virtual Car IdCarNavigation { get; set; }
        public virtual Person IdPersonNavigation { get; set; }
    }
}