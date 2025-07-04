using MedNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedNet.Infrastructure.Data.Configurations;

public class UserTestSessionQuestionSpecification : IEntityTypeConfiguration<UserTestSessionQuestion>
{
    public void Configure(EntityTypeBuilder<UserTestSessionQuestion> builder)
    {
        builder.HasKey(sq => sq.Id);
        builder.Property(sq => sq.SessionId);
        builder.Property(sq => sq.QuestionId);
        builder.Property(sq => sq.AnswerId);
        
        builder.HasOne(sq => sq.Session)
            .WithMany(s => s.Questions)
            .HasForeignKey(sq => sq.SessionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sq => sq.Question)
            .WithMany()
            .HasForeignKey(sq => sq.QuestionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(sq => sq.Answer)
            .WithMany()
            .HasForeignKey(sq => sq.AnswerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade); // yes, cascade
    }
}
