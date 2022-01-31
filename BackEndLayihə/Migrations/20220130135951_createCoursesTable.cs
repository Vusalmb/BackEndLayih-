using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndLayihə.Migrations
{
    public partial class createCoursesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 150, nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Detail = table.Column<string>(maxLength: 500, nullable: false),
                    About = table.Column<string>(maxLength: 500, nullable: false),
                    Apply = table.Column<string>(maxLength: 500, nullable: false),
                    Certification = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
