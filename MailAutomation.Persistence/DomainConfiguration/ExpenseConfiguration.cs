using MailAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailAutomation.Persistence.DomainConfiguration;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(Expense => Expense.ExpenseID);
        builder.Property(Expense => Expense.ExpenseID).ValueGeneratedOnAdd();

        builder.Property(Expense => Expense.CostCentre)
            .HasColumnType("varchar(15)")
            .IsRequired();

        builder.Property(Expense => Expense.SalesTaxAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(Expense => Expense.TaxableAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(Expense => Expense.NetAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(Expense => Expense.PaymentMethod)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(Expense => Expense.ExtractedContent)
            .HasColumnType("varchar(500)")
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
