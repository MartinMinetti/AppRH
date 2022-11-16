using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppRH.Migrations
{
    public partial class PrimeraMigraciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CustomerSurname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CustomerDNI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    HouseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HouseAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OwnerHouse = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PhotoHouse = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EstaAlquilada = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.HouseID);
                });

            migrationBuilder.CreateTable(
                name: "RentalDetailTemp",
                columns: table => new
                {
                    RentalDetailTempID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseID = table.Column<int>(type: "int", nullable: false),
                    HouseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDetailTemp", x => x.RentalDetailTempID);
                });

            migrationBuilder.CreateTable(
                name: "ReturnDetailTemp",
                columns: table => new
                {
                    ReturnDetailTempID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseID = table.Column<int>(type: "int", nullable: false),
                    HouseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnDetailTemp", x => x.ReturnDetailTempID);
                });

            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    RentalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.RentalID);
                    table.ForeignKey(
                        name: "FK_Rental_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Return",
                columns: table => new
                {
                    ReturnID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Return", x => x.ReturnID);
                    table.ForeignKey(
                        name: "FK_Return_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalDetail",
                columns: table => new
                {
                    RentalDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalID = table.Column<int>(type: "int", nullable: false),
                    HouseID = table.Column<int>(type: "int", nullable: false),
                    HouseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDetail", x => x.RentalDetailID);
                    table.ForeignKey(
                        name: "FK_RentalDetail_House_HouseID",
                        column: x => x.HouseID,
                        principalTable: "House",
                        principalColumn: "HouseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalDetail_Rental_RentalID",
                        column: x => x.RentalID,
                        principalTable: "Rental",
                        principalColumn: "RentalID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnDetail",
                columns: table => new
                {
                    ReturnDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnID = table.Column<int>(type: "int", nullable: false),
                    HouseID = table.Column<int>(type: "int", nullable: false),
                    HouseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnDetail", x => x.ReturnDetailID);
                    table.ForeignKey(
                        name: "FK_ReturnDetail_House_HouseID",
                        column: x => x.HouseID,
                        principalTable: "House",
                        principalColumn: "HouseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnDetail_Return_ReturnID",
                        column: x => x.ReturnID,
                        principalTable: "Return",
                        principalColumn: "ReturnID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rental_CustomerID",
                table: "Rental",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetail_HouseID",
                table: "RentalDetail",
                column: "HouseID");

            migrationBuilder.CreateIndex(
                name: "IX_RentalDetail_RentalID",
                table: "RentalDetail",
                column: "RentalID");

            migrationBuilder.CreateIndex(
                name: "IX_Return_CustomerID",
                table: "Return",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnDetail_HouseID",
                table: "ReturnDetail",
                column: "HouseID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnDetail_ReturnID",
                table: "ReturnDetail",
                column: "ReturnID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalDetail");

            migrationBuilder.DropTable(
                name: "RentalDetailTemp");

            migrationBuilder.DropTable(
                name: "ReturnDetail");

            migrationBuilder.DropTable(
                name: "ReturnDetailTemp");

            migrationBuilder.DropTable(
                name: "Rental");

            migrationBuilder.DropTable(
                name: "House");

            migrationBuilder.DropTable(
                name: "Return");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
