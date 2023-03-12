using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteTakingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_userId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_userId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Notes",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IsVisible",
                table: "Notes",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notes",
                newName: "userId");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Notes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsVisible",
                table: "Notes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_userId",
                table: "Notes",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_userId",
                table: "Notes",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");
        }
    }
}
