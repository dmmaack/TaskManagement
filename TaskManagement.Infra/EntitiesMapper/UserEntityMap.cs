using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infra.EntitiesMapper;

public class UserEntityMap : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("UserId")
            .UseIdentityColumn()
            .HasColumnType("BIGINT");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(180)
            .HasColumnType("VARCHAR(180)");

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(12)
            .HasColumnType("VARCHAR(12)");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnType("VARCHAR(1000)");

        builder.Property(x => x.RegisterDate)
            .IsRequired()
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnType("BIT");

        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.TasksAssigned)
            .WithOne(x => x.UserAssigned)
            .HasForeignKey(x => x.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}