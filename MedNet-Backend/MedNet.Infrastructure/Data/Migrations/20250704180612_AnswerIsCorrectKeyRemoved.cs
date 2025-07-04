using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedNet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnswerIsCorrectKeyRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_answers_parent_question_id_is_correct",
                table: "answers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_answers_parent_question_id_is_correct",
                table: "answers",
                columns: new[] { "parent_question_id", "is_correct" },
                unique: true);
        }
    }
}
