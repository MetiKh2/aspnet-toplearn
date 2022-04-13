using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Courses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseGroupes_GropeID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseGroupes_SubGroupe",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseStatus_CourseStatusStatusID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Users_TeacherID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisode_Course_CourseID",
                table: "CourseEpisode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStatus",
                table: "CourseStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEpisode",
                table: "CourseEpisode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "CourseStatus",
                newName: "CourseStatuses");

            migrationBuilder.RenameTable(
                name: "CourseEpisode",
                newName: "CourseEpisodes");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEpisode_CourseID",
                table: "CourseEpisodes",
                newName: "IX_CourseEpisodes_CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_TeacherID",
                table: "Courses",
                newName: "IX_Courses_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_SubGroupe",
                table: "Courses",
                newName: "IX_Courses_SubGroupe");

            migrationBuilder.RenameIndex(
                name: "IX_Course_GropeID",
                table: "Courses",
                newName: "IX_Courses_GropeID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_CourseStatusStatusID",
                table: "Courses",
                newName: "IX_Courses_CourseStatusStatusID");

            migrationBuilder.AddColumn<int>(
                name: "CourseLevelLevelID",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStatuses",
                table: "CourseStatuses",
                column: "StatusID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEpisodes",
                table: "CourseEpisodes",
                column: "EpisodeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "CourseID");

            migrationBuilder.CreateTable(
                name: "CourseLevels",
                columns: table => new
                {
                    LevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLevels", x => x.LevelID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseLevelLevelID",
                table: "Courses",
                column: "CourseLevelLevelID");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisodes_Courses_CourseID",
                table: "CourseEpisodes",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroupes_GropeID",
                table: "Courses",
                column: "GropeID",
                principalTable: "CourseGroupes",
                principalColumn: "GroupeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroupes_SubGroupe",
                table: "Courses",
                column: "SubGroupe",
                principalTable: "CourseGroupes",
                principalColumn: "GroupeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseLevels_CourseLevelLevelID",
                table: "Courses",
                column: "CourseLevelLevelID",
                principalTable: "CourseLevels",
                principalColumn: "LevelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusStatusID",
                table: "Courses",
                column: "CourseStatusStatusID",
                principalTable: "CourseStatuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_TeacherID",
                table: "Courses",
                column: "TeacherID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisodes_Courses_CourseID",
                table: "CourseEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroupes_GropeID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroupes_SubGroupe",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseLevels_CourseLevelLevelID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusStatusID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_TeacherID",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStatuses",
                table: "CourseStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseLevelLevelID",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEpisodes",
                table: "CourseEpisodes");

            migrationBuilder.DropColumn(
                name: "CourseLevelLevelID",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "CourseStatuses",
                newName: "CourseStatus");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "CourseEpisodes",
                newName: "CourseEpisode");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_TeacherID",
                table: "Course",
                newName: "IX_Course_TeacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_SubGroupe",
                table: "Course",
                newName: "IX_Course_SubGroupe");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_GropeID",
                table: "Course",
                newName: "IX_Course_GropeID");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseStatusStatusID",
                table: "Course",
                newName: "IX_Course_CourseStatusStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEpisodes_CourseID",
                table: "CourseEpisode",
                newName: "IX_CourseEpisode_CourseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStatus",
                table: "CourseStatus",
                column: "StatusID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "CourseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEpisode",
                table: "CourseEpisode",
                column: "EpisodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseGroupes_GropeID",
                table: "Course",
                column: "GropeID",
                principalTable: "CourseGroupes",
                principalColumn: "GroupeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseGroupes_SubGroupe",
                table: "Course",
                column: "SubGroupe",
                principalTable: "CourseGroupes",
                principalColumn: "GroupeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseStatus_CourseStatusStatusID",
                table: "Course",
                column: "CourseStatusStatusID",
                principalTable: "CourseStatus",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Users_TeacherID",
                table: "Course",
                column: "TeacherID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisode_Course_CourseID",
                table: "CourseEpisode",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
