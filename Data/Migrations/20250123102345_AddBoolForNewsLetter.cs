using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fantastic4News.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBoolForNewsLetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WantNewsLetter",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WantNewsLetter",
                table: "AspNetUsers");
        }
    }
}
