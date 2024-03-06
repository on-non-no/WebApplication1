using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TestAPP.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "사용자들",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    아이디 = table.Column<string>(type: "text", nullable: false),
                    비밀번호 = table.Column<string>(type: "text", nullable: false),
                    이메일 = table.Column<string>(type: "text", nullable: false),
                    이름 = table.Column<string>(type: "text", nullable: false),
                    소속 = table.Column<string>(type: "text", nullable: false),
                    생년월일 = table.Column<string>(type: "text", nullable: false),
                    전화번호 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_사용자들", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "계정권한들",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    권한분류 = table.Column<string>(type: "text", nullable: false),
                    사용자Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_계정권한들", x => x.Id);
                    table.ForeignKey(
                        name: "FK_계정권한들_사용자들_사용자Id",
                        column: x => x.사용자Id,
                        principalTable: "사용자들",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_계정권한들_사용자Id",
                table: "계정권한들",
                column: "사용자Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "계정권한들");

            migrationBuilder.DropTable(
                name: "사용자들");
        }
    }
}
