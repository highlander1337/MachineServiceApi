using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineServiceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "ServiceHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceHistories_EmployerId",
                table: "ServiceHistories",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceHistories_Employers_EmployerId",
                table: "ServiceHistories",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceHistories_Employers_EmployerId",
                table: "ServiceHistories");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_ServiceHistories_EmployerId",
                table: "ServiceHistories");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "ServiceHistories");
        }
    }
}
