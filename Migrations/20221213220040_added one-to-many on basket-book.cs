using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookie.Migrations
{
    public partial class addedonetomanyonbasketbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Baskets_BasketId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BasketId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Baskets_BasketId",
                table: "Books",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Baskets_BasketId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BasketId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Baskets_BasketId",
                table: "Books",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");
        }
    }
}
