using MailAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailAutomation.Persistence.DomainConfiguration;

public class ProcessedMailInfoConfiguration : IEntityTypeConfiguration<ProcessedMailInfo>
{
    public void Configure(EntityTypeBuilder<ProcessedMailInfo> builder)
    {
        builder.HasKey(ProcessedMailInfo => ProcessedMailInfo.ProcessedMailInfoID);
        builder.Property(ProcessedMailInfo => ProcessedMailInfo.ProcessedMailInfoID).ValueGeneratedOnAdd();

        builder.Property(ProcessedMailInfo => ProcessedMailInfo.MessageID)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(ProcessedMailInfo => ProcessedMailInfo.SenderName)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(ProcessedMailInfo => ProcessedMailInfo.SenderEmailAddress)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(ProcessedMailInfo => ProcessedMailInfo.Subject)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(ProcessedMailInfo => ProcessedMailInfo.Body)
            .HasColumnType("varchar(1000)")
            .IsRequired();

        builder.HasOne(ProcessedMailInfo => ProcessedMailInfo.Expense)
            .WithOne(Expense => Expense.MailInfo)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property(Expense => Expense.CreatedBy)
            .IsRequired();

        builder.Property(Expense => Expense.CreatedDate)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(Expense => Expense.ModifiedBy)
            .IsRequired(false);

        builder.Property(Expense => Expense.ModifiedDate)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired(false);
    }
}
