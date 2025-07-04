using MedNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedNet.Infrastructure.Data.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Body).IsUnicode().HasMaxLength(1024).IsRequired();
        builder.Property(a => a.IsCorrect).IsRequired().HasDefaultValue(false);
        builder.Property(a => a.ParentQuestionId);

        builder.HasOne(a => a.ParentQuestion)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.ParentQuestionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => new {a.ParentQuestionId, a.Body}).IsUnique();
    }
}
