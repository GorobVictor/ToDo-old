using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addTaskGroupsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "task_group",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_task_group_user_id",
                table: "task_group",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_task_group_users_user_id",
                table: "task_group",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_group_users_user_id",
                table: "task_group");

            migrationBuilder.DropIndex(
                name: "IX_task_group_user_id",
                table: "task_group");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "task_group");
        }
    }
}
