using DotNetCore6.Models;

namespace DotNetCore6.Data.EFCore
{
    public interface ILabsRepository : IGenericRepository<Lab>
    {
        IEnumerable<Lab> GetLabs(int count);
    }

    public class LabsRepository : GenericRepository<Lab>, ILabsRepository
    {
        public LabsRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IEnumerable<Lab> GetLabs(int count)
        {
            return _context.Labs.ToList();
        }
    }
}


