﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class useradmincomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserAdminId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserAdminId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserAdminId1",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserAdminId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserAdminId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserAdminId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "UserAdminId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAdminId1",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserAdminId1",
                table: "Comments",
                column: "UserAdminId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserAdminId1",
                table: "Comments",
                column: "UserAdminId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
