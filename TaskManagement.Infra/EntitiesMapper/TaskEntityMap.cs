using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infra.EntitiesMapper;

public class TaskEntityMap : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
        .HasColumnName("TaskIdId")
        .UseIdentityColumn()
        .HasColumnType("BIGINT");

    builder.Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(150)
        .HasColumnType("VARCHAR(150)");

    builder.Property(x => x.Description)
        .HasMaxLength(5000)
        .HasColumnType("VARCHAR(5000)");

    builder.Property(x => x.StartDate)
        .HasColumnType("DATETIME");

    builder.Property(x => x.EndDate)
        .HasColumnType("DATETIME");

    builder.Property(x => x.RegisterDate)
        .IsRequired()
        .HasColumnType("DATETIME");
    
    builder.Property(x => x.Status)
        .IsRequired()
        .HasColumnType("INT");
    
    builder.Property(x => x.Priority)
        .IsRequired()
        .HasColumnType("INT");
    
    builder.Property(x => x.UserId)
        .IsRequired()
        .HasColumnType("BIGINT");
    
    builder.Property(x => x.AssignedTo)
        .HasColumnType("BIGINT");
    }
}