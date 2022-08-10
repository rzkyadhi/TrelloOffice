using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category, CategoryRepository>
    {
        CategoryRepository repository;

        public CategoryController(CategoryRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
