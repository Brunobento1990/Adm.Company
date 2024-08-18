using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MinhaMensagemColumnMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MinhaMensagem",
                table: "MensagemAtendimentos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RemoteId",
                table: "MensagemAtendimentos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoMensagem",
                table: "MensagemAtendimentos",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinhaMensagem",
                table: "MensagemAtendimentos");

            migrationBuilder.DropColumn(
                name: "RemoteId",
                table: "MensagemAtendimentos");

            migrationBuilder.DropColumn(
                name: "TipoMensagem",
                table: "MensagemAtendimentos");
        }
    }
}
