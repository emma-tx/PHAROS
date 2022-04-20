using DotNetCore6.Models;

namespace DotNetCore6.Data.EFCore
{
    public interface IComputersRepository : IGenericRepository<Computer>
    {
        IEnumerable<Computer> GetComputers(int count);
    }

    public class ComputersRepository : GenericRepository<Computer>, IComputersRepository
    {
        public ComputersRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IEnumerable<Computer> GetComputers(int count)
        {
            return _context.Computers.ToList();
        }
    }
}
