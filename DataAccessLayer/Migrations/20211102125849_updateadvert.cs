using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class updateadvert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AirConditoner",
                table: "Adverts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Fireplace",
                table: "Adverts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Furniture",
                table: "Adverts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Garage",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Garden",
                table: "Adverts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pool",
                table: "Adverts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Teras",
                table: "Adverts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirConditoner",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Fireplace",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Furniture",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Garage",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Garden",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Pool",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Teras",
                table: "Adverts");
        }
    }
}
