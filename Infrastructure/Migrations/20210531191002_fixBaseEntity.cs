using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class fixBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "tasks");

            migrationBuilder.AddColumn<long>(
                name: "updated_by",
                table: "users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "created_by",
                table: "users",
                type: "bigint",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "updated_by",
                table: "tasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "created_by",
                table: "tasks",
                type: "bigint",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_by",
                table: "users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_by",
                table: "users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_by",
                table: "tasks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_by",
                table: "tasks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
