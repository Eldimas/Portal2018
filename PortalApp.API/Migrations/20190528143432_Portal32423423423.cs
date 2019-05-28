using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortalApp.API.Migrations
{
    public partial class Portal32423423423 : Migration
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
                name: "OUForListDto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OUForListDto", x => x.Id);
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
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DepartmentName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTemps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<string>(nullable: true),
                    DepartmentName = table.Column<string>(nullable: true),
                    DeputyUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Roles = table.Column<string>(nullable: true),
                    PrefferedCulture = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Cabinet = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayNameRus = table.Column<string>(nullable: true),
                    FromNameRus = table.Column<string>(nullable: true),
                    ToNameRus = table.Column<string>(nullable: true),
                    DisplayNameEng = table.Column<string>(nullable: true),
                    FromNameEng = table.Column<string>(nullable: true),
                    ToNameEng = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<byte[]>(nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    Disabled = table.Column<bool>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    PositionRus = table.Column<string>(nullable: true),
                    PositionKaz = table.Column<string>(nullable: true),
                    PositionEng = table.Column<string>(nullable: true),
                    DisplayNameKaz = table.Column<string>(nullable: true),
                    FromNameKaz = table.Column<string>(nullable: true),
                    ToNameKaz = table.Column<string>(nullable: true),
                    RegionString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTemps", x => x.Id);
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
                name: "WfProcessInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NeedRegister = table.Column<bool>(nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Ended = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WfProcessInfo", x => x.Id);
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
                name: "DocumentConfigVs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NeedRegister = table.Column<bool>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    TitleGeneration = table.Column<string>(nullable: true),
                    WfConfig = table.Column<string>(nullable: true),
                    ContentConfigs = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ReadOnly = table.Column<bool>(nullable: false),
                    CopyDocumentFunction = table.Column<bool>(nullable: false),
                    CloseDocumentFunction = table.Column<bool>(nullable: false),
                    CreateControlcardFunction = table.Column<bool>(nullable: false),
                    DocumentConfigId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentConfigVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentConfigVs_DocumentConfigs_DocumentConfigId",
                        column: x => x.DocumentConfigId,
                        principalTable: "DocumentConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ReadOnly = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Control = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DocumentType = table.Column<string>(nullable: true),
                    RegNumber = table.Column<string>(nullable: true),
                    RegDate = table.Column<DateTime>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    DocumentConfigVsId = table.Column<Guid>(nullable: false),
                    WfInfoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_OUForListDto_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "OUForListDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_WfProcessInfo_WfInfoId",
                        column: x => x.WfInfoId,
                        principalTable: "WfProcessInfo",
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
                    Created = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false),
                    DepartmentVId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_DepartmentVs_DepartmentVId",
                        column: x => x.DepartmentVId,
                        principalTable: "DepartmentVs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowProcessItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    TaskDescription = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    ProcessType = table.Column<int>(nullable: false),
                    ProcessResult = table.Column<int>(nullable: false),
                    ProcessIteration = table.Column<int>(nullable: false),
                    Added = table.Column<DateTime>(nullable: false),
                    Submitted = table.Column<DateTime>(nullable: true),
                    Register = table.Column<bool>(nullable: false),
                    Opened = table.Column<bool>(nullable: false),
                    Marked = table.Column<bool>(nullable: false),
                    GroupBy = table.Column<string>(nullable: true),
                    DocumentId = table.Column<Guid>(nullable: false),
                    IsSavedInTask = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowProcessItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowProcessItems_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkflowProcessItems_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
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

            migrationBuilder.CreateTable(
                name: "UserVs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
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
                    UserId = table.Column<int>(nullable: false),
                    DepartmentVId = table.Column<Guid>(nullable: false),
                    DeputyUserName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVs_DepartmentVs_DepartmentVId",
                        column: x => x.DepartmentVId,
                        principalTable: "DepartmentVs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVs_AspNetUsers_UserId",
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
                name: "IX_DepartmentVs_DepartmentId",
                table: "DepartmentVs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentVs_RegionId",
                table: "DepartmentVs",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConfigVs_DocumentConfigId",
                table: "DocumentConfigVs",
                column: "DocumentConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AuthorId",
                table: "Documents",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_WfInfoId",
                table: "Documents",
                column: "WfInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVs_DepartmentVId",
                table: "UserVs",
                column: "DepartmentVId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVs_UserId",
                table: "UserVs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowProcessItems_DocumentId",
                table: "WorkflowProcessItems",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowProcessItems_UserId",
                table: "WorkflowProcessItems",
                column: "UserId");
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
                name: "DocumentConfigVs");

            migrationBuilder.DropTable(
                name: "UserTemps");

            migrationBuilder.DropTable(
                name: "UserVs");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "WorkflowProcessItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DocumentConfigs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "DepartmentVs");

            migrationBuilder.DropTable(
                name: "OUForListDto");

            migrationBuilder.DropTable(
                name: "WfProcessInfo");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
