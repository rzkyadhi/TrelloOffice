using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager, Employee")]
    public class TaskController : BaseController<Task, TaskRepository>
    {
        private readonly TaskRepository repository;

        public TaskController(TaskRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult<List<TaskVM>> Get()
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
