using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class commentuseradmindelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserAdminId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserAdminId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserAdminId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserAdminId",
                table: "Comments",
                column: "UserAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserAdminId",
                table: "Comments",
                column: "UserAdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
