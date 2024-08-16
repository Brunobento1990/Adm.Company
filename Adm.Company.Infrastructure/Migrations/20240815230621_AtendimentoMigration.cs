using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtendimentoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsuarioCancelamentoId = table.Column<Guid>(type: "uuid", nullable: true),
                    Observacao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MensagemCancelamento = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NumeroWhats = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Numero = table.Column<long>(type: "bigint", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Usuarios_UsuarioCancelamentoId",
                        column: x => x.UsuarioCancelamentoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Atendimentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MensagemAtendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Mensagem = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    AtendimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Numero = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagemAtendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensagemAtendimentos_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_EmpresaId",
                table: "Atendimentos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_Numero",
                table: "Atendimentos",
                column: "Numero");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_NumeroWhats",
                table: "Atendimentos",
                column: "NumeroWhats");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_Status",
                table: "Atendimentos",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_UsuarioCancelamentoId",
                table: "Atendimentos",
                column: "UsuarioCancelamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_UsuarioId",
                table: "Atendimentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemAtendimentos_AtendimentoId",
                table: "MensagemAtendimentos",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemAtendimentos_Numero",
                table: "MensagemAtendimentos",
                column: "Numero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MensagemAtendimentos");

            migrationBuilder.DropTable(
                name: "Atendimentos");
        }
    }
}
