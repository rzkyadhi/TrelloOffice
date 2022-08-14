using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize(Roles = "Manager,Employee")]
    public class TaskController : Controller
    {
        private readonly TaskRepository taskRepository;

        public TaskController(TaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        #region Get
        public IActionResult Index()
        {
            var result = taskRepository.Get();
            if (result != null)
                return View(result);
            return View();
        }
        #endregion Get

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Task task)
        {
            var result = taskRepository.Post(task);
            if (result > 0)
                return RedirectToAction("Index", "Task");
            return View();
        }
        #endregion Create

        #region Edit
        public IActionResult Edit(int id)
        {
            var result = taskRepository.Get(id);
            if (result != null)
                return View(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                var result = taskRepository.Put(task);
                if (result > 0)
                    return RedirectToAction("Index", "Task");
            }
            return View();
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int id)
        {
            var result = taskRepository.Get(id);
            if (result != null)
                return View(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Task task)
        {
            var result = taskRepository.Delete(task);
            if (result > 0)
                return RedirectToAction("Index", "Project");
            return View();
        }
        #endregion Delete

        #region GetJSON
        public ActionResult GetJSON()
        {
            var result = taskRepository.Get();
            if (result != null) return Ok(new
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
        #endregion

        #region EditJSON
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditJson(Task task)
        {   
            var result = taskRepository.Put(task);
            if (result == System.Net.HttpStatusCode.OK) return Ok(new
            {
                status = 200,
                message = "EDITED"
            });
            return BadRequest(new
            {
                status = 400,
                message = "Bad Request"
            });
        }
        #endregion DeleteJSON
    }
}
