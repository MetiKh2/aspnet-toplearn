using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class UserDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDiscount",
                columns: table => new
                {
                    UD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    DiscountID = table.Column<long>(type: "bigint", nullable: false),
                    CourseDiscountDiscountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDiscount", x => x.UD_ID);
                    table.ForeignKey(
                        name: "FK_UserDiscount_CourseDiscounts_CourseDiscountDiscountID",
                        column: x => x.CourseDiscountDiscountID,
                        principalTable: "CourseDiscounts",
                        principalColumn: "DiscountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDiscount_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscount_CourseDiscountDiscountID",
                table: "UserDiscount",
                column: "CourseDiscountDiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscount_UserID",
                table: "UserDiscount",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDiscount");
        }
    }
}
