using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController<Project, ProjectRepository>
    {
        ProjectRepository repository;

        public ProjectController(ProjectRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
