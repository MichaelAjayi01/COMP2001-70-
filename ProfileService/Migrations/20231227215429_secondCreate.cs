using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Migrations
{
    /// <inheritdoc />
    public partial class secondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Profiles_User_ID1",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileCompletedTrails_Profiles_User_ID1",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileCompletedTrails_Trails_Trail_ID1",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileCompletedTrails_Trail_ID1",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileCompletedTrails_User_ID1",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_User_ID1",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Trail_ID1",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropColumn(
                name: "User_ID1",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropColumn(
                name: "User_ID1",
                table: "AuditLogs");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileCompletedTrails_Trail_ID",
                table: "UserProfileCompletedTrails",
                column: "Trail_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileCompletedTrails_User_ID",
                table: "UserProfileCompletedTrails",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_User_ID",
                table: "AuditLogs",
                column: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Profiles_User_ID",
                table: "AuditLogs",
                column: "User_ID",
                principalTable: "Profiles",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileCompletedTrails_Profiles_User_ID",
                table: "UserProfileCompletedTrails",
                column: "User_ID",
                principalTable: "Profiles",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileCompletedTrails_Trails_Trail_ID",
                table: "UserProfileCompletedTrails",
                column: "Trail_ID",
                principalTable: "Trails",
                principalColumn: "Trail_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Profiles_User_ID",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileCompletedTrails_Profiles_User_ID",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileCompletedTrails_Trails_Trail_ID",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileCompletedTrails_Trail_ID",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileCompletedTrails_User_ID",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_User_ID",
                table: "AuditLogs");

            migrationBuilder.AddColumn<int>(
                name: "Trail_ID1",
                table: "UserProfileCompletedTrails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_ID1",
                table: "UserProfileCompletedTrails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_ID1",
                table: "AuditLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileCompletedTrails_Trail_ID1",
                table: "UserProfileCompletedTrails",
                column: "Trail_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileCompletedTrails_User_ID1",
                table: "UserProfileCompletedTrails",
                column: "User_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_User_ID1",
                table: "AuditLogs",
                column: "User_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Profiles_User_ID1",
                table: "AuditLogs",
                column: "User_ID1",
                principalTable: "Profiles",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileCompletedTrails_Profiles_User_ID1",
                table: "UserProfileCompletedTrails",
                column: "User_ID1",
                principalTable: "Profiles",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileCompletedTrails_Trails_Trail_ID1",
                table: "UserProfileCompletedTrails",
                column: "Trail_ID1",
                principalTable: "Trails",
                principalColumn: "Trail_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
