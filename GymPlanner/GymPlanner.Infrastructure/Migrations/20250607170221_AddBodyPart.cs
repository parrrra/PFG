using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBodyPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyParts", x => x.Id);
                });

            migrationBuilder.Sql(@"
                INSERT INTO [BodyParts] ([Id], [Name]) VALUES
                (NEWID(), N'Pecho'),
                (NEWID(), N'Espalda'),
                (NEWID(), N'Bíceps'),
                (NEWID(), N'Tríceps'),
                (NEWID(), N'Hombros'),
                (NEWID(), N'Cuádriceps'),
                (NEWID(), N'Femorales'),
                (NEWID(), N'Glúteos'),
                (NEWID(), N'Gemelos'),
                (NEWID(), N'Abdomen'),
                (NEWID(), N'Antebrazos')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyParts");
        }
    }
}
