using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CreateCredentailsUserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredUsers_AspNetUsers_UserCredentialsId1",
                table: "RegisteredUsers");

            migrationBuilder.DropIndex(
                name: "IX_RegisteredUsers_UserCredentialsId1",
                table: "RegisteredUsers");

            migrationBuilder.DropColumn(
                name: "UserCredentialsId1",
                table: "RegisteredUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserCredentialsId",
                table: "RegisteredUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredUsers_UserCredentialsId",
                table: "RegisteredUsers",
                column: "UserCredentialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredUsers_AspNetUsers_UserCredentialsId",
                table: "RegisteredUsers",
                column: "UserCredentialsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredUsers_AspNetUsers_UserCredentialsId",
                table: "RegisteredUsers");

            migrationBuilder.DropIndex(
                name: "IX_RegisteredUsers_UserCredentialsId",
                table: "RegisteredUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserCredentialsId",
                table: "RegisteredUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCredentialsId1",
                table: "RegisteredUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredUsers_UserCredentialsId1",
                table: "RegisteredUsers",
                column: "UserCredentialsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredUsers_AspNetUsers_UserCredentialsId1",
                table: "RegisteredUsers",
                column: "UserCredentialsId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
