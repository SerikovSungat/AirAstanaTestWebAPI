using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Initialization19052022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flight_entity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    origin = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    destination = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    departure = table.Column<DateTimeOffset>(type: "timestamp with time zone", maxLength: 100, nullable: false),
                    arrival = table.Column<DateTimeOffset>(type: "timestamp with time zone", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flight_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "journal_events",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entity_created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    event_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    event_name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    event_json = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_journal_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_entity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_refresh_token_entity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    expires = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_refresh_token_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_entity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_entity_role_entity_role_entity_id",
                        column: x => x.role_id,
                        principalTable: "role_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_journal_events_entity_id",
                table: "journal_events",
                column: "entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_entity_code",
                table: "role_entity",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_entity_role_id",
                table: "user_entity",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_entity_user_name",
                table: "user_entity",
                column: "user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flight_entity");

            migrationBuilder.DropTable(
                name: "journal_events");

            migrationBuilder.DropTable(
                name: "user_entity");

            migrationBuilder.DropTable(
                name: "user_refresh_token_entity");

            migrationBuilder.DropTable(
                name: "role_entity");
        }
    }
}
