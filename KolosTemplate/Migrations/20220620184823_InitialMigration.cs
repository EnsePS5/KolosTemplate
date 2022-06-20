using Microsoft.EntityFrameworkCore.Migrations;

namespace KolosTemplate.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    IdCar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProductionYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Car_pk", x => x.IdCar);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    IdPerson = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DrivingLicense = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Person_pk", x => x.IdPerson);
                });

            migrationBuilder.CreateTable(
                name: "Car_Person",
                columns: table => new
                {
                    IdCar = table.Column<int>(type: "int", nullable: false),
                    IdPerson = table.Column<int>(type: "int", nullable: false),
                    MainOwner = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CarPerson_pk", x => new { x.IdPerson, x.IdCar });
                    table.ForeignKey(
                        name: "CarPerson_Car",
                        column: x => x.IdCar,
                        principalTable: "Car",
                        principalColumn: "IdCar",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "CarPerson_Person",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "IdCar", "Make", "ProductionYear" },
                values: new object[,]
                {
                    { 1, "Audi", 2021 },
                    { 2, "Audi", 2019 },
                    { 3, "Toyota", 2021 }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "IdPerson", "DrivingLicense", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "B", "Oskar", "Dudzik" },
                    { 2, "B", "Dominik", "Kozluk" },
                    { 3, null, "Kacper", "Godlewski" }
                });

            migrationBuilder.InsertData(
                table: "Car_Person",
                columns: new[] { "IdCar", "IdPerson", "MainOwner" },
                values: new object[,]
                {
                    { 1, 1, (byte)1 },
                    { 3, 1, (byte)0 },
                    { 3, 2, (byte)1 },
                    { 1, 3, (byte)0 },
                    { 2, 3, (byte)1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_Person_IdCar",
                table: "Car_Person",
                column: "IdCar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car_Person");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
