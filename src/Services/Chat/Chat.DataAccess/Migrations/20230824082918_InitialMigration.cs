using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessangerUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessangerUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRooms_MessangerUsers_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "MessangerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatParticipants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    MessangerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatParticipants_ChatRoles_ChatRoleId",
                        column: x => x.ChatRoleId,
                        principalTable: "ChatRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatParticipants_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatParticipants_MessangerUsers_MessangerUserId",
                        column: x => x.MessangerUserId,
                        principalTable: "MessangerUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessangerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_MessangerUsers_MessangerUserId",
                        column: x => x.MessangerUserId,
                        principalTable: "MessangerUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_ChatRoleId",
                table: "ChatParticipants",
                column: "ChatRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_ChatRoomId",
                table: "ChatParticipants",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_MessangerUserId",
                table: "ChatParticipants",
                column: "MessangerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_CreaterId",
                table: "ChatRooms",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessangerUserId",
                table: "Messages",
                column: "MessangerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatParticipants");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ChatRoles");

            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.DropTable(
                name: "MessangerUsers");
        }
    }
}
