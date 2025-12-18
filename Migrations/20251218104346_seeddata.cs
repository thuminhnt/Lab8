using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab8.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_Brands_BrandId",
                table: "CarModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarModel_CarModelId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModel",
                table: "CarModel");

            migrationBuilder.RenameTable(
                name: "CarModel",
                newName: "CarModels");

            migrationBuilder.RenameIndex(
                name: "IX_CarModel_BrandId",
                table: "CarModels",
                newName: "IX_CarModels_BrandId");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "CarModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModels",
                table: "CarModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_Brands_BrandId",
                table: "CarModels",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars",
                column: "CarModelId",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_Brands_BrandId",
                table: "CarModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModels",
                table: "CarModels");

            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "CarModels");

            migrationBuilder.RenameTable(
                name: "CarModels",
                newName: "CarModel");

            migrationBuilder.RenameIndex(
                name: "IX_CarModels_BrandId",
                table: "CarModel",
                newName: "IX_CarModel_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModel",
                table: "CarModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_Brands_BrandId",
                table: "CarModel",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarModel_CarModelId",
                table: "Cars",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
