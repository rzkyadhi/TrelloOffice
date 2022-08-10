using API.Models;

namespace API.Repositories.Data
{
    public class ProjectRepository : GenericRepository<Project>
    {
        public ProjectRepository(string request = "Project", string joinRequest="None") : base(request, joinRequest)
        {

        }
    }
}
