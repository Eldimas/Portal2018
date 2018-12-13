using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortalApp.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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
                name: "DocumentConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DocumentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentConfigs", x => x.Id);
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
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentConfigV",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NeedRegister = table.Column<bool>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    TitleGeneration = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ReadOnly = table.Column<bool>(nullable: false),
                    CopyDocumentFunction = table.Column<bool>(nullable: false),
                    CloseDocumentFunction = table.Column<bool>(nullable: false),
                    CreateControlcardFunction = table.Column<bool>(nullable: false),
                    DocumentConfigId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentConfigV", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentConfigV_DocumentConfigs_DocumentConfigId",
                        column: x => x.DocumentConfigId,
                        principalTable: "DocumentConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "ContentConfigsSerialized",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FieldName = table.Column<string>(nullable: true),
                    FieldDisplayName = table.Column<string>(nullable: true),
                    Formula = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    DocumentConfigVId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentConfigsSerialized", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentConfigsSerialized_DocumentConfigV_DocumentConfigVId",
                        column: x => x.DocumentConfigVId,
                        principalTable: "DocumentConfigV",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WfConfigsSerializeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    ProcessType = table.Column<int>(nullable: false),
                    Computed = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    DocumentConfigVId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WfConfigsSerializeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WfConfigsSerializeds_DocumentConfigV_DocumentConfigVId",
                        column: x => x.DocumentConfigVId,
                        principalTable: "DocumentConfigV",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    PrefferedCulture = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Cabinet = table.Column<string>(nullable: true),
                    DisplayNameRus = table.Column<string>(nullable: true),
                    DisplayNameEng = table.Column<string>(nullable: true),
                    DisplayNameKaz = table.Column<string>(nullable: true),
                    FromNameRus = table.Column<string>(nullable: true),
                    FromNameEng = table.Column<string>(nullable: true),
                    FromNameKaz = table.Column<string>(nullable: true),
                    ToNameRus = table.Column<string>(nullable: true),
                    ToNameEng = table.Column<string>(nullable: true),
                    ToNameKaz = table.Column<string>(nullable: true),
                    PositionRus = table.Column<string>(nullable: true),
                    PositionEng = table.Column<string>(nullable: true),
                    PositionKaz = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false),
                    DepartmentVId = table.Column<Guid>(nullable: false),
                    DocumentConfigVId = table.Column<Guid>(nullable: true),
                    WfConfigsSerializedId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_DepartmentVs_DepartmentVId",
                        column: x => x.DepartmentVId,
                        principalTable: "DepartmentVs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_DocumentConfigV_DocumentConfigVId",
                        column: x => x.DocumentConfigVId,
                        principalTable: "DocumentConfigV",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_WfConfigsSerializeds_WfConfigsSerializedId",
                        column: x => x.WfConfigsSerializedId,
                        principalTable: "WfConfigsSerializeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentVId",
                table: "AspNetUsers",
                column: "DepartmentVId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DocumentConfigVId",
                table: "AspNetUsers",
                column: "DocumentConfigVId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WfConfigsSerializedId",
                table: "AspNetUsers",
                column: "WfConfigsSerializedId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentConfigsSerialized_DocumentConfigVId",
                table: "ContentConfigsSerialized",
                column: "DocumentConfigVId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentVs_DepartmentId",
                table: "DepartmentVs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentVs_RegionId",
                table: "DepartmentVs",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConfigV_DocumentConfigId",
                table: "DocumentConfigV",
                column: "DocumentConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_WfConfigsSerializeds_DocumentConfigVId",
                table: "WfConfigsSerializeds",
                column: "DocumentConfigVId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContentConfigsSerialized");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DepartmentVs");

            migrationBuilder.DropTable(
                name: "WfConfigsSerializeds");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "DocumentConfigV");

            migrationBuilder.DropTable(
                name: "DocumentConfigs");
        }
    }
}
