using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages.Panel
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
