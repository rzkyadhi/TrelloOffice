using Client.Models;

namespace Client.Repositories.Data
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(string request = "User") : base(request)
        {
        }
    }
}
