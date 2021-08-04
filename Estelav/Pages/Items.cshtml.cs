using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Estelav.Models;


namespace Estelav.Pages
{
    public class ItemsModel : PageModel
    {

        private readonly EstelavContext _context;
        public List<Models.Items> items { get; set; }
        public string _categoryName { get; set; }

        public string _categoryDescription { get; set; }
        public int _itemsCount { get; set; }
        public ItemsModel(EstelavContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == 0 || id == null)
            {
                return RedirectToPage("/Index");
            }

            var category = _context.Categories.FirstOrDefault(c => c.CatergoryId == id);
            items = _context.Items.Where(c => c.CategoryId == id).ToList();
            _itemsCount = items.Count;

            if (category != null){
                _categoryName = category.CategoryName;
                _categoryDescription = category.CategoryDescription;
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}