using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskUserController : BaseController<TaskUser, TaskUserRepository>
    {
        private readonly TaskUserRepository repository;

        public TaskUserController(TaskUserRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<List<TaskUser>> Get()
        {
            var result = repository.Get();
            if (result != null)
                return Ok(new
                {
                    status = 200,
                    message = "SUCCESS",
                    data = result
                });
            return NotFound(new
            {
                status = 404,
                message = "NOT FOUND"
            });
        }
    }
}
