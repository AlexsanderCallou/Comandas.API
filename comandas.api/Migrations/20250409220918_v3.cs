using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace comandas.api.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_MESA",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SituacaoMesa",
                table: "TB_MESA",
                newName: "IC_SITUACAO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TB_MESA",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IC_SITUACAO",
                table: "TB_MESA",
                newName: "SituacaoMesa");
        }
    }
}
