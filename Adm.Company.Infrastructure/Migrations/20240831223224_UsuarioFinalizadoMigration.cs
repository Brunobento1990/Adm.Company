using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioFinalizadoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MensagemCancelamento",
                table: "Atendimentos",
                newName: "MotivoCancelamento");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioFinalizadoId",
                table: "Atendimentos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_UsuarioFinalizadoId",
                table: "Atendimentos",
                column: "UsuarioFinalizadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimentos_Usuarios_UsuarioFinalizadoId",
                table: "Atendimentos",
                column: "UsuarioFinalizadoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimentos_Usuarios_UsuarioFinalizadoId",
                table: "Atendimentos");

            migrationBuilder.DropIndex(
                name: "IX_Atendimentos_UsuarioFinalizadoId",
                table: "Atendimentos");

            migrationBuilder.DropColumn(
                name: "UsuarioFinalizadoId",
                table: "Atendimentos");

            migrationBuilder.RenameColumn(
                name: "MotivoCancelamento",
                table: "Atendimentos",
                newName: "MensagemCancelamento");
        }
    }
}
