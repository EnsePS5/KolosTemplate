
using KolosTemplate.DTO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KolosTemplate.Services {



    public interface IDbService {


        Task<Result1DTO> GetCarByCarIdLinq(int CarId);

        //Task<Result1DTO> GetCarByCarIdSQL(int CarId);

        Task<Result2DTO> AddNewCarLinq(CarDTO carDTO);

        Task<Result3DTO> DeleteOwnerFromCar(int CarId, int OwnerId);
    }
}