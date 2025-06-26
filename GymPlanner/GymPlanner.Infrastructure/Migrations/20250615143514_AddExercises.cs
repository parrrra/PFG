using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BodyPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_BodyParts_BodyPartId",
                        column: x => x.BodyPartId,
                        principalTable: "BodyParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTrainingLevelProgressionEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeeklyImprovement = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTrainingLevelProgressionEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseTrainingLevelProgressionEntity_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTrainingLevelProgressionEntity_TrainingLevels_TrainingLevelId",
                        column: x => x.TrainingLevelId,
                        principalTable: "TrainingLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_BodyPartId",
                table: "Exercises",
                column: "BodyPartId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTrainingLevelProgressionEntity_ExerciseId",
                table: "ExerciseTrainingLevelProgressionEntity",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTrainingLevelProgressionEntity_TrainingLevelId",
                table: "ExerciseTrainingLevelProgressionEntity",
                column: "TrainingLevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseTrainingLevelProgressionEntity");

            migrationBuilder.DropTable(
                name: "Exercises");
        }
    }
}
