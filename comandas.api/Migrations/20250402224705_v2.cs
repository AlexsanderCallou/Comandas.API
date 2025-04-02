using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comandas.api.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Mesas",
                table: "Mesas");

            migrationBuilder.RenameTable(
                name: "Mesas",
                newName: "TB_MESA");

            migrationBuilder.RenameColumn(
                name: "NumeroMesa",
                table: "TB_MESA",
                newName: "NUM_MESA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_MESA",
                table: "TB_MESA",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_MESA",
                table: "TB_MESA");

            migrationBuilder.RenameTable(
                name: "TB_MESA",
                newName: "Mesas");

            migrationBuilder.RenameColumn(
                name: "NUM_MESA",
                table: "Mesas",
                newName: "NumeroMesa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mesas",
                table: "Mesas",
                column: "Id");
        }
    }
}
