using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages.Account
{
    public class RegisterModel : PageModel
    {

        public IActionResult Login()
        {



            return new PageResult();
        }

        public void OnGet()
        {
        }
    }
}
