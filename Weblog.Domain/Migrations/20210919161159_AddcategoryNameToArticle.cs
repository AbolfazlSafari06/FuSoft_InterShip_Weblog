using Microsoft.EntityFrameworkCore.Migrations;

namespace Weblog.Domain.Migrations
{
    public partial class AddcategoryNameToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryTitle",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryTitle",
                table: "Articles");
        }
    }
}
