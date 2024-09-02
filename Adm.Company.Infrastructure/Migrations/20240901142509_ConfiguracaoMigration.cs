using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracaoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimeiraMensagem",
                table: "ConfiguracaoAtendimentoEmpresa",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "ConfiguracaoAtendimentoEmpresa",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimeiraMensagem",
                table: "ConfiguracaoAtendimentoEmpresa");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ConfiguracaoAtendimentoEmpresa");
        }
    }
}
