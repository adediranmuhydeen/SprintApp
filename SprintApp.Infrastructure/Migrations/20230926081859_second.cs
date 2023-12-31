﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SprintApp.Infrastructure.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginAtempt = table.Column<int>(type: "int", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VarificationTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SprintId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sprints_ProjectManagers_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManagers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserStorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SprintId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SprintId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStorys_ProjectManagers_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManagers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserStorys_Sprints_SprintId1",
                        column: x => x.SprintId1,
                        principalTable: "Sprints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Voters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VotedPoint = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserStoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voters_ProjectManagers_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManagers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Voters_UserStorys_UserStoryId",
                        column: x => x.UserStoryId,
                        principalTable: "UserStorys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoterId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votes_Voters_VoterId1",
                        column: x => x.VoterId1,
                        principalTable: "Voters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectManagerId",
                table: "Sprints",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStorys_ProjectManagerId",
                table: "UserStorys",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStorys_SprintId1",
                table: "UserStorys",
                column: "SprintId1");

            migrationBuilder.CreateIndex(
                name: "IX_Voters_ProjectManagerId",
                table: "Voters",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Voters_UserStoryId",
                table: "Voters",
                column: "UserStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_SprintId",
                table: "Votes",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterId1",
                table: "Votes",
                column: "VoterId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Voters");

            migrationBuilder.DropTable(
                name: "UserStorys");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "ProjectManagers");
        }
    }
}
