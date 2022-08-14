using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class TaskUserRepository : GenericRepository<TaskUser>
    {
        private readonly MyContext myContext;

        public TaskUserRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public List<TaskUser> Get()
        {
            var data = myContext.TB_M_TASKUSER
                .Include(x => x.Task)
                .ToList();
            return data;
        }
    }
}
