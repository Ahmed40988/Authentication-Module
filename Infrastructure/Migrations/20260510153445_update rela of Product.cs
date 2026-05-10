using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterelaofProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubSubCategories_SubSubCategoryId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategoryId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubSubCategoryId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubCategoryId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubSubCategoryId1",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "Products",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrandId1",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId1",
                table: "Products",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId1",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "imageUrl");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrandId1",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryId1",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubSubCategoryId1",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId1",
                table: "Products",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId1",
                table: "Products",
                column: "SubCategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubSubCategoryId1",
                table: "Products",
                column: "SubSubCategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId1",
                table: "Products",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId1",
                table: "Products",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId1",
                table: "Products",
                column: "SubCategoryId1",
                principalTable: "SubCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubSubCategories_SubSubCategoryId1",
                table: "Products",
                column: "SubSubCategoryId1",
                principalTable: "SubSubCategories",
                principalColumn: "Id");
        }
    }
}
