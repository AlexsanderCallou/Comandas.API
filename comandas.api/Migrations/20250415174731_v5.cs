using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comandas.api.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_USUARIO",
                table: "TB_USUARIO");

            migrationBuilder.RenameTable(
                name: "TB_USUARIO",
                newName: "tb_usuario");

            migrationBuilder.RenameColumn(
                name: "TX_SENHA",
                table: "tb_usuario",
                newName: "tx_senha");

            migrationBuilder.RenameColumn(
                name: "TX_EMAIL",
                table: "tb_usuario",
                newName: "tx_email");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_usuario",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NO_USUARIO",
                table: "tb_usuario",
                newName: "no_ususario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_usuario",
                table: "tb_usuario",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_usuario",
                table: "tb_usuario");

            migrationBuilder.RenameTable(
                name: "tb_usuario",
                newName: "TB_USUARIO");

            migrationBuilder.RenameColumn(
                name: "tx_senha",
                table: "TB_USUARIO",
                newName: "TX_SENHA");

            migrationBuilder.RenameColumn(
                name: "tx_email",
                table: "TB_USUARIO",
                newName: "TX_EMAIL");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TB_USUARIO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "no_ususario",
                table: "TB_USUARIO",
                newName: "NO_USUARIO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_USUARIO",
                table: "TB_USUARIO",
                column: "ID");
        }
    }
}
