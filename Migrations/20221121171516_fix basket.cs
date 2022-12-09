using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookie.Migrations
{
    public partial class fixbasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Books_BookId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_BookId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Baskets");

            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BasketId",
                table: "Books",
                column: "BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Baskets_BasketId",
                table: "Books",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Baskets_BasketId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BasketId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Baskets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BookId",
                table: "Baskets",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Books_BookId",
                table: "Baskets",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
