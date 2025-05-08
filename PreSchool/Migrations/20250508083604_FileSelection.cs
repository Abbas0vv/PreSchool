using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreSchool.Migrations
{
    /// <inheritdoc />
    public partial class FileSelection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "SlideBanners",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "SlideBanners",
                newName: "Image");
        }
    }
}
