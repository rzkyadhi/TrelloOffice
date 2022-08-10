using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class ProjectRepository : GenericRepository<Project>
    {
        MyContext myContext;
        public ProjectRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
