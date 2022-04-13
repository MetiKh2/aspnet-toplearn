using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Courses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStatus", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    LevelID = table.Column<int>(type: "int", nullable: false),
                    GropeID = table.Column<int>(type: "int", nullable: false),
                    SubGroupe = table.Column<int>(type: "int", nullable: true),
                    TeacherID = table.Column<long>(type: "bigint", nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DemoFile = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    CourseStatusStatusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Course_CourseGroupes_GropeID",
                        column: x => x.GropeID,
                        principalTable: "CourseGroupes",
                        principalColumn: "GroupeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_CourseGroupes_SubGroupe",
                        column: x => x.SubGroupe,
                        principalTable: "CourseGroupes",
                        principalColumn: "GroupeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_CourseStatus_CourseStatusStatusID",
                        column: x => x.CourseStatusStatusID,
                        principalTable: "CourseStatus",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Users_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEpisode",
                columns: table => new
                {
                    EpisodeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    EpisodeTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EpisodeFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    EpisodeTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEpisode", x => x.EpisodeID);
                    table.ForeignKey(
                        name: "FK_CourseEpisode_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseStatusStatusID",
                table: "Course",
                column: "CourseStatusStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_GropeID",
                table: "Course",
                column: "GropeID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_SubGroupe",
                table: "Course",
                column: "SubGroupe");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TeacherID",
                table: "Course",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEpisode_CourseID",
                table: "CourseEpisode",
                column: "CourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEpisode");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "CourseStatus");
        }
    }
}
