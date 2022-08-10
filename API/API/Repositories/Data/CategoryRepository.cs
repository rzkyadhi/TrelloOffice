using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
