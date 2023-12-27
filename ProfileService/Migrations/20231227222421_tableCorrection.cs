using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Migrations
{
    /// <inheritdoc />
    public partial class tableCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Profiles_User_ID",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedTrails_UserProfileCompletedTrails_User_Trail_ID",
                table: "CompletedTrails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileCompletedTrails_Profiles_User_ID",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileCompletedTrails_Trails_Trail_ID",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfileCompletedTrails",
                table: "UserProfileCompletedTrails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trails",
                table: "Trails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedTrails",
                table: "CompletedTrails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs");

            migrationBuilder.RenameTable(
                name: "UserProfileCompletedTrails",
                newName: "CW2_UserProfile_CompletedTrails_JT");

            migrationBuilder.RenameTable(
                name: "Trails",
                newName: "CW2_Trails");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "CW2_USER_PROFILE");

            migrationBuilder.RenameTable(
                name: "CompletedTrails",
                newName: "CW2_COMPLETED_TRAILS");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                newName: "CW2_Audit_Log");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfileCompletedTrails_User_ID",
                table: "CW2_UserProfile_CompletedTrails_JT",
                newName: "IX_CW2_UserProfile_CompletedTrails_JT_User_ID");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfileCompletedTrails_Trail_ID",
                table: "CW2_UserProfile_CompletedTrails_JT",
                newName: "IX_CW2_UserProfile_CompletedTrails_JT_Trail_ID");

            migrationBuilder.RenameIndex(
                name: "IX_AuditLogs_User_ID",
                table: "CW2_Audit_Log",
                newName: "IX_CW2_Audit_Log_User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CW2_UserProfile_CompletedTrails_JT",
                table: "CW2_UserProfile_CompletedTrails_JT",
                column: "User_Trail_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CW2_Trails",
                table: "CW2_Trails",
                column: "Trail_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CW2_USER_PROFILE",
                table: "CW2_USER_PROFILE",
                column: "User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CW2_COMPLETED_TRAILS",
                table: "CW2_COMPLETED_TRAILS",
                column: "User_Trail_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CW2_Audit_Log",
                table: "CW2_Audit_Log",
                column: "Audit_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CW2_Audit_Log_CW2_USER_PROFILE_User_ID",
                table: "CW2_Audit_Log",
                column: "User_ID",
                principalTable: "CW2_USER_PROFILE",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CW2_COMPLETED_TRAILS_CW2_UserProfile_CompletedTrails_JT_User_Trail_ID",
                table: "CW2_COMPLETED_TRAILS",
                column: "User_Trail_ID",
                principalTable: "CW2_UserProfile_CompletedTrails_JT",
                principalColumn: "User_Trail_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CW2_UserProfile_CompletedTrails_JT_CW2_Trails_Trail_ID",
                table: "CW2_UserProfile_CompletedTrails_JT",
                column: "Trail_ID",
                principalTable: "CW2_Trails",
                principalColumn: "Trail_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CW2_UserProfile_CompletedTrails_JT_CW2_USER_PROFILE_User_ID",
                table: "CW2_UserProfile_CompletedTrails_JT",
                column: "User_ID",
                principalTable: "CW2_USER_PROFILE",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CW2_Audit_Log_CW2_USER_PROFILE_User_ID",
                table: "CW2_Audit_Log");

            migrationBuilder.DropForeignKey(
                name: "FK_CW2_COMPLETED_TRAILS_CW2_UserProfile_CompletedTrails_JT_User_Trail_ID",
                table: "CW2_COMPLETED_TRAILS");

            migrationBuilder.DropForeignKey(
                name: "FK_CW2_UserProfile_CompletedTrails_JT_CW2_Trails_Trail_ID",
                table: "CW2_UserProfile_CompletedTrails_JT");

            migrationBuilder.DropForeignKey(
                name: "FK_CW2_UserProfile_CompletedTrails_JT_CW2_USER_PROFILE_User_ID",
                table: "CW2_UserProfile_CompletedTrails_JT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CW2_UserProfile_CompletedTrails_JT",
                table: "CW2_UserProfile_CompletedTrails_JT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CW2_USER_PROFILE",
                table: "CW2_USER_PROFILE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CW2_Trails",
                table: "CW2_Trails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CW2_COMPLETED_TRAILS",
                table: "CW2_COMPLETED_TRAILS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CW2_Audit_Log",
                table: "CW2_Audit_Log");

            migrationBuilder.RenameTable(
                name: "CW2_UserProfile_CompletedTrails_JT",
                newName: "UserProfileCompletedTrails");

            migrationBuilder.RenameTable(
                name: "CW2_USER_PROFILE",
                newName: "Profiles");

            migrationBuilder.RenameTable(
                name: "CW2_Trails",
                newName: "Trails");

            migrationBuilder.RenameTable(
                name: "CW2_COMPLETED_TRAILS",
                newName: "CompletedTrails");

            migrationBuilder.RenameTable(
                name: "CW2_Audit_Log",
                newName: "AuditLogs");

            migrationBuilder.RenameIndex(
                name: "IX_CW2_UserProfile_CompletedTrails_JT_User_ID",
                table: "UserProfileCompletedTrails",
                newName: "IX_UserProfileCompletedTrails_User_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CW2_UserProfile_CompletedTrails_JT_Trail_ID",
                table: "UserProfileCompletedTrails",
                newName: "IX_UserProfileCompletedTrails_Trail_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CW2_Audit_Log_User_ID",
                table: "AuditLogs",
                newName: "IX_AuditLogs_User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfileCompletedTrails",
                table: "UserProfileCompletedTrails",
                column: "User_Trail_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trails",
                table: "Trails",
                column: "Trail_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedTrails",
                table: "CompletedTrails",
                column: "User_Trail_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs",
                column: "Audit_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Profiles_User_ID",
                table: "AuditLogs",
                column: "User_ID",
                principalTable: "Profiles",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedTrails_UserProfileCompletedTrails_User_Trail_ID",
                table: "CompletedTrails",
                column: "User_Trail_ID",
                principalTable: "UserProfileCompletedTrails",
                principalColumn: "User_Trail_ID",
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
    }
}
