using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comandas.Data.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "tb_usuario",
                newName: "tb_usuario",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "tb_pedido_cozinha_item",
                newName: "tb_pedido_cozinha_item",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "tb_pedido_cozinha",
                newName: "tb_pedido_cozinha",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "tb_mesa",
                newName: "tb_mesa",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "tb_comanda_item",
                newName: "tb_comanda_item",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "tb_comanda",
                newName: "tb_comanda",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "tb_cardapio_item",
                newName: "tb_cardapio_item",
                newSchema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "tb_usuario",
                schema: "dbo",
                newName: "tb_usuario");

            migrationBuilder.RenameTable(
                name: "tb_pedido_cozinha_item",
                schema: "dbo",
                newName: "tb_pedido_cozinha_item");

            migrationBuilder.RenameTable(
                name: "tb_pedido_cozinha",
                schema: "dbo",
                newName: "tb_pedido_cozinha");

            migrationBuilder.RenameTable(
                name: "tb_mesa",
                schema: "dbo",
                newName: "tb_mesa");

            migrationBuilder.RenameTable(
                name: "tb_comanda_item",
                schema: "dbo",
                newName: "tb_comanda_item");

            migrationBuilder.RenameTable(
                name: "tb_comanda",
                schema: "dbo",
                newName: "tb_comanda");

            migrationBuilder.RenameTable(
                name: "tb_cardapio_item",
                schema: "dbo",
                newName: "tb_cardapio_item");
        }
    }
}
