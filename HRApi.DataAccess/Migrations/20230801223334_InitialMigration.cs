using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRApi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartWorkingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleWorker",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    WorkersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWorker", x => new { x.RolesId, x.WorkersId });
                    table.ForeignKey(
                        name: "FK_RoleWorker_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleWorker_Worker_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryIncreases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryIncreases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryIncreases_Worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryRevisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryRevisions_Worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Worker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Worker" },
                    { 2, "Specialist" },
                    { 3, "Manager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleWorker_WorkersId",
                table: "RoleWorker",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryIncreases_WorkerId",
                table: "SalaryIncreases",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryRevisions_WorkerId",
                table: "SalaryRevisions",
                column: "WorkerId");*/
            var createProcSql1 = @"CREATE OR ALTER PROCEDURE [dbo].[GetSalaryIncreaseByWorkerId] 	
    @workerId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    select s.Id, s.Amount, s.Date, s.WorkerId
    from dbo.Worker w 
    join dbo.SalaryIncreases s on w.Id = s.WorkerId
    WHERE s.WorkerId >= @workerId
END";
            var createProcSql2 = @"CREATE OR ALTER PROCEDURE [dbo].[GetWorkerWithRoles] 	
   
AS
BEGIN
    SET NOCOUNT ON;
    select w.Id as WorkerId, w.Name, w.LastName, w.Email, w.Phone, w.Salary, w.StartWorkingDate as StartDate, w.Address, r.Id as RolId, r.Name as RoleName from dbo.Worker w 
    join dbo.RoleWorker rw on w.Id = rw.WorkersId
    join dbo.Role r on r.Id = rw.RolesId
END";
            migrationBuilder.Sql(createProcSql1);
            migrationBuilder.Sql(createProcSql2);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropTable(
                 name: "RoleWorker");

             migrationBuilder.DropTable(
                 name: "SalaryIncreases");

             migrationBuilder.DropTable(
                 name: "SalaryRevisions");

             migrationBuilder.DropTable(
                 name: "Role");

             migrationBuilder.DropTable(
                 name: "Worker");*/

            var dropProcSql1 = "DROP PROC GetSalaryIncreaseByWorkerId";
            var dropProcSql2 = "DROP PROC GetWorkerWithRoles";
            migrationBuilder.Sql(dropProcSql1);
            migrationBuilder.Sql(dropProcSql2);
        }
    }
}
