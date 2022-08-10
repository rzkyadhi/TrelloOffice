using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class ProjectRepository : GenericRepository<Project>
    {
        public ProjectRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
