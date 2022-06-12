using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Musicorum.Web.Migrations
{
    public partial class Migration1_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "IsProfilePicture",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Photos",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_PostId",
                table: "Photos",
                newName: "IX_Photos_EventId");

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: true),
                    VideoAsBytes = table.Column<byte[]>(maxLength: 5242880, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Video_EventId",
                table: "Video",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Events_EventId",
                table: "Photos",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Events_EventId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Photos",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_EventId",
                table: "Photos",
                newName: "IX_Photos_PostId");

            migrationBuilder.AddColumn<bool>(
                name: "IsProfilePicture",
                table: "Photos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
