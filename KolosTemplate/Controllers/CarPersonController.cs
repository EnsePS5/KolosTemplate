using KolosTemplate.DTO;
using KolosTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KolosTemplate.Controller {


    [ApiController]
    [Route("api/[controller]")]
    public class CarPersonController : ControllerBase {

        private readonly IDbService _dbService;

        public CarPersonController(IDbService dbService) {
            _dbService = dbService;
        }

        [HttpGet("/{CarId}")]
        public async Task<Result1DTO> GetCarByCarId(int CarId) {

            return await _dbService.GetCarByCarId(CarId);
        }
    }
}