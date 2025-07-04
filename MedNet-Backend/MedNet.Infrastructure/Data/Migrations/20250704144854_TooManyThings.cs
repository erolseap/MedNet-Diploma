using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedNet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class TooManyThings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "questions");

            migrationBuilder.AddColumn<int>(
                name: "blank_question_number",
                table: "questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "user_tests_sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    questions_set_id = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tests_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_tests_sessions_questions_set_questions_set_id",
                        column: x => x.questions_set_id,
                        principalTable: "questions_set",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_tests_sessions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_test_session_questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    session_id = table.Column<int>(type: "integer", nullable: false),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    answer_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_test_session_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_test_session_questions_answers_answer_id",
                        column: x => x.answer_id,
                        principalTable: "answers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_test_session_questions_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_test_session_questions_user_tests_sessions_session_id",
                        column: x => x.session_id,
                        principalTable: "user_tests_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_answers_parent_question_id_is_correct",
                table: "answers",
                columns: new[] { "parent_question_id", "is_correct" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_test_session_questions_answer_id",
                table: "user_test_session_questions",
                column: "answer_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_test_session_questions_question_id",
                table: "user_test_session_questions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_test_session_questions_session_id",
                table: "user_test_session_questions",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_tests_sessions_questions_set_id",
                table: "user_tests_sessions",
                column: "questions_set_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_tests_sessions_user_id",
                table: "user_tests_sessions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_test_session_questions");

            migrationBuilder.DropTable(
                name: "user_tests_sessions");

            migrationBuilder.DropIndex(
                name: "ix_answers_parent_question_id_is_correct",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "blank_question_number",
                table: "questions");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
