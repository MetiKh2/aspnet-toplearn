using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class setlonguserIdinfroum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_UserID1",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_UserID1",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_UserID1",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_UserID1",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Answers");

            migrationBuilder.AlterColumn<long>(
                name: "UserID",
                table: "Questions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "UserID",
                table: "Answers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserID",
                table: "Questions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserID",
                table: "Answers",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_UserID",
                table: "Answers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_UserID",
                table: "Questions",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_UserID",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_UserID",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_UserID",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_UserID",
                table: "Answers");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Questions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserID1",
                table: "Questions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Answers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserID1",
                table: "Answers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserID1",
                table: "Questions",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserID1",
                table: "Answers",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_UserID1",
                table: "Answers",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_UserID1",
                table: "Questions",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
