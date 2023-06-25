using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Microwave.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exceptionslog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    innerexception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stacktrace = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exceptionslog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "microwave",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    food = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<TimeSpan>(type: "time", nullable: false),
                    potency = table.Column<int>(type: "int", nullable: false),
                    instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    warmingchar = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    isstatic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_microwave", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordhash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordsalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "microwave",
                columns: new[] { "id", "food", "instructions", "isstatic", "name", "potency", "time", "warmingchar" },
                values: new object[,]
                {
                    { new Guid("46ee2c37-d664-458e-9bc7-d91d14e650f8"), "Pipoca de Microondas", "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento.", true, "Pipoca", 7, new TimeSpan(0, 0, 3, 0, 0), "P" },
                    { new Guid("50ae3207-737b-410d-a39f-0c44c41861b9"), "Frango (qualquer corte)", "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.", true, "Frango", 7, new TimeSpan(0, 0, 8, 0, 0), "F" },
                    { new Guid("b1a5048d-5b9b-46d0-abb5-af8f0c4fec0d"), "Carne em pedaço ou fatias", "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.", true, "Carnes de boi", 4, new TimeSpan(0, 0, 14, 0, 0), "C" },
                    { new Guid("ce2ff2b6-7944-4d2f-947c-576882e74459"), "Leite", "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras.", true, "Leite", 5, new TimeSpan(0, 0, 5, 0, 0), "L" },
                    { new Guid("e27a01f0-091c-43e8-adb8-a13a5f424f17"), "Feijão congelado", "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.", true, "Feijão", 9, new TimeSpan(0, 0, 8, 0, 0), "Q" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exceptionslog");

            migrationBuilder.DropTable(
                name: "microwave");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
