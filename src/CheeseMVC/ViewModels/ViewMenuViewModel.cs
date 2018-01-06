using CheeseMVC.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {

        [Required]
        [Display(Name = "Menu")]
        public Menu Menu { get; set; }

        public IList<CheeseMenu> Items { get; set; }


    }
}
