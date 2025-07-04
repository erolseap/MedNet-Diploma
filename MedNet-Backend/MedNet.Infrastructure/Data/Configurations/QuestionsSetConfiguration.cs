using MedNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedNet.Infrastructure.Data.Configurations;

public class QuestionsSetConfiguration : IEntityTypeConfiguration<QuestionsSet>
{
    public void Configure(EntityTypeBuilder<QuestionsSet> builder)
    {
        builder.HasKey(qs => qs.Id);
        builder.Property(qs => qs.Name).IsUnicode().HasMaxLength(256).IsRequired();
        
        builder.HasIndex(qs => qs.Name).IsUnique();
    }
}
