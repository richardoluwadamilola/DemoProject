using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoProject.Migrations
{
    /// <inheritdoc />
    public partial class SeededDataAndUpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolPrefixCode",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Candidates");

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "SchoolId", "Name", "SchoolCode" },
                values: new object[,]
                {
                    { 1, "Today's Solutions", "123TSN" },
                    { 2, "Alphaone School", "456APS" },
                    { 3, "Alphaone Technologies", "456APT" },
                    { 4, "Amaka and Mary School", "456AMS" },
                    { 5, "JT Mentoring School", "456JTS" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolId",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "SchoolPrefixCode",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Candidates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Candidates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
