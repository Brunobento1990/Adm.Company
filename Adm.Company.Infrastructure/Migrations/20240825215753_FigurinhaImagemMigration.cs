using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FigurinhaImagemMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Figurinha",
                table: "MensagemAtendimentos",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "MensagemAtendimentos",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Figurinha",
                table: "MensagemAtendimentos");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "MensagemAtendimentos");
        }
    }
}
