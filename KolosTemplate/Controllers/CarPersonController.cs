using KolosTemplate.DTO;
using KolosTemplate.Entities;
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

        [HttpGet("/{CarIdLinq}")]
        public async Task<IActionResult> GetCarByCarIdLinq(int CarIdLinq) {

            Result1DTO result = await _dbService.GetCarByCarIdLinq(CarIdLinq);
            if (result.httpStatusCode != System.Net.HttpStatusCode.OK) {

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("/car")]
        public async Task<IActionResult> AddNewCar(CarDTO carDTO) {

            var car = await _dbService.AddNewCarLinq(carDTO);

            if (!car.isDone)
                return NotFound("Owner not found");


            return Ok("New car has been added");
        }

        [HttpDelete("/{CarId}/{OwnerId}")]
        public async Task<IActionResult> DeleteOwnerFromCar(int CarId, int OwnerId)
        {

            var result = await _dbService.DeleteOwnerFromCar(CarId, OwnerId);

            if (result.isDone)
                return Ok("Owner removed");

            return BadRequest(result.mes);
        }
        
        /*[HttpGet("/{CarIdSql}")]
        public async Task<IActionResult> GetCarByCarIdSQL(int CarIdSql) {

            var result = await _dbService.GetCarByCarIdSQL(CarIdSql);
            if (result.httpStatusCode == System.Net.HttpStatusCode.OK)
                return Ok();

            else return NotFound();
        }*/
    }
}