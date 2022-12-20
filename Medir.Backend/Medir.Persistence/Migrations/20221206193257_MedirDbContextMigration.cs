using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medir.Persistence.Migrations
{
    public partial class MedirDbContextMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Medics",
                columns: table => new
                {
                    MedicId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicFullName = table.Column<string>(type: "text", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false),
                    FullDescription = table.Column<string>(type: "text", nullable: false),
                    MedicPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    MedicPhoto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medics", x => x.MedicId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Polyclinics",
                columns: table => new
                {
                    PolyclinicId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PolyclinicName = table.Column<string>(type: "text", nullable: false),
                    PolyclinicAddress = table.Column<string>(type: "text", nullable: false),
                    PolyclinicPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PolyclinicPhoto = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polyclinics", x => x.PolyclinicId);
                    table.ForeignKey(
                        name: "FK_Polyclinics_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicPositions",
                columns: table => new
                {
                    MedicId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOnPosition = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicPositions", x => new { x.MedicId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_MedicPositions_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "MedicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicAppointmentAvailabilities",
                columns: table => new
                {
                    MedicAppointmentAvailabilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PolyclinicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicAppointmentAvailabilities", x => x.MedicAppointmentAvailabilityId);
                    table.ForeignKey(
                        name: "FK_MedicAppointmentAvailabilities_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "MedicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicAppointmentAvailabilities_Polyclinics_PolyclinicId",
                        column: x => x.PolyclinicId,
                        principalTable: "Polyclinics",
                        principalColumn: "PolyclinicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicAppointmentAvailabilities_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicPolyclinics",
                columns: table => new
                {
                    MedicId = table.Column<Guid>(type: "uuid", nullable: false),
                    PolyclinicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicPolyclinics", x => new { x.MedicId, x.PolyclinicId });
                    table.ForeignKey(
                        name: "FK_MedicPolyclinics_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "MedicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicPolyclinics_Polyclinics_PolyclinicId",
                        column: x => x.PolyclinicId,
                        principalTable: "Polyclinics",
                        principalColumn: "PolyclinicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true),
                    Prescription = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    MedicAppointmentAvailabilityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_MedicAppointmentAvailabilities_MedicAppointmen~",
                        column: x => x.MedicAppointmentAvailabilityId,
                        principalTable: "MedicAppointmentAvailabilities",
                        principalColumn: "MedicAppointmentAvailabilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentId",
                table: "Appointments",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicAppointmentAvailabilityId",
                table: "Appointments",
                column: "MedicAppointmentAvailabilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CityId",
                table: "Cities",
                column: "CityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicAppointmentAvailabilities_MedicId",
                table: "MedicAppointmentAvailabilities",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicAppointmentAvailabilities_PolyclinicId",
                table: "MedicAppointmentAvailabilities",
                column: "PolyclinicId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicAppointmentAvailabilities_PositionId",
                table: "MedicAppointmentAvailabilities",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicPolyclinics_PolyclinicId",
                table: "MedicPolyclinics",
                column: "PolyclinicId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicPositions_PositionId",
                table: "MedicPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Medics_MedicId",
                table: "Medics",
                column: "MedicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Polyclinics_CityId",
                table: "Polyclinics",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Polyclinics_PolyclinicId",
                table: "Polyclinics",
                column: "PolyclinicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PositionId",
                table: "Positions",
                column: "PositionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "MedicPolyclinics");

            migrationBuilder.DropTable(
                name: "MedicPositions");

            migrationBuilder.DropTable(
                name: "MedicAppointmentAvailabilities");

            migrationBuilder.DropTable(
                name: "Medics");

            migrationBuilder.DropTable(
                name: "Polyclinics");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
