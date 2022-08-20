using Client.Models;

namespace Client.Repositories.Data
{
    public class TaskUserRepository : GenericRepository<TaskUser>
    {
        public TaskUserRepository(string request = "TaskUser") : base(request)
        {
        }
    }
}
