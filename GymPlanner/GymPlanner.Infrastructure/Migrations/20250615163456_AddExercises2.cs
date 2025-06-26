using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExercises2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTrainingLevelProgressionEntity_Exercises_ExerciseId",
                table: "ExerciseTrainingLevelProgressionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTrainingLevelProgressionEntity_TrainingLevels_TrainingLevelId",
                table: "ExerciseTrainingLevelProgressionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseTrainingLevelProgressionEntity",
                table: "ExerciseTrainingLevelProgressionEntity");

            migrationBuilder.RenameTable(
                name: "ExerciseTrainingLevelProgressionEntity",
                newName: "ExerciseTrainingLevelProgressions");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseTrainingLevelProgressionEntity_TrainingLevelId",
                table: "ExerciseTrainingLevelProgressions",
                newName: "IX_ExerciseTrainingLevelProgressions_TrainingLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseTrainingLevelProgressionEntity_ExerciseId",
                table: "ExerciseTrainingLevelProgressions",
                newName: "IX_ExerciseTrainingLevelProgressions_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseTrainingLevelProgressions",
                table: "ExerciseTrainingLevelProgressions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTrainingLevelProgressions_Exercises_ExerciseId",
                table: "ExerciseTrainingLevelProgressions",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTrainingLevelProgressions_TrainingLevels_TrainingLevelId",
                table: "ExerciseTrainingLevelProgressions",
                column: "TrainingLevelId",
                principalTable: "TrainingLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTrainingLevelProgressions_Exercises_ExerciseId",
                table: "ExerciseTrainingLevelProgressions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTrainingLevelProgressions_TrainingLevels_TrainingLevelId",
                table: "ExerciseTrainingLevelProgressions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseTrainingLevelProgressions",
                table: "ExerciseTrainingLevelProgressions");

            migrationBuilder.RenameTable(
                name: "ExerciseTrainingLevelProgressions",
                newName: "ExerciseTrainingLevelProgressionEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseTrainingLevelProgressions_TrainingLevelId",
                table: "ExerciseTrainingLevelProgressionEntity",
                newName: "IX_ExerciseTrainingLevelProgressionEntity_TrainingLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseTrainingLevelProgressions_ExerciseId",
                table: "ExerciseTrainingLevelProgressionEntity",
                newName: "IX_ExerciseTrainingLevelProgressionEntity_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseTrainingLevelProgressionEntity",
                table: "ExerciseTrainingLevelProgressionEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTrainingLevelProgressionEntity_Exercises_ExerciseId",
                table: "ExerciseTrainingLevelProgressionEntity",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTrainingLevelProgressionEntity_TrainingLevels_TrainingLevelId",
                table: "ExerciseTrainingLevelProgressionEntity",
                column: "TrainingLevelId",
                principalTable: "TrainingLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
