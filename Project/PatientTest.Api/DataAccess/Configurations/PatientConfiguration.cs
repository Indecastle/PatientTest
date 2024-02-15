using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientTest.Models;

namespace PatientTest.DataAccess.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Active).IsRequired();
        builder.OwnsOne(x => x.Gender, e => e.Property(x => x.Value).HasColumnName("Gender"));

        
        builder.OwnsOne<PatientDetails>(x => x.Name, o =>
        {
            o.WithOwner().HasForeignKey("PatientId");
            o.ToTable("PatientDetails");
            
            o.Property<Guid>("Id").ValueGeneratedOnAdd();
            o.HasKey("Id");
            
            o.Property(x => x.Family).IsRequired();
            o.OwnsMany<PatientDetailsGiven>(x => x.Given, p =>
            {
                p.WithOwner().HasForeignKey("PatientDetailsId");
                p.ToTable("PatientDetailsGiven");
                
                p.Property<Guid>("Id").ValueGeneratedOnAdd();
                p.HasKey("Id");
            });
        });
    }
}