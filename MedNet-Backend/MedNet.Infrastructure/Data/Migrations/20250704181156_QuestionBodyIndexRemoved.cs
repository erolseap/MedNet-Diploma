using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedNet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuestionBodyIndexRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_questions_parent_questions_set_id_body",
                table: "questions");

            migrationBuilder.CreateIndex(
                name: "ix_questions_parent_questions_set_id",
                table: "questions",
                column: "parent_questions_set_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_questions_parent_questions_set_id",
                table: "questions");

            migrationBuilder.CreateIndex(
                name: "ix_questions_parent_questions_set_id_body",
                table: "questions",
                columns: new[] { "parent_questions_set_id", "body" },
                unique: true);
        }
    }
}
