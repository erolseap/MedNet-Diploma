using MedNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedNet.Infrastructure.Data.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.BlankQuestionNumber).HasDefaultValue(0);
        builder.Property(q => q.Body).IsUnicode().HasMaxLength(1024).IsRequired();

        builder.HasOne(q => q.ParentQuestionsSet)
            .WithMany(qs => qs.Questions)
            .HasForeignKey(q => q.ParentQuestionsSetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        // We may have multiple questions with the same body, so yeah... It's pointless
        // builder.HasIndex(q => new {q.ParentQuestionsSetId, q.Body}).IsUnique();
    }
}
