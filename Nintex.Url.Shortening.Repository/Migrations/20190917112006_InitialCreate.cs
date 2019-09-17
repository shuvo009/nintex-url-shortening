using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nintex.Url.Shortening.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShortUrl",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    ExpiresUtc = table.Column<DateTime>(nullable: true),
                    CreatorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShortUrlVisitLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ShortUrlId = table.Column<long>(nullable: false),
                    AccessTimeUtc = table.Column<DateTime>(nullable: false),
                    ClientIp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrlVisitLog", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "UpdateDate", "Username" },
                values: new object[] { 1L, null, new byte[] { 106, 204, 190, 54, 155, 152, 171, 212, 28, 192, 78, 167, 71, 74, 108, 208, 67, 175, 115, 203, 120, 112, 79, 150, 206, 170, 95, 182, 10, 1, 32, 39, 189, 71, 116, 134, 10, 129, 243, 135, 86, 82, 224, 201, 207, 215, 183, 118, 152, 36, 240, 216, 215, 4, 99, 221, 77, 90, 173, 253, 92, 242, 37, 81 }, new byte[] { 252, 84, 34, 63, 220, 130, 140, 54, 102, 43, 50, 92, 201, 130, 5, 132, 23, 204, 120, 137, 51, 212, 145, 175, 45, 240, 172, 151, 213, 178, 22, 189, 27, 252, 240, 162, 120, 100, 131, 130, 217, 8, 117, 184, 44, 80, 122, 11, 73, 120, 249, 17, 10, 72, 96, 184, 84, 213, 249, 137, 118, 202, 155, 90, 127, 227, 179, 158, 28, 135, 114, 182, 205, 69, 119, 52, 185, 99, 106, 34, 188, 165, 108, 73, 115, 101, 120, 5, 92, 27, 183, 100, 211, 195, 81, 251, 18, 166, 3, 174, 253, 170, 28, 254, 195, 192, 229, 62, 42, 69, 224, 98, 169, 26, 31, 147, 30, 26, 49, 6, 211, 10, 85, 79, 124, 225, 151, 79 }, new DateTime(2019, 9, 17, 11, 20, 6, 391, DateTimeKind.Utc).AddTicks(1613), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrl_Key",
                table: "ShortUrl",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "ShortUrl");

            migrationBuilder.DropTable(
                name: "ShortUrlVisitLog");
        }
    }
}
