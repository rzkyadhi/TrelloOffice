using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly MyContext myContext;

        public UserRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public List<User> Get()
        {
            var result = myContext.TB_M_USER
                .Include(x => x.Employee)
                .ToList();
            return result;
        }
    }
}
