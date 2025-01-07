using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AddingNormalizedusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41cd8a1b-468f-4a41-b5ba-49f7a2047b77",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07a20438-04f2-4745-ba15-1cbff80e7558", "SUPERADMIN@GMAIL.COM", "SUPERADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEFFLlZ7/9HJxRV8xVyJl1MsaeryXMvQ9ehF5XEU38Kt+zlDDhTGL4B230qmUtx71QQ==", "d486f153-e474-4cbb-a71b-1ef504f6acd2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41cd8a1b-468f-4a41-b5ba-49f7a2047b77",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "012390fb-0e2a-4230-931b-c78dc2114845", null, null, "AQAAAAIAAYagAAAAEP4usFSeuQ6L9koeSYaiflI+5Sm0fh3effiWN1UJ4jy1ZSKc6nPQ3ZEN55hzInP97w==", "fc83f6e5-1698-42b1-a571-c01c6fc1cded" });
        }
    }
}
