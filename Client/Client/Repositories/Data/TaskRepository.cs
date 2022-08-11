using Client.Models;

namespace Client.Repositories.Data
{
    public class TaskRepository : GenericRepository<Task>
    {
        public TaskRepository(string request = "Task") : base(request)
        {

        }
    }
}
