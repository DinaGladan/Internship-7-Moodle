using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoodleSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_initial_data3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "users",
                columns: new[] { "id", "created_at", "email", "first_name", "last_name", "password", "role" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7757), "admin@fesb.hr", "Lolek", "Bokic", "ananas", 3 },
                    { 2, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7761), "profesorr@fesb.hr", "Boban", "Porke", "ananas", 2 },
                    { 3, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7762), "elprofesorr@fesb.hr", "Ivica", "Porke", "ananas", 2 },
                    { 4, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7764), "dinastud@fesb.hr", "Dina", "Gladan", "ananas", 1 },
                    { 5, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7765), "dujestud@fesb.hr", "Duje", "Nincevic", "ananas", 1 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "courses",
                columns: new[] { "id", "created_at", "name", "professor_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7862), "Programiranje", 2 },
                    { 2, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7863), "Programiranje za Unix", 2 },
                    { 3, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7864), "Programiranje za Internet", 3 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "private_messages",
                columns: new[] { "id", "content", "created_at", "receiver_id", "sender_id" },
                values: new object[,]
                {
                    { 1, "Sadrzaj privatne poruke 1", new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7916), 2, 1 },
                    { 2, "Sadrzaj privatne poruke 2", new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7917), 3, 1 },
                    { 3, "Sadrzaj privatne poruke 3", new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7918), 1, 2 }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "announcements",
                columns: new[] { "id", "content", "course_id", "created_at", "title" },
                values: new object[,]
                {
                    { 1, "sadrzaj 1 annon", 1, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7937), "Kolokvij1" },
                    { 2, "sadrzaj 2 annon", 2, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7939), "Kolokvij2" },
                    { 3, "sadrzaj 3 annon", 1, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7940), "Kolokvij3" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "materials",
                columns: new[] { "id", "course_id", "created_at", "name", "url" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7899), "Materijal1", "https://url1.hr" },
                    { 2, 2, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7901), "Materijal2", "https://url2.hr" },
                    { 3, 3, new DateTime(2025, 12, 22, 17, 25, 41, 336, DateTimeKind.Utc).AddTicks(7902), "Materijal3", "https://url3.hr" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "user_courses",
                columns: new[] { "course_id", "user_id" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 1, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "announcements",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "materials",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "private_messages",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "private_messages",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "private_messages",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "user_courses",
                keyColumns: new[] { "course_id", "user_id" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "user_courses",
                keyColumns: new[] { "course_id", "user_id" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                schema: "public",
                table: "courses",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "courses",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "courses",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
