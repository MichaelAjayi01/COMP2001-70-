using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Migrations
{
    /// <inheritdoc />
    public partial class AuditTableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CW2_Audit_Log_CW2_USER_PROFILE_User_ID",
                table: "CW2_Audit_Log");

            migrationBuilder.DropIndex(
                name: "IX_CW2_Audit_Log_User_ID",
                table: "CW2_Audit_Log");

            migrationBuilder.AddColumn<int>(
                name: "ProfileUser_ID",
                table: "CW2_Audit_Log",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CW2_Audit_Log_ProfileUser_ID",
                table: "CW2_Audit_Log",
                column: "ProfileUser_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CW2_Audit_Log_CW2_USER_PROFILE_ProfileUser_ID",
                table: "CW2_Audit_Log",
                column: "ProfileUser_ID",
                principalTable: "CW2_USER_PROFILE",
                principalColumn: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CW2_Audit_Log_CW2_USER_PROFILE_ProfileUser_ID",
                table: "CW2_Audit_Log");

            migrationBuilder.DropIndex(
                name: "IX_CW2_Audit_Log_ProfileUser_ID",
                table: "CW2_Audit_Log");

            migrationBuilder.DropColumn(
                name: "ProfileUser_ID",
                table: "CW2_Audit_Log");

            migrationBuilder.CreateIndex(
                name: "IX_CW2_Audit_Log_User_ID",
                table: "CW2_Audit_Log",
                column: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CW2_Audit_Log_CW2_USER_PROFILE_User_ID",
                table: "CW2_Audit_Log",
                column: "User_ID",
                principalTable: "CW2_USER_PROFILE",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
