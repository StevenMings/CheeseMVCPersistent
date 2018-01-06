using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            var allCategories = context.Categories.ToList();
            return View(allCategories);
        }

        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)

        {
            context = dbContext;
        }

        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory = new CheeseCategory
                {
                    Name = addCategoryViewModel.Name,
                };

                context.Categories.Add(newCheeseCategory);
                context.SaveChanges();

                return Redirect("/Category");
            }

            return View(addCategoryViewModel);
        }
    }
}