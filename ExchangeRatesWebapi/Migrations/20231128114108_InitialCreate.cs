using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExchangeRatesWebapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Bank_Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Bank_Id = table.Column<int>(type: "integer", nullable: false),
                    Bank_Url = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Buy_Xpath = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Sell_Xpath = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    UrlsId = table.Column<int>(type: "integer", nullable: false),
                    Buy = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    Sell = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Urls",
                        column: x => x.UrlsId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UrlsId",
                table: "Rates",
                column: "UrlsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
