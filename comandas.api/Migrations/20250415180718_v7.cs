using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comandas.api.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_COMANDA_ITEM_TB_CARDAPIO_ITEM_ID_CARDAPIO_ITEM",
                table: "TB_COMANDA_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_COMANDA_ITEM_TB_COMANDA_ID_COMANDA",
                table: "TB_COMANDA_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_PEDIDO_COZINHA_TB_COMANDA_ID_COMANDA",
                table: "TB_PEDIDO_COZINHA");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_PEDIDO_COZINHA_ITEM_TB_COMANDA_ITEM_ID_COMANDA_ITEM",
                table: "TB_PEDIDO_COZINHA_ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_PEDIDO_COZINHA_ITEM_TB_PEDIDO_COZINHA_ID_PEDIDO_COZINHA",
                table: "TB_PEDIDO_COZINHA_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_PEDIDO_COZINHA_ITEM",
                table: "TB_PEDIDO_COZINHA_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_PEDIDO_COZINHA",
                table: "TB_PEDIDO_COZINHA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_MESA",
                table: "TB_MESA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_COMANDA_ITEM",
                table: "TB_COMANDA_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_COMANDA",
                table: "TB_COMANDA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_CARDAPIO_ITEM",
                table: "TB_CARDAPIO_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_usuario",
                table: "tbl_usuario");

            migrationBuilder.RenameTable(
                name: "TB_PEDIDO_COZINHA_ITEM",
                newName: "tb_pedido_cozinha_item");

            migrationBuilder.RenameTable(
                name: "TB_PEDIDO_COZINHA",
                newName: "tb_pedido_cozinha");

            migrationBuilder.RenameTable(
                name: "TB_MESA",
                newName: "tb_mesa");

            migrationBuilder.RenameTable(
                name: "TB_COMANDA_ITEM",
                newName: "tb_comanda_item");

            migrationBuilder.RenameTable(
                name: "TB_COMANDA",
                newName: "tb_comanda");

            migrationBuilder.RenameTable(
                name: "TB_CARDAPIO_ITEM",
                newName: "tb_cardapio_item");

            migrationBuilder.RenameTable(
                name: "tbl_usuario",
                newName: "tb_usuario");

            migrationBuilder.RenameColumn(
                name: "ID_PEDIDO_COZINHA",
                table: "tb_pedido_cozinha_item",
                newName: "id_pedido_cozinha");

            migrationBuilder.RenameColumn(
                name: "ID_COMANDA_ITEM",
                table: "tb_pedido_cozinha_item",
                newName: "id_comanda_item");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_pedido_cozinha_item",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_TB_PEDIDO_COZINHA_ITEM_ID_PEDIDO_COZINHA",
                table: "tb_pedido_cozinha_item",
                newName: "IX_tb_pedido_cozinha_item_id_pedido_cozinha");

            migrationBuilder.RenameIndex(
                name: "IX_TB_PEDIDO_COZINHA_ITEM_ID_COMANDA_ITEM",
                table: "tb_pedido_cozinha_item",
                newName: "IX_tb_pedido_cozinha_item_id_comanda_item");

            migrationBuilder.RenameColumn(
                name: "ID_COMANDA",
                table: "tb_pedido_cozinha",
                newName: "id_comanda");

            migrationBuilder.RenameColumn(
                name: "IC_SITUACAO",
                table: "tb_pedido_cozinha",
                newName: "ic_situacao");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_pedido_cozinha",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_TB_PEDIDO_COZINHA_ID_COMANDA",
                table: "tb_pedido_cozinha",
                newName: "IX_tb_pedido_cozinha_id_comanda");

            migrationBuilder.RenameColumn(
                name: "IC_SITUACAO",
                table: "tb_mesa",
                newName: "ic_situacao");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_mesa",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NUM_MESA",
                table: "tb_mesa",
                newName: "nu_mesa");

            migrationBuilder.RenameColumn(
                name: "ID_COMANDA",
                table: "tb_comanda_item",
                newName: "id_comanda");

            migrationBuilder.RenameColumn(
                name: "ID_CARDAPIO_ITEM",
                table: "tb_comanda_item",
                newName: "id_cardapio_item");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_comanda_item",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_TB_COMANDA_ITEM_ID_COMANDA",
                table: "tb_comanda_item",
                newName: "IX_tb_comanda_item_id_comanda");

            migrationBuilder.RenameIndex(
                name: "IX_TB_COMANDA_ITEM_ID_CARDAPIO_ITEM",
                table: "tb_comanda_item",
                newName: "IX_tb_comanda_item_id_cardapio_item");

            migrationBuilder.RenameColumn(
                name: "NU_MESA",
                table: "tb_comanda",
                newName: "nu_mesa");

            migrationBuilder.RenameColumn(
                name: "NO_CLIENTE",
                table: "tb_comanda",
                newName: "no_cliente");

            migrationBuilder.RenameColumn(
                name: "IC_SITUACAO",
                table: "tb_comanda",
                newName: "ic_situacao");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_comanda",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VL_PRECO",
                table: "tb_cardapio_item",
                newName: "vl_preco");

            migrationBuilder.RenameColumn(
                name: "TX_DESCRICAO",
                table: "tb_cardapio_item",
                newName: "tx_descricao");

            migrationBuilder.RenameColumn(
                name: "NO_ITEM",
                table: "tb_cardapio_item",
                newName: "no_item");

            migrationBuilder.RenameColumn(
                name: "IC_PREPARO",
                table: "tb_cardapio_item",
                newName: "ic_preparo");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_cardapio_item",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_pedido_cozinha_item",
                table: "tb_pedido_cozinha_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_pedido_cozinha",
                table: "tb_pedido_cozinha",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_mesa",
                table: "tb_mesa",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_comanda_item",
                table: "tb_comanda_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_comanda",
                table: "tb_comanda",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_cardapio_item",
                table: "tb_cardapio_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_usuario",
                table: "tb_usuario",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_comanda_item_tb_cardapio_item_id_cardapio_item",
                table: "tb_comanda_item",
                column: "id_cardapio_item",
                principalTable: "tb_cardapio_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_comanda_item_tb_comanda_id_comanda",
                table: "tb_comanda_item",
                column: "id_comanda",
                principalTable: "tb_comanda",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedido_cozinha_tb_comanda_id_comanda",
                table: "tb_pedido_cozinha",
                column: "id_comanda",
                principalTable: "tb_comanda",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedido_cozinha_item_tb_comanda_item_id_comanda_item",
                table: "tb_pedido_cozinha_item",
                column: "id_comanda_item",
                principalTable: "tb_comanda_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedido_cozinha_item_tb_pedido_cozinha_id_pedido_cozinha",
                table: "tb_pedido_cozinha_item",
                column: "id_pedido_cozinha",
                principalTable: "tb_pedido_cozinha",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_comanda_item_tb_cardapio_item_id_cardapio_item",
                table: "tb_comanda_item");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_comanda_item_tb_comanda_id_comanda",
                table: "tb_comanda_item");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedido_cozinha_tb_comanda_id_comanda",
                table: "tb_pedido_cozinha");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedido_cozinha_item_tb_comanda_item_id_comanda_item",
                table: "tb_pedido_cozinha_item");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedido_cozinha_item_tb_pedido_cozinha_id_pedido_cozinha",
                table: "tb_pedido_cozinha_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_pedido_cozinha_item",
                table: "tb_pedido_cozinha_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_pedido_cozinha",
                table: "tb_pedido_cozinha");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_mesa",
                table: "tb_mesa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_comanda_item",
                table: "tb_comanda_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_comanda",
                table: "tb_comanda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_cardapio_item",
                table: "tb_cardapio_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_usuario",
                table: "tb_usuario");

            migrationBuilder.RenameTable(
                name: "tb_pedido_cozinha_item",
                newName: "TB_PEDIDO_COZINHA_ITEM");

            migrationBuilder.RenameTable(
                name: "tb_pedido_cozinha",
                newName: "TB_PEDIDO_COZINHA");

            migrationBuilder.RenameTable(
                name: "tb_mesa",
                newName: "TB_MESA");

            migrationBuilder.RenameTable(
                name: "tb_comanda_item",
                newName: "TB_COMANDA_ITEM");

            migrationBuilder.RenameTable(
                name: "tb_comanda",
                newName: "TB_COMANDA");

            migrationBuilder.RenameTable(
                name: "tb_cardapio_item",
                newName: "TB_CARDAPIO_ITEM");

            migrationBuilder.RenameTable(
                name: "tb_usuario",
                newName: "tbl_usuario");

            migrationBuilder.RenameColumn(
                name: "id_pedido_cozinha",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "ID_PEDIDO_COZINHA");

            migrationBuilder.RenameColumn(
                name: "id_comanda_item",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "ID_COMANDA_ITEM");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_tb_pedido_cozinha_item_id_pedido_cozinha",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "IX_TB_PEDIDO_COZINHA_ITEM_ID_PEDIDO_COZINHA");

            migrationBuilder.RenameIndex(
                name: "IX_tb_pedido_cozinha_item_id_comanda_item",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "IX_TB_PEDIDO_COZINHA_ITEM_ID_COMANDA_ITEM");

            migrationBuilder.RenameColumn(
                name: "id_comanda",
                table: "TB_PEDIDO_COZINHA",
                newName: "ID_COMANDA");

            migrationBuilder.RenameColumn(
                name: "ic_situacao",
                table: "TB_PEDIDO_COZINHA",
                newName: "IC_SITUACAO");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TB_PEDIDO_COZINHA",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_tb_pedido_cozinha_id_comanda",
                table: "TB_PEDIDO_COZINHA",
                newName: "IX_TB_PEDIDO_COZINHA_ID_COMANDA");

            migrationBuilder.RenameColumn(
                name: "ic_situacao",
                table: "TB_MESA",
                newName: "IC_SITUACAO");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TB_MESA",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "nu_mesa",
                table: "TB_MESA",
                newName: "NUM_MESA");

            migrationBuilder.RenameColumn(
                name: "id_comanda",
                table: "TB_COMANDA_ITEM",
                newName: "ID_COMANDA");

            migrationBuilder.RenameColumn(
                name: "id_cardapio_item",
                table: "TB_COMANDA_ITEM",
                newName: "ID_CARDAPIO_ITEM");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TB_COMANDA_ITEM",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_tb_comanda_item_id_comanda",
                table: "TB_COMANDA_ITEM",
                newName: "IX_TB_COMANDA_ITEM_ID_COMANDA");

            migrationBuilder.RenameIndex(
                name: "IX_tb_comanda_item_id_cardapio_item",
                table: "TB_COMANDA_ITEM",
                newName: "IX_TB_COMANDA_ITEM_ID_CARDAPIO_ITEM");

            migrationBuilder.RenameColumn(
                name: "nu_mesa",
                table: "TB_COMANDA",
                newName: "NU_MESA");

            migrationBuilder.RenameColumn(
                name: "no_cliente",
                table: "TB_COMANDA",
                newName: "NO_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "ic_situacao",
                table: "TB_COMANDA",
                newName: "IC_SITUACAO");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TB_COMANDA",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "vl_preco",
                table: "TB_CARDAPIO_ITEM",
                newName: "VL_PRECO");

            migrationBuilder.RenameColumn(
                name: "tx_descricao",
                table: "TB_CARDAPIO_ITEM",
                newName: "TX_DESCRICAO");

            migrationBuilder.RenameColumn(
                name: "no_item",
                table: "TB_CARDAPIO_ITEM",
                newName: "NO_ITEM");

            migrationBuilder.RenameColumn(
                name: "ic_preparo",
                table: "TB_CARDAPIO_ITEM",
                newName: "IC_PREPARO");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TB_CARDAPIO_ITEM",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_PEDIDO_COZINHA_ITEM",
                table: "TB_PEDIDO_COZINHA_ITEM",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_PEDIDO_COZINHA",
                table: "TB_PEDIDO_COZINHA",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_MESA",
                table: "TB_MESA",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_COMANDA_ITEM",
                table: "TB_COMANDA_ITEM",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_COMANDA",
                table: "TB_COMANDA",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_CARDAPIO_ITEM",
                table: "TB_CARDAPIO_ITEM",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_usuario",
                table: "tbl_usuario",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_COMANDA_ITEM_TB_CARDAPIO_ITEM_ID_CARDAPIO_ITEM",
                table: "TB_COMANDA_ITEM",
                column: "ID_CARDAPIO_ITEM",
                principalTable: "TB_CARDAPIO_ITEM",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_COMANDA_ITEM_TB_COMANDA_ID_COMANDA",
                table: "TB_COMANDA_ITEM",
                column: "ID_COMANDA",
                principalTable: "TB_COMANDA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PEDIDO_COZINHA_TB_COMANDA_ID_COMANDA",
                table: "TB_PEDIDO_COZINHA",
                column: "ID_COMANDA",
                principalTable: "TB_COMANDA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PEDIDO_COZINHA_ITEM_TB_COMANDA_ITEM_ID_COMANDA_ITEM",
                table: "TB_PEDIDO_COZINHA_ITEM",
                column: "ID_COMANDA_ITEM",
                principalTable: "TB_COMANDA_ITEM",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PEDIDO_COZINHA_ITEM_TB_PEDIDO_COZINHA_ID_PEDIDO_COZINHA",
                table: "TB_PEDIDO_COZINHA_ITEM",
                column: "ID_PEDIDO_COZINHA",
                principalTable: "TB_PEDIDO_COZINHA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
