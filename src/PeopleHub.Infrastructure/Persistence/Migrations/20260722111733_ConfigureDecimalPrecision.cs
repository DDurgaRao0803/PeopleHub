using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CustomerLatitude",
                table: "ServiceRequests",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CustomerLongitude",
                table: "ServiceRequests",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "ProviderProfiles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CompletedJobs",
                table: "ProviderProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveUtc",
                table: "ProviderProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "ProviderProfiles",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "ProviderProfiles",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ResponseRate",
                table: "ProviderProfiles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerLatitude",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "CustomerLongitude",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "ProviderProfiles");

            migrationBuilder.DropColumn(
                name: "CompletedJobs",
                table: "ProviderProfiles");

            migrationBuilder.DropColumn(
                name: "LastActiveUtc",
                table: "ProviderProfiles");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ProviderProfiles");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ProviderProfiles");

            migrationBuilder.DropColumn(
                name: "ResponseRate",
                table: "ProviderProfiles");
        }
    }
}
