using API.Context;
using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class ProjectRepository : GenericRepository<Project>
    {
        private readonly MyContext myContext;

        public ProjectRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public List<Project> Get()
        {
            var data = myContext.TB_M_PROJECT.ToList();
            return data;
        }
    }
}
