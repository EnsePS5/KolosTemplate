
using KolosTemplate.DTO;
using System.Threading.Tasks;

namespace KolosTemplate.Services {



    public interface IDbService {


        //Task<Result1DTO> GetCarByCarIdLinq(int CarId);

        Task<Result1DTO> GetCarByCarIdSQL(int CarId);
    }
}