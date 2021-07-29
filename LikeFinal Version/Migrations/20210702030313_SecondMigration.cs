using Microsoft.EntityFrameworkCore.Migrations;

namespace LikeFinal_Version.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoTime",
                table: "Youtubes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoTime",
                table: "Youtubes");
        }
    }
}
