using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovementReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    FirstEntryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastEntryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovementReports_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    MovementTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movements_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "bilgeisbaratamgan.gunday97@yahoo.com", "Akkuş", "Balaban", "+90-671-202-09-72" },
                    { 2, "irtis43@hotmail.com", "Akçora", "Uca", "+90-619-896-6-244" },
                    { 3, "ilbilge_gumuspala@hotmail.com", "Ayızdağ", "Yıldırım ", "+90-650-645-92-37" },
                    { 4, "budag.durak10@hotmail.com", "Bozan", "Mayhoş", "+90-814-010-2-282" },
                    { 5, "budak96@yahoo.com", "Büküşboğa", "Tekand", "+90-408-298-0-678" },
                    { 6, "basar.gonultas37@yahoo.com", "Azak", "Çevik", "+90-518-066-49-89" },
                    { 7, "gulegen.turkyilmaz@gmail.com", "Bölen", "Solmaz", "+90-900-983-02-55" },
                    { 8, "bokde.denkel24@hotmail.com", "Asuğ", "Eliçin", "+90-229-275-43-70" },
                    { 9, "beltir_kunter@gmail.com", "Baran", "Türkyılmaz", "+90-097-124-4-414" },
                    { 10, "boga.dalkiran@gmail.com", "Bulak", "Karadaş", "+90-890-518-6-941" },
                    { 11, "aygirak.aydan@hotmail.com", "Atalan", "Kahveci", "+90-896-248-4-205" },
                    { 12, "berginsenge_sepetci46@yahoo.com", "Açuk", "Erkekli", "+90-598-169-94-66" },
                    { 13, "aruk.berberoglu68@gmail.com", "Bardıbay", "Kocabıyık", "+90-756-518-20-94" },
                    { 14, "buluc10@yahoo.com", "Basu", "Doğan ", "+90-632-424-9-320" },
                    { 15, "balcar14@hotmail.com", "Çağatay", "Korol", "+90-205-693-80-30" },
                    { 16, "barbol_akal@gmail.com", "Alpata", "Aydan", "+90-849-193-39-31" },
                    { 17, "bakagul.aykac99@hotmail.com", "Aldemir", "Menemencioğlu", "+90-688-283-5-752" },
                    { 18, "aybeg23@yahoo.com", "Balkık", "Karadaş", "+90-268-232-43-14" },
                    { 19, "barskan4@hotmail.com", "Alpturan", "Özbey", "+90-855-637-47-56" },
                    { 20, "agabay60@hotmail.com", "Berendey", "Demirel", "+90-170-520-56-12" },
                    { 21, "ila73@gmail.com", "Altın", "Ekşioğlu", "+90-821-591-3-504" },
                    { 22, "bitri.catalbas98@yahoo.com", "Bölükbaşı", "Yazıcı", "+90-602-123-0-334" },
                    { 23, "baskin_durmaz@hotmail.com", "Bakırsokum", "Toraman", "+90-251-013-1-390" },
                    { 24, "gunes55@gmail.com", "Adıkutlu", "Ertepınar", "+90-055-705-60-10" },
                    { 25, "abar95@yahoo.com", "Bekeçtegin", "Çetin", "+90-904-449-79-15" }
                });

            migrationBuilder.InsertData(
                table: "Movements",
                columns: new[] { "Id", "EventType", "MovementTime", "PersonId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 11, 12, 20, 21, 57, 808, DateTimeKind.Utc).AddTicks(2578), 1 },
                    { 2, 1, new DateTime(2023, 11, 12, 17, 56, 15, 909, DateTimeKind.Utc).AddTicks(9078), 2 },
                    { 3, 1, new DateTime(2023, 11, 12, 19, 27, 47, 9, DateTimeKind.Utc).AddTicks(916), 3 },
                    { 4, 1, new DateTime(2023, 11, 12, 13, 56, 48, 582, DateTimeKind.Utc).AddTicks(7782), 4 },
                    { 5, 1, new DateTime(2023, 11, 12, 12, 20, 5, 942, DateTimeKind.Utc).AddTicks(2958), 5 },
                    { 6, 1, new DateTime(2023, 11, 12, 15, 56, 58, 42, DateTimeKind.Utc).AddTicks(9390), 6 },
                    { 7, 1, new DateTime(2023, 11, 12, 17, 22, 8, 752, DateTimeKind.Utc).AddTicks(6673), 7 },
                    { 8, 1, new DateTime(2023, 11, 12, 15, 35, 8, 48, DateTimeKind.Utc).AddTicks(9215), 8 },
                    { 9, 1, new DateTime(2023, 11, 12, 14, 48, 44, 705, DateTimeKind.Utc).AddTicks(7799), 9 },
                    { 10, 1, new DateTime(2023, 11, 12, 13, 50, 58, 727, DateTimeKind.Utc).AddTicks(3699), 10 },
                    { 11, 1, new DateTime(2023, 11, 12, 15, 53, 59, 959, DateTimeKind.Utc).AddTicks(6341), 11 },
                    { 12, 1, new DateTime(2023, 11, 12, 14, 31, 3, 588, DateTimeKind.Utc).AddTicks(2431), 12 },
                    { 13, 1, new DateTime(2023, 11, 12, 19, 28, 43, 958, DateTimeKind.Utc).AddTicks(2615), 13 },
                    { 14, 1, new DateTime(2023, 11, 12, 11, 52, 32, 466, DateTimeKind.Utc).AddTicks(4838), 14 },
                    { 15, 1, new DateTime(2023, 11, 12, 13, 1, 28, 800, DateTimeKind.Utc).AddTicks(1440), 15 },
                    { 16, 1, new DateTime(2023, 11, 12, 10, 44, 45, 730, DateTimeKind.Utc).AddTicks(3897), 16 },
                    { 17, 1, new DateTime(2023, 11, 12, 14, 21, 56, 954, DateTimeKind.Utc).AddTicks(9941), 17 },
                    { 18, 1, new DateTime(2023, 11, 12, 14, 49, 46, 582, DateTimeKind.Utc).AddTicks(801), 18 },
                    { 19, 1, new DateTime(2023, 11, 12, 14, 6, 46, 797, DateTimeKind.Utc).AddTicks(4323), 19 },
                    { 20, 1, new DateTime(2023, 11, 12, 17, 22, 26, 893, DateTimeKind.Utc).AddTicks(4379), 20 },
                    { 21, 1, new DateTime(2023, 11, 12, 10, 43, 33, 618, DateTimeKind.Utc).AddTicks(3245), 21 },
                    { 22, 1, new DateTime(2023, 11, 12, 13, 42, 29, 636, DateTimeKind.Utc).AddTicks(6957), 22 },
                    { 23, 1, new DateTime(2023, 11, 12, 18, 58, 18, 312, DateTimeKind.Utc).AddTicks(2755), 23 },
                    { 24, 1, new DateTime(2023, 11, 12, 15, 43, 34, 234, DateTimeKind.Utc).AddTicks(7869), 24 },
                    { 25, 1, new DateTime(2023, 11, 12, 13, 0, 2, 365, DateTimeKind.Utc).AddTicks(2912), 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovementReports_PersonId",
                table: "MovementReports",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_PersonId",
                table: "Movements",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovementReports");

            migrationBuilder.DropTable(
                name: "Movements");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
