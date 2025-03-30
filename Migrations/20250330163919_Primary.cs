using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeuPontoOnline.Migrations
{
    /// <inheritdoc />
    public partial class Primary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "funcoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcoes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    senha_hash = table.Column<string>(type: "text", nullable: false),
                    codigo_indetificacao = table.Column<string>(type: "text", nullable: false),
                    tipo_contrato = table.Column<string>(type: "text", nullable: false),
                    setor_id = table.Column<int>(type: "integer", nullable: false),
                    funcao_id = table.Column<int>(type: "integer", nullable: false),
                    horario_entrada = table.Column<TimeSpan>(type: "interval", nullable: false),
                    horario_saida = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_funcionarios_funcoes_funcao_id",
                        column: x => x.funcao_id,
                        principalTable: "funcoes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_funcionarios_setores_setor_id",
                        column: x => x.setor_id,
                        principalTable: "setores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registro_ponto",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    funcioario_id = table.Column<int>(type: "integer", nullable: false),
                    data_hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tipo_registro = table.Column<string>(type: "text", nullable: true),
                    Observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registro_ponto", x => x.id);
                    table.ForeignKey(
                        name: "FK_registro_ponto_funcionarios_funcioario_id",
                        column: x => x.funcioario_id,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_funcao_id",
                table: "funcionarios",
                column: "funcao_id");

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_setor_id",
                table: "funcionarios",
                column: "setor_id");

            migrationBuilder.CreateIndex(
                name: "IX_registro_ponto_funcioario_id",
                table: "registro_ponto",
                column: "funcioario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registro_ponto");

            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "funcoes");

            migrationBuilder.DropTable(
                name: "setores");
        }
    }
}
