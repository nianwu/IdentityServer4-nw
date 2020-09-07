using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Data.Migrations.UserConfigurationDb
{
    public partial class InitialUserConfigurationDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEntities",
                columns: table => new
                {
                    SubjectId = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    PasswordSaltMd5 = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true),
                    ProviderSubjectId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    UserEntitySubjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Roles_UserEntities_UserEntitySubjectId",
                        column: x => x.UserEntitySubjectId,
                        principalTable: "UserEntities",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_UserEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntities",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserAccount = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleName, x.UserAccount });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleName",
                        column: x => x.RoleName,
                        principalTable: "Roles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UserEntities_UserAccount",
                        column: x => x.UserAccount,
                        principalTable: "UserEntities",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserEntitySubjectId",
                table: "Roles",
                column: "UserEntitySubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserAccount",
                table: "UserRoles",
                column: "UserAccount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserEntities");
        }
    }
}
