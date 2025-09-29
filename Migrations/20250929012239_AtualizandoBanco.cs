using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProjeto.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidade",
                table: "Especialidade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Demanda",
                table: "Demanda");

            migrationBuilder.RenameTable(
                name: "Especialidade",
                newName: "Especialidades");

            migrationBuilder.RenameTable(
                name: "Demanda",
                newName: "Demandas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Demandas",
                table: "Demandas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Demandas",
                table: "Demandas");

            migrationBuilder.RenameTable(
                name: "Especialidades",
                newName: "Especialidade");

            migrationBuilder.RenameTable(
                name: "Demandas",
                newName: "Demanda");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidade",
                table: "Especialidade",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Demanda",
                table: "Demanda",
                column: "Id");
        }
    }
}
