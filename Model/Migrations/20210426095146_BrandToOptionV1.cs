using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class BrandToOptionV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 450, nullable: false),
                    Slug = table.Column<string>(maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });
                    migrationBuilder.AddColumn<long>(
                    name: "BrandId",
                    table: "Product",
                    nullable: true,
                    defaultValue: null
                    );
                    migrationBuilder.AddForeignKey(
                    name: "FK_Product_Brand_BrandId",
                    table: "Product",
                    column: "BrandId",
                    principalTable: "Brand",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            migrationBuilder.CreateIndex(
                 name: "IX_Product_BrandId",
                 table: "Product",
                 column: "BrandId");
               
            migrationBuilder.CreateTable(
                name: "ProductOption",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog_ProductOption", x => x.Id);
                });
             migrationBuilder.CreateTable(
                name: "ProductOptionValue",
                columns: table => new
                    {
                        Id = table.Column<long>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                        OptionId = table.Column<long>(nullable: false),
                        ProductId = table.Column<long>(nullable: false),
                        Value = table.Column<string>(maxLength: 450, nullable: true),
                        DisplayType = table.Column<string>(maxLength: 450, nullable: true),
                        SortIndex = table.Column<int>(nullable: false)
                    },
                constraints: table =>
                    {
                        table.PrimaryKey("PK_ProductOptionValue", x => x.Id);
                        table.ForeignKey(
                            name: "FK_ProductOptionValue_ProductOption_OptionId",
                            column: x => x.OptionId,
                            principalTable: "ProductOption",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                        table.ForeignKey(
                            name: "FK_ProductOptionValue_Product_ProductId",
                            column: x => x.ProductId,
                            principalTable: "Product",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
               
                    });
            migrationBuilder.CreateIndex(
                  name: "IX_ProductOptionValue_OptionId",
                  table: "ProductOptionValue",
                  column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionValue_ProductId",
                table: "ProductOptionValue",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                    name: "Brand");

            migrationBuilder.DropTable(
                name: "ProductOptionValue");

            migrationBuilder.DropTable(
            name: "ProductOption");

        }
    }
}
