using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<Menu> cheeseMenu = context.Menus.ToList();
            return View(cheeseMenu);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new menu to my existing menu
                Menu newMenu = new Menu

                {
                    Name = addMenuViewModel.Name
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }

            return View(addMenuViewModel);
        }

        [HttpGet]
        public IActionResult ViewMenu(int id)
        {

            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            Menu menu = context.Menus.Single(m => m.ID == id);

            ViewMenuViewModel viewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddItem(int id)
        {

            Menu menu = context.Menus.Single(m => m.ID == id);
            IList<Cheese> cheeses = context.Cheeses.ToList();
            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu, cheeses);


            return View(addMenuItemViewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == addMenuItemViewModel.cheeseID)
                    .Where(cm => cm.MenuID == addMenuItemViewModel.menuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu newCheeseMenu = new CheeseMenu
                    {
                        CheeseID = addMenuItemViewModel.cheeseID,
                        MenuID = addMenuItemViewModel.menuID
                    };

                    context.CheeseMenus.Add(newCheeseMenu);
                    context.SaveChanges();
                    return Redirect("/Menu/ViewMenu/" + newCheeseMenu.MenuID);
                }

                return Redirect("/Menu/ViewMenu/" + addMenuItemViewModel.menuID);
            }

            return View(addMenuItemViewModel);
        }
    }
}