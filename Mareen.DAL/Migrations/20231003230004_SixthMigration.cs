using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mareen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Attachments_AttachmentId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Attachments_AttachmentId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_AttachmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Hotels_HotelId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Attachments_AttachmentId",
                table: "Guests",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Attachments_AttachmentId",
                table: "Rooms",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_AttachmentId",
                table: "Users",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Hotels_HotelId",
                table: "Users",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Attachments_AttachmentId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Attachments_AttachmentId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_AttachmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Hotels_HotelId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Attachments_AttachmentId",
                table: "Guests",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Attachments_AttachmentId",
                table: "Rooms",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_AttachmentId",
                table: "Users",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Hotels_HotelId",
                table: "Users",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
