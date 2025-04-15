using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comandas.api.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_usuario",
                table: "tb_usuario");

            migrationBuilder.RenameTable(
                name: "tb_usuario",
                newName: "tbl_usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_usuario",
                table: "tbl_usuario",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_usuario",
                table: "tbl_usuario");

            migrationBuilder.RenameTable(
                name: "tbl_usuario",
                newName: "tb_usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_usuario",
                table: "tb_usuario",
                column: "id");
        }
    }
}
