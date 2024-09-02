using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MensagemRespostaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resposta",
                table: "MensagemAtendimentos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RespostaId",
                table: "MensagemAtendimentos",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resposta",
                table: "MensagemAtendimentos");

            migrationBuilder.DropColumn(
                name: "RespostaId",
                table: "MensagemAtendimentos");
        }
    }
}
