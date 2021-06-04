using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addGroupTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "favorites",
                table: "tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "task_group_id",
                table: "tasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "task_group",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    favorites = table.Column<bool>(type: "bit", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_group", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tasks_task_group_id",
                table: "tasks",
                column: "task_group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_task_group_task_group_id",
                table: "tasks",
                column: "task_group_id",
                principalTable: "task_group",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_task_group_task_group_id",
                table: "tasks");

            migrationBuilder.DropTable(
                name: "task_group");

            migrationBuilder.DropIndex(
                name: "IX_tasks_task_group_id",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "photo",
                table: "users");

            migrationBuilder.DropColumn(
                name: "favorites",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "task_group_id",
                table: "tasks");
        }
    }
}
