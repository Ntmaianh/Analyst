using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPLSP_Analyst.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassIndicator");
            migrationBuilder.DropTable(
                name: "IndicatorConfig");
            migrationBuilder.DropTable(
                               name: "Indicator");
            migrationBuilder.DropTable(
                               name: "Config");

            migrationBuilder.DropTable(
                name: "LecturerIndicator");

            migrationBuilder.DropTable(
                name: "MajorIndicator");

            migrationBuilder.DropTable(
                name: "SubjectIndicator");

            

            migrationBuilder.DropTable(
                name: "Lecturer");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "TrainingFacility");

            migrationBuilder.DropTable(
                name: "GroupConfig");

            migrationBuilder.DropTable(
                name: "Major");
            migrationBuilder.CreateTable(
                name: "GroupConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 275, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Table = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    Predication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 276, DateTimeKind.Unspecified).AddTicks(4514), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 277, DateTimeKind.Unspecified).AddTicks(640), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingFacility",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 278, DateTimeKind.Unspecified).AddTicks(1282), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingFacility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 277, DateTimeKind.Unspecified).AddTicks(4950), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semester_GroupConfig_GroupConfigId",
                        column: x => x.GroupConfigId,
                        principalTable: "GroupConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupIndicatorConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndicatorConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 276, DateTimeKind.Unspecified).AddTicks(356), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupIndicatorConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupIndicatorConfig_GroupConfig_GroupConfigId",
                        column: x => x.GroupConfigId,
                        principalTable: "GroupConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupIndicatorConfig_IndicatorConfig_IndicatorConfigId",
                        column: x => x.IndicatorConfigId,
                        principalTable: "IndicatorConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 277, DateTimeKind.Unspecified).AddTicks(6716), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Major_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecturer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainingFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 276, DateTimeKind.Unspecified).AddTicks(6695), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturer_Major_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lecturer_TrainingFacility_TrainingFacilityId",
                        column: x => x.TrainingFacilityId,
                        principalTable: "TrainingFacility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MajorIndicator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentTotalNumber = table.Column<int>(type: "int", nullable: false),
                    StudentPassedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentFailedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentMissedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentPassedPercent = table.Column<float>(type: "real", nullable: false),
                    StudentFailedPercent = table.Column<float>(type: "real", nullable: false),
                    StudentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    SubjectTotalNumber = table.Column<int>(type: "int", nullable: false),
                    SubjectGreaterThan20PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    SubjectGreaterThan10PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    SubjectLessThan3PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    SubjectGreaterThan20PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    SubjectGreaterThan10PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    SubjectLessThan3PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    ClassTotalNumber = table.Column<int>(type: "int", nullable: false),
                    ClassGreaterThan20PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    ClassGreaterThan10PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    ClassLessThan3PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    ClassGreaterThan20PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    ClassGreaterThan10PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    ClassLessThan3PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 277, DateTimeKind.Unspecified).AddTicks(2898), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorIndicator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MajorIndicator_Major_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MajorIndicator_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectIndicator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentTotalNumber = table.Column<int>(type: "int", nullable: false),
                    StudentPassedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentFailedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentMissedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentPassedPercent = table.Column<float>(type: "real", nullable: false),
                    StudentFailedPercent = table.Column<float>(type: "real", nullable: false),
                    StudentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 277, DateTimeKind.Unspecified).AddTicks(9318), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectIndicator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectIndicator_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectIndicator_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassIndicator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LecturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentTotalNumber = table.Column<int>(type: "int", nullable: false),
                    StudentPassedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentFailedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentPassedPercent = table.Column<float>(type: "real", nullable: false),
                    StudentFailedPercent = table.Column<float>(type: "real", nullable: false),
                    StudentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    SubjectEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 274, DateTimeKind.Unspecified).AddTicks(5602), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassIndicator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassIndicator_Lecturer_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassIndicator_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassIndicator_Subject_SubjectEntityId",
                        column: x => x.SubjectEntityId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassIndicator_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LecturerIndicator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LecturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentTotalNumber = table.Column<int>(type: "int", nullable: false),
                    StudentPassedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentFailedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    StudentPassedPercent = table.Column<float>(type: "real", nullable: false),
                    StudentFailedPercent = table.Column<float>(type: "real", nullable: false),
                    StudentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    ClassTotalNumber = table.Column<int>(type: "int", nullable: false),
                    ClassGreaterThan20PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    ClassGreaterThan10PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    ClassLessThan3PercentBannedNumber = table.Column<int>(type: "int", nullable: false),
                    ClassGreaterThan20PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    ClassGreaterThan10PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    ClassLessThan3PercentBannedPercent = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 1, 20, 3, 59, 49, 276, DateTimeKind.Unspecified).AddTicks(8684), new TimeSpan(0, 0, 0, 0, 0))),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturerIndicator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LecturerIndicator_Lecturer_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LecturerIndicator_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GroupConfig",
                columns: new[] { "Id", "Code", "CreatedBy", "Deleted", "DeletedBy", "DeletedTime", "ModifiedBy", "ModifiedTime", "Status" },
                values: new object[] { new Guid("27d13198-88c3-438c-ad10-e08d6bf7c4a4"), "Config_For_FA23", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 });

            migrationBuilder.InsertData(
                table: "TrainingFacility",
                columns: new[] { "Id", "Code", "CreatedBy", "Deleted", "DeletedBy", "DeletedTime", "ModifiedBy", "ModifiedTime", "Name", "Status" },
                values: new object[] { new Guid("27d13198-88c3-438c-ad10-e08d6bf7c4a4"), "HN", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Hà Nội", 1 });

            migrationBuilder.InsertData(
                table: "Semester",
                columns: new[] { "Id", "Code", "CreatedBy", "Deleted", "DeletedBy", "DeletedTime", "EndTime", "GroupConfigId", "ModifiedBy", "ModifiedTime", "StartTime", "Status" },
                values: new object[] { new Guid("27d13198-88c3-438c-ad10-e08d6bf7c4a4"), "FA23", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("27d13198-88c3-438c-ad10-e08d6bf7c4a4"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ClassIndicator_LecturerId",
                table: "ClassIndicator",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassIndicator_SemesterId",
                table: "ClassIndicator",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassIndicator_SubjectEntityId",
                table: "ClassIndicator",
                column: "SubjectEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassIndicator_SubjectId",
                table: "ClassIndicator",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupIndicatorConfig_GroupConfigId",
                table: "GroupIndicatorConfig",
                column: "GroupConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupIndicatorConfig_IndicatorConfigId",
                table: "GroupIndicatorConfig",
                column: "IndicatorConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturer_MajorId",
                table: "Lecturer",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturer_TrainingFacilityId",
                table: "Lecturer",
                column: "TrainingFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerIndicator_LecturerId",
                table: "LecturerIndicator",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturerIndicator_SemesterId",
                table: "LecturerIndicator",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_MajorIndicator_MajorId",
                table: "MajorIndicator",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_MajorIndicator_SemesterId",
                table: "MajorIndicator",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_GroupConfigId",
                table: "Semester",
                column: "GroupConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_MajorId",
                table: "Subject",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectIndicator_SemesterId",
                table: "SubjectIndicator",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectIndicator_SubjectId",
                table: "SubjectIndicator",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassIndicator");

            migrationBuilder.DropTable(
                name: "GroupIndicatorConfig");

            migrationBuilder.DropTable(
                name: "LecturerIndicator");

            migrationBuilder.DropTable(
                name: "MajorIndicator");

            migrationBuilder.DropTable(
                name: "SubjectIndicator");

            migrationBuilder.DropTable(
                name: "IndicatorConfig");

            migrationBuilder.DropTable(
                name: "Lecturer");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "TrainingFacility");

            migrationBuilder.DropTable(
                name: "GroupConfig");

            migrationBuilder.DropTable(
                name: "Major");
        }
    }
}
