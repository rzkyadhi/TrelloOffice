using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_CATEGORY",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_CATEGORY", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_EMPLOYEE",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    BirthDate = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    HireDate = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_EMPLOYEE", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_PROJECT",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_PROJECT", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_ROLE",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_ROLE", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_USER",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_USER", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_TB_M_USER_TB_M_EMPLOYEE_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_M_EMPLOYEE",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_ROLEUSER",
                columns: table => new
                {
                    RoleUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_ROLEUSER", x => x.RoleUserId);
                    table.ForeignKey(
                        name: "FK_TB_M_ROLEUSER_TB_M_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_M_ROLE",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_M_ROLEUSER_TB_M_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_M_USER",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_ROLEUSERTASK",
                columns: table => new
                {
                    RoleUserTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleUserId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TB_M_TASK",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleUserTaskId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DueDate = table.Column<string>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_TASK", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TB_M_TASK_TB_M_CATEGORY_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TB_M_CATEGORY",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_M_TASK_TB_M_PROJECT_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "TB_M_PROJECT",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_M_TASK_TB_M_ROLEUSERTASK_RoleUserTaskId",
                        column: x => x.RoleUserTaskId,
                        principalTable: "TB_M_ROLEUSERTASK",
                        principalColumn: "RoleUserTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_ROLEUSER_RoleId",
                table: "TB_M_ROLEUSER",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_ROLEUSER_UserId",
                table: "TB_M_ROLEUSER",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_ROLEUSERTASK_RoleUserId",
                table: "TB_M_ROLEUSERTASK",
                column: "RoleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_TASK_CategoryId",
                table: "TB_M_TASK",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_TASK_ProjectId",
                table: "TB_M_TASK",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_TASK_RoleUserTaskId",
                table: "TB_M_TASK",
                column: "RoleUserTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_TASK");

            migrationBuilder.DropTable(
                name: "TB_M_CATEGORY");

            migrationBuilder.DropTable(
                name: "TB_M_PROJECT");

            migrationBuilder.DropTable(
                name: "TB_M_ROLEUSERTASK");

            migrationBuilder.DropTable(
                name: "TB_M_ROLEUSER");

            migrationBuilder.DropTable(
                name: "TB_M_ROLE");

            migrationBuilder.DropTable(
                name: "TB_M_USER");

            migrationBuilder.DropTable(
                name: "TB_M_EMPLOYEE");
        }
    }
}
