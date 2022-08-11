using Client.Models;

namespace Client.Repositories.Data
{
    public class ProjectRepository : GenericRepository<Project>
    {
        public ProjectRepository(string request = "Project") : base(request)
        {

        }
    }
}
