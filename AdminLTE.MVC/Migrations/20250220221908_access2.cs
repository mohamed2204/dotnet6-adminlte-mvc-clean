﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminLTE.MVC.Migrations
{
    /// <inheritdoc />
    public partial class access2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Name = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    Id = table.Column<long>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Name = table.Column<string>(type: "longchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longchar", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "longchar", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Name = table.Column<string>(type: "TEXT(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Name = table.Column<string>(type: "longchar", maxLength: 256, nullable: false),
                    Promotion = table.Column<string>(type: "longchar", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "longbinary", nullable: true),
                    UserName = table.Column<string>(type: "longchar", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "longchar", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "longchar", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "longchar", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "smallint", nullable: false),
                    PasswordHash = table.Column<string>(type: "longchar", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longchar", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longchar", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longchar", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "smallint", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "smallint", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "smallint", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longchar", nullable: true),
                    ClaimValue = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stagiaires",
                columns: table => new
                {
                    Id = table.Column<long>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Grade = table.Column<string>(type: "longchar", nullable: true),
                    Prenom = table.Column<string>(type: "longchar", nullable: true),
                    Nom = table.Column<string>(type: "longchar", nullable: true),
                    Mle = table.Column<string>(type: "longchar", nullable: true),
                    Cin = table.Column<string>(type: "longchar", nullable: true),
                    NomAr = table.Column<string>(type: "longchar", nullable: true),
                    PrenomAr = table.Column<string>(type: "longchar", nullable: true),
                    SpecialiteId = table.Column<long>(type: "integer", nullable: true),
                    Branche = table.Column<string>(type: "longchar", nullable: true),
                    Promotion = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stagiaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stagiaires_Specialites_SpecialiteId",
                        column: x => x.SpecialiteId,
                        principalTable: "Specialites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StagePhases",
                columns: table => new
                {
                    StageId = table.Column<long>(type: "integer", nullable: false),
                    PhaseId = table.Column<long>(type: "integer", nullable: false),
                    SpecialileId = table.Column<long>(type: "integer", nullable: false),
                    DateDebut = table.Column<string>(type: "longchar", nullable: true),
                    DateFin = table.Column<string>(type: "longchar", nullable: true),
                    AddedOn = table.Column<string>(type: "longchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePhases", x => new { x.StageId, x.PhaseId, x.SpecialileId });
                    table.ForeignKey(
                        name: "FK_StagePhases_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StagePhases_Specialites_SpecialileId",
                        column: x => x.SpecialileId,
                        principalTable: "Specialites",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StagePhases_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longchar", nullable: true),
                    ClaimValue = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longchar", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StagiaireStages",
                columns: table => new
                {
                    StagiaireId = table.Column<long>(type: "integer", nullable: false),
                    StageId = table.Column<long>(type: "integer", nullable: false),
                    SpecialiteId = table.Column<long>(type: "integer", nullable: false),
                    DateDebut = table.Column<string>(type: "longchar", nullable: false),
                    DateFin = table.Column<string>(type: "longchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagiaireStages", x => new { x.StagiaireId, x.StageId, x.SpecialiteId });
                    table.ForeignKey(
                        name: "FK_StagiaireStages_Specialites_SpecialiteId",
                        column: x => x.SpecialiteId,
                        principalTable: "Specialites",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StagiaireStages_Stagiaires_StagiaireId",
                        column: x => x.StagiaireId,
                        principalTable: "Stagiaires",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "IGNORE NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StagePhases_PhaseId",
                table: "StagePhases",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_StagePhases_SpecialileId",
                table: "StagePhases",
                column: "SpecialileId");

            migrationBuilder.CreateIndex(
                name: "IX_Stagiaires_SpecialiteId",
                table: "Stagiaires",
                column: "SpecialiteId");

            migrationBuilder.CreateIndex(
                name: "IX_StagiaireStages_SpecialiteId",
                table: "StagiaireStages",
                column: "SpecialiteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "IGNORE NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "StagePhases");

            migrationBuilder.DropTable(
                name: "StagiaireStages");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Stagiaires");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Specialites");
        }
    }
}
