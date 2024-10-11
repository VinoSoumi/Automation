using MailAutomation.Domain.Entities;
using MailAutomation.Persistence.DomainConfiguration;
using Microsoft.EntityFrameworkCore;

namespace MailAutomation.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }

            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new ProcessedMailInfoConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ProcessedMailInfo> ProcessedMailInfos { get; set; }
    }
}
