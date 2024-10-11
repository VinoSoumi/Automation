using MailAutomation.Domain;
using MailAutomation.Domain.RepositoryInterface;
using MailAutomation.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace MailAutomation.Persistence
{
    public class RepositoryManager : IRepositoryManager, IDisposable
    {
        private bool disposed;
        private readonly ApplicationDbContext context;
        private IDbContextTransaction trans;
        private readonly Lazy<IExpenseRepository> lazyExpenseRepo;
        private readonly Lazy<IProcessedMailInfoRepository> lazyProcessedMailInfoRepo;

        public RepositoryManager(ApplicationDbContext context)
        {
            this.context = context;
            lazyExpenseRepo = new Lazy<IExpenseRepository>(() => new ExpenseRepository(context));
            lazyProcessedMailInfoRepo = new Lazy<IProcessedMailInfoRepository>(() => new ProcessedMailInfoRepository(context));
        }

        public IExpenseRepository ExpenseRepo => lazyExpenseRepo.Value;

        public IProcessedMailInfoRepository ProcessedMailInfoRepo => lazyProcessedMailInfoRepo.Value;

        public void Commit()
        {
            trans.Commit();
        }

        public void CreateTransaction()
        {
            trans = context.Database.BeginTransaction();
        }

        public void Rollback()
        {
            trans.Rollback();
            trans.Dispose();
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();
            disposed = true;
        }
    }
}
