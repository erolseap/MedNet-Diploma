using MedNet.Domain.Entities;
using MedNet.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedNet.Infrastructure.Data.Configurations;

public class UserTestSessionConfiguration : IEntityTypeConfiguration<UserTestSession>
{
    public void Configure(EntityTypeBuilder<UserTestSession> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.UserId);
        builder.Property(s => s.QuestionsSetId);
        builder.Property(s => s.CreationDate);
        
        builder.HasOne(s => (AppUser?)s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(s => s.QuestionsSet)
            .WithMany()
            .HasForeignKey(s => s.QuestionsSetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
