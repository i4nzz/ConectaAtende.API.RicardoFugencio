using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConectaAtende.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contatos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContatosRecentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAcesso = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatosRecentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContatosRecentes_Contatos_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Idade", "Nome" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), 30, "Carlos" });

            migrationBuilder.InsertData(
                table: "Contatos",
                columns: new[] { "Id", "DataAtualizacao", "DataCriacao", "Nome", "Numero" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4298), new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4296), "João Silva", "11999999999" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4300), new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4300), "Maria Souza", "11888888888" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "ContatoId", "DataAlteracao", "DataCriacao", "Descricao", "Prioridade", "Status" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4450), null, 1, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_ContatosRecentes_ContatoId",
                table: "ContatosRecentes",
                column: "ContatoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "ContatosRecentes");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Contatos");
        }
    }
}
