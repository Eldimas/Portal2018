using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortalApp.API.Migrations
{
    public partial class adddep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentVId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    KeyIndex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NameRu = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameKz = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentVs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    DisplayNameRus = table.Column<string>(nullable: true),
                    DisplayNameEng = table.Column<string>(nullable: true),
                    DisplayNameKaz = table.Column<string>(nullable: true),
                    FromNameRus = table.Column<string>(nullable: true),
                    FromNameEng = table.Column<string>(nullable: true),
                    FromNameKaz = table.Column<string>(nullable: true),
                    ToNameRus = table.Column<string>(nullable: true),
                    ToNameEng = table.Column<string>(nullable: true),
                    ToNameKaz = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Disabled = table.Column<bool>(nullable: false),
                    RegionId = table.Column<Guid>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentVs_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentVs_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentVId",
                table: "AspNetUsers",
                column: "DepartmentVId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentVs_DepartmentId",
                table: "DepartmentVs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentVs_RegionId",
                table: "DepartmentVs",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DepartmentVs_DepartmentVId",
                table: "AspNetUsers",
                column: "DepartmentVId",
                principalTable: "DepartmentVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DepartmentVs_DepartmentVId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DepartmentVs");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentVId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentVId",
                table: "AspNetUsers");
        }
    }
}
