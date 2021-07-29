using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LikeFinal_Version.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facebook",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserNumber = table.Column<int>(nullable: false),
                    ItemLink = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    ItemTybe = table.Column<string>(nullable: true),
                    CoinsNumber = table.Column<int>(nullable: false),
                    IsClicked = table.Column<int>(nullable: false),
                    VideoTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facebook", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "FacebookOperations",
                columns: table => new
                {
                    OperationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookOperations", x => x.OperationID);
                });

            migrationBuilder.CreateTable(
                name: "Instegram",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserNumber = table.Column<int>(nullable: false),
                    ItemLink = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    ItemTybe = table.Column<string>(nullable: true),
                    CoinsNumber = table.Column<int>(nullable: false),
                    IsClicked = table.Column<int>(nullable: false),
                    VideoTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instegram", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "InstegramOperations",
                columns: table => new
                {
                    OperationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstegramOperations", x => x.OperationID);
                });

            migrationBuilder.CreateTable(
                name: "TikTok",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserNumber = table.Column<int>(nullable: false),
                    ItemLink = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    ItemTybe = table.Column<string>(nullable: true),
                    CoinsNumber = table.Column<int>(nullable: false),
                    IsClicked = table.Column<int>(nullable: false),
                    VideoTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TikTok", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "TikTokOperations",
                columns: table => new
                {
                    OperationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TikTokOperations", x => x.OperationID);
                });

            migrationBuilder.CreateTable(
                name: "Twitter",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserNumber = table.Column<int>(nullable: false),
                    ItemLink = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    ItemTybe = table.Column<string>(nullable: true),
                    CoinsNumber = table.Column<int>(nullable: false),
                    IsClicked = table.Column<int>(nullable: false),
                    VideoTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Twitter", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "TwitterOperations",
                columns: table => new
                {
                    OperationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwitterOperations", x => x.OperationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facebook");

            migrationBuilder.DropTable(
                name: "FacebookOperations");

            migrationBuilder.DropTable(
                name: "Instegram");

            migrationBuilder.DropTable(
                name: "InstegramOperations");

            migrationBuilder.DropTable(
                name: "TikTok");

            migrationBuilder.DropTable(
                name: "TikTokOperations");

            migrationBuilder.DropTable(
                name: "Twitter");

            migrationBuilder.DropTable(
                name: "TwitterOperations");
        }
    }
}
