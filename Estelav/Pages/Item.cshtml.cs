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


        public ItemModel(EstelavContext context)
        {
            _context = context;
        }

        public IActionResult Item()
        {
            return NotFound();

        }
        public IActionResult Index()
        {
            return NotFound();

        }

        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            return NotFound();

        }


    }
}
