using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lanews.Migrations
{
    public partial class updateArticleDateTimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "ARTICLES",
                type: "DateTime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "ARTICLES",
                type: "DateTime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "(getutcdate())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "modificationDate",
                table: "ARTICLES",
                type: "date",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "DateTime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "ARTICLES",
                type: "date",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "DateTime",
                oldDefaultValueSql: "(getutcdate())");
        }
    }
}
