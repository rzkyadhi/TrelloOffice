using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class TaskUserController : Controller
    {
        private readonly TaskUserRepository taskUserRepository;

        public TaskUserController(TaskUserRepository taskUserRepository)
        {
            this.taskUserRepository = taskUserRepository;
        }

        #region Get
        public IActionResult Index()
        {
            var result = taskUserRepository.Get();
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
        public IActionResult Create(TaskUser taskUser)
        {
            var result = taskUserRepository.Post(taskUser);
            if (result > 0)
                return RedirectToAction("Index", "TaskUser");
            return View();
        }
        #endregion Create

        #region Edit
        public IActionResult Edit(int id)
        {
            var result = taskUserRepository.Get(id);
            if (result != null)
                return View(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskUser taskUser)
        {
            if (ModelState.IsValid)
            {
                var result = taskUserRepository.Put(taskUser);
                if (result > 0)
                    return RedirectToAction("Index", "TaskUser");
            }
            return View();
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int id)
        {
            var result = taskUserRepository.Get(id);
            if (result != null)
                return View(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TaskUser taskUser)
        {
            var result = taskUserRepository.Delete(taskUser);
            if (result > 0)
                return RedirectToAction("Index", "TaskUser");
            return View();
        }
        #endregion Delete

        #region GetJSON
        public ActionResult GetJSON()
        {
            var result = taskUserRepository.Get();
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

        #region PostJSON
        public ActionResult PostJSON(TaskUser taskUser)
        {
            var result = taskUserRepository.Post(taskUser);
            if (result == System.Net.HttpStatusCode.Created) return Ok(new
            {
                status = result,
                message = "CREATED"
            });
            return BadRequest(new
            {
                status = 400,
                message = "Bad Request"
            });
        }
        #endregion PostJSON

        #region EditJSON
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditJson(TaskUser taskUser)
        {
            var result = taskUserRepository.Put(taskUser);
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
        #endregion EditJSON
    }
}
