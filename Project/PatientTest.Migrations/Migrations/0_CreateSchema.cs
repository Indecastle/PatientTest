using System;
using FluentMigrator;
using Webinex.Migrations.Extensions;

[Migration(0)]
public class AddUsersTable : Migration 
{
    public override void Up()
    {
        Create
            .Table("Patients")
            .WithColumn("Id", x => x
                .AsGuid()
                .NotNullable()
                .PrimaryKey())
            .WithColumn("Gender", x => x
                .AsString()
                .Nullable())
            .WithColumn("Birthdate", x => x
                .AsDateTimeOffset()
                .NotNullable())
            .WithColumn("Active", x => x
                .AsBoolean()
                .Nullable());
        
        Create
            .Table("PatientDetails")
            .WithColumn("Id", x => x
                .AsGuid()
                .NotNullable()
                .PrimaryKey())
            .WithColumn("PatientId", x => x
                .AsGuid()
                .ForeignKey("Patients", "Id"))
            .WithColumn("Use", x => x
                .AsString(250)
                .Nullable())
            .WithColumn("Family", x => x
                .AsString(250)
                .NotNullable());

        Create
            .Table("PatientDetailsGiven")
            .WithColumn("Id", x => x
                .AsGuid()
                .NotNullable()
                .PrimaryKey())
            .WithColumn("PatientDetailsId", x => x
                .AsGuid()
                .ForeignKey("PatientDetails", "Id"))
            .WithColumn("Name", x => x
                .AsString(250)
                .NotNullable());

        
        var patientId = Guid.NewGuid();
        var patientDetailsId = Guid.NewGuid();

        Insert.IntoTable("Patients").Row(new { Id = patientId, Gender = "Male", Birthdate = RawSql.Insert("GETDATE()"), Active = true });
        Insert.IntoTable("PatientDetails").Row(new { Id = patientDetailsId, PatientId = patientId, Use = "official", Family = "Ivanov" });
        Insert.IntoTable("PatientDetailsGiven").Row(new { Id = Guid.NewGuid(), PatientDetailsId = patientDetailsId, Name = "Ivan" });
        Insert.IntoTable("PatientDetailsGiven").Row(new { Id = Guid.NewGuid(), PatientDetailsId = patientDetailsId, Name = "Ivanovich" });
    }

    public override void Down()
    {
        Delete.Table("PatientDetailsGiven");
        Delete.Table("PatientDetails");
        Delete.Table("Patients");
    }
}