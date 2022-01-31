using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndLayihə.Migrations
{
    public partial class createCourseFeaturesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<string>(nullable: false),
                    ClassDuration = table.Column<string>(nullable: false),
                    SkillLevel = table.Column<string>(nullable: false),
                    Language = table.Column<string>(nullable: false),
                    Student = table.Column<int>(nullable: false),
                    Assesment = table.Column<string>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFeatures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseFeatures");
        }
    }
}
