using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NAF.Infra.Data.Migrations
{
    public partial class addinghoraComplementar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoraComplementar",
                table: "Servico",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraComplementar",
                table: "Servico");
        }
    }
}
