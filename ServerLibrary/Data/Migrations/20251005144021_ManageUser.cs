using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManageUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_DoctorTypes_DoctorTypeId",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_DoctorTypeId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "DoctorTypeId",
                table: "Vacations");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AppUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorTypeId",
                table: "Vacations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_DoctorTypeId",
                table: "Vacations",
                column: "DoctorTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_DoctorTypes_DoctorTypeId",
                table: "Vacations",
                column: "DoctorTypeId",
                principalTable: "DoctorTypes",
                principalColumn: "Id");
        }
    }
}
