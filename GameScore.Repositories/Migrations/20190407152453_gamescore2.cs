using Microsoft.EntityFrameworkCore.Migrations;

namespace GameScore.Repositories.Migrations
{
    public partial class gamescore2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuatindadePontos",
                table: "Pontuacao",
                newName: "QuantidadePontos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantidadePontos",
                table: "Pontuacao",
                newName: "QuatindadePontos");
        }
    }
}
