using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoutines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routines_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoutineDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoutineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineDays_Routines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoutineDayTrainingTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoutineDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineDayTrainingTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineDayTrainingTemplates_RoutineDays_RoutineDayId",
                        column: x => x.RoutineDayId,
                        principalTable: "RoutineDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutineDayTrainingTemplates_TrainingTemplates_TrainingTemplateId",
                        column: x => x.TrainingTemplateId,
                        principalTable: "TrainingTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDays_RoutineId",
                table: "RoutineDays",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDayTrainingTemplates_RoutineDayId",
                table: "RoutineDayTrainingTemplates",
                column: "RoutineDayId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDayTrainingTemplates_TrainingTemplateId",
                table: "RoutineDayTrainingTemplates",
                column: "TrainingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Routines_UserId",
                table: "Routines",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutineDayTrainingTemplates");

            migrationBuilder.DropTable(
                name: "RoutineDays");

            migrationBuilder.DropTable(
                name: "Routines");
        }
    }
}
