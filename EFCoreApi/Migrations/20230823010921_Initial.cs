using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "EmailAddress", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("adfc1ffb-0066-4bf6-9103-0249d289d065"), 25, "JhonDoe@gmail.com", "Jhon", "doe" },
                    { new Guid("2122f873-1851-4514-90db-52d7924185ce"), 30, "JacobHilderth@gmail.com", "Jacob", "Hilderth" },
                    { new Guid("721b216b-cef0-4c1e-8184-e7cfcccaec0c"), 30, "JacobHilderth@gmail.com", "Jacob", "Hilderth" },
                    { new Guid("dbb8fdbf-44d9-4c49-b600-99e5864bf1be"), 22, "AlexWilliam@gmail.com", "Alexandra", "William" },
                    { new Guid("d6c4b89b-1bb3-4791-acd2-64af319e9dc3"), 27, "JonitaPeter@gmail.com", "Jonita", "Peter" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
