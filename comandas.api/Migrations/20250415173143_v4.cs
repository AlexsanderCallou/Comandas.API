using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comandas.api.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComandaItems_CardapioItems_CardapioItemId",
                table: "ComandaItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ComandaItems_Comandas_ComandaId",
                table: "ComandaItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoCozinhaItems_ComandaItems_ComanadaItemId",
                table: "PedidoCozinhaItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoCozinhaItems_PedidosCozinha_PedidoCozinhaId",
                table: "PedidoCozinhaItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosCozinha_Comandas_ComandaId",
                table: "PedidosCozinha");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidosCozinha",
                table: "PedidosCozinha");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoCozinhaItems",
                table: "PedidoCozinhaItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comandas",
                table: "Comandas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComandaItems",
                table: "ComandaItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardapioItems",
                table: "CardapioItems");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "TB_USUARIO");

            migrationBuilder.RenameTable(
                name: "PedidosCozinha",
                newName: "TB_PEDIDO_COZINHA");

            migrationBuilder.RenameTable(
                name: "PedidoCozinhaItems",
                newName: "TB_PEDIDO_COZINHA_ITEM");

            migrationBuilder.RenameTable(
                name: "Comandas",
                newName: "TB_COMANDA");

            migrationBuilder.RenameTable(
                name: "ComandaItems",
                newName: "TB_COMANDA_ITEM");

            migrationBuilder.RenameTable(
                name: "CardapioItems",
                newName: "TB_CARDAPIO_ITEM");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_USUARIO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "TB_USUARIO",
                newName: "TX_SENHA");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TB_USUARIO",
                newName: "NO_USUARIO");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "TB_USUARIO",
                newName: "TX_EMAIL");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_PEDIDO_COZINHA",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SituacaoId",
                table: "TB_PEDIDO_COZINHA",
                newName: "IC_SITUACAO");

            migrationBuilder.RenameColumn(
                name: "ComandaId",
                table: "TB_PEDIDO_COZINHA",
                newName: "ID_COMANDA");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosCozinha_ComandaId",
                table: "TB_PEDIDO_COZINHA",
                newName: "IX_TB_PEDIDO_COZINHA_ID_COMANDA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PedidoCozinhaId",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "ID_PEDIDO_COZINHA");

            migrationBuilder.RenameColumn(
                name: "ComanadaItemId",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "ID_COMANDA_ITEM");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoCozinhaItems_PedidoCozinhaId",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "IX_TB_PEDIDO_COZINHA_ITEM_ID_PEDIDO_COZINHA");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoCozinhaItems_ComanadaItemId",
                table: "TB_PEDIDO_COZINHA_ITEM",
                newName: "IX_TB_PEDIDO_COZINHA_ITEM_ID_COMANDA_ITEM");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_COMANDA",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SituacaoComanda",
                table: "TB_COMANDA",
                newName: "IC_SITUACAO");

            migrationBuilder.RenameColumn(
                name: "NumeroMesa",
                table: "TB_COMANDA",
                newName: "NU_MESA");

            migrationBuilder.RenameColumn(
                name: "NomeCliente",
                table: "TB_COMANDA",
                newName: "NO_CLIENTE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_COMANDA_ITEM",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ComandaId",
                table: "TB_COMANDA_ITEM",
                newName: "ID_COMANDA");

            migrationBuilder.RenameColumn(
                name: "CardapioItemId",
                table: "TB_COMANDA_ITEM",
                newName: "ID_CARDAPIO_ITEM");

            migrationBuilder.RenameIndex(
                name: "IX_ComandaItems_ComandaId",
                table: "TB_COMANDA_ITEM",
                newName: "IX_TB_COMANDA_ITEM_ID_COMANDA");

            migrationBuilder.RenameIndex(
                name: "IX_ComandaItems_CardapioItemId",
                table: "TB_COMANDA_ITEM",
                newName: "IX_TB_COMANDA_ITEM_ID_CARDAPIO_ITEM");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_CARDAPIO_ITEM",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "TB_CARDAPIO_ITEM",
                newName: "NO_ITEM");

            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "TB_CARDAPIO_ITEM",
                newName: "VL_PRECO");

            migrationBuilder.RenameColumn(
                name: "PossuiPreparo",
                table: "TB_CARDAPIO_ITEM",
                newName: "IC_PREPARO");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TB_CARDAPIO_ITEM",
                newName: "TX_DESCRICAO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_USUARIO",
                table: "TB_USUARIO",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_PEDIDO_COZINHA",
                table: "TB_PEDIDO_COZINHA",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_PEDIDO_COZINHA_ITEM",
                table: "TB_PEDIDO_COZINHA_ITEM",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_COMANDA",
                table: "TB_COMANDA",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_COMANDA_ITEM",
                table: "TB_COMANDA_ITEM",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_CARDAPIO_ITEM",
                table: "TB_CARDAPIO_ITEM",
                column: "ID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PK_TB_USUARIO",
                table: "TB_USUARIO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_PEDIDO_COZINHA_ITEM",
                table: "TB_PEDIDO_COZINHA_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_PEDIDO_COZINHA",
                table: "TB_PEDIDO_COZINHA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_COMANDA_ITEM",
                table: "TB_COMANDA_ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_COMANDA",
                table: "TB_COMANDA");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_CARDAPIO_ITEM",
                table: "TB_CARDAPIO_ITEM");

            migrationBuilder.RenameTable(
                name: "TB_USUARIO",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "TB_PEDIDO_COZINHA_ITEM",
                newName: "PedidoCozinhaItems");

            migrationBuilder.RenameTable(
                name: "TB_PEDIDO_COZINHA",
                newName: "PedidosCozinha");

            migrationBuilder.RenameTable(
                name: "TB_COMANDA_ITEM",
                newName: "ComandaItems");

            migrationBuilder.RenameTable(
                name: "TB_COMANDA",
                newName: "Comandas");

            migrationBuilder.RenameTable(
                name: "TB_CARDAPIO_ITEM",
                newName: "CardapioItems");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TX_SENHA",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "TX_EMAIL",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "NO_USUARIO",
                table: "Usuarios",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PedidoCozinhaItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID_PEDIDO_COZINHA",
                table: "PedidoCozinhaItems",
                newName: "PedidoCozinhaId");

            migrationBuilder.RenameColumn(
                name: "ID_COMANDA_ITEM",
                table: "PedidoCozinhaItems",
                newName: "ComanadaItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_PEDIDO_COZINHA_ITEM_ID_PEDIDO_COZINHA",
                table: "PedidoCozinhaItems",
                newName: "IX_PedidoCozinhaItems_PedidoCozinhaId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_PEDIDO_COZINHA_ITEM_ID_COMANDA_ITEM",
                table: "PedidoCozinhaItems",
                newName: "IX_PedidoCozinhaItems_ComanadaItemId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PedidosCozinha",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID_COMANDA",
                table: "PedidosCozinha",
                newName: "ComandaId");

            migrationBuilder.RenameColumn(
                name: "IC_SITUACAO",
                table: "PedidosCozinha",
                newName: "SituacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_PEDIDO_COZINHA_ID_COMANDA",
                table: "PedidosCozinha",
                newName: "IX_PedidosCozinha_ComandaId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ComandaItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID_COMANDA",
                table: "ComandaItems",
                newName: "ComandaId");

            migrationBuilder.RenameColumn(
                name: "ID_CARDAPIO_ITEM",
                table: "ComandaItems",
                newName: "CardapioItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_COMANDA_ITEM_ID_COMANDA",
                table: "ComandaItems",
                newName: "IX_ComandaItems_ComandaId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_COMANDA_ITEM_ID_CARDAPIO_ITEM",
                table: "ComandaItems",
                newName: "IX_ComandaItems_CardapioItemId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Comandas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "NU_MESA",
                table: "Comandas",
                newName: "NumeroMesa");

            migrationBuilder.RenameColumn(
                name: "NO_CLIENTE",
                table: "Comandas",
                newName: "NomeCliente");

            migrationBuilder.RenameColumn(
                name: "IC_SITUACAO",
                table: "Comandas",
                newName: "SituacaoComanda");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CardapioItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "VL_PRECO",
                table: "CardapioItems",
                newName: "Preco");

            migrationBuilder.RenameColumn(
                name: "TX_DESCRICAO",
                table: "CardapioItems",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "NO_ITEM",
                table: "CardapioItems",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "IC_PREPARO",
                table: "CardapioItems",
                newName: "PossuiPreparo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoCozinhaItems",
                table: "PedidoCozinhaItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidosCozinha",
                table: "PedidosCozinha",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComandaItems",
                table: "ComandaItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comandas",
                table: "Comandas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardapioItems",
                table: "CardapioItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComandaItems_CardapioItems_CardapioItemId",
                table: "ComandaItems",
                column: "CardapioItemId",
                principalTable: "CardapioItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComandaItems_Comandas_ComandaId",
                table: "ComandaItems",
                column: "ComandaId",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoCozinhaItems_ComandaItems_ComanadaItemId",
                table: "PedidoCozinhaItems",
                column: "ComanadaItemId",
                principalTable: "ComandaItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoCozinhaItems_PedidosCozinha_PedidoCozinhaId",
                table: "PedidoCozinhaItems",
                column: "PedidoCozinhaId",
                principalTable: "PedidosCozinha",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosCozinha_Comandas_ComandaId",
                table: "PedidosCozinha",
                column: "ComandaId",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
