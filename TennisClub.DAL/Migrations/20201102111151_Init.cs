using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisClub.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "tblGenders",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("varchar(10)", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_tblGenders", x => x.Id); });

            migrationBuilder.CreateTable(
                "tblLeagues",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("varchar(20)", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_tblLeagues", x => x.Id); });

            migrationBuilder.CreateTable(
                "tblRoles",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("varchar(20)", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_tblRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                "tblMembers",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FederationNr = table.Column<string>("varchar(10)", nullable: false),
                    FirstName = table.Column<string>("varchar(25)", nullable: false),
                    LastName = table.Column<string>("varchar(35)", nullable: false),
                    BirthDate = table.Column<DateTime>("date", nullable: false),
                    GenderId = table.Column<int>("integer", nullable: false),
                    Address = table.Column<string>("varchar(70)", nullable: false),
                    Number = table.Column<string>("varchar(6)", nullable: false),
                    Addition = table.Column<string>("varchar(2)", nullable: true),
                    Zipcode = table.Column<string>("varchar(6)", nullable: false),
                    City = table.Column<string>("varchar(30)", nullable: false),
                    PhoneNr = table.Column<string>("varchar(15)", nullable: true),
                    Deleted = table.Column<bool>("bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMembers", x => x.Id);
                    table.ForeignKey(
                        "FK_tblMembers_tblGenders_GenderId",
                        x => x.GenderId,
                        "tblGenders",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "tblGames",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameNumber = table.Column<string>("varchar(10)", nullable: false),
                    MemberId = table.Column<int>("integer", nullable: false),
                    LeagueId = table.Column<int>("integer", nullable: false),
                    Date = table.Column<DateTime>("date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGames", x => x.Id);
                    table.ForeignKey(
                        "FK_tblGames_tblLeagues_LeagueId",
                        x => x.LeagueId,
                        "tblLeagues",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_tblGames_tblMembers_MemberId",
                        x => x.MemberId,
                        "tblMembers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "tblMemberFines",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FineNumber = table.Column<int>("integer", nullable: false),
                    MemberId = table.Column<int>("integer", nullable: false),
                    Amount = table.Column<decimal>("decimal(7, 2)", nullable: false),
                    HandoutDate = table.Column<DateTime>("date", nullable: false),
                    PaymentDate = table.Column<DateTime>("date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMemberFines", x => x.Id);
                    table.ForeignKey(
                        "FK_tblMemberFines_tblMembers_MemberId",
                        x => x.MemberId,
                        "tblMembers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "tblMemberRoles",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>("integer", nullable: false),
                    RoleId = table.Column<int>("integer", nullable: false),
                    StartDate = table.Column<DateTime>("date", nullable: false),
                    EndDate = table.Column<DateTime>("date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMemberRoles", x => x.Id);
                    table.ForeignKey(
                        "FK_tblMemberRoles_tblMembers_MemberId",
                        x => x.MemberId,
                        "tblMembers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_tblMemberRoles_tblRoles_RoleId",
                        x => x.RoleId,
                        "tblRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "tblGameResults",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>("integer", nullable: false),
                    SetNr = table.Column<byte>("tinyint", nullable: false),
                    ScoreTeamMember = table.Column<byte>("tinyint", nullable: false),
                    ScoreOpponent = table.Column<byte>("tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGameResults", x => x.Id);
                    table.ForeignKey(
                        "FK_tblGameResults_tblGames_GameId",
                        x => x.GameId,
                        "tblGames",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "tblGenders",
                new[] {"Id", "Name"},
                new object[,]
                {
                    {1, "Man"},
                    {2, "Vrouw"}
                });

            migrationBuilder.InsertData(
                "tblLeagues",
                new[] {"Id", "Name"},
                new object[,]
                {
                    {1, "Recreatief"},
                    {2, "Competitie"},
                    {3, "Toptennis"}
                });

            migrationBuilder.InsertData(
                "tblRoles",
                new[] {"Id", "Name"},
                new object[,]
                {
                    {1, "Voorzitter"},
                    {2, "Bestuurslid"},
                    {3, "Secretaris"},
                    {4, "Penningmeester"},
                    {5, "Speler"}
                });

            migrationBuilder.CreateIndex(
                "IX_tblGameResults_GameId_SetNr",
                "tblGameResults",
                new[] {"GameId", "SetNr"},
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_tblGames_GameNumber",
                "tblGames",
                "GameNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_tblGames_LeagueId",
                "tblGames",
                "LeagueId");

            migrationBuilder.CreateIndex(
                "IX_tblGames_MemberId",
                "tblGames",
                "MemberId");

            migrationBuilder.CreateIndex(
                "IX_tblGenders_Name",
                "tblGenders",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_tblLeagues_Name",
                "tblLeagues",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_tblMemberFines_FineNumber",
                "tblMemberFines",
                "FineNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_tblMemberFines_MemberId",
                "tblMemberFines",
                "MemberId");

            migrationBuilder.CreateIndex(
                "IX_tblMemberRoles_RoleId",
                "tblMemberRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "IX_tblMemberRoles_MemberId_RoleId_StartDate_EndDate",
                "tblMemberRoles",
                new[] {"MemberId", "RoleId", "StartDate", "EndDate"},
                unique: true,
                filter: "[EndDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_tblMembers_FederationNr",
                "tblMembers",
                "FederationNr",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_tblMembers_GenderId",
                "tblMembers",
                "GenderId");

            migrationBuilder.CreateIndex(
                "IX_tblRoles_Name",
                "tblRoles",
                "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "tblGameResults");

            migrationBuilder.DropTable(
                "tblMemberFines");

            migrationBuilder.DropTable(
                "tblMemberRoles");

            migrationBuilder.DropTable(
                "tblGames");

            migrationBuilder.DropTable(
                "tblRoles");

            migrationBuilder.DropTable(
                "tblLeagues");

            migrationBuilder.DropTable(
                "tblMembers");

            migrationBuilder.DropTable(
                "tblGenders");
        }
    }
}