using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndLayihə.Migrations
{
    public partial class createSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(maxLength: 150, nullable: true),
                    LogoTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SearchIcon = table.Column<string>(nullable: false),
                    FacebookIcon = table.Column<string>(nullable: false),
                    FacebookUrl = table.Column<string>(maxLength: 150, nullable: true),
                    PinterestIcon = table.Column<string>(nullable: false),
                    PinterestUrl = table.Column<string>(maxLength: 150, nullable: true),
                    VimeoIcon = table.Column<string>(nullable: false),
                    VimeoUrl = table.Column<string>(maxLength: 150, nullable: true),
                    TwitterIcon = table.Column<string>(nullable: false),
                    TwitterUrl = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
