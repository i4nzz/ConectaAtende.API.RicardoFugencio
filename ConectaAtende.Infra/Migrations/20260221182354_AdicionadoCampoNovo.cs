using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectaAtende.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoCampoNovo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DataAtualizacao", "DataCriacao" },
                values: new object[] { new DateTime(2026, 2, 21, 18, 23, 54, 657, DateTimeKind.Utc).AddTicks(9131), new DateTime(2026, 2, 21, 18, 23, 54, 657, DateTimeKind.Utc).AddTicks(9128) });

            migrationBuilder.UpdateData(
                table: "Contatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "DataAtualizacao", "DataCriacao" },
                values: new object[] { new DateTime(2026, 2, 21, 18, 23, 54, 657, DateTimeKind.Utc).AddTicks(9134), new DateTime(2026, 2, 21, 18, 23, 54, 657, DateTimeKind.Utc).AddTicks(9133) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "DataCriacao", "Descricao" },
                values: new object[] { new DateTime(2026, 2, 21, 18, 23, 54, 657, DateTimeKind.Utc).AddTicks(9270), "Problema com o produto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contatos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DataAtualizacao", "DataCriacao" },
                values: new object[] { new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4298), new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4296) });

            migrationBuilder.UpdateData(
                table: "Contatos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "DataAtualizacao", "DataCriacao" },
                values: new object[] { new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4300), new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "DataCriacao", "Descricao" },
                values: new object[] { new DateTime(2026, 2, 21, 18, 15, 49, 255, DateTimeKind.Utc).AddTicks(4450), null });
        }
    }
}
