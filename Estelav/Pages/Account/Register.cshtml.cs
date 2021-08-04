using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Estelav.Helpers;

namespace Estelav.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly EstelavContext _context;

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public RegisterModel(EstelavContext context)
        {
            _context = context;
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(f => f.EmailAddress == Input.Email);
                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, Input.Email + " Already exists");
                }
                else
                {
                    user = new Users {  Name = Input.Name, PhoneNumber = Input.Phone, EmailAddress = Input.Email, PasswordHash = Helper.CreateMD5(Input.Password),  CreatedUtc = DateTime.UtcNow };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                }

            }

            return Page();
        }

    }
}
