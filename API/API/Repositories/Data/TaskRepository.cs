using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class TaskRepository : GenericRepository<Task>
    {
        private readonly MyContext myContext;

        public TaskRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public List<Task> Get()
        {
            var data = myContext.TB_M_TASK
                .Include(x => x.Project)
                .Include(x => x.Category)
                .Include(x => x.RoleUserTask)
                .ToList();
            return data;
        }
    }
}
