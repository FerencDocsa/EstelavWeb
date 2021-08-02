using Estelav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estelav.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private EstelavContext _context;
        public IList<Items> _recItems { get; set; }

        public IndexModel(ILogger<IndexModel> logger, EstelavContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            _recItems = _context.Items.Where(c => c.CategoryId == 1).Take(4).ToList();
        }
    }
}
