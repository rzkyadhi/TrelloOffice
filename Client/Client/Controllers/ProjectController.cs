using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository projectRepository;

        public ProjectController(ProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        #region Get
        public IActionResult Index()
        {
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
        public IActionResult Create(Project project)
        {
            var result = projectRepository.Post(project);
            if (result > 0)
                return RedirectToAction("Index", "Project");
            return View();
        }
        #endregion Create

        #region Edit
        public IActionResult Edit(int id)
        {
            var result = projectRepository.Get(id);
            if (result != null)
                return View(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                var result = projectRepository.Put(project);
                if (result > 0)
                    return RedirectToAction("Index", "Project");
            }
            return View();
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int id)
        {
            var result = projectRepository.Get(id);
            if (result != null)
                return View(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Project project)
        {
            var result = projectRepository.Delete(project);
            if (result > 0)
                return RedirectToAction("Index", "Project");
            return View();
        }
        #endregion Delete

        #region GetJSON
        public ActionResult GetJSON()
        {
            var result = projectRepository.Get();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJSON(Project project)
        {
            var result = projectRepository.Post(project);
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
        #endregion

        #region GetJSONById
        public ActionResult GetJSONById(int id)
        {
            var result = projectRepository.Get(id);
            if (result != null) return Ok(new
            {
                status = 200,
                message = "SUCCESS",
                data = result
            });
            return BadRequest(new
            {
                status = 400,
                message = "BAD REQUEST"
            });
        }
        #endregion GetJSONById

        #region EditJSON
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditJson(Project project)
        {
            var result = projectRepository.Put(project);
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

        #region DeleteJSON
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJSON(Project project)
        {
            var result = projectRepository.Delete(project);
            if (result == System.Net.HttpStatusCode.OK) return Ok(new
            {
                status = 200,
                message = "DELETED"
            });
            return BadRequest(new
            {
                status = 400,
                message = "BAD REQUEST"
            });
        }
        #endregion DeleteJSON
    }
}
