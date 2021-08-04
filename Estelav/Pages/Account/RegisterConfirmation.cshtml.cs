using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages.Account
{
    public class RegisterConfirmationModel : PageModel
    {
        private readonly EstelavContext _context;

        public RegisterConfirmationModel(EstelavContext context)
        {
            _context = context;
        }

        public string Email { get; set; }


        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = _context.Users.FirstOrDefault(f => f.EmailAddress == email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;

            return Page();
        }
    }
}
