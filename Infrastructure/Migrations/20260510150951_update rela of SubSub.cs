using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterelaofSubSub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubSubCategories_SubCategories_SubCategoryId1",
                table: "SubSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubSubCategories_SubCategoryId1",
                table: "SubSubCategories");

            migrationBuilder.DropColumn(
                name: "SubCategoryId1",
                table: "SubSubCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryId1",
                table: "SubSubCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SubSubCategories_SubCategoryId1",
                table: "SubSubCategories",
                column: "SubCategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSubCategories_SubCategories_SubCategoryId1",
                table: "SubSubCategories",
                column: "SubCategoryId1",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
