using DotNetCore6.Data.EFCore;

namespace DotNetCore6.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IComputersRepository Computers { get; }
        ILabsRepository Labs { get; }
        IUsersRepository Users { get; }
        int Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Computers = new ComputersRepository(_context);
            Labs = new LabsRepository(_context);
            Users = new UsersRepository(_context);
        }
        public IComputersRepository Computers { get; private set; }
        public ILabsRepository Labs { get; private set; }
        public IUsersRepository Users { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
