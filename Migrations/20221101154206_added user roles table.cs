using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookie.Migrations
{
    public partial class addeduserrolestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Trades_TradeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TradeId",
                table: "Users",
                newName: "RolesRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TradeId",
                table: "Users",
                newName: "IX_Users_RolesRoleId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Trades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Trades",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trades_UserId",
                table: "Trades",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trades_UserId1",
                table: "Trades",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_BookieRestUser_UserId1",
                table: "Trades",
                column: "UserId1",
                principalTable: "BookieRestUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_Users_UserId",
                table: "Trades",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RolesRoleId",
                table: "Users",
                column: "RolesRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trades_BookieRestUser_UserId1",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_Trades_Users_UserId",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RolesRoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Trades_UserId",
                table: "Trades");

            migrationBuilder.DropIndex(
                name: "IX_Trades_UserId1",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Trades");

            migrationBuilder.RenameColumn(
                name: "RolesRoleId",
                table: "Users",
                newName: "TradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RolesRoleId",
                table: "Users",
                newName: "IX_Users_TradeId");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Trades_TradeId",
                table: "Users",
                column: "TradeId",
                principalTable: "Trades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
