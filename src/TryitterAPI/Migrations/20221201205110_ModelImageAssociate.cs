using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TryitterAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModelImageAssociate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Post_PostId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Post_PostId",
                table: "Image",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Post_PostId",
                table: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Image",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Post_PostId",
                table: "Image",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }
    }
}
