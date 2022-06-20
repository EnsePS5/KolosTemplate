
using KolosTemplate.DTO;
using System.Threading.Tasks;

namespace KolosTemplate.Services {



    public interface IDbService {


        Task<Result1DTO> GetCarByCarId(int CarId);
    }
}