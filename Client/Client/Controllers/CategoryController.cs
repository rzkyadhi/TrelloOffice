using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        #region Get
        public IActionResult Index()
        {
            var result = categoryRepository.Get();
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
        public IActionResult Create(Category category)
        {
            var result = categoryRepository.Post(category);
            if (result > 0)
                return RedirectToAction("Index", "Category");
            return View();
        }
        #endregion Create

        #region Edit
        public IActionResult Edit(int id)
        {
            var result = categoryRepository.Get(id);
            if (result != null)
                return View(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = categoryRepository.Put(category);
                if (result > 0)
                    return RedirectToAction("Index", "Category");
            }
            return View();
        }
        #endregion Edit

        #region Delete
        public IActionResult Delete(int id)
        {
            var result = categoryRepository.Get(id);
            if (result != null)
                return View(result);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            var result = categoryRepository.Delete(category);
            if (result > 0)
                return RedirectToAction("Index", "Category");
            return View();
        }
        #endregion Delete
    }
}
