using DotNetCore6.Models;

namespace DotNetCore6.Data.EFCore
{

    public interface IUsersRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetUsers(int count);
    }

    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IEnumerable<User> GetUsers(int count)
        {
            return _context.Users.ToList();
        }
    }
}


