using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages.Cart
{
    [Authorize]
    public class ConfirmModel : PageModel
    {
        public void OnGet(string o)
        {
            if (o == null)
            {
                RedirectToPage("/Index");
            }

            ViewData["orderId"] = o;
        }
    }
}
