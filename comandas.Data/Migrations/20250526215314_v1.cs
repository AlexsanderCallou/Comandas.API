using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace comandas.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_cardapio_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    no_item = table.Column<string>(type: "text", nullable: false),
                    tx_descricao = table.Column<string>(type: "text", nullable: false),
                    vl_preco = table.Column<decimal>(type: "numeric", nullable: false),
                    ic_preparo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_cardapio_item", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_comanda",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nu_mesa = table.Column<int>(type: "integer", nullable: false),
                    no_cliente = table.Column<string>(type: "text", nullable: false),
                    ic_situacao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_comanda", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_mesa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nu_mesa = table.Column<int>(type: "integer", nullable: false),
                    ic_situacao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_mesa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    no_ususario = table.Column<string>(type: "text", nullable: false),
                    tx_email = table.Column<string>(type: "text", nullable: false),
                    tx_senha = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_comanda_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_cardapio_item = table.Column<int>(type: "integer", nullable: false),
                    id_comanda = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_comanda_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_comanda_item_tb_cardapio_item_id_cardapio_item",
                        column: x => x.id_cardapio_item,
                        principalTable: "tb_cardapio_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_comanda_item_tb_comanda_id_comanda",
                        column: x => x.id_comanda,
                        principalTable: "tb_comanda",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_pedido_cozinha",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_comanda = table.Column<int>(type: "integer", nullable: false),
                    ic_situacao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pedido_cozinha", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_pedido_cozinha_tb_comanda_id_comanda",
                        column: x => x.id_comanda,
                        principalTable: "tb_comanda",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_pedido_cozinha_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_pedido_cozinha = table.Column<int>(type: "integer", nullable: false),
                    id_comanda_item = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pedido_cozinha_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_pedido_cozinha_item_tb_comanda_item_id_comanda_item",
                        column: x => x.id_comanda_item,
                        principalTable: "tb_comanda_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_pedido_cozinha_item_tb_pedido_cozinha_id_pedido_cozinha",
                        column: x => x.id_pedido_cozinha,
                        principalTable: "tb_pedido_cozinha",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_comanda_item_id_cardapio_item",
                table: "tb_comanda_item",
                column: "id_cardapio_item");

            migrationBuilder.CreateIndex(
                name: "IX_tb_comanda_item_id_comanda",
                table: "tb_comanda_item",
                column: "id_comanda");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedido_cozinha_id_comanda",
                table: "tb_pedido_cozinha",
                column: "id_comanda");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedido_cozinha_item_id_comanda_item",
                table: "tb_pedido_cozinha_item",
                column: "id_comanda_item");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedido_cozinha_item_id_pedido_cozinha",
                table: "tb_pedido_cozinha_item",
                column: "id_pedido_cozinha");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_mesa");

            migrationBuilder.DropTable(
                name: "tb_pedido_cozinha_item");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_comanda_item");

            migrationBuilder.DropTable(
                name: "tb_pedido_cozinha");

            migrationBuilder.DropTable(
                name: "tb_cardapio_item");

            migrationBuilder.DropTable(
                name: "tb_comanda");
        }
    }
}
