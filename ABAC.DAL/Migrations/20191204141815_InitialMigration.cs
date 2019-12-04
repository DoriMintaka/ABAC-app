using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABAC.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "resource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    login = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    password = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ResourceId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attribute_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "rule",
                columns: new[] { "Id", "value" },
                values: new object[] { 1, "{\"type\": \"single\",\"value\": {\"left\": \"user.id\",\"right\": \"resource.createdby\",\"operation\": \"stringequal\"}}" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "login", "name", "password" },
                values: new object[] { 1, "admin", "admin", "password" });

            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "Id", "Name", "ResourceId", "UserId", "Value" },
                values: new object[] { 1, "role", null, 1, "admin" });

            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "Id", "Name", "ResourceId", "UserId", "Value" },
                values: new object[] { 2, "id", null, 1, "1" });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ResourceId",
                table: "Attribute",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_UserId",
                table: "Attribute",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "rule");

            migrationBuilder.DropTable(
                name: "resource");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
