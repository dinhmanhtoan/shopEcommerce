using System;
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

        
             
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        
        }
    }
}
