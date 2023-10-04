using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mareen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AttachmentId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AttachmentId",
                table: "Rooms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AttachmentId",
                table: "Guests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AttachmentId",
                table: "Users",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AttachmentId",
                table: "Rooms",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_AttachmentId",
                table: "Guests",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Attachments_AttachmentId",
                table: "Guests",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Attachments_AttachmentId",
                table: "Rooms",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_AttachmentId",
                table: "Users",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "FK_Users_Attachments_AttachmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AttachmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_AttachmentId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Guests_AttachmentId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Guests");
        }
    }
}
