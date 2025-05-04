using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace capicon.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    MdText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });
                            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "dateTime", "Title", "MdText" },
                values: new object[,]
                {
                    { DateTime.UtcNow.AddDays(-14), "Новость 1", "## Технологии\nНовый процессор от Intel" },
                    { DateTime.UtcNow.AddDays(-13), "Новость 2", "**Спорт**\nФутбольный матч завершился" },
                    { DateTime.UtcNow.AddDays(-12), "Новость 3", "### Культура\nВыставка современного искусства" },
                    { DateTime.UtcNow.AddDays(-11), "Новость 4", "Экономика: курс доллара стабилизировался" },
                    { DateTime.UtcNow.AddDays(-10), "Новость 5", "```\nКод для нового алгоритма\n```" },
                    { DateTime.UtcNow.AddDays(-9), "Новость 6", "Наука: новое открытие в медицине" },
                    { DateTime.UtcNow.AddDays(-8), "Новость 7", "#### Авто\nНовая модель электромобиля" },
                    { DateTime.UtcNow.AddDays(-7), "Новость 8", "Космос: запуск новой ракеты" },
                    { DateTime.UtcNow.AddDays(-6), "Новость 9", "IT: выпуск новой версии ПО" },
                    { DateTime.UtcNow.AddDays(-5), "Новость 10", "Образование: новые стандарты" },
                    { DateTime.UtcNow.AddDays(-4), "Новость 11", "Политика: международная встреча" },
                    { DateTime.UtcNow.AddDays(-3), "Новость 12", "Туризм: популярные направления" },
                    { DateTime.UtcNow.AddDays(-2), "Новость 13", "Криптовалюты: биткоин растет" },
                    { DateTime.UtcNow.AddDays(-1), "Новость 14", "Недвижимость: цены на жилье" },
                    { DateTime.UtcNow, "Новость 15", "Экология: новые инициативы" }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
