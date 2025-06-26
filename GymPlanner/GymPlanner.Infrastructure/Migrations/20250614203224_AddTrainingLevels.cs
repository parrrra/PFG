using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLevels", x => x.Id);
                });
            var beginnerId = Guid.NewGuid();

            migrationBuilder.InsertData(
                table: "TrainingLevels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { beginnerId, "Principiante" },
                    { Guid.NewGuid(), "Intermedio" },
                    { Guid.NewGuid(), "Avanzado" }
                });

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingLevelId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: beginnerId);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrainingLevelId",
                table: "AspNetUsers",
                column: "TrainingLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TrainingLevels_TrainingLevelId",
                table: "AspNetUsers",
                column: "TrainingLevelId",
                principalTable: "TrainingLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TrainingLevels_TrainingLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TrainingLevels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrainingLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrainingLevelId",
                table: "AspNetUsers");
        }
    }
}
