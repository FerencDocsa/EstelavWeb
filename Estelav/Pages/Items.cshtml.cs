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
        public List<Models.Item> items { get; set; }
        public string _categoryName { get; set; }

        public int _itemsCount { get; set; }
        public ItemsModel(EstelavContext context)
        {
            _context = context;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                items = _context.Items.Where(c => c.CategoryId == id).ToList();
                _itemsCount = items.Count;
            }

            string category = _context.Categories.Where(c => c.CatergoryId == id).FirstOrDefault().CategoryName
                .ToString();
            _categoryName = category;
        }
    }
}