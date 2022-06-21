using KolosTemplate.DTO;
using KolosTemplate.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace KolosTemplate.Services {


    public class DbService : IDbService
    {
        private readonly CarPersonsDbContext _carPersonsDbContext;

        public DbService(CarPersonsDbContext context) {

            _carPersonsDbContext = context;
        }

        //Koncowka uzywajac linq
        public async Task<Result1DTO> GetCarByCarIdLinq(int CarId)
        {
            if (_carPersonsDbContext.Cars.Where(x => x.IdCar == CarId) == null)
                return new Result1DTO { httpStatusCode = System.Net.HttpStatusCode.BadRequest};

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

        //Koncowka uzywajac SQL
        public async Task<Result1DTO> GetCarByCarIdSQL(int CarId) {

            Result1DTO result = new Result1DTO();

            SqlConnection connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            await connection.OpenAsync();
            DbTransaction transaction = await connection.BeginTransactionAsync();
            command.Transaction = (SqlTransaction)transaction;

            //Car finding
            command.Parameters.Clear();
            command.CommandText = $"SELECT * FROM Car WHERE IdCar = ${CarId}";

            using (var reader = await command.ExecuteReaderAsync())
            {
                bool wasFound = false;

                while (await reader.ReadAsync())
                {
                    result.IdCar = int.Parse(reader["IdCar"].ToString());
                    result.Make = reader["Make"].ToString();
                    result.ProductionYear = int.Parse(reader["ProductionYear"].ToString());

                    wasFound = true;
                }
                if (!wasFound) {

                    result.httpStatusCode = System.Net.HttpStatusCode.NotFound;
                    return result;
                }
            }

            //MainOwner
            command.CommandText = $"SELECT Name FROM Person p " +
                $"JOIN Car_Person cp ON p.IdPerson = cp.IdPerson WHERE cp.MainOwner = 1 AND cp.IdCar = ${CarId}";

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.MainOwner = reader["Name"].ToString();
                }
            }

            //List of owners 
            command.CommandText = $"SELECT p.IdPerson, Name, Surname, DrivingLicense FROM Person p " +
                $"JOIN Car_Person cp ON p.IdPerson = cp.IdPerson WHERE cp.IdCar = ${CarId}";

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.PersonsDTO.Add(new PersonDTO { 
                        IdPerson = int.Parse(reader["IdPerson"].ToString()),
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        DrivingLicense = reader["DrivingLicense"].ToString()
                    });
                    if (result.MainOwner == reader["Name"].ToString())
                        result.Insurence = insurenceCalculator(result.ProductionYear, reader["DrivingLicense"].ToString(), 1);
                }
            }

            result.httpStatusCode = System.Net.HttpStatusCode.OK;
            return result;


        }

        private static int insurenceCalculator(int ProductionYear, string DrivingLicense, byte MainOwner) {

            return MainOwner == 1 ? !string.IsNullOrEmpty(DrivingLicense) ? ProductionYear - 300 : ProductionYear + 200 : -1;

        }
    }
}