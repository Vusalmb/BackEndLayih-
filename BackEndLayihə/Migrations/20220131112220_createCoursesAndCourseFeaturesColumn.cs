using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndLayihə.Migrations
{
    public partial class createCoursesAndCourseFeaturesColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseFeatureId",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseFeatureId",
                table: "Courses",
                column: "CourseFeatureId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseFeatures_CourseFeatureId",
                table: "Courses",
                column: "CourseFeatureId",
                principalTable: "CourseFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseFeatures_CourseFeatureId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseFeatureId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseFeatureId",
                table: "Courses");
        }
    }
}
