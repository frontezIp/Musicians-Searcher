using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesAndConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatParticipants_MessangerUsers_MessangerUserId",
                table: "ChatParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_MessangerUsers_CreaterId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessangerUsers_MessangerUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_ChatParticipants_MessangerUserId",
                table: "ChatParticipants");

            migrationBuilder.RenameColumn(
                name: "MessangerUserId",
                table: "Messages",
                newName: "MessengerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_MessangerUserId",
                table: "Messages",
                newName: "IX_Messages_MessengerUserId");

            migrationBuilder.RenameColumn(
                name: "CreaterId",
                table: "ChatRooms",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRooms_CreaterId",
                table: "ChatRooms",
                newName: "IX_ChatRooms_CreatorId");

            migrationBuilder.RenameColumn(
                name: "MessangerUserId",
                table: "ChatParticipants",
                newName: "MessengerUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "MessangerUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSentMessageAt",
                table: "ChatRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MembersNumber",
                table: "ChatRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MessagesCount",
                table: "ChatRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_MessengerUserId_ChatRoomId",
                table: "ChatParticipants",
                columns: new[] { "MessengerUserId", "ChatRoomId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatParticipants_MessangerUsers_MessengerUserId",
                table: "ChatParticipants",
                column: "MessengerUserId",
                principalTable: "MessangerUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_MessangerUsers_CreatorId",
                table: "ChatRooms",
                column: "CreatorId",
                principalTable: "MessangerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessangerUsers_MessengerUserId",
                table: "Messages",
                column: "MessengerUserId",
                principalTable: "MessangerUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatParticipants_MessangerUsers_MessengerUserId",
                table: "ChatParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_MessangerUsers_CreatorId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessangerUsers_MessengerUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_ChatParticipants_MessengerUserId_ChatRoomId",
                table: "ChatParticipants");

            migrationBuilder.DropColumn(
                name: "LastSentMessageAt",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "MembersNumber",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "MessagesCount",
                table: "ChatRooms");

            migrationBuilder.RenameColumn(
                name: "MessengerUserId",
                table: "Messages",
                newName: "MessangerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_MessengerUserId",
                table: "Messages",
                newName: "IX_Messages_MessangerUserId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "ChatRooms",
                newName: "CreaterId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRooms_CreatorId",
                table: "ChatRooms",
                newName: "IX_ChatRooms_CreaterId");

            migrationBuilder.RenameColumn(
                name: "MessengerUserId",
                table: "ChatParticipants",
                newName: "MessangerUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "MessangerUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_MessangerUserId",
                table: "ChatParticipants",
                column: "MessangerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatParticipants_MessangerUsers_MessangerUserId",
                table: "ChatParticipants",
                column: "MessangerUserId",
                principalTable: "MessangerUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_MessangerUsers_CreaterId",
                table: "ChatRooms",
                column: "CreaterId",
                principalTable: "MessangerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessangerUsers_MessangerUserId",
                table: "Messages",
                column: "MessangerUserId",
                principalTable: "MessangerUsers",
                principalColumn: "Id");
        }
    }
}
