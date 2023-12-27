using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calorie_Counter_Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Set_Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Profile_Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Trail_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trail_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Trail_ID);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Audit_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Operation_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operation_DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Operation_Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_ID1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Audit_ID);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Profiles_User_ID1",
                        column: x => x.User_ID1,
                        principalTable: "Profiles",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileCompletedTrails",
                columns: table => new
                {
                    User_Trail_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Trail_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID1 = table.Column<int>(type: "int", nullable: false),
                    Trail_ID1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileCompletedTrails", x => x.User_Trail_ID);
                    table.ForeignKey(
                        name: "FK_UserProfileCompletedTrails_Profiles_User_ID1",
                        column: x => x.User_ID1,
                        principalTable: "Profiles",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileCompletedTrails_Trails_Trail_ID1",
                        column: x => x.Trail_ID1,
                        principalTable: "Trails",
                        principalColumn: "Trail_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedTrails",
                columns: table => new
                {
                    User_Trail_ID = table.Column<int>(type: "int", nullable: false),
                    Completed_Trail_Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedTrails", x => x.User_Trail_ID);
                    table.ForeignKey(
                        name: "FK_CompletedTrails_UserProfileCompletedTrails_User_Trail_ID",
                        column: x => x.User_Trail_ID,
                        principalTable: "UserProfileCompletedTrails",
                        principalColumn: "User_Trail_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_User_ID1",
                table: "AuditLogs",
                column: "User_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileCompletedTrails_Trail_ID1",
                table: "UserProfileCompletedTrails",
                column: "Trail_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileCompletedTrails_User_ID1",
                table: "UserProfileCompletedTrails",
                column: "User_ID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CompletedTrails");

            migrationBuilder.DropTable(
                name: "UserProfileCompletedTrails");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Trails");
        }
    }
}
