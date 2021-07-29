using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages
{
    public class ItemModel : PageModel
    {
        private readonly EstelavContext _context;
        public Models.Item foundItem { get; set; }

        public ItemModel(EstelavContext context)
        {
            _context = context;
        }

        //public IActionResult Item()
        //{
        //    return NotFound();

        //}
        //public IActionResult Index()
        //{
        //    return NotFound();

        //}

        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            foundItem = _context.Items.Where(c => c.ItemId == id).FirstOrDefault();

            return new PageResult();  
        }


    }
}
