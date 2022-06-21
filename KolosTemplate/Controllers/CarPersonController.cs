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

        /*[HttpGet("/{CarIdLinq}")]
        public async Task<Result1DTO> GetCarByCarIdLinq(int CarIdLinq) {

            return await _dbService.GetCarByCarIdLinq(CarIdLinq);
        }*/

        [HttpGet("/{CarIdSql}")]
        public async Task<IActionResult> GetCarByCarIdSQL(int CarIdSql) {

            var result = await _dbService.GetCarByCarIdSQL(CarIdSql);
            if (result.httpStatusCode == System.Net.HttpStatusCode.OK)
                return Ok();

            else return NotFound();
        }
    }
}