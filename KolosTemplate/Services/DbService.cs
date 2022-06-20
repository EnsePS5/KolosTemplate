using KolosTemplate.DTO;
using KolosTemplate.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KolosTemplate.Services {


    public class DbService : IDbService
    {
        private readonly CarPersonsDbContext _carPersonsDbContext;

        public DbService(CarPersonsDbContext context) {

            _carPersonsDbContext = context;
        }

        public async Task<Result1DTO> GetCarByCarId(int CarId)
        {


            return await _carPersonsDbContext.Cars.Include(x => x.CarPersons).ThenInclude(x => x.IdPersonNavigation)
                .Where(x => x.IdCar == CarId).Select(x => new Result1DTO { 
                
                    IdCar = x.IdCar,
                    Make = x.Make,
                    ProductionYear = x.ProductionYear,
                    PersonsDTO = x.CarPersons.Select(y => new PersonDTO { 
                        IdPerson = y.IdPerson,
                        Name = y.IdPersonNavigation.Name,
                        Surname = y.IdPersonNavigation.Surname,
                        DrivingLicense = y.IdPersonNavigation.DrivingLicense
                    }).ToList(),
                    MainOwner = x.CarPersons.Where(y => y.MainOwner == 1).Select(y => y.IdPersonNavigation.Name).FirstOrDefault(),
                    Insurence = insurenceCalculator(
                        x.ProductionYear, 
                        x.CarPersons.Where(y => y.MainOwner == 1).Select(y => y.IdPersonNavigation.DrivingLicense).FirstOrDefault(), 
                        x.CarPersons.Where(y => y.MainOwner == 1).Select(y => y.MainOwner).FirstOrDefault()),
                    httpStatusCode = System.Net.HttpStatusCode.OK

                }).FirstOrDefaultAsync();
        }

        private static int insurenceCalculator(int ProductionYear, string DrivingLicense, byte MainOwner) {

            return MainOwner == 1 ? DrivingLicense != null ? ProductionYear - 300 : ProductionYear + 200 : -1;

        }
    }
}