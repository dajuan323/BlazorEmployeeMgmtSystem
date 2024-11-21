using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Migrations
{
    /// <inheritdoc />
    public partial class updateall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Overtimes_OverTimeTypes_OvertimeTypeId",
                table: "Overtimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OverTimeTypes",
                table: "OverTimeTypes");

            migrationBuilder.RenameTable(
                name: "OverTimeTypes",
                newName: "OvertimeTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OvertimeTypes",
                table: "OvertimeTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtimes_OvertimeTypes_OvertimeTypeId",
                table: "Overtimes",
                column: "OvertimeTypeId",
                principalTable: "OvertimeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Overtimes_OvertimeTypes_OvertimeTypeId",
                table: "Overtimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OvertimeTypes",
                table: "OvertimeTypes");

            migrationBuilder.RenameTable(
                name: "OvertimeTypes",
                newName: "OverTimeTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OverTimeTypes",
                table: "OverTimeTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtimes_OverTimeTypes_OvertimeTypeId",
                table: "Overtimes",
                column: "OvertimeTypeId",
                principalTable: "OverTimeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
