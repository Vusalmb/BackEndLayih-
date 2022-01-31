using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndLayihə.Migrations
{
    public partial class createTeachersHobbiesFaculitiesTeacherHobbiesTeacherFaculitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Speciality = table.Column<string>(maxLength: 150, nullable: false),
                    Desc = table.Column<string>(maxLength: 500, nullable: false),
                    Degree = table.Column<string>(maxLength: 150, nullable: false),
                    Experience = table.Column<int>(nullable: false),
                    Mail = table.Column<string>(maxLength: 150, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherFaculities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(nullable: false),
                    FaculityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherFaculities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherFaculities_Faculities_FaculityId",
                        column: x => x.FaculityId,
                        principalTable: "Faculities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherFaculities_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherHobbies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(nullable: false),
                    HobbyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherHobbies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherHobbies_Hobbies_HobbyId",
                        column: x => x.HobbyId,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherHobbies_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherFaculities_FaculityId",
                table: "TeacherFaculities",
                column: "FaculityId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherFaculities_TeacherId",
                table: "TeacherFaculities",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherHobbies_HobbyId",
                table: "TeacherHobbies",
                column: "HobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherHobbies_TeacherId",
                table: "TeacherHobbies",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherFaculities");

            migrationBuilder.DropTable(
                name: "TeacherHobbies");

            migrationBuilder.DropTable(
                name: "Faculities");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
