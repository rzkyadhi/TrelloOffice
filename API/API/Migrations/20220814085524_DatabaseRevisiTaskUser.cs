using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class DatabaseRevisiTaskUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_TASK_TB_M_CATEGORY_CategoryId",
                table: "TB_M_TASK");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_TASK_TB_M_ROLEUSERTASK_RoleUserTaskId",
                table: "TB_M_TASK");

            migrationBuilder.DropTable(
                name: "TB_M_CATEGORY");

            migrationBuilder.DropTable(
                name: "TB_M_ROLEUSERTASK");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_TASK_CategoryId",
                table: "TB_M_TASK");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_TASK_RoleUserTaskId",
                table: "TB_M_TASK");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TB_M_TASK");

            migrationBuilder.DropColumn(
                name: "RoleUserTaskId",
                table: "TB_M_TASK");

            migrationBuilder.CreateTable(
                name: "TB_M_TASKUSER",
                columns: table => new
                {
                    TaskUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_TASKUSER", x => x.TaskUserId);
                    table.ForeignKey(
                        name: "FK_TB_M_TASKUSER_TB_M_TASK_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TB_M_TASK",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_M_TASKUSER_TB_M_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_M_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_TASKUSER_TaskId",
                table: "TB_M_TASKUSER",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_TASKUSER_UserId",
                table: "TB_M_TASKUSER",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_TASKUSER");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TB_M_TASK",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoleUserTaskId",
                table: "TB_M_TASK",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TB_M_CATEGORY",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_CATEGORY", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_ROLEUSERTASK",
                columns: table => new
                {
                    RoleUserTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_ROLEUSERTASK", x => x.RoleUserTaskId);
                    table.ForeignKey(
                        name: "FK_TB_M_ROLEUSERTASK_TB_M_ROLEUSER_RoleUserId",
                        column: x => x.RoleUserId,
                        principalTable: "TB_M_ROLEUSER",
                        principalColumn: "RoleUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_TASK_CategoryId",
                table: "TB_M_TASK",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_TASK_RoleUserTaskId",
                table: "TB_M_TASK",
                column: "RoleUserTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_ROLEUSERTASK_RoleUserId",
                table: "TB_M_ROLEUSERTASK",
                column: "RoleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_TASK_TB_M_CATEGORY_CategoryId",
                table: "TB_M_TASK",
                column: "CategoryId",
                principalTable: "TB_M_CATEGORY",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_TASK_TB_M_ROLEUSERTASK_RoleUserTaskId",
                table: "TB_M_TASK",
                column: "RoleUserTaskId",
                principalTable: "TB_M_ROLEUSERTASK",
                principalColumn: "RoleUserTaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
