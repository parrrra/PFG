using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingLevelOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TrainingLevels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                    UPDATE TrainingLevels
                    SET [Order] = 0
                    WHERE Name = 'Principiante';
                ");
            migrationBuilder.Sql(@"
                    UPDATE TrainingLevels
                    SET [Order] = 1
                    WHERE Name = 'Intermedio';
                ");
            migrationBuilder.Sql(@"
                    UPDATE TrainingLevels
                    SET [Order] = 2
                    WHERE Name = 'Avanzado';
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "TrainingLevels");
        }
    }
}
